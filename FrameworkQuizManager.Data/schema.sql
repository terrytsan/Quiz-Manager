create table quiz
(
	id int identity
		constraint quiz_pk
			primary key nonclustered,
	hostId nvarchar(128)
		constraint quiz_AspNetUsers_Id_fk
			references AspNetUsers,
	name varchar(100),
	startDate date
);

create table question
(
	id int identity
		constraint question_pk
			primary key nonclustered,
	quizId int
		constraint question_quiz_id_fk
			references quiz,
	round int,
	questionNumber int
);

create table response
(
	id int identity
		constraint response_pk
			primary key nonclustered,
	userId nvarchar(128)
		constraint response_AspNetUsers_Id_fk
			references AspNetUsers,
	questionId int
		constraint response_question_id_fk
			references question,
	responseText nvarchar(256),
	points int,
	timestamp datetime
);

create table AspNetUsers_quiz
(
	id int identity
		constraint AspNetUsers_quiz_pk
			primary key nonclustered,
    QuizId int
        constraint AspNetUsers_quiz_quiz_id_fk
            references quiz,
	UserId nvarchar(128)
		constraint AspNetUsers_quiz_AspNetUsers_Id_fk
			references AspNetUsers
	
);

create table gameState
(
    quizId     int not null
        constraint gameState_pk
            primary key nonclustered
        constraint gameState_quiz_id_fk
            references quiz,
    questionId int
        constraint gameState_question_id_fk
            references question
);

exec sp_addextendedproperty 'MS_Description', 'Quiz Participants', 'SCHEMA', 'dbo', 'TABLE', 'AspNetUsers_quiz'
go

