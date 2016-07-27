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