using static Core.Db.Migrations.DbSchema;

namespace Core.Db;

using Dapper;

public class SessionRepository(DbConnectionFactory db)
{
    public void CreateSession(Guid sessionId, string userName, string formId, int version, string status, DateTime startedAt)
    {
        using var connection = db.CreateConnection();
        var sql = $"""
                   INSERT INTO {Sessions.TableName} 
                   ({Sessions.SessionId}, {Sessions.UserName}, {Sessions.FormId}, 
                    {Sessions.Version}, {Sessions.Status}, {Sessions.StartedAt})
                   VALUES (@sessionId, @userName, @formId, @version, @status, @startedAt)
                   """;

        connection.Execute(sql, new { sessionId, userName, formId, version, status, startedAt });
    }

    public void UpdateSession(Guid sessionId, DateTime updatedAt)
    {
        using var connection = db.CreateConnection();
        var sql = $"""
                   UPDATE {Sessions.TableName} 
                   SET {Sessions.UpdatedAt} = @updatedAt
                   WHERE {Sessions.SessionId} = @sessionId
                   """;

        connection.Execute(sql, new { sessionId, updatedAt });
    }

    public Session? GetSession(Guid sessionId)
    {
        using var connection = db.CreateConnection();
        var sql = $"""
                   SELECT * FROM {Sessions.TableName} 
                   WHERE {Sessions.SessionId} = @sessionId
                   """;
        return connection.QuerySingleOrDefault<Session>(sql, new { sessionId });
    }

    public IEnumerable<Answer> GetAnswers(Guid sessionId)
    {
        using var connection = db.CreateConnection();
        var sql = $"""
                   SELECT *
                   FROM {Answers.TableName} 
                   WHERE {Answers.SessionId} = @sessionId
                   """;
        var answers = connection.Query<Answer>(sql, new { sessionId });
        return answers;
    }

    public void SaveAnswer(Guid sessionId, Answer answer)
    {
        using var connection = db.CreateConnection();
        var sql = $"""
                   INSERT INTO {Answers.TableName} 
                   (
                           {Answers.AnswerId}, 
                           {Answers.SessionId}, 
                           {Answers.QuestionId}, 
                           {Answers.Value}, 
                           {Answers.Comment}, 
                           {Answers.Timestamp}
                   )
                   VALUES (
                           @AnswerId,
                           @SessionId, 
                           @QuestionId, 
                           @Value, 
                           @Comment, 
                           @Timestamp)
                   """;

        connection.Execute(sql, new
        {
            AnswerId = answer.AnswerId,
            SessionId = sessionId,
            answer.QuestionId,
            answer.Value,
            answer.Comment,
            answer.Timestamp
        });
    }

    public void CompleteSession(Guid sessionId)
    {
        using var connection = db.CreateConnection();
        var sql = $"""
                   UPDATE 
                        {Sessions.TableName} 
                   SET 
                        {Sessions.Status} = 'Completed', 
                        {Sessions.CompletedAt} = NOW() 
                   WHERE 
                        {Sessions.SessionId} = @SessionId
                   """;

        connection.Execute(sql, new { SessionId = sessionId });
    }

    public IEnumerable<Session> List(string userName)
    {
        using var connection = db.CreateConnection();
        var sql = $"""
                   SELECT * FROM {Sessions.TableName} 
                   WHERE {Sessions.UserName} = @userName
                   ORDER BY {Sessions.UpdatedAt} DESC
                   """;
        return connection.Query<Session>(sql, new { userName });
    }
}