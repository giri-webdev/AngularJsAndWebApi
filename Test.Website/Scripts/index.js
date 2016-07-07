var routeTest = angular.module('routeTest', ['ngRoute','ngResource']);

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








