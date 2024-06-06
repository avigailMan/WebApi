
window.onload = async function windowLoading() {
    presentAllProductInBasket();

}

function presentAllProductInBasket() {
    const template = document.getElementById('temp-row');
    const tbody = document.querySelector('table.items tbody');
    document.getElementById("itemCount").textContent = sessionStorage.getItem("countBasket");
    document.getElementById("totalAmount").textContent = sessionStorage.getItem("sumBasket");
    data = sessionStorage.getItem("basket");
    data = JSON.parse(data);
    console.log(data)
    data.forEach(item => {
        const clone = document.importNode(template.content, true);
        clone.querySelector('.itemName').textContent = item.product.productName;
        clone.querySelector('.itemNumber').textContent = item.count;
        clone.querySelector('.price').textContent = item.product.price * item.count + "$";
        clone.querySelector(".img-w img").src = item.product.image;

      
        clone.querySelector('.DeleteButton').addEventListener('click', function () {
            deleteFunction(item);
        });
        tbody.appendChild(clone);

    });
}
function deleteFunction(item) {
    
    let basketData = JSON.parse(sessionStorage.getItem("basket"));

     basketData = basketData.filter(a => a.product.productid !== item.product.productid);

    sessionStorage.setItem("basket", JSON.stringify(basketData));
    sessionStorage.setItem("countBasket", parseInt(sessionStorage.getItem("countBasket")) - item.count)
    sessionStorage.setItem("sumBasket", parseInt(sessionStorage.getItem("sumBasket")) - item.product.price * item.count)
    let tbody = document.querySelector('table.items tbody');
    console.log(tbody)
    tbody.innerHTML = "";
    presentAllProductInBasket();
}
//function placeOrder() {
//    // Add your order placement logic here
//    console.log('Placing order...');
//    // You can include code to handle order submission, API requests, or any other order-related tasks
//}
//async function placeOrder() {
//    let items = [];
//    let sum = 0;
//    basket = sessionStorage.getItem("basket");
//    basket.forEach(i => {
//        sum = sum + i.product.price;
//        let ord = new OrderItemDtoPost(i.product, i.count)
//        items.push(ord)
//    })
//    let newOrder = new OrderDtoPost(sum,1,items)
//    const doOrder = await fetch("api/Order",
//        {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify(newOrder),
//        }).then(response => {
//            if (!response.ok) {
//                throw new Error('Network response was not ok');
//            }) .then(response => {

//                sessionStorage.setItem("basket", []);
//                sessionStorage.setItem("count",0);

//            })
//}
async function placeOrder() {
    const cartItems = JSON.parse(sessionStorage.getItem('basket'));
    const userid = parseInt(sessionStorage.getItem('user')).userid;
    if (!cartItems || cartItems.length === 0) {
        alert('No items in the cart');
        return;
    }

    const totalPrice = parseInt(sessionStorage.getItem("sumBasket"))
    

    let orderDetails = 'Order Details:\n\n';
    cartItems.forEach(item => {
        orderDetails += `${item.product.productName} - Quantity: ${item.count} - Price: ${item.product.price}\n`;
    });
    orderDetails += `\nTotal Amount: ${totalPrice}`;
    console.log(cartItems);

    const orderItems = cartItems.map(item => ({
        Quantity: item.count,
        Productid: item.product.productid
    }));

    if (confirm(orderDetails)) {
        // Logic to place the order
        
        await fetch('api/Order', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                orederItems: orderItems,
                price: totalPrice,
                userid: parseInt(JSON.parse(sessionStorage.getItem("user")).userid)
            })
        }).then(response => {
            if (response.ok) {
                alert('Order placed successfully!');
                sessionStorage.removeItem('basket');
                sessionStorage.setItem("countBasket", 0);
                sessionStorage.setItem("sumBasket", 0);

                window.location.href = 'products.html';
            } else {
                debugger
                alert('Failed to place the order. Please try again.');
            }
        });
    } else {
        window.location.href = 'ShoppingBag.html';
    }
}