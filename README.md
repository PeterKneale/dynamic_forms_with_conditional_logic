# Presents dynamic forms with criteria determing which questions are asked

- there are many versions of a form
- there are many questions in a form
- users are asked to complete the questions in a single form
- a session is created when a user starts filling out a form and can be resumed later
- the questions are asked in order until none remain
- each question can have different response types such as text, yesno, single select, multiselect 
- each question may have have an optional comments field enabled or not.
- each answer must be recorded
- each question has a title, body and help text
- the title body and help text uses a templating engine so it can be customised based on the context
- each question has dynamic criteria that determine whether a question is applicable or not. The expression may refer to the answers given to previously asked questions or other information such as the users organisation or role. If a question is not applicable, it will proceed to the next question and check its applicability. 
- The expression may refer to the answers given to previously asked questions or other information such as the users organisation or role

# implementation
- Uses postgres + npgsql + dapper
- Uses Scriban for templating questions titles etc
- Uses DynamicExpresso.Core for expressions like `"Expression": "answers.ContainsKey(\"Q1\") && answers[\"Q1\"].Value == \"Yes\""`
