//function validateProduct() {
//    event.preventDefault()

//    var productId = document.getElementById('inputProductId').value;
//    var orderItemsList = @Html.Raw(Json.Encode(orderItemsList));

//    // Verifica se o productId está presente na lista de productIds
//    if (orderItemsList.includes(productId)) {
//        alert("Erro: O Produto já existe na lista!");
//        // Você pode fazer outras ações para tratar o erro, como exibir uma mensagem de erro na tela ou tomar outra ação específica
//        event.preventDefault(); // Impede o redirecionamento para o link
//    }
//}

const inputElement = document.getElementById('valueProduct');

inputElement.addEventListener('keypress', function (event) {
    const keyCode = event.keyCode || event.which;
    const forbiddenKeyCodes = [48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 44];

    if (currentValue.includes(',')) {
        event.preventDefault();
    } else if (!forbiddenKeyCodes.includes(keyCode)) {
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
    let count = number.split(",").length - 1;
    let numberParse = parseFloat(number.replace(",", "."));

    if (count < 2 && numberParse > 0 && numberParse < 100000000) {
            return true;
    }
    else
        return false;
}
    