using System;

namespace DriverTracker.Models {
  public class Analyst {
  
    public int UserID {get; set;}
    public string Username {get; set;}
    public string FullName {get; set;}
    public DateTime AccountCreationDate {get;}
    public DateTime AccountStatus {get; set;}
    public 
  }
  
  public class AnalystDBContext : DbContext {
    public DbSet<Analyst> Analysts {get; set;}
  }
}
