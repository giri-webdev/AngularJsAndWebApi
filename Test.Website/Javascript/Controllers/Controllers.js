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