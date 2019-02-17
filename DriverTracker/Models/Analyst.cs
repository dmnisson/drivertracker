using System;
using System.Collections.Generic;

public enum AccountStatusType
{
    AwaitingConfirmation,
    Active,
    PendingUserTermination,
    UserTerminated,
    AdminTerminated
}

namespace DriverTracker.Models
{
    public class Analyst
    {
        public int ID { get; set; }

        [Obsolete("Integer User ID is not supported. Please use UserIDString.", true)]
        public int UserID { get; set; }

        public string UserIDString { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string SMSNumber { get; set; }
        public int AccountStatus { get; set; }

        public bool ReceivesSMSAlertsNewDrivers { get; set; }
        public bool ReceivesSMSAlertsDriversTerminated { get; set; }
        public bool ReceivesSMSAlertsLongDriverWaits { get; set; }
        public double SMSAlertDriverWaitTime { get; set; }

        public ICollection<Analysis> Analyses { get; set; }
    }
}