// Converts the C# DateTime (yyyy-mm-ddThh:mm:ss.fff%K) into a human readable form (date and time)
function convertDateTime(timestamp) {
	// Create js date from iso formatted string
	let jsDate = new Date(timestamp);
	return jsDate.toLocaleString();
}

// Converts the C# DateTime (yyyy-mm-ddThh:mm:ss.fff%K) into a human readable form (hh:mm:ss.fff)
function convertDateTimeTimeOnly(timestamp) {
	// Create js date from iso formatted string
	let jsDate = new Date(timestamp);
	return (`${("0" + jsDate.getHours()).substr(-2)}:${("0" + jsDate.getMinutes()).substr(-2)}:${("0" + jsDate.getSeconds()).substr(-2)}.${("00" + jsDate.getMilliseconds()).substr(-3)}`);
}

// Gets the participants for the question and populates a list with live response information
function getParticipantsForQuestionAndPopulateList(questionId, listToPopulate, includeTimestamp) {
	$.ajax({
		type: 'GET',
		url: '/api/question/participants',
		data: {
			questionId: questionId
		},
		success: function (participants) {
			// Populate the list of participants
			listToPopulate.empty().append(
				participants.map(participant => {
						// Create the list item
						let listItem = document.createElement('li');
						// Set the class if they've answered
						if (participant.HasAnswered) {
							listItem.setAttribute('class', 'list-group-item list-group-item-success')
						} else {
							listItem.setAttribute('class', 'list-group-item')
						}

						let container = document.createElement('div');
						container.setAttribute('class', 'd-flex');

						let name = document.createElement('div');
						name.setAttribute('class', 'mr-auto');
						name.innerText = participant.Name
						container.appendChild(name)

						let timestampDiv = document.createElement('div');
						// Add the timestamp if it's requested
						if (participant.HasAnswered && includeTimestamp) {
							timestampDiv.innerText = convertDateTimeTimeOnly(participant.LatestAnswerTime + 'Z');
						}
						container.appendChild(timestampDiv)

						// Append the entire container to the list item
						listItem.appendChild(container);

						return listItem
					}
				)
			);
		},
		error: function () {
			console.log("Error fetching participants for question");
		}
	});
}

// Updates the live participants list with the newly submitted response item
function updateLiveParticipantResponsesList(responseItem, participantsList, includeTimestamp) {
	participantsList.find('li').each(function () {
		if ($(this).children(":first").children(":first").text() === responseItem.Name) {
			$(this).addClass("list-group-item-success");
			// Add the timestamp if requested
			if (includeTimestamp) {
				$(this).children(":first").children().eq(1).text(convertDateTimeTimeOnly(responseItem.Timestamp));
			}

			// Leave the .each() 
			return false;
		}
	})
}