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

app.controller('CartController', ['$scope','apiservice',function ($scope, apiservice) {
    $scope.totalPrice = 0;

    apiservice.cart.list(function (data) {
        $scope.items = data;
    });

    $scope.$on('updatePrice', function (evt, value) {
        $scope.totalPrice = parseFloat($scope.totalPrice) + parseFloat(value);
    });
}]);

app.controller('LoginController', function ($scope, apiservice, $location,$window) {

    $scope.login = function () {
        $scope.userModel.grant_type = 'password';
        $scope.userModel.userName = $scope.userModel.email;

        apiservice.login.validateUser($scope.userModel, function (data) {
            $window.sessionStorage.setItem('token', data.access_token);
            $location.path('/Cart');
        });
    };

});

app.controller('RegisterController', function ($scope, apiservice, $location, $window) {
    $scope.register = function () {
        apiservice.register.addUser($scope.userModel, function (data) {
            $location.path('/Cart');
        });
    };
});