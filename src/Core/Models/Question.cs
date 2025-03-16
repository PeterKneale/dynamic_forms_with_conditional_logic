namespace Core.Models;

public class Question
{
    public string Id { get; set; }                 // Unique identifier for the question
    public string TitleTemplate { get; set; }      // Scriban template for the title
    public string BodyTemplate { get; set; }       // Scriban template for the body/content
    public string HelpTextTemplate { get; set; }   // Scriban template for help text
    public QuestionType ResponseType { get; set; } // Type of response (text, yes/no, single/multi-select)
    public List<string> Options { get; set; }      // Options for single/multi-select questions
    public bool AllowsComment { get; set; }        // Indicates if an optional comment can be added
    public string ApplicabilityExpression { get; set; } // Expression to determine if the question should be asked
}