using DynamicExpresso;

namespace Core.Evaluation;

public class ExpressionEvaluator
{
    /// <summary>
    /// Evaluates an applicability expression in the context of a questionnaire session.
    /// </summary>
    public bool Evaluate(string expression, Session session, IDictionary<string, Answer> answers)
    {
        if (string.IsNullOrWhiteSpace(expression))
            return true; // No condition means always applicable

        // Set up the interpreter context
        var interpreter = new Interpreter();

        // Provide session properties as variables that the expression can use
        interpreter.SetVariable("userId", session.UserName);
        interpreter.SetVariable("session", session);
        interpreter.SetVariable("answers", answers);

        // Evaluate the expression as a boolean
        return interpreter.Eval<bool>(expression);
    }
}