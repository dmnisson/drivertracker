using System; 

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
        public int UserID { get; set; }
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

    }
}