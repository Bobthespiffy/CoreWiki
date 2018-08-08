// Write your Javascript code.
(function () {
    var reformatTimeStamps = function () {
        var timeStamps = document.getElementsByClassName('timeStampValue');
        for (var ts of timeStamps) {
            var thisTimestamp = ts.getAttribute('data-value');
            var date = new Date(thisTimestamp);
            ts.textContent = moment(date).format('LLL');
        }
    }

    reformatTimeStamps();
})();