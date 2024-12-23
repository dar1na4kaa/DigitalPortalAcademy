document.addEventListener('DOMContentLoaded', function () {
    const links = document.querySelectorAll('a[href="#"]');
    links.forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            alert('Обратитесь к системному администратору в 509к. 1 корпуса');
        });
    });
});

document.getElementById('registrationForm').addEventListener('submit', function (event) {
    let isValid = true;

    let email = document.getElementById('email');
    if (!email.value || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email.value)) {
        email.classList.add('is-invalid');
        isValid = false;
    } else {
        email.classList.remove('is-invalid');
    }

    let password = document.getElementById('password');
    if (!password.value || password.value.length < 8) {
        password.classList.add('is-invalid');
        isValid = false;
    } else {
        password.classList.remove('is-invalid');
    }

    let confirmPassword = document.getElementById('confirmPassword');
    if (confirmPassword.value !== password.value) {
        confirmPassword.classList.add('is-invalid');
        isValid = false;
    } else {
        confirmPassword.classList.remove('is-invalid');
    }

    let fullName = document.getElementById('fullName');
    if (!fullName.value || fullName.value.trim().split(' ').length < 2) {
        fullName.classList.add('is-invalid');
        isValid = false;
    } else {
        fullName.classList.remove('is-invalid');
    }

    let uniqueCode = document.getElementById('uniqueCode');
    if (!uniqueCode.value) {
        uniqueCode.classList.add('is-invalid');
        isValid = false;
    } else {
        uniqueCode.classList.remove('is-invalid');
    }

    let role = document.getElementById('role');
    if (!role.value) {
        role.classList.add('is-invalid');
        isValid = false;
    } else {
        role.classList.remove('is-invalid');
    }

    if (!isValid) {
        event.preventDefault();
    }
});
