app.config(function ($provide, $httpProvider) {

    $provide.factory('httpInterceptor', ['$q','$location',function ($q,$location) {
        return {

            request: function(config)
            {
                $('#pIndicator').show();
                //alert('Loading...');
                return config;
            },
            requestError:function(error)
            {
                //alert(error.status)
                $('#pIndicator').hide();
                return $q.reject(error);
            },

            response: function(response)
            {
                $('#pIndicator').hide();
                //alert('hiding...');
                return response;
            },
            responseError: function(error)
            {
                if (error.status == 401)
                    $location.path('/Login');

                return $q.reject(error);
            }
        }
    }]);

    $httpProvider.interceptors.push('httpInterceptor');
});