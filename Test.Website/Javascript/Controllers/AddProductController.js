
app.controller('AddProductController', function ($scope, $resource) {
    $scope.categories = [{ id: 3, text: 'Fruits' },
        { id: 4, text: 'Devices' }];



    $scope.product = {
        name: 'Apple',
        item: $scope.categories[0]
    };

    var p = $resource('http://localhost:55626/api/Values/AddProduct');

    $scope.saveProduct = function () {
        alert('hello');
        console.log($scope.product);
        return;
        p.save($scope.product, function (data) {

            alert('Product saved successfully.')
        }, function (response) {//Exception Handling
            alert(response.statusText)
            if (response.data.exceptionMessage)
                alert(response.data.exceptionMessage)
        });
    };
});