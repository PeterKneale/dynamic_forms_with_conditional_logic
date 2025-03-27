using Core.Db;
using Core.Models;

namespace Web.Pages;

public class Completed(FormService service, FormRepository repository) : PageModel
{
    [Required]
    [BindProperty(SupportsGet = true)]
    public Guid SessionId { get; set; }

    public void OnGet()
    {
        Session = service.LoadSession(SessionId);
        Form = repository.GetFormById(Session.FormId).Versions.Single(x=>x.Version == Session.Version);
        Answers = service.GetAnswers(SessionId);
    }

    public FormVersion Form { get; set; } = null!;

    public IEnumerable<Answer> Answers { get; set; } = null!;

    public Session Session { get; set; } = null!;
}