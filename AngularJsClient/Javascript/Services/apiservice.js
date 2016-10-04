app.factory('apiservice', ['$resource', function ($resource) {
   return {
        products: $resource(url.products.list, null, {
            'list': {
                method: 'GET',
                isArray:true
            }
        })
    };


}]);