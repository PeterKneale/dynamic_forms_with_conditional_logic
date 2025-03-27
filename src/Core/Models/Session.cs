namespace Core.Models;

public class Session
{
    public Guid SessionId { get; init; }
    
    public string UserName { get; init; }        
    
    public required string FormId { get; set; }  
    
    public int Version { get; init; }             
    
    public string Status { get; init; } = null!;
    
    public DateTime StartedAt { get; init; }  
    
    public DateTime? UpdatedAt{ get; init; }      
    
    public DateTime? CompletedAt { get; set; }   
    
    public bool IsCompleted() => Status == Constants.SessionStatus.Completed;
}

public static class Constants
{
    public static class SessionStatus
    {
        public const string InProgress = "InProgress";
        public const string Completed = "Completed";
    }
}