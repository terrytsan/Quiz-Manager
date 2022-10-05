<h1 align="center">Quiz Manager</h1>

<div align="center">A cross-platform quiz management application for hosting and participating in quizzes.</div>

## About

This application provides a platform for quiz participants to submit answers and quiz hosts to assign scores for each submission. Quiz questions should be presented via another medium.


## Built with

* ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
* ![JavaScript](https://img.shields.io/badge/javascript-%23323330.svg?style=for-the-badge&logo=javascript&logoColor=%23F7DF1E)
* ![Bootstrap](https://img.shields.io/badge/bootstrap-%23563D7C.svg?style=for-the-badge&logo=bootstrap&logoColor=white)

## Features

* See when other participants submit answers in real-time (via SignalR).
  * Allows for speed rounds - where extra points are awarded for answering quickly.
* Calculate the score for each participant based on correct/incorrect submissions.
* Quiz master features:
  * Add / remove participants.
  * Advance the current question.
  * Enable / disable submissions with an optional delay (useful for timed rounds).
  * See responses live as they are submitted.
  * Award points for each submission.
* Supports multiple simultaneous quiz sessions.
* Email / password authentication.
* Supports mobile and desktop devices with reactive layouts. 

## Usage

### Setting up a Quiz

1. Navigate to the Quiz Manager page by clicking 'Quiz Manager' on the navigation bar.
2. Click the add button to create a new quiz.
3. Enter the name of the quiz and the number of questions in each round. This means submissions can be assigned to individual questions. 

<p align="center">
    <img src="https://user-images.githubusercontent.com/12361038/193458107-e456705c-de01-4d23-a98e-933b9e78cd11.png" alt="Create Quiz Page" width="80%">
</p>

### Quiz Management

<p align="center">
    <img src="https://user-images.githubusercontent.com/12361038/193457692-4516d6ac-14c7-4b70-8e18-a38ae064a701.png" alt="Creating Quiz Page" width="80%">
</p>

A quiz can be managed from this screen using the following features:

* Enable/disable submissions with the following settings for delay:
  * Reset delay each time - Delay is reset to 0 each time submissions are enabled or disabled.
  * No delay on enable - Delay only applies when disabling submissions.
  * Auto enable on next question - Submissions enabled as soon as question is advanced.
* Advance the current question. This changes which question submissions are assigned to.
* Add/remove participants from the quiz.
* See who has submitted for the current question (Live Responses section).
* See a full history of all responses submitted for the quiz and assign scores to each response.

### Participating

1. Navigate to the Participate page by clicking 'Participate' on the navigation bar.
2. Use the dropdown to select from a list of quizzes you've been invited to. After selecting a quiz:
   * The current question for the quiz is shown and a label indicating if submissions are enabled.
   * See the submission status of other participants for the current question.
   * See scores of other participants.

<p align="center">
    <img src="https://user-images.githubusercontent.com/12361038/193458179-d424c4ba-dd05-4e0e-bbb4-7e6c12b0612c.png" alt="Participate on desktop" height="285px">
    &nbsp; &nbsp; &nbsp; &nbsp;
    <img src="https://user-images.githubusercontent.com/12361038/193458182-d7d113e7-dcb9-4bf1-9670-ed3e5d9c5574.png" alt="Participate on mobile" height="285px">
</p>
<p align="center">
    <em>Supports mobile and desktop devices.</em>
</p>
<br>

<p align="center">
    <img src="https://user-images.githubusercontent.com/12361038/194128936-d0cf045e-9501-4cd0-baef-5241166d278a.gif" alt="Delay countdown timer" height="400px" align="center">
</p>
<p align="center">
    <em>If a delay has been set, a countdown bar will be shown to indicate amount of time remaining.</em>
</p>

## Setting up Project Locally

### Prerequisites

* Existing SQL database

### Initializing Database

1. Define an app secret `DefaultConnection` containing sql connection string. Example:

   ```xml
    <secret name="DefaultConnection" value="Data Source=tcp:[ServerAddress],[Port];Initial Catalog=[DatabaseName];User Id=[Username];Password=[Password]" />
    ```

2. In the UI Project, run the following commands in the Packager Manger Console to generate the schema and write to the database:
   ```shell
   add-migration init
   ```
   ```shell
   update-database
   ```

3. Run sql script `QuizManager.Data/schema.sql`

### Running QuizManager (IDE)

1. Set QuizManager.UI as the default startup project and run the application.

### Running QuizManager (Docker)

A `DockerFile` is included in the UI project. To build and run the docker image:

1. Run the following command in the root of the solution to build the docker image.
   ```shell
   docker build -f QuizManager.UI\Dockerfile --force-rm -t quizmanagerapp .
   ```
2. Run the container with the sql connection string as an environment variable.
   ```shell
   docker run -dp 8888:80 -e "ConnectionStrings:DefaultConnection"="Data Source=tcp:[serverAddress],[Port];Initial Catalog=[DatabaseName];User Id=[Username];Password=[Password]"
   ```
3. Go to http://localhost:8888