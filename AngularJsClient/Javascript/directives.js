app.directive('calculatePrice',['$timeout', function ($timeout) {
    var directive = {
        restrict: 'A',
        controller: ['$scope', '$element', '$attrs',
           function ($scope, $element, $attrs) {
               $timeout(function () {
                   $scope.$emit('updatePrice', $scope.item.unitPrice);
               });
           }
        ]
    };
    return directive;
}]);