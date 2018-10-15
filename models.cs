using System;

public enum AccountStatus {
  
}

namespace DriverTracker.Models {
  public class Analyst {
  
    public int UserID {get; set;}
    public string Username {get; set;}
    public string FullName {get; set;}
    public string Email {get; set;}
    public string? PhoneNumber {get; set;}
    public string? SMSNumber {get; set;}
    public DateTime AccountCreationDate {get;}
    public int AccountStatus {get; set;}
    public 
  }
  
  public class AnalystDBContext : DbContext {
    public DbSet<Analyst> Analysts {get; set;}
  }
}
