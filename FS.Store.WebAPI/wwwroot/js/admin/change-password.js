
document.addEventListener('DOMContentLoaded', function () {
    const passwordFields = document.querySelectorAll('.account-toggle-password');

    passwordFields.forEach(button => {
        button.addEventListener('click', function () {
            const input = document.getElementById(this.dataset.target);
            const icon = this.querySelector('i');

            if (!input || !icon) return;

            const showPassword = input.type === 'password';

            input.type = showPassword ? 'text' : 'password';
            icon.classList.toggle('bi-eye', !showPassword);
            icon.classList.toggle('bi-eye-slash', showPassword);

            this.setAttribute('aria-label', showPassword ? 'Ocultar senha' : 'Mostrar senha');
        });
    });

    const newPassword = document.getElementById('NewPassword');
    const confirmPassword = document.getElementById('ConfirmPassword');
    const strengthBar = document.getElementById('passwordStrengthBar');
    const strengthText = document.getElementById('passwordStrengthText');
    const matchText = document.getElementById('passwordMatchText');
    const passwordForm = document.getElementById('changePasswordForm');

    if (!newPassword || !confirmPassword) return;

    function validatePasswordStrength() {
        const password = newPassword.value;
        let score = 0;

        if (password.length >= 8) score++;
        if (password.length >= 12) score++;
        if (/[a-z]/.test(password) && /[A-Z]/.test(password)) score++;
        if (/\d/.test(password)) score++;
        if (/[^a-zA-Z0-9]/.test(password)) score++;

        const levels = [
            { text: 'Informe sua nova senha.', width: '0%', color: '' },
            { text: 'Muito fraca', width: '20%', color: 'bg-danger' },
            { text: 'Fraca', width: '40%', color: 'bg-danger' },
            { text: 'Razoável', width: '60%', color: 'bg-warning' },
            { text: 'Boa', width: '80%', color: 'bg-info' },
            { text: 'Forte', width: '100%', color: 'bg-success' }
        ];

        const level = password ? levels[score] : levels[0];

        strengthBar.style.width = level.width;
        strengthBar.className = `progress-bar ${level.color}`;
        strengthText.textContent = level.text;
    }

    function validatePasswordMatch() {
        const password = newPassword.value;
        const confirmation = confirmPassword.value;
        const hasConfirmation = confirmation.length > 0;
        const matches = password === confirmation;

        confirmPassword.setCustomValidity(
            hasConfirmation && !matches ? 'As senhas não coincidem.' : ''
        );

        confirmPassword.classList.toggle('is-invalid', hasConfirmation && !matches);
        confirmPassword.classList.toggle('is-valid', hasConfirmation && matches);

        if (!hasConfirmation) {
            matchText.textContent = '';
            matchText.className = 'text-muted';
        } else if (matches) {
            matchText.textContent = 'As senhas coincidem.';
            matchText.className = 'text-success';
        } else {
            matchText.textContent = 'As senhas não coincidem.';
            matchText.className = 'text-danger';
        }

        return matches;
    }

    newPassword.addEventListener('input', function () {
        validatePasswordStrength();
        validatePasswordMatch();
    });

    confirmPassword.addEventListener('input', validatePasswordMatch);

    passwordForm?.addEventListener('submit', function (event) {
        validatePasswordMatch();

        if (!passwordForm.checkValidity()) {
            event.preventDefault();
            passwordForm.reportValidity();
        }
    });

    validatePasswordStrength();
    validatePasswordMatch();
});
