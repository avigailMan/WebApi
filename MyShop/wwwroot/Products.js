window.onload = async function windowLoading() {
    try {
        await presentAllProduct();

        if (!sessionStorage.getItem("countBasket")) {
            sessionStorage.setItem("countBasket", 0);
        }
        document.getElementById('ItemsCountText').textContent = sessionStorage.getItem("countBasket");

        if (!sessionStorage.getItem("sumBasket")) {
            sessionStorage.setItem("sumBasket", 0);
        }

        presentCategory();
    } catch (error) {
        console.error('Error during window loading:', error);
    }
}

let a = 0, n = 0, m = 0;

async function presentAllProduct() {
    try {
        const response = await fetch("api/Product", {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json();
        const template = document.getElementById('temp-card');
        let max = 0;
        let min = 10000000;

        data.forEach(item => {
            if (item.price > max) max = item.price;
            if (item.price < min) min = item.price;

            if (item.categoryId == 1) a++;
            else if (item.categoryId == 2) n++;
            else m++;

            const clone = document.importNode(template.content, true);
            clone.querySelector('.img-w img').src = item.image;
            clone.querySelector('h1').textContent = item.productName;
            clone.querySelector('.description').textContent = item.description;
            clone.querySelector('.price').textContent = "price: " + item.price + "$";
            clone.querySelector('button').addEventListener('click', function () { AddBasket(item) });
            document.getElementById('PoductList').appendChild(clone);
        });

        document.getElementById('maxPrice').value = max;
        document.getElementById('minPrice').value = min;
    } catch (error) {
        console.error('Error in presentAllProduct:', error);
    }
}

async function presentCategory() {
    try {
        const response = await fetch("api/Category", {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json();
        const template = document.getElementById('temp-category');

        data.forEach(item => {
            const clone = document.importNode(template.content, true);
            clone.querySelector('.opt').id = item.categoryId;
            clone.querySelector('.opt').value = item.categoryName;
            clone.querySelector('label').htmlFor = item.categoryName;
            clone.querySelector('.OptionName').textContent = item.categoryName;
            clone.querySelector('.opt').addEventListener('change', function () { filterProducts() });

            if (item.categoryId == 1) clone.querySelector('.Count').textContent = a;
            else if (item.categoryId == 2) clone.querySelector('.Count').textContent = n;
            else clone.querySelector('.Count').textContent = m;

            document.getElementById('categoryList').appendChild(clone);
        });
    } catch (error) {
        console.error('Error in presentCategory:', error);
    }
}

function AddBasket(product) {
    let basket = [];
    const basketData = sessionStorage.getItem("basket");

    if (basketData) {
        try {
            basket = JSON.parse(basketData);
        } catch (error) {
            console.error('Error parsing basket data:', error);
        }
    }

    let existingProduct = basket.find(item => item.product.productName === product.productName);

    if (existingProduct) {
        existingProduct.count += 1;
    } else {
        basket.push({ product: product, count: 1 });
    }

    sessionStorage.setItem("basket", JSON.stringify(basket));

    try {
        sessionStorage.setItem("countBasket", parseInt(sessionStorage.getItem("countBasket")) + 1);
        document.getElementById('ItemsCountText').textContent = sessionStorage.getItem("countBasket");

        sessionStorage.setItem("sumBasket", parseInt(sessionStorage.getItem("sumBasket")) + product.price);
    } catch (error) {
        console.error('Error updating basket or sum:', error);
    }
}

async function filterProducts() {
    try {
        const desc = document.getElementById("nameSearch")?.value ?? "";
        const minPrice = document.getElementById("minPrice")?.value ?? "";
        const maxPrice = document.getElementById("maxPrice")?.value ?? "";
        const category1 = document.getElementById("1");
        const category2 = document.getElementById("2");
        const category3 = document.getElementById("3");

        let categoryArr = [];
        if (category1.checked) categoryArr.push(1);
        if (category2.checked) categoryArr.push(2);
        if (category3.checked) categoryArr.push(3);

        let catString = '';
        for (let r = 0; r < categoryArr.length; r++) {
            catString += `&categoryId=${categoryArr[r]}`;
        }

        const response = await fetch(`api/product/?desc=${desc}&min=${minPrice}&max=${maxPrice}${catString}`, {
            method: "GET",
        });

        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const products = await response.json();
        const template = document.querySelector("#temp-card");
        const productList = document.getElementById("PoductList");
        productList.innerHTML = "";
        
        products.forEach(product => {
            const clone = template.content.cloneNode(true);
            clone.querySelector('h1').textContent = product.productName;
            clone.querySelector('.description').textContent = product.description;
            clone.querySelector('.price').textContent = "price: " + product.price + "$";
            clone.querySelector('.img-w img').src = product.image;
            clone.querySelector('button').addEventListener('click', function () {
                AddBasket(product);
            });
            document.getElementById('PoductList').appendChild(clone);
        });
    } catch (error) {
        console.error('Error in filterProducts:', error);
    }
}

function TrackLinkID(element) {
    try {
        window.location.replace("UpdateUser.html");
    } catch (error) {
        console.error('Error in TrackLinkID:', error);
    }
}