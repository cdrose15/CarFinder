// This is creating the controller
(function () { // immediatly invoked function

    var app = angular.module('myCarApp');
    app.controller('myCarController', ['carSvc', function (carSvc) {

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
        scope.id = [];

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

        scope.getCar = function (id) {
            CarSvc.getCar(id).then(function (data) {
                scope.getCars(id)
                id = data;
                id = scope.id;
            })
        }

        scope.getYears();

    }]);
})();
