namespace Web.Pages;

public class Question(FormService service) : PageModel
{
    public ActionResult OnGet()
    {
        var question = service.GetNextApplicableQuestion(SessionId);
        if (question == null)
        {
            return RedirectToPage(nameof(Completed), new { SessionId });
        }

        var context = new QuestionModelContext()
        {
            UserName = AuthConstants.UserName
        };
        CurrentQuestion = new QuestionModel(question, context);
        return Page();
    }

    public QuestionModel CurrentQuestion { get; set; } = null!;

    [Required]
    [BindProperty(SupportsGet = true)]
    public Guid SessionId { get; set; }

    [Required] [BindProperty] public string QuestionId { get; set; } = null!;

    [Required] [BindProperty] public string Answer { get; set; } = null!;

    [BindProperty] public string? Comment { get; set; }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        service.AnswerQuestion(SessionId, QuestionId, Answer, Comment);
        return RedirectToPage(nameof(Question), new { SessionId }); // Redirect to the next question
    }
}