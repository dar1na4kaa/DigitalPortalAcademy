document.addEventListener('DOMContentLoaded', function () {
    const links = document.querySelectorAll('a[href="#"]');
    links.forEach(function (link) {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            alert('Обратитесь к системному администратору в 509к. 1 корпуса');
        });
    });
});

document.getElementById('login-form').addEventListener('submit',function (event) {
    let isValid = true;

    let email = document.getElementById('email');
    if (!email.value || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email.value)) {
        email.classList.add('is-invalid');
        isValid = false;
    }
    else {
        email.classList.remove('is-invalid')
    }

    let password = document.getElementById('password');
    if (!password.value || password.value.length < 8) {
        password.classList.add('is-invalid');
        isValid = false;
    } else {
        password.classList.remove('is-invalid');
    }

    if (!isValid) {
        event.preventDefault();
    }
})
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

    let lastName = document.getElementById('lastName');
    let firstName = document.getElementById('firstName');
    let middleName = document.getElementById('middleName');

    if (!lastName.value.trim()) {
        lastName.classList.add('is-invalid');
        isValid = false;
    } else {
        lastName.classList.remove('is-invalid');
    }

    if (!firstName.value.trim()) {
        firstName.classList.add('is-invalid');
        isValid = false;
    } else {
        firstName.classList.remove('is-invalid');
    }

    if (!middleName.value.trim()) {
        middleName.classList.add('is-invalid');
        isValid = false;
    } else {
        middleName.classList.remove('is-invalid');
    }

    let role = document.getElementById('role');
    if (!role.value) {
        role.classList.add('is-invalid');
        isValid = false;
    } else {
        role.classList.remove('is-invalid');
    }

    let identifier = document.getElementById('uniqueNumber');
    if (!identifier.value || !/^[A-Za-z0-9\-]{6,15}$/.test(identifier.value)) {
        identifier.classList.add('is-invalid');
        isValid = false;
    } else {
        identifier.classList.remove('is-invalid');
    }

    if (!isValid) {
        event.preventDefault();
    }
});

document.getElementById('role').addEventListener('change', function () {
    let studentEmployeeField = document.getElementById('studentEmployeeField');
    let identifier = document.getElementById('uniqueNumber');

    studentEmployeeField.classList.remove('d-none');
    identifier.setAttribute("required", "true");
});


