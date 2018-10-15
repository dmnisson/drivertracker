using System;

public enum AccountStatus {
  AwaitingConfirmation,
  Active,
  PendingUserTermination,
  UserTerminated,
  AdminTerminated
}

namespace DriverTracker.Models {
  public class Analyst {
  
    public int UserID {get; set;}
    public string Username {get; set;}
    public string FullName {get; set;}
    public string Email {get; set;}
    public string? PhoneNumber {get; set;}
    public string? SMSNumber {get; set;}
    public int AccountStatus {get; set;}
    public bool ReceivesSMSAlertsNewDrivers {get; set;}
  }
  
  public class AnalystDBContext : DbContext {
    public DbSet<Analyst> Analysts {get; set;}
  }
}
