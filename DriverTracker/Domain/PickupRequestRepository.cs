using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using DriverTracker.Models;

namespace DriverTracker.Domain
{
    public class PickupRequestRepository : IPickupRequestRepository
    {
        private readonly MvcDriverContext _context;

        public PickupRequestRepository(MvcDriverContext context)
        {
            _context = context;
        }

        void IPickupRequestRepository.Add(PickupRequest request)
        {
            _context.PickupRequests.Add(request);
            _context.SaveChanges();
        }

        void IPickupRequestRepository.Cancel(PickupRequest request)
        {
            if (request.Answered != null)
                _context.AnsweredPickupRequests.Remove(request.Answered);

            _context.PickupRequests.Remove(request);
            _context.SaveChanges();
        }

        int IPickupRequestRepository.Count()
        {
            return _context.PickupRequests.Count();
        }

        int IPickupRequestRepository.Count(Expression<Func<PickupRequest, bool>> predicate)
        {
            return _context.PickupRequests.Count(predicate);
        }

        PickupRequest IPickupRequestRepository.Get(int id)
        {
            return _context.PickupRequests.FirstOrDefault(req => req.PickupRequestID == id);
        }

        Leg IPickupRequestRepository.GetAnswerLeg(int id)
        {
            PickupRequest req = _context.PickupRequests.Include(r => r.Answered)
                .Include(r => r.Answered.AnswerLeg).FirstOrDefault(r => r.PickupRequestID == id);
            if (req == null) return null;

            AnsweredPickupRequest ans = req.Answered;
            if (ans == null) return null;

            return ans.AnswerLeg;
        }

        PickupRequest IPickupRequestRepository.GetForLeg(Leg leg)
        {
            _context.Entry(leg).Reference(l => l.AnsweredPickupRequest).Load();

            AnsweredPickupRequest ans = leg.AnsweredPickupRequest;
            if (ans == null)
                return null;

            _context.Entry(ans).Reference(a => a.Request).Load();
            return ans.Request;
        }

        bool IPickupRequestRepository.IsAnswered(int id)
        {
            PickupRequest req = _context.PickupRequests.Include(r => r.Answered).FirstOrDefault(r => r.PickupRequestID == id);
            if (req == null) return false;

            return req.Answered != null;
        }

        bool IPickupRequestRepository.IsResponseToRequest(Leg leg)
        {
            _context.Entry(leg).Reference(l => l.AnsweredPickupRequest).Load();

            return leg.AnsweredPickupRequest != null;
        }

        IEnumerable<PickupRequest> IPickupRequestRepository.List()
        {
            return _context.PickupRequests.ToList();        
        }

        IEnumerable<PickupRequest> IPickupRequestRepository.List(Expression<Func<PickupRequest, bool>> predicate)
        {
            return _context.PickupRequests.Where(predicate).ToList();
        }

        void IPickupRequestRepository.Edit(PickupRequest request)
        {
            PickupRequest existingRequest = _context.PickupRequests.FirstOrDefault(r => request.PickupRequestID == r.PickupRequestID);
            if (existingRequest == null)
            {
                return;
            }

            existingRequest.RequestedAddress = request.RequestedAddress;
            existingRequest.RequestedTime = request.RequestedTime;

            _context.Update(existingRequest);
            _context.SaveChanges();
        }

        public IEnumerable<PickupRequest> ListForDriver(Driver driver)
        {
            return _context.PickupRequests
                .Include(req => req.Assigned)
                .Where(req => req.Assigned != null && req.Assigned.AssignedDriver == driver)
                .ToList();
        }

        public int CountForDriver(Driver driver)
        {
            return _context.PickupRequests
                .Include(req => req.Assigned)
                .Count(req => req.Assigned != null && req.Assigned.AssignedDriver == driver);
        }

        public void Answer(PickupRequest request, Leg leg)
        {
            AnsweredPickupRequest ans = new AnsweredPickupRequest
            {
                AnswerLeg = leg,
                Request = request
            };
            _context.AnsweredPickupRequests.Add(ans);
            _context.SaveChanges();
        }

        public void Assign(PickupRequest request, Driver driver)
        {


            // search for the PickupDriverAssignment corresponding to this driver
            PickupDriverAssignment assignment = _context.PickupDriverAssignments.FirstOrDefault(a => a.Request == request);
            if (assignment == null)
            {
                assignment = new PickupDriverAssignment
                {
                    AssignedDriver = driver,
                    Request = request
                };
                _context.PickupDriverAssignments.Add(assignment);
            }
            else
            {
                assignment.AssignedDriver = driver;
            }

            _context.SaveChanges();
        }

        public Driver GetAssignedDriver(int id)
        {
            return _context.PickupRequests
                .Include(req => req.Assigned.AssignedDriver)
                .FirstOrDefault(req => req.PickupRequestID == id)?
                .Assigned?.AssignedDriver;
        }
    }
}
