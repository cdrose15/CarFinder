angular.module('myCarApp').factory('carSvc', ['$http','$q', function ($http, $q) {

    var service = {};

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

    service.getCar = function (id) {
        return $http.post("/api/car/GetCar",id).then(function(response){
            return response.data;
        });
    }

    return service;

}]);
        
