var serviceUrl = "http://localhost:55626/";
//var serviceUrl = "http://angularjsservices.giri-webdev.net/";

var url = {
    products:{
        list: serviceUrl + 'api/Product/ListProducts',
        addToCart: serviceUrl+'api/Product/AddToCart'
    },
    cart: {
        list:serviceUrl+'api/Cart/ListProducts'
    }
};