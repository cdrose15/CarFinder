// This is creating the controller
(function () { // immediatly invoked function

    var app = angular.module('myCarApp');
    app.controller('myCarController', ['carSvc','$uibModal', function (carSvc, $uibModal) {

        var scope = this;
        scope.years = [];
        scope.makes = [];
        scope.models = [];
        scope.trims = [];
        scope.selected = {
            year: '',
            make: '',
            model: '',
            trim: ''
        }
        scope.cars = [];

        scope.getYears = function () {
            carSvc.getYears().then(function (data) {
                scope.years = data;
                scope.makes = [];
                scope.models = [];
                scope.trims = [];
                scope.selected.year = '';
                scope.selected.make = '';
                scope.selected.model = '';
                scope.selected.trim = '';

            })
        }

        scope.getMakes = function () {
            carSvc.getMakes(scope.selected).then(function (data) {
                scope.makes = data;
                scope.models = [];
                scope.trims = [];
                scope.selected.make = '';
                scope.selected.model = '';
                scope.selected.trim = '';
                scope.getCars();

            })
        }

        scope.getModels = function () {
            carSvc.getModels(scope.selected).then(function (data) {
                scope.models = data;
                scope.trims = [];
                scope.selected.model = '';
                scope.selected.trim = '';
                scope.getCars();
            })
        }

        scope.getTrims = function () {
            carSvc.getTrims(scope.selected).then(function (data) {
                scope.trims = data;
                scope.selected.trim = '';
                scope.getCars();
            })
        }

        scope.getCars = function () {
            carSvc.getCars(scope.selected).then(function (data) {
                scope.cars = data;
            })
        }

        scope.open = function (id) {
            console.log("Id in open " + id)
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: 'carModal.html',
                controller: 'carModalCtrl as cm',
                size: 'lg',
                resolve: {
                    car: function() {
                        return carSvc.getDetails(id)
                    }
                 }
            })
        };

        scope.getYears();

    }]);

    app.controller('carModalCtrl', function ($uibModalInstance, car) { // add car later to params

        var scope = this;
        scope.n = 0;
        scope.car = car;

        scope.ok = function () {
            $uibModalInstance.close();
        };

        scope.cancel = function () {
            $uibModalInstance.dismiss();
        };

    })

})();
