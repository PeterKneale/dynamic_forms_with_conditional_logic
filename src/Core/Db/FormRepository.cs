namespace Core.Db;

public class FormRepository(IConfiguration configuration)
{
    private readonly IEnumerable<Form> _forms = configuration.GetSection("Forms")
        .Get<IEnumerable<Form>>() ?? throw new InvalidOperationException("Failed to load forms from configuration.");
    
    public Form GetFormById(string formId) => 
        _forms.Single(f => f.Id == formId);
    
    public IEnumerable<Form> GetAllForms()=> _forms;
    
}