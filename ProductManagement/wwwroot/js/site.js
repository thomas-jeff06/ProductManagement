
const inputElement = document.getElementById('valueProduct');

inputElement.addEventListener('keypress', function (event) {
    const keyCode = event.keyCode || event.which;
    const forbiddenKeyCodes = [48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 44];

    if (!forbiddenKeyCodes.includes(keyCode)) {
        event.preventDefault();
    }
});

function closedButtonDivErro() {
    event.preventDefault()

    var divErro = document.getElementById('divErro');

    divErro.style.display = 'none';
}

function validateData() {
    event.preventDefault()

    var amountProduct = document.getElementById('amountProduct').value;
    var valueProduct = document.getElementById('valueProduct').value;

    var amountProductValid = isValid(amountProduct);
    var valueProductValid = isValid(valueProduct);

    if (amountProductValid && valueProductValid) {
        document.getElementById('formInsertOrders').submit();
    } else if (!amountProductValid) {
        alert('Quantidade inválido');
    } else {
        alert('Valor inválido');
    }
}

function isValid(number) {
    let numberToParse = number.replace(".", ",").replace(",", ".")

    let count = (number.split(".").length - 1) + number.split(",").length - 1;

    let numberParse = parseFloat(numberToParse);

    if (count < 2 && numberParse > 0 && numberParse < 100000000) {
            return true;
    }
    else
        return false;
}

function validateDataInsertProduct() {
    event.preventDefault()

    var selectCategory = document.getElementById('selectCategory').value;
    var productName = document.getElementById('productName').value;

    if (productName.trim() === '') {
        alert('Nome é Obrigatorio.');
    }
    else if (selectCategory > 0) {
        document.getElementById('formInsertProduct').submit();
    } else {
        alert('Selecione uma Categoria.');
    }
}
    