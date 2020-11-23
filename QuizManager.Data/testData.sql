INSERT INTO QuizManagerTest.dbo.AspNetUsers (Id, UserName)
VALUES (N'1ab1cf34-da50-4b1b-9c25-b292b909bcb6', N'Mary');
INSERT INTO QuizManagerTest.dbo.AspNetUsers (Id, UserName)
VALUES (N'6d49ac46-be22-476b-9055-ac649039348e', N'Terry');
INSERT INTO QuizManagerTest.dbo.AspNetUsers (Id, UserName)
VALUES (N'b3887680-9278-436a-86eb-2d224e944658', N'Anthony');
INSERT INTO QuizManagerTest.dbo.AspNetUsers (Id, UserName)
VALUES (N'b3b50224-1c60-4fb7-8adc-e6457f1a8185', N'James');
INSERT INTO QuizManagerTest.dbo.AspNetUsers (Id, UserName)
VALUES (N'd3b94c0a-4c50-4d3c-ab57-00aba31da471', N'Jimmy');

INSERT INTO QuizManagerTest.dbo.quiz (id, hostId, name, startDate)
VALUES (1, N'6d49ac46-be22-476b-9055-ac649039348e', N'Terry''s Friday Quiz', N'2020-11-23');
INSERT INTO QuizManagerTest.dbo.quiz (id, hostId, name, startDate)
VALUES (2, N'd3b94c0a-4c50-4d3c-ab57-00aba31da471', N'Jimmy''s Quiz', N'2020-11-25');

INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (1, 1, 1, 1);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (2, 1, 1, 2);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (3, 1, 1, 3);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (4, 1, 1, 4);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (5, 1, 1, 5);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (6, 1, 2, 1);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (7, 1, 2, 2);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (8, 1, 2, 3);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (9, 1, 2, 4);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (10, 1, 2, 5);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (11, 2, 1, 1);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (12, 2, 1, 2);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (13, 2, 1, 3);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (14, 2, 1, 4);
INSERT INTO QuizManagerTest.dbo.question (id, quizId, round, questionNumber)
VALUES (15, 2, 1, 5);

INSERT INTO QuizManagerTest.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (1, 1, N'b3b50224-1c60-4fb7-8adc-e6457f1a8185');
INSERT INTO QuizManagerTest.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (2, 2, N'b3b50224-1c60-4fb7-8adc-e6457f1a8185');
INSERT INTO QuizManagerTest.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (3, 2, N'6d49ac46-be22-476b-9055-ac649039348e');
INSERT INTO QuizManagerTest.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (4, 2, N'd3b94c0a-4c50-4d3c-ab57-00aba31da471');
INSERT INTO QuizManagerTest.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (5, 2, N'b3887680-9278-436a-86eb-2d224e944658');
INSERT INTO QuizManagerTest.dbo.AspNetUsers_quiz (id, QuizId, UserId)
VALUES (6, 1, N'd3b94c0a-4c50-4d3c-ab57-00aba31da471');

INSERT INTO QuizManagerTest.dbo.gameState (id, quizId, questionId)
VALUES (1, 1, 3);
INSERT INTO QuizManagerTest.dbo.gameState (id, quizId, questionId)
VALUES (2, 2, 11);