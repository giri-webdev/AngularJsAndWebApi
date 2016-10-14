app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
    .when('/Products', {
        templateUrl: 'Templates/Products.html',
        controller: 'ProductsController'
    })

    .when('/Cart', {
        templateUrl: 'Templates/Cart.html',
        controller:'CartController'
    })

    .when('/Register', {
        templateUrl: 'Templates/Security/Register.html',
        controller: 'RegisterController'
    })

    .when('/Login', {
        templateUrl: 'Templates/Security/Login.html',
        controller:'LoginController'
    })

    .otherwise(
    {
        redirectTo: '/Products'
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

app.factory('httpInterceptor', ['$q', '$location', '$window', function ($q, $location, $window) {
    return {

        request: function (config) {
            $('#pIndicator').show();

            if (config.headers.Authorization === 'Bearer')
                config.headers.Authorization = 'Bearer ' + $window.sessionStorage.getItem('token');

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

            if (error.status == 401) {
                $location.path("/Login");
            }
            else {
                infoDialog(error.statusText, error.data.message);
            }
            return $q.reject(error);
        }
    }


}]);

app.config(['$httpProvider', function ($httpProvider) {

    $httpProvider.interceptors.push('httpInterceptor');
}]);


function infoDialog(title,message) {
    $("#infoDialog").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        open: function () {
            $(this).html(message);
        },
        title:title,
        modal: true,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });
}