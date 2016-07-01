var routeTest = angular.module('routeTest', ['ngRoute']);

routeTest.config(['$routeProvider', function ($routeProvider) {

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

}]);

routeTest.controller('AboutController', function ($scope,$routeParams) {
    $scope.message = "About Page";
    $scope.index = $routeParams.id;
});

routeTest.controller('ContactUsController', function ($scope,$routeParams) {
    $scope.message = "Contact Us Page";
    $scope.index = $routeParams.id;
});