using Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Core.UnitTests;

public class SessionResumeTests (IntegrationTestFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public void Session_can_be_resumed()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var username = "bradley";
        var formId = "EmployeeSurvey";
        int version = 1;

        // Act - Start a new session
        var session1 = CreateSession(sessionId, username, formId, version);
        AssertSession(session1, username, formId, version);

        // Act - Resume the session
        var session2 = ResumeSession(sessionId);
        AssertSession(session2, username, formId, version);
    }

    private Session CreateSession(Guid sessionId, string userId, string formId, int version)
    {
        using var scope = _fixture.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<FormService>();
        var session = service.StartSession(sessionId, userId, formId, version);
        return session;
    }

    private Session ResumeSession(Guid sessionId)
    {
        using var scope = _fixture.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<FormService>();
        var session = service.LoadSession(sessionId);
        return session;
    }

    private static void AssertSession(Session session, string userId, string formId, int version)
    {
        session.ShouldNotBeNull();
        session.SessionId.ShouldBe(session.SessionId);
        session.UserName.ShouldBe(userId);
        session.FormId.ShouldBe(formId);
        session.Version.ShouldBe(version);
        session.Status.ShouldBe("InProgress");
    }
}