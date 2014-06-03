var promiseTestApp = angular.module('ngApp', ['ng']);

promiseTestApp.controller('sampleCtrl', ['$scope', '$http', function ($scope, $http) {
    $scope.notes = [];

    $http.get("http://localhost:47503/api/Notes").success(function (notes) {
        $scope.notes = notes;
    });
}]);

