var app = angular.module('Reception', ['ngMockE2E']);

app.run(function ($httpBackend, mocks) {
    $httpBackend.whenGET('/api/getUsersData').respond(mocks.usersData);
});

app.constant('mocks', {
    usersData:  {
        dates: ['06/24/2014', '06/25/2014', '06/26/2014', '06/27/2014', '06/28/2014'],
        worktime: [
            { name: 'starmonkey', time: ['8', '8', '8', '8', '8' ] },
            { name: 'antik', time: ['2', '', '3', '', '1' ] },
            { name: 'lk', time: [ '', '14', '', '23', '' ] }
        ]
    }
});

app.controller('TableController', function ($scope, $http) {
    var parse = function (str) {
        var number = parseInt(str);
        return isNaN(number) ? 0 : number;
    };

    $http.get('/api/getUsersData').then(function (response) {
        var usersData = response.data;
        _.forEach(usersData.worktime, function (user) {
            user.summary = _.reduce(user.time, function (a, b) {
                return parse(a) + parse(b);
            })
        });
        $scope.usersData = usersData;
    }, function (reason) {
        $scope.error = reason;
    });
});