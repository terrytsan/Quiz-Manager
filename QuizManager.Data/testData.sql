INSERT INTO quizmanager.dbo.AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber,
										 PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled,
										 AccessFailedCount, UserName)
VALUES (N'1ab1cf34-da50-4b1b-9c25-b292b909bcb6', N'anotheruser3@test.com', 0,
		N'ALMA0AluFwVdCDmr4EBJjKgr612FWzelXyjMay71F+gUxBFJvzs8ElFk6/U9TQqy0w==',
		N'71bf51f4-fddb-4287-a91c-635d6e9295ff', NULL, 0, 0, 1, 0, N'Mary');
INSERT INTO quizmanager.dbo.AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber,
										 PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled,
										 AccessFailedCount, UserName)
VALUES (N'6d49ac46-be22-476b-9055-ac649039348e', N'user@test.com', 0,
		N'ACCY278getBeqlb4CaW28v5KCnGJiUMVHX7PcxOeie8ZlyeZu6CBR1BDzXcdCgZ/5g==',
		N'03c58ff2-2612-4d7a-8c47-f609e7dca2f5', NULL, 0, 0, 1, 0, N'Terry');
INSERT INTO quizmanager.dbo.AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber,
										 PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled,
										 AccessFailedCount, UserName)
VALUES (N'b3887680-9278-436a-86eb-2d224e944658', N'user4@test.com', 0,
		N'ANoFJA4Nh1b95YLDL7XJFSFq4/mb9+7kavRjNC+qrjB5VbY98SeBGrGuQ75m8tDAYg==',
		N'a58ad6c3-aa3d-4254-81c7-2660419024e4', NULL, 0, 0, 1, 0, N'Anthony');
INSERT INTO quizmanager.dbo.AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber,
										 PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled,
										 AccessFailedCount, UserName)
VALUES (N'b3b50224-1c60-4fb7-8adc-e6457f1a8185', N'anotheruser@test.com', 0,
		N'ADOOu79Lz7VyCsV0ka7pxa/A7fkgFd3sQD0t557lqYOnrKliN3srgKhQjsvsnE1Abw==',
		N'd3b1e4bd-b267-437f-a387-5b4f6e42c2be', NULL, 0, 0, 1, 0, N'James');
INSERT INTO quizmanager.dbo.AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber,
										 PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled,
										 AccessFailedCount, UserName)
VALUES (N'd3b94c0a-4c50-4d3c-ab57-00aba31da471', N'user2@test.com', 0,
		N'APWrzrjCXntEkSa3DZ/RlIYrclKV4kOlR6bsg/YMl2OFDyQSjm9MEQs9vJ55IFJLxA==',
		N'23b22a6c-72f0-45e0-a241-53b0b3111dda', NULL, 0, 0, 1, 0, N'Jimmy');

SET IDENTITY_INSERT quizmanager.dbo.quiz ON;
INSERT INTO quizmanager.dbo.quiz (id, hostId, name, startDate)
VALUES (1, N'6d49ac46-be22-476b-9055-ac649039348e', N'Terry''s Friday Quiz', N'2020-11-23');
INSERT INTO quizmanager.dbo.quiz (id, hostId, name, startDate)
VALUES (2, N'd3b94c0a-4c50-4d3c-ab57-00aba31da471', N'Jimmy''s Quiz', N'2020-11-25');
SET IDENTITY_INSERT quizmanager.dbo.quiz OFF;

SET IDENTITY_INSERT quizmanager.dbo.question ON;
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (1, 1, 1, 1);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (2, 1, 1, 2);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (3, 1, 1, 3);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (4, 1, 1, 4);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (5, 1, 1, 5);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (6, 1, 2, 1);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (7, 1, 2, 2);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (8, 1, 2, 3);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (9, 1, 2, 4);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (10, 1, 2, 5);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (11, 2, 1, 1);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (12, 2, 1, 2);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (13, 2, 1, 3);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (14, 2, 1, 4);
INSERT INTO quizmanager.dbo.question (id, quizId, round, questionNumber)
VALUES (15, 2, 1, 5);
SET IDENTITY_INSERT quizmanager.dbo.question OFF;

SET IDENTITY_INSERT quizmanager.dbo.AspNetUsers_quiz ON;
INSERT INTO quizmanager.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (1, 1, N'b3b50224-1c60-4fb7-8adc-e6457f1a8185');
SET IDENTITY_INSERT quizmanager.dbo.AspNetUsers_quiz ON;
INSERT INTO quizmanager.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (2, 2, N'b3b50224-1c60-4fb7-8adc-e6457f1a8185');
SET IDENTITY_INSERT quizmanager.dbo.AspNetUsers_quiz OFF;
INSERT INTO quizmanager.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (3, 2, N'6d49ac46-be22-476b-9055-ac649039348e');
INSERT INTO quizmanager.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (4, 2, N'd3b94c0a-4c50-4d3c-ab57-00aba31da471');
INSERT INTO quizmanager.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (5, 2, N'b3887680-9278-436a-86eb-2d224e944658');
INSERT INTO quizmanager.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (6, 1, N'd3b94c0a-4c50-4d3c-ab57-00aba31da471');
SET IDENTITY_INSERT quizmanager.dbo.AspNetUsers_quiz OFF;

INSERT INTO quizmanager.dbo.gameState (quizId, questionId)
VALUES (1, 3);
INSERT INTO quizmanager.dbo.gameState (quizId, questionId)
VALUES (2, 11);
