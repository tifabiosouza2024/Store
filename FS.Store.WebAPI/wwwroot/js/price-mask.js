
document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('input.price').forEach(input => {
        const originalName = input.name;

        if (!originalName) {
            alert('O input.price precisa ter um atributo name.');
            return;
        }

        // Cria automaticamente o campo oculto que será enviado ao servidor.
        const hidden = document.createElement('input');
        hidden.type = 'hidden';
        hidden.name = originalName;
        hidden.value = '0.00';

        input.insertAdjacentElement('afterend', hidden);

        // Evita enviar o campo visível junto com o hidden.
        input.removeAttribute('name');

        // Mantém a máscara baseada em dígitos.
        let digits = '0';

        function formatPrice() {
            const padded = digits.padStart(3, '0');
            const integer = padded.slice(0, -2);
            const cents = padded.slice(-2);

            return integer.replace(/\B(?=(\d{3})+(?!\d))/g, '.') +
                ',' + cents;
        }

        function update() {
            input.value = formatPrice();

            const padded = digits.padStart(3, '0');
            const integer = padded.slice(0, -2).replace(/^0+(?=\d)/, '');
            const cents = padded.slice(-2);

            hidden.value = integer + ',' + cents;
        }

        input.addEventListener('focus', () => {
            input.select();
        });

        input.addEventListener('keydown', event => {
            if (/^\d$/.test(event.key)) {
                event.preventDefault();

                const selectedAll =
                    input.selectionStart === 0 &&
                    input.selectionEnd === input.value.length;

                digits = selectedAll
                    ? event.key
                    : (digits + event.key).replace(/^0+(?=\d)/, '');

                update();
                return;
            }

            if (event.key === 'Backspace' || event.key === 'Delete') {
                event.preventDefault();

                const selectedAll =
                    input.selectionStart === 0 &&
                    input.selectionEnd === input.value.length;

                digits = selectedAll
                    ? '0'
                    : digits.slice(0, -1) || '0';

                update();
                return;
            }

            if (
                event.ctrlKey ||
                event.metaKey ||
                [
                    'Tab', 'ArrowLeft', 'ArrowRight',
                    'Home', 'End'
                ].includes(event.key)
            ) {
                return;
            }

            if (event.key.length === 1) {
                event.preventDefault();
            }
        });

        input.addEventListener('paste', event => {
            event.preventDefault();

            const pasted = event.clipboardData.getData('text');
            const numbers = pasted.replace(/\D/g, '');

            if (numbers) {
                digits = numbers.replace(/^0+(?=\d)/, '') || '0';
                update();
            }
        });

        update();
    });

    // Opcional: foca automaticamente no primeiro campo monetário.
    document.querySelector('input.price')?.focus();
});
