using FluentMigrator;
using static Core.Db.Migrations.DbSchema;

namespace Core.Db.Migrations;

[Migration(1)]
public class DbMigrations : Migration
{
    public override void Up()
    {
        Create.Table(Sessions.TableName)
            .WithColumn(Sessions.SessionId).AsGuid().PrimaryKey()
            .WithColumn(Sessions.UserName).AsString(50)
            .WithColumn(Sessions.FormId).AsString(50)
            .WithColumn(Sessions.Version).AsInt32()
            .WithColumn(Sessions.Status).AsString(20)
            .WithColumn(Sessions.StartedAt).AsDateTime()
            .WithColumn(Sessions.UpdatedAt).AsDateTime().Nullable()
            .WithColumn(Sessions.CompletedAt).AsDateTime().Nullable();

        Create.Table(Answers.TableName)
            .WithColumn(Answers.AnswerId).AsGuid().PrimaryKey()
            .WithColumn(Answers.SessionId).AsGuid().ForeignKey(Sessions.TableName, Sessions.SessionId)
            .WithColumn(Answers.QuestionId).AsString(255)
            .WithColumn(Answers.Value).AsString(500)
            .WithColumn(Answers.Comment).AsString(1000).Nullable()
            .WithColumn(Answers.Timestamp).AsDateTime();

        Create.UniqueConstraint(Constraints.UniqueSessionQuestion)
            .OnTable(Answers.TableName)
            .Columns(Answers.SessionId, Answers.QuestionId);
    }

    public override void Down()
    {
        Delete.Table(Answers.TableName);
        Delete.Table(Sessions.TableName);
    }
};