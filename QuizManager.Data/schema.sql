CREATE TABLE quiz
(
	id        int IDENTITY
		CONSTRAINT quiz_pk
			PRIMARY KEY NONCLUSTERED,
	hostId    nvarchar(128)
		CONSTRAINT quiz_AspNetUsers_Id_fk
			REFERENCES AspNetUsers,
	name      varchar(100),
	startDate date
);

CREATE TABLE question
(
	id             int IDENTITY
		CONSTRAINT question_pk
			PRIMARY KEY NONCLUSTERED,
	quizId         int
		CONSTRAINT question_quiz_id_fk
			REFERENCES quiz,
	round          int,
	questionNumber int
);

CREATE TABLE response
(
	id           int IDENTITY
		CONSTRAINT response_pk
			PRIMARY KEY NONCLUSTERED,
	userId       nvarchar(128)
		CONSTRAINT response_AspNetUsers_Id_fk
			REFERENCES AspNetUsers,
	questionId   int
		CONSTRAINT response_question_id_fk
			REFERENCES question,
	responseText nvarchar(256),
	points       int,
	timestamp    datetime
);

CREATE TABLE AspNetUsers_quiz
(
	id     int IDENTITY
		CONSTRAINT AspNetUsers_quiz_pk
			PRIMARY KEY NONCLUSTERED,
	QuizId int
		CONSTRAINT AspNetUsers_quiz_quiz_id_fk
			REFERENCES quiz,
	UserId nvarchar(128)
		CONSTRAINT AspNetUsers_quiz_AspNetUsers_Id_fk
			REFERENCES AspNetUsers

);

CREATE TABLE gameState
(
	quizId     int NOT NULL
		CONSTRAINT gameState_pk
			PRIMARY KEY NONCLUSTERED
		CONSTRAINT gameState_quiz_id_fk
			REFERENCES quiz,
	questionId int
		CONSTRAINT gameState_question_id_fk
			REFERENCES question
);

EXEC sp_addextendedproperty 'MS_Description', 'Quiz Participants', 'SCHEMA', 'dbo', 'TABLE', 'AspNetUsers_quiz'
GO

