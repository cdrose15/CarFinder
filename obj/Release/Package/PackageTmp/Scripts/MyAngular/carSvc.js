// service used to organize and share data throughout app

angular.module('myCarApp').factory('carSvc', ['$http', '$q', function ($http, $q) {

    var service = {};

    // returns data from /api/car/getYears
    service.getYears = function () {
        return $http.post("/api/car/GetYears").then(function (response) {
            return response.data;
        });
    }

    service.getMakes = function (selected) {
        return $http.post("/api/car/GetMakeByYear", selected).then(function(response){
            return response.data;
        });

    }

    service.getModels = function (selected){
        return $http.post("/api/car/GetModelByYearMake",selected).then(function(response){
            return response.data;
        });
    }

    service.getTrims = function (selected) {
        return $http.post("/api/car/GetTrimByYearMakeModel",selected).then(function (response) {
            return response.data;
        });
    }

    service.getCars = function (selected) {
        return $http.post("/api/car/GetCarsByYearMakeModelTrim",selected).then(function (response) {
            return response.data;
        });
    }

    service.getDetails = function (id) {
        return $http.post("/api/car/getCar", { id: id }).then(function (response) {
            return response.data;
        });
    }

    return service;

}]);
        
