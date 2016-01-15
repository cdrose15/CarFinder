(function () {
    'use strict';

    angular.module('app').controller('controller', Carcontroller);

    controller.$inject = ['$location'];

    user.GetYears = function() {
        return $http.post('api/Cars/GetYears').then(
            function (response) {
                return response.data
            })
    }

    return user;
})();