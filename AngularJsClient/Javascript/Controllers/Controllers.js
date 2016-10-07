app.controller('ProductsController', function ($scope,apiservice) {
    apiservice.products.list(function (data) {
        $scope.products = data;
    });

    $scope.toggleProducts = function ($event) {

        var anchor = $event.currentTarget;
        var div = $(anchor).parent().siblings('#divproducts');
        $(div).toggle();
    };

    $scope.addToCart = function ($event) {

        var btn = $event.currentTarget;

        $scope.cartViewModel = {
            productID: $(btn).data('id'),
            isActive:true
        };

        apiservice.addToCart.save($scope.cartViewModel, function (result) {

        });
    };
});

app.controller('CartController', function ($scope, apiservice) {

    apiservice.cart.list(function (data) {

    });
});