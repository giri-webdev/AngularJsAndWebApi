var token = null;
var isValidUser = false;
//Get the ajax call data from reslove and promise through 'info' object
app.controller('AboutController', function ($scope, $routeParams, info) {
    $scope.message = "About Page";
    $scope.index = $routeParams.id;
    $scope.info = info;
});

app.controller('ContactUsController', function ($scope, $routeParams) {
    $scope.message = "Contact Us Page";
    $scope.index = $routeParams.id;
});


app.controller('AngularFilterController', function ($scope) {
    $scope.fruit = "Orange";
    $scope.date = new Date();
});

app.controller('AddProductController', function ($scope, $resource) {
    $scope.categories = [{ id: 1, text: 'Fruits' },
        { id: 2, text: 'Devices' }];

    $scope.product = {
        name: '',
        item: { id: 2, text: 'Devices' }
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

app.controller('CountryController', function ($scope, apiservice) {
    $scope.template = "Templates/Login.html";
    if (isValidUser) {
        $scope.template = "Templates/ListCountries.html";
    }
});


app.controller('ListCountriesController', function ($scope, apiservice,$resource) {
    var ds = $resource('http://localhost:55626/api/Values/GetCountries', null, {
        'list': {
            method: 'Get',
            headers: { 'Authorization': 'Bearer ' + token },
            isArray: true
        }
    });
       
    ds.list(function (data) {
        $scope.info = data;
    });
});


app.controller('RegisterController', function ($scope, apiservice,$location) {
    $scope.userModel = {
        email: '',
        password: '',
        confirmPassword:''
    };

    $scope.register = function () {
        apiservice.register.addUser($scope.userModel, function (data) {
            $location.path('/ListCountries');
        });
    };
});


app.controller('LoginController', function ($scope, apiservice,$location) {

    $scope.login = function () {
        $scope.userModel.grant_type = 'password';
        $scope.userModel.userName = $scope.userModel.email;
        apiservice.login.validateUser($scope.userModel, function (data) {
            isValidUser = true;
            token = data.access_token;
            $scope.info = 'User authenticated successfully..';
            $location.path('/Country');
        });
    };
});
