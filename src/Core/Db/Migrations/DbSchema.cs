namespace Core.Db.Migrations;

public static class DbSchema
{
    public static class Sessions
    {
        public const string TableName = "sessions";
        public const string SessionId = "session_id";
        public const string UserName = "user_name";
        public const string FormId = "form_id";
        public const string Version = "version";
        public const string Status = "status";
        public const string StartedAt = "started_at";
        public const string UpdatedAt = "updated_at";
        public const string CompletedAt = "completed_at";
    }

    public static class Answers
    {
        public const string TableName = "answers";
        public const string AnswerId = "answer_id";
        public const string SessionId = "session_id";
        public const string QuestionId = "question_id";
        public const string Value = "value";
        public const string Comment = "comment";
        public const string Timestamp = "timestamp";
    }

    public static class Constraints
    {
        public const string UniqueSessionQuestion = "session_id_question_id_per_answers";
    }
}