app.controller('ProductsController', function ($scope,apiservice) {
    apiservice.products.list(function (data) {
        $scope.products = data;
    });
});