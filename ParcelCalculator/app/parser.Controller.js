/// <reference path="master-sheet-all.controller.js" />
(function () {
    'use strict'
    angular.module("parcel.parser").controller("parcelController", parcelController);

    parcelController.$inject = ["$scope", "httpCallService", "$http", "popupService"];

    function parcelController(scope, httpCallService, $http, popupService) {
        var vm = this;
        vm.data = {};
        vm.func = {};

        vm.data.gridOptions = {
            data: [],
            enablePinning: true,
            columnDefs: [{ field: "Id", width: 80, enablePinning: true, enableFiltering: false },
                        { field: "Package Type", enablePinning: true },
                        { field: "Length", width: 120, type: 'number', enableFiltering: false },
                        { field: "Breadth", width: 120, type: 'number', enableFiltering: false },
                        { field: "Height", width: 120, type: 'number', enableFiltering: false },
                        { field: "Cost", width: 120, type: 'number', enableFiltering: false }
            ],
            enableSorting: true,
            multiSelect: false,
            enableFiltering: true,
            paginationPageSizes: [25, 50, 75],
            paginationPageSize: 25
        };

        function init() {
            vm.data.Parcel = {
                Length: 0,
                Breadth: 0,
                Height: 0,
                Weight: 0,
                PackageType: "",
                Cost: 0,
                Message: "OK"
            };

            setTimeout(function () {
                vm.func.getParcelList();
            }, 200);
        }

        vm.func.getParcelList = function () {
            httpCallService.httpCall("parceldimension/getparceldimensions", null, false,false).then(function (response) {
                if (response.data && response.data.length > 0) {
                    vm.data.parcelData = response.data;
                }
                else {
                    vm.data.personData = [];
                }
                console.log(vm.data.parcelData);
            }, function onError(reason) {
                popupService.successErrorPopUp("Error Occured: " + reason.data && reason.data.ExceptionMessage + ". Please try again later or contact your system administrator.", "ERROR");
            });
        }

        vm.func.getParcel = function () {
            if (vm.data.Parcel.Length <= 0) {
                popupService.successErrorPopUp("Parcel Length should not be less than or equal to zero.", "ERROR");
                return;
            }
            if (vm.data.Parcel.Breadth <= 0) {
                popupService.successErrorPopUp("Parcel Breadth should not be less than or equal to zero.", "ERROR");
                return;
            }
            if (vm.data.Parcel.Height <= 0) {
                popupService.successErrorPopUp("Parcel Height should not be less than or equal to zero.", "ERROR");
                return;
            }
            if (vm.data.Parcel.Weight <= 0) {
                popupService.successErrorPopUp("Parcel Weight should not be less than or equal to zero.", "ERROR");
                return;
            }
            var parcel = {
                Length: vm.data.Parcel.Length,
                Breadth: vm.data.Parcel.Breadth,
                Height: vm.data.Parcel.Height,
                Weight: vm.data.Parcel.Weight
            };
            httpCallService.httpCall("parseparcel/getparcelprice", parcel, true,false).then(function (response) {
                if (response.data) {
                    var response = response.data;
                    vm.data.Parcel.Cost = response.Price;
                    vm.data.Parcel.PackageType = response.PackageType;
                    vm.data.Parcel.Message = response.Message;
                }
                if (vm.data.Parcel.Message !== "OK") {
                    popupService.successErrorPopUp(vm.data.Parcel.Message, "ERROR");
                }
                
            }, function onError(reason) {
                popupService.successErrorPopUp("Error Occured: " + reason.data && reason.data.ExceptionMessage + ". Please try again later or contact your system administrator.", "ERROR");
            });
        }
        init();
    }
})();