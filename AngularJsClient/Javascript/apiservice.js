app.factory('apiservice', ['$resource', function ($resource) {
    var service= {
        products: $resource(url.products.list, null, {
            'list': {
                method: 'GET',
                isArray:true
            }
        }),
        addToCart: $resource(url.products.addToCart,null,{
            'save': {
                method: 'POST',
                headers:{'Authorization':'Bearer'}
            }
        }),
        cart: $resource(url.cart.list, null, {
            'list': {
                method: 'GET',
                headers:{'Authorization':'Bearer'},
                isArray:true
            }
        }),
        login: $resource(url.authorize.login, null, {
            'validateUser': {
                method: 'POST',
                headers: {
                    'Content-Type':'application/x-www-form-urlencoded'
                },
                transformRequest: function (data, headers) {
                    var str = [];
                    for (var c in data)
                        str.push(encodeURIComponent(c) + "=" + encodeURIComponent(data[c]));
                    return str.join("&");
                }

            }
        }),
        register: $resource(url.authorize.register, null, {
            'addUser': {
                method:'POST'
            }
        })
    };

    return service;
}]);