﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using QuizManager.Data.Factories;
using QuizManager.Models.Queries;
using QuizManager.Models.Tables;

namespace QuizManager.UI.Hubs
{
    public class ParticipateHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        // Test method to return the calling client's user id
        public string GetOwnUserId()
        {
            return Context.User.Identity.GetUserId();
            // return Clients.Caller.printUserId(Context.User.Identity.Name);
        }

        public void SubmitResponse(int questionId, string responseText)
        {
            var responseRepo = ResponseRepositoryFactory.GetRepository();
            var questionRepo = QuestionRepositoryFactory.GetRepository();
            // Add to database
            var response = new Response
            {
                UserId = Context.User.Identity.GetUserId(), QuestionId = questionId, ResponseText = responseText,
                Points = 0, Timestamp = DateTime.Now
            };

            var responseId = responseRepo.AddResponse(response);
            var question = questionRepo.GetQuestion(questionId);

            // Broadcast who just submitted a response
            Clients.All.handleNewResponseSubmission(new ResponseItem
            {
                Id = responseId, QuizId = question.QuizId, Round = question.Round,
                QuestionNumber = question.QuestionNumber, Name = Context.User.Identity.Name,
                ResponseText = response.ResponseText,
                Points = response.Points, Timestamp = response.Timestamp,
                TimestampString = response.Timestamp.ToString("HH:mm:ss.fff")
            });
        }
    }
}