using Core.Models;
using Scriban;

namespace Web.Pages;

public class QuestionModel(Core.Models.Question question, QuestionModelContext context)
{
    public string Id { get; set; } = question.Id;
    public string Title { get; set; } = Template.Parse(question.TitleTemplate).Render(context.AsDynamic());
    public string Body { get; set; } = Template.Parse(question.BodyTemplate).Render(context.AsDynamic());
    public string HelpText { get; set; } = Template.Parse(question.HelpTextTemplate).Render(context.AsDynamic());
    public QuestionType ResponseType { get; set; } = question.ResponseType;
    public List<string> Options { get; set; } = question.Options;
    public bool AllowsComment { get; set; } = question.AllowsComment;
}

public class QuestionModelContext
{
    public string UserName { get; set; }

    public dynamic AsDynamic() => new { username = UserName };
}