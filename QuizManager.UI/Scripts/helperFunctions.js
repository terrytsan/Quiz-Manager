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