app.controller('LoginController', function ($scope, $resource, service) {

    $scope.userModel = {
        email: '',
        password: '',
        confirmPassword: '',
        userName: ''
    };

    $scope.register = function () {
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
            $scope.list();
        });
    };

    $scope.list = function () {
        var ds = $resource('http://localhost:55626/api/Values/GetProducts', null, {
            'list': {
                method: 'Get',
                headers: { 'Authorization': 'Bearer ' + token },
                isArray: true
            }
        });

        ds.list(function (data) {
            $scope.info = "User authenticated successfully";
        });
    }
});