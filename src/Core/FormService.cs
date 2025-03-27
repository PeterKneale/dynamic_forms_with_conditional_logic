using Microsoft.Extensions.Logging;

namespace Core;

public class FormService(ExpressionEvaluator eval, SessionRepository sessions, FormRepository forms, ILogger<FormService> logger)
{
    public Session StartSession(Guid sessionId, string username, string formId, int version)
    {
        logger.LogInformation("Starting a new session");
        sessions.CreateSession(sessionId, username, formId, version, Constants.SessionStatus.InProgress, DateTime.UtcNow);
        return sessions.GetSession(sessionId) ?? throw new InvalidOperationException();
    }

    public Session LoadSession(Guid sessionId)
    {
        logger.LogInformation("Load a session");
        var session = sessions.GetSession(sessionId);
        if (session == null)
            throw new Exception($"No session found for session {sessionId}.");
        return session;
    }
    
    public IEnumerable<Answer> GetAnswers(Guid sessionId)
    {
        logger.LogInformation("Getting answers for session {SessionId}", sessionId);
        return sessions.GetAnswers(sessionId);
    }

    public void AnswerQuestion(Guid sessionId, string questionId, string value, string? comment = null)
    {
        logger.LogInformation("Answering a question {QuestionId} with value {AnswerValue}", questionId, value);
        var session = sessions.GetSession(sessionId);

        if (session == null)
            throw new Exception($"No session found for session {sessionId}.");

        var now = DateTime.UtcNow;
        var answer = new Answer
        {
            AnswerId = Guid.NewGuid(),
            SessionId = session.SessionId,
            QuestionId = questionId,
            Value = value,
            Comment = comment,
            Timestamp = now
        };

        sessions.SaveAnswer(sessionId, answer);
        sessions.UpdateSession(sessionId, now);
        if (GetNextApplicableQuestion(sessionId) == null)
        {
            logger.LogInformation("No more questions are applicable, completing the session");
            sessions.CompleteSession(sessionId);
        }
    }

    public Question? GetNextApplicableQuestion(Guid sessionId)
    {
        var session = sessions.GetSession(sessionId)!;
        var form = forms.GetFormById(session.FormId);
        var version = form.Versions.Single(x=> x.Version == session.Version);

        var answers = sessions
            .GetAnswers(session.SessionId)
            .ToDictionary(x => x.QuestionId, x => x);

        logger.LogInformation("Getting next applicable question for {UserName} as part of session {SessionId} in {FormId}", session.UserName, session.SessionId, session.FormId);
        
        foreach (var question in version.Questions)
        {
            if (answers.ContainsKey(question.Id))
            {
                logger.LogInformation("Question {QuestionId} has already been answered", question.Id);
                continue;
            }

            if (string.IsNullOrWhiteSpace(question.ApplicabilityExpression))
            {
                logger.LogInformation("Question {QuestionId} has no applicability expression", question.Id);
                return question;
            }

            var isApplicable = eval.Evaluate(question.ApplicabilityExpression, session, answers);

            if (isApplicable)
            {
                logger.LogInformation("Question {QuestionId} is applicable", question.Id);
                return question;
            }

            logger.LogInformation("Question {QuestionId} is not applicable", question.Id);
        }

        logger.LogInformation("No more questions are applicable");
        return null;
    }
}