namespace Core.Models;

public class Answer
{
    public Guid AnswerId { get; init; } 
    public Guid SessionId { get; init; }      
    public string QuestionId { get; init; }  
    public required string Value { get; init; }      
    public string? Comment { get; init; }     
    public DateTime Timestamp { get; init; }
}