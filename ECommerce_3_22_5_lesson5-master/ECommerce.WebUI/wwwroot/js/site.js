document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('input-box');
    searchInput.addEventListener('keyup', function () {
        const input = searchInput.value;

        if (input.length > 0) {
            fetchProducts(input);
        } else {
            fetchProducts('');  
        }
    });
});

function fetchProducts(searchTerm) {
    const currentCategory = '@Model.CurrentCategory';  
    const currentPage = '@Model.CurrentPage'; 

    fetch(`/Product/SearchProducts?searchTerm=${searchTerm}&category=${currentCategory}&page=${currentPage}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.text())
        .then(data => {
            const productList = document.getElementById('productList');
            if (productList) {
                productList.innerHTML = data;
            }
        })
        .catch(error => console.error('Fetch Error:', error));
}