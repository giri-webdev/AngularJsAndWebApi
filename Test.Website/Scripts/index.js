var token = null;
var isValidUser = false;
//Routing
var routeTest = angular.module('routeTest', ['ngRoute', 'ngResource']);

routeTest.config(['$routeProvider', '$locationProvider', function ($routeProvider,$locationProvider) {

    $routeProvider.when('/About/:id', {
        templateUrl: 'Templates/About.html',
        controller: 'AboutController'
    })
    .when('/ContactUs/:id', {
        templateUrl: 'Templates/ContactUs.html',
        controller: 'ContactUsController'
    })
    .otherwise(
    {
        redirectTo:'/About'
    });

    $locationProvider.html5Mode(true);


}]);

routeTest.controller('AboutController', function ($scope,$routeParams) {
    $scope.message = "About Page";
    $scope.index = $routeParams.id;
});

routeTest.controller('ContactUsController', function ($scope,$routeParams) {
    $scope.message = "Contact Us Page";
    $scope.index = $routeParams.id;
});


routeTest.controller('HomeController', function ($scope,$resource) {
   
    var product = this;
        var ds = $resource('http://localhost:55626/api/Values/GetProducts');
        ds.query({name:'Orange'},function (data) {
            product.list = data;
        });

      
});


//Add Product
var module = angular.module('product', ['ngResource']);

module.controller('ProductController', function ($scope, $resource) {
 
    var p = $resource('http://localhost:55626/api/Values/AddProduct');
 
    $scope.saveProduct = function () {
        p.save($scope.product, function (data) {
            alert('Product saved successfully.')
        },function(response)
        {
            alert(response.statusText)
            if(response.data.exceptionMessage)
            alert(response.data.exceptionMessage)
        });
    };
});


//Login/Register

var module = angular.module('authenticateUser', ['ngResource']);

module.factory('service', ['$resource', function ($resource) {
    return {
        register : $resource('http://localhost:55626/api/Account/Register', null, {
            'addUser': {
            method: 'POST'
}
        }),
            login: $resource('http://localhost:55626/Token', null, {
                'validateUser': {
                        method: 'POST',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded'
                },
                    transformRequest: function (data, headers) {
                    var str =[];
                    for (var c in data)
                        str.push(encodeURIComponent(c) + "=" +encodeURIComponent(data[c]));
                    return str.join("&");
                    }
                    }
        }),
            listProducts : $resource('http://localhost:55626/api/Values/GetProducts', null, {
            'list': {
                    method: 'Get',
                headers: { 'Authorization': 'Bearer ' +token }
                }
        })

        };
        }]);

module.controller('LoginController', function ($scope, $resource,service) {

    $scope.userModel = {
        email:'',
        password: '',
        confirmPassword: '',
        userName:''
    };

    $scope.products = null;
   
    $scope.register = function () {
        alert($scope.userModel.email);
        $scope.userModel.confirmPassword = $scope.userModel.password;
        service.register.addUser($scope.userModel, function (data) {
            alert('User registered successfully.');
        });
    };

    $scope.login = function () {
     
        $scope.userModel.grant_type = 'password';
        $scope.userModel.userName = $scope.userModel.email;
        service.login.validateUser($scope.userModel, function (data) {
            isValidUser = true;
            token = data.access_token;
            alert(token);
            $scope.list();
        });
    };

    $scope.list=function()
    {
        var ds = $resource('http://localhost:55626/api/Values/GetProducts', null, {
            'list': {
                method: 'Get',
                headers: { 'Authorization': 'Bearer ' + token }
            }
        });

        ds.list(function (data) {
            $scope.products = data;
            alert(data.length);
        });
    }
});







