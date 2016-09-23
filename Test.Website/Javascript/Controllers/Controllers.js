//Get the ajax call data from reslove and promise through 'info' object
app.controller('AboutController', function ($scope, $routeParams, info) {
    $scope.message = "About Page";
    $scope.index = $routeParams.id;
    $scope.info = info;
});

app.controller('ContactUsController', function ($scope, $routeParams,apiservice,$sce) {
    $scope.message = "Contact Us Page";
    $scope.index = $routeParams.id;

    //Get the html content from web api service
    apiservice.htmlContent.get(function (data) {
        $scope.htmlContent = data.html;
    });

    $scope.renderHtml=function(html)
    {
        return $sce.trustAsHtml(html);
    }
});

app.controller('AngularFilterController', function ($scope) {
    $scope.fruit = "Orange";
    $scope.date = new Date();
});

app.controller('AddProductController', function ($scope, $resource,apiservice) {
    $scope.categories = [{ id: 1, text: 'Fruits' },
        { id: 2, text: 'Devices' }];
 
    $scope.product = {
        name: '',
        item: { id: 2, text: 'Devices' }
    };

    $scope.saveProduct = function () {
        
        apiservice.addProduct.save($scope.product, function (data) {
            alert('Product saved successfully.')
        }, function (response) {//Exception Handling
            alert(response.statusText)
            if (response.data.exceptionMessage)
                alert(response.data.exceptionMessage)
        });
    };
});

app.controller('CountryController', function ($scope, apiservice) {
  $scope.template = "Templates/ListCountries.html";
});


app.controller('ListCountriesController',['$scope', 'apiservice', '$resource','$location', function ($scope, apiservice, $resource,$location) {

    apiservice.getCountries.list(function (data) {
        $scope.info = data;
    });

    }]);


app.controller('RegisterController', function ($scope, apiservice, $location) {
    $scope.userModel = {
        email: '',
        password: '',
    confirmPassword: ''
    };

$scope.register = function () {
    apiservice.register.addUser($scope.userModel, function (data) {
        $location.path('/ListCountries');
        });
    };
});


app.controller('LoginController', function ($scope,$window, apiservice,$location) {

    $scope.login = function () {
        $scope.userModel.grant_type = 'password';
        $scope.userModel.userName = $scope.userModel.email;
        apiservice.login.validateUser($scope.userModel, function (data) {
            $window.sessionStorage.setItem('token', data.access_token);
            $scope.info = 'User authenticated successfully..';
            $location.path('/Country');
        });
    };
});