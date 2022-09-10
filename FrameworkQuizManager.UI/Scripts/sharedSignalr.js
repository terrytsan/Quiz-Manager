function updateCurrentQuestionText(question) {
	$('#txtCurrentQuestion').text('Round ' + question.Round + ' Question ' + question.QuestionNumber);
}

/**
 * Draws an end of quiz alert after the specified component
 * @param afterComponent Component to insert alert after
 */
function showEndOfQuizAlert(afterComponent) {
	if (!$('#endOfQuizAlert').length) {
		afterComponent.after(
			'<div id="endOfQuizAlert" class="alert alert-danger" role="alert">' +
			'End of Quiz' +
			'<button type="button" class="close" data-dismiss="alert">' +
			'<span>&times</span>' +
			'</button>' +
			'</div>'
		);
	}
}

function updateQuizStatusBadge(newStatus) {
	// newStatus is true if submission enabled
	let statusBadge = $('#statusBadge');

	if (newStatus) {
		statusBadge.text('Enabled');
		statusBadge.removeClass('badge-danger').addClass('badge-success');
	} else {
		statusBadge.text('Disabled');
		statusBadge.removeClass('badge-success').addClass('badge-danger');
	}
}

/**
 * Draws a progress bar from a starting time
 * @param startingTime Initial starting time to count down from (ms)
 * @param afterComponent Component to insert progress bar after
 */
function startCountdown(startingTime, afterComponent) {
	let updateFreq = 150;
	let currentTime = startingTime;

	// Add the progress bar if it doesn't already exist
	if (!$('#progressContainer').length) {
		afterComponent.after(
			'<div id="progressContainer" class="progress" style="height: 1px">' +
			'<div id="cdProgressBar" class="progress-bar" style="width:100%"></div>' +
			'</div>')
	}

	let x = setInterval(function () {
		currentTime = currentTime - updateFreq;
		let progressBarPercent = (currentTime / startingTime) * 100;
		// Update the progress bar
		$('#cdProgressBar').css({width: progressBarPercent + '%'});
		if (currentTime <= 0) {
			clearInterval(x);
			// Remove the progress bar
			$('#progressContainer').remove();
		}
	}, updateFreq);
}

/**
 * Updates the table with the participant's scores.
 * @param tableId ID of the table to update
 * @param scores the scores
 */
function refreshParticipantScoresTable(tableId, scores) {
	// Empty table
	$('#' + tableId + ' tbody').empty();

	scores.forEach(score => {
		//Create a new row
		let newRow = document.getElementById(tableId).getElementsByTagName('tbody')[0].insertRow();

		// Fill in the cells
		let nameCell = newRow.insertCell(0);
		nameCell.appendChild(document.createTextNode(score.Name));
		let scoreCell = newRow.insertCell(1);
		scoreCell.appendChild(document.createTextNode(score.Score));
	});
}