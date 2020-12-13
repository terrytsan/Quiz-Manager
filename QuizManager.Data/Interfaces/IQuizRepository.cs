using System.Collections.Generic;
using QuizManager.Models.Queries;
using QuizManager.Models.Tables;

namespace QuizManager.Data.Interfaces
{
    public interface IQuizRepository
    {
        /**
         * Get a quiz given the quiz id. Null if not found
         */
        Quiz GetQuiz(int quizId);

        /**
         * Get a a list of quizzes the user is a participant of 
         */
        IEnumerable<Quiz> GetQuizzesForParticipant(string userId);

        /**
         * Get the participants of the quiz given the question id
         */
        IEnumerable<QuestionParticipant> GetParticipantsOfQuestion(int questionId);

        /**
         * Get the participants of the quiz given the quiz id
         */
        IEnumerable<QuestionParticipant> GetParticipantsOfQuiz(int quizId);

        /**
         * Get a list of quizzes where the user is the host
         */
        IEnumerable<Quiz> GetQuizzesForUser(string userId);

        int AddQuiz(Quiz quiz);

        /**
         * Add a participant to the quiz
         */
        int AddParticipantToQuiz(int quizId, string userId);

        /**
         * Gets the total score of the user for a particular quiz
         */
        QuizParticipant GetParticipantQuizScore(int quizId, string userId);

        /**
         * Get scores of each participant in the quiz
         */
        IEnumerable<QuizParticipant> GetQuizScores(int quizId);

        /**
         * Remove a participant from the quiz. This will also remove any responses made by the participant.
         */
        void RemoveParticipantFromQuiz(int quizId, string userId);
    }
}