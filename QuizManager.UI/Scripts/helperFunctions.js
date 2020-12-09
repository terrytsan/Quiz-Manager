// Converts the C# DateTime into a human readable form (date and time)
function convertDateTime(timestamp) {
	let tIndex = timestamp.indexOf("T");
	return timestamp.substring(0, tIndex) + ' ' + timestamp.substring(tIndex + 1);
}

// Converts the C# DateTime into a human readable form (time only)
function convertDateTimeTimeOnly(timestamp) {
	let tIndex = timestamp.indexOf("T");
	return timestamp.substring(tIndex + 1);
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
							timestampDiv.innerText = convertDateTimeTimeOnly(participant.LatestAnswerTime);
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
				$(this).children(":first").children().eq(1).text(convertDateTimeTimeOnly(responseItem.TimestampString));
			}

			// Leave the .each() 
			return false;
		}
	})
}