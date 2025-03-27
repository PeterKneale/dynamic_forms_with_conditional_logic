namespace Web.Pages;

public class Start(FormService service) : PageModel
{
    public RedirectToPageResult OnGet()
    {
        var sessionId = Guid.NewGuid();
        service.StartSession(sessionId, AuthConstants.UserName, FormId, FormVersion);
        return RedirectToPage(nameof(Question), new { SessionId = sessionId });
    }

    [Required]
    [BindProperty(SupportsGet = true)]
    public string FormId { get; set; } = null!;

    [Required]
    [BindProperty(SupportsGet = true)]
    public int FormVersion { get; set; }
}