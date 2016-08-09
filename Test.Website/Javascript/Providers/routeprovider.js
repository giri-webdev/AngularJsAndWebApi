app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

    /*Get the data from the ajax call before loading the view using
      resolve property from the routeprovider and promise
    */
    $routeProvider.when('/About/:id', {
        templateUrl: 'Templates/About.html',
        controller: 'AboutController',
        resolve: {
            info: function ($q, $http) {
                var deferred = $q.defer();

                $http({ method: 'GET', url: 'http://localhost:55626/api/Values/GetProducts' })
                    .success(function (data) {
                        deferred.resolve(data)
                    })
                    .error(function (data) {

                        deferred.resolve("Error occurred.");
                    });

                return deferred.promise;
            }
        }
    })
    .when('/ContactUs/:id', {
        templateUrl: 'Templates/ContactUs.html',
        controller: 'ContactUsController'
    })
    .when('/AddProduct', {
        templateUrl: 'Templates/AddProduct.html',
        controller:'AddProductController'
    })

    .when('/AngularFilters', {
        templateUrl: 'Templates/AngularFilters.html',
        controller:'AngularFilterController'
    })

    .when('/Login', {
        templateUrl:'Templates/Login.html',
        controller:'LoginController'
    })

    .otherwise(
    {
        redirectTo: '/About'
    });

    $locationProvider.html5Mode(true);


}]);

//Show the progress bar while loading the view through routing
app.run(['$rootScope', function ($root) {

    $root.$on('$routeChangeStart', function () {
        $('#pIndicator').show();
    });
    $root.$on('$routeChangeSuccess', function () {
        $('#pIndicator').hide();
    });
}]);