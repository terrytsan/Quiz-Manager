﻿function updateCurrentQuestion(question) {
	$('#txtCurrentQuestion').text('Round ' + question.Round + ' Question ' + question.QuestionNumber);
}

function showEndOfQuizAlert() {
	$('#endOfQuizAlert').show()
}