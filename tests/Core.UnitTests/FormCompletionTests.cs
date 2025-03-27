using Microsoft.Extensions.DependencyInjection;

namespace Core.UnitTests;

public class FormCompletionTests(IntegrationTestFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public void Form_can_be_completed()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var username = "charlie";
        var formId = "EmployeeSurvey";
        int version = 1;

        // Act - Start a new session
        using var scope = _fixture.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<FormService>();
        var session = service.StartSession(sessionId, username, formId, version);

        var q1 = service.GetNextApplicableQuestion(sessionId);
        q1.ShouldSatisfyAllConditions(
            () => q1.ShouldNotBeNull(),
            () => q1.Id.ShouldBe("Q1")
        );
        service.AnswerQuestion(sessionId, "Q1", "Yes");

        var q2 = service.GetNextApplicableQuestion(sessionId);
        q2.ShouldSatisfyAllConditions(
            () => q2.ShouldNotBeNull(),
            () => q2.Id.ShouldBe("Q2")
        );
        service.AnswerQuestion(sessionId, "Q2", "Toy Department");

        var complete = service.GetNextApplicableQuestion(sessionId);
        complete.ShouldBeNull();
        
        var answers = service.GetAnswers(sessionId).ToArray();
        answers.ShouldSatisfyAllConditions(
            () => answers.ShouldNotBeNull(),
            () => answers.Count().ShouldBe(2)
        );
        answers[0].ShouldSatisfyAllConditions(
            () => answers[0].QuestionId.ShouldBe("Q1"),
            () => answers[0].Value.ShouldBe("Yes")
        );
        answers[1].ShouldSatisfyAllConditions(
            () => answers[1].QuestionId.ShouldBe("Q2"),
            () => answers[1].Value.ShouldBe("Toy Department")
        );
    }
}