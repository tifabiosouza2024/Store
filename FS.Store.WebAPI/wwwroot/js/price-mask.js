let phoneValue;
$(document).ready(function () {
    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
    };

    $.validator.methods.range = function (value, element, param) {
        if (this.optional(element)) return true;

        value = value.replace(/\./g, '').replace(',', '.');

        return !isNaN(value) && parseFloat(value) >= param[0] && parseFloat(value) <= param[1];
    };

    $('.email').inputmask("email");
    $('.price').focus(function () {
        $(this).select();
    });
    $('.price').inputmask('currency', {
        radixPoint: ',',
        removeMaskOnSubmit: true,
        digits: 2,
        onBeforePaste: function onBeforePaste(pastedValue) {
            return pastedValue.replace('.', ',');
        }
    });
    $('.phone').on('focus', function () {
        if (phoneValue == null) {
            phoneValue = $(this).val();
        }
    })
    $('.phone').on('blur', function () {
        verifyPhoneNumber($(this));
    });
    $('.percent').focus(function () {
        $(this).select();
    });
    $('.percent').inputmask('percentage', {
        min: 0,
        max: 100,
        suffix: " %",
        removeMaskOnSubmit: true,
        digits: 2,
        radixPoint: (typeof radixPoint !== 'undefined') ? radixPoint : ','
    });
    $('.cnpj').inputmask('99.999.999/9999-99');
});
