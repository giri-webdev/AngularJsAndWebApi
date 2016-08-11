app.factory('apiservice', ['$resource', function ($resource) {
    return {
        register: $resource('http://localhost:55626/api/Account/Register', null, {
            'addUser': {
                method: 'POST'
            }
        }),
        login: $resource('http://localhost:55626/Token', null, {
            'validateUser': {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                transformRequest: function (data, headers) {
                    var str = [];
                    for (var c in data)
                        str.push(encodeURIComponent(c) + "=" + encodeURIComponent(data[c]));
                    return str.join("&");
                }
            }
        }),
        listProducts: $resource('http://localhost:55626/api/Values/GetProducts', null, {
            'list': {
                method: 'Get',
                headers: { 'Authorization': 'Bearer ' + token }
            }
        }),
        htmlContent: $resource('http://localhost:55626/api/Values/HtmlContent', null, {
            'get': {
                method: 'Get'
            }
        })
    };
}]);