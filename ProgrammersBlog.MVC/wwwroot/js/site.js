function ConvertFirstLetterToUpperCase(text) {
    return text.charAt(0).toUpperCase() + text.slice(1);
}

function ConvertToShortDate(dateString) {
    let shortDate = new Date(dateString).toLocaleDateString('en-US');
    return shortDate;
}