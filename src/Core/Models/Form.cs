namespace Core.Models;

public class Form
{
    public string Id { get; init; }
    public string Description { get; init; }
    public string Icon { get; init; } // https://tabler.io/icons
    public IList<FormVersion> Versions { get; init; }
}

public class FormVersion
{
    public int Version { get; init; }
    public List<Question> Questions { get; init; } = [];
}