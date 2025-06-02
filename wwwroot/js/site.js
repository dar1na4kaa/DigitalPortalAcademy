document.addEventListener('DOMContentLoaded', function () {
    // Инициализация страницы расписания (если есть кнопки семестров)


    // Инициализация формы логина
    if (document.getElementById('login-form')) {
        initLoginFormValidation();
    }

    // Инициализация формы регистрации
    if (document.getElementById('registrationForm')) {
        initRegistrationFormValidation();
    }

    // Заглушки для ссылок с href="#"
    document.querySelectorAll('a[href="#"]').forEach(link => {
        link.addEventListener('click', function (e) {
            e.preventDefault();
            alert('Обратитесь к системному администратору в 509к. 1 корпуса');
        });
    });

    // Маска телефона (если подключен jQuery и inputmask)
    if (typeof $ !== 'undefined' && $.fn.inputmask && document.getElementById('Phone')) {
        $('#Phone').inputmask('+7 (999) 999-99-99');
    }
});
function initLoginFormValidation() {
    const loginForm = document.getElementById('login-form');
    loginForm.addEventListener('submit', function (event) {
        let isValid = true;

        const email = document.getElementById('email');
        if (!email.value || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email.value)) {
            email.classList.add('is-invalid');
            isValid = false;
        } else {
            email.classList.remove('is-invalid');
        }

        const password = document.getElementById('password');
        if (!password.value || password.value.length < 8) {
            password.classList.add('is-invalid');
            isValid = false;
        } else {
            password.classList.remove('is-invalid');
        }

        if (!isValid) {
            event.preventDefault();
        }
    });
}

function initRegistrationFormValidation() {
    const regForm = document.getElementById('registrationForm');
    regForm.addEventListener('submit', function (event) {
        let isValid = true;

        const fields = {
            email: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
            password: value => value.length >= 8,
            confirmPassword: value => value === document.getElementById('password').value,
            lastName: value => value.trim() !== '',
            firstName: value => value.trim() !== '',
            middleName: value => value.trim() !== '',
            role: value => !!value,
            uniqueNumber: /^[A-Za-z0-9\-]{4,15}$/
        };

        for (const [id, rule] of Object.entries(fields)) {
            const input = document.getElementById(id);
            if (!input) continue;

            const isValidField = typeof rule === 'function' ? rule(input.value) : rule.test(input.value);

            if (!isValidField) {
                input.classList.add('is-invalid');
                isValid = false;
            } else {
                input.classList.remove('is-invalid');
            }
        }

        if (!isValid) {
            event.preventDefault();
        }
    });
}

// ====== Расписание ======
document.addEventListener('DOMContentLoaded', function () {
    initSchedulePage();
});

function initSchedulePage() {
    // Кнопки выбора семестра
    document.querySelectorAll('.semester-btn[data-semester]').forEach(button => {
        button.addEventListener('click', function () {
            const semesterId = this.getAttribute('data-semester');
            showWeeks(semesterId);
        });
    });

    // Кнопки выбора дней
    document.querySelectorAll('.semester-weeks button[data-day]').forEach(button => {
        button.addEventListener('click', function () {
            const dayId = this.getAttribute('data-day');
            toggleScheduleVisibility(dayId);
        });
    });

    // Показываем 1-й семестр по умолчанию
    showWeeks('semester1');
}

function showWeeks(semesterId) {
    document.getElementById('semester1').style.display = semesterId === 'semester1' ? 'block' : 'none';
    document.getElementById('semester2').style.display = semesterId === 'semester2' ? 'block' : 'none';

    // НЕ трогаем расписание, чтобы не сбивать выбранный день
}

function toggleScheduleVisibility(dayId) {
    const scheduleContainer = document.getElementById('schedule-container');
    scheduleContainer.style.display = 'block';

    hideAllDays();

    const dayElem = document.getElementById(dayId);
    if (dayElem) {
        dayElem.style.display = 'block';
    }
}

function hideAllDays() {
    const days = ['monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday'];
    days.forEach(day => {
        const el = document.getElementById(day);
        if (el) el.style.display = 'none';
    });
}

