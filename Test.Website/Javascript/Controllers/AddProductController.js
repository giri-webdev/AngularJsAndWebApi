
app.controller('AddProductController', function ($scope, $resource) {
    $scope.categories = [{ id: 1, text: 'Fruits' },
        { id: 2, text: 'Devices' }];

    $scope.product = {
        name: 'Apple',
        item: 2
    };

    var p = $resource('http://localhost:55626/api/Values/AddProduct');

    $scope.saveProduct = function () {
        p.save($scope.product, function (data) {
            alert('Product saved successfully.')
        }, function (response) {//Exception Handling
            alert(response.statusText)
            if (response.data.exceptionMessage)
                alert(response.data.exceptionMessage)
        });
    };
});