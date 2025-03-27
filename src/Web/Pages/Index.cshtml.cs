using Core.Db;
using Core.Models;

namespace Web.Pages;

public class IndexModel(FormRepository formRepository, SessionRepository sessionRepository) : PageModel
{
    public void OnGet()
    {
        Forms = formRepository.GetAllForms();
        Sessions = sessionRepository.List(AuthConstants.UserName);
    }

    public IEnumerable<Session> Sessions { get; set; }

    public IEnumerable<Form> Forms { get; set; }
}