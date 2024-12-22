$(document).ready(function () {
    $('a[href="#"]').on('click', function (e) {
        e.preventDefault();
        alert('Обратитесь к системному администратору в 509к. 1 корпуса');
    });
});
