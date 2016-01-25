// This is creating the controller
(function () { // immediatly invoked function

    var app = angular.module('myCarApp');
    app.controller('myCarController', ['carSvc','$uibModal', function (carSvc, $uibModal) {

        var scope = this;
        // using this reference variable to hold values
        scope.years = [];
        scope.makes = [];
        scope.models = [];
        scope.trims = [];
        // string of selected variables chosen by user
        scope.selected = {
            year: '',
            make: '',
            model: '',
            trim: ''
        }
        scope.cars = [];

        // functions for all services

        //get all years
        scope.getYears = function () {
            carSvc.getYears().then(function (data) {
                scope.years = data;
                scope.makes = []; //clear makes
                scope.models = []; //clear models
                scope.trims = []; //clear trims
                scope.selected.year = '';
                scope.selected.make = '';
                scope.selected.model = '';
                scope.selected.trim = '';

            })
        }
        // get all makes by year
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
        //get all models by year and make
        scope.getModels = function () {
            carSvc.getModels(scope.selected).then(function (data) {
                scope.models = data;
                scope.trims = [];
                scope.selected.model = '';
                scope.selected.trim = '';
                scope.getCars();
            })
        }
        //get all trims by year, make, and model
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

        // only function called in controller other than the iffy. All other functions are called when needed with ng-change in html
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
