app.factory('apiservice', ['$resource', function ($resource) {
   return {
        products: $resource(url.products.list, null, {
            'list': {
                method: 'GET',
                isArray:true
            }
        }),
        addToCart: $resource(url.products.addToCart,null,{
            'save': {
                method: 'POST'
            }
        }),
        cart: $resource(url.cart.list, null, {
            'list': {
                method: 'GET',
                isArray:true
            }
        })
    };


}]);