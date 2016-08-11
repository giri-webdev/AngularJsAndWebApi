//Get the ajax call data from reslove and promise through 'info' object
app.controller('AboutController', function ($scope, $routeParams, info) {
    $scope.message = "About Page";
    $scope.index = $routeParams.id;
    $scope.info = info;
});

app.controller('ContactUsController', function ($scope, $routeParams,apiservice) {
    $scope.message = "Contact Us Page";
    $scope.index = $routeParams.id;

    //$.get('http://localhost:55626/api/Values/HtmlContent', function (data) {
    //    alert(data);
    //    $('#htmlContent').html(data);
    //    console.log(data);
    //    $scope.htmlContent = 'hello';
    //});

    //apiservice.htmlContent.get(function (data) {
    //    console.log(data);
    //    $scope.htmlContent = data;
    //});
});


app.controller('AngularFilterController', function ($scope) {
    $scope.fruit = "Orange";
    $scope.date = new Date();
 
});