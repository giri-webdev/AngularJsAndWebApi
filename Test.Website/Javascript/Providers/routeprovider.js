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

    .when('/Register', {
        templateUrl: 'Templates/Register.html',
        controller:'RegisterController'
    })
    .when('/Country', {
        templateUrl:'Templates/Country.html',
        controller:'CountryController'
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

/*Http Interceptors to show the progress bar 
while making $resource service request*/
app.config(function ($provide, $httpProvider) {

    $provide.factory('httpInterceptor', ['$q', '$location', function ($q, $location) {
        return {

            request: function (config) {
                $('#pIndicator').show();

                if (config.headers.Authorization === 'Bearer')
                    config.headers.Authorization = 'Bearer ' + token;

                return config;
            },
            requestError: function (error) {
                $('#pIndicator').hide();
                return $q.reject(error);
            },

            response: function (response) {
                $('#pIndicator').hide();
                return response;
            },
            responseError: function (error) {
                if (error.status == 401)
                    $location.path('/Login');
                return $q.reject(error);
            }
        }
    }]);

    $httpProvider.interceptors.push('httpInterceptor');
});