/// <reference path="master-sheet-all.controller.js" />
(function () {
    'use strict'
    angular.module("parcel.parser").controller("parcelController", parcelController);

    parcelController.$inject = ["$scope", "httpCallService", "$http", "popupService", "uiGridConstants"];

    function parcelController(scope, httpCallService, $http, popupService, uiGridConstants) {
        var vm = this;
        vm.data = {};
        vm.func = {};
        vm.data.packageTransactions = [];
        vm.data.gridOptions = {
            data: [],
            enablePinning: true,
            columnDefs: [{ field: "Id", width: 80, enablePinning: true, enableFiltering: false },
                        { field: "PackageType", enablePinning: true },
                        { field: "Length", width: 120, type: 'number', enableFiltering: false },
                        { field: "Breadth", width: 120, type: 'number', enableFiltering: false },
                        { field: "Height", width: 120, type: 'number', enableFiltering: false },
                        { field: "Cost", displayName:'Cost (in $)', width: 120, type: 'number', enableFiltering: false },
                        { field: "Quantity", width: 120, type: 'number', enableFiltering: false },
                        { field: "TotalPrice", displayName: 'Total Price (in $)', aggregationType: uiGridConstants.aggregationTypes.sum, width: 300, type: 'number', enableFiltering: false, footerCellTemplate: '<div class="ui-grid-cell-contents">\Grand Total:\ {{col.getAggregationValue() | number:2 }}</div>' },
                        {
                            name: 'Action',
                            cellEditableCondition: false,
                            enableFiltering: false,
                            cellTemplate: '<button class="btn-danger" ng-click="grid.appScope.vm.func.deleteTransaction(row)" title="Click to Remove from List">Delete</button>'
                        }
            ],
            enableSorting: true,
            multiSelect: false,
            enableFiltering: true,
            showColumnFooter: true,
            paginationPageSizes: [25, 50, 75],
            paginationPageSize: 25
        };

        function init() {
            vm.data.Parcel = {
                Length: 0,
                Breadth: 0,
                Height: 0,
                Weight: 0,
                Quantity: 1,
                PackageType: "",
                Cost: 0,
                TotalPrice: 0,
                Message: "OK"
            };

            setTimeout(function () {
                vm.func.getParcelList();
            }, 200);
        }

        vm.func.getParcelList = function () {
            httpCallService.httpCall("parceldimension/getparceldimensions", null, false, false).then(function (response) {
                if (response.data && response.data.length > 0) {
                    vm.data.parcelData = response.data;
                }
                else {
                    vm.data.personData = [];
                }
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
            if (vm.data.Parcel.Quantity <= 0) {
                popupService.successErrorPopUp("Quantity should not be less than or equal to zero.", "ERROR");
                return;
            }
            var parcel = {
                Length: vm.data.Parcel.Length,
                Breadth: vm.data.Parcel.Breadth,
                Height: vm.data.Parcel.Height,
                Weight: vm.data.Parcel.Weight,
                Quantity: vm.data.Parcel.Quantity
            };
            httpCallService.httpCall("parseparcel/getparcelprice", parcel, true, false).then(function (response) {
                if (response.data) {
                    var response = response.data;
                    vm.data.Parcel.Cost = response.Cost;
                    vm.data.Parcel.TotalPrice = response.TotalPrice;
                    vm.data.Parcel.PackageType = response.PackageType;
                    vm.data.Parcel.Message = response.Message;
                }
                if (vm.data.Parcel.Message !== "OK") {
                    popupService.successErrorPopUp(vm.data.Parcel.Message, "ERROR");
                }
                else {
                    var id = 1;
                    if (vm.data.packageTransactions.length > 0) {
                        id = vm.data.packageTransactions[vm.data.packageTransactions.length - 1].Id + 1;
                    }
                    vm.data.packageTransactions.push({
                        Id: id,
                        Length: vm.data.Parcel.Length,
                        Breadth: vm.data.Parcel.Breadth,
                        Height: vm.data.Parcel.Height,
                        Weight: vm.data.Parcel.Weight,
                        Quantity: vm.data.Parcel.Quantity,
                        PackageType: vm.data.Parcel.PackageType,
                        Cost: vm.data.Parcel.Cost,
                        TotalPrice: vm.data.Parcel.TotalPrice
                    });
                    vm.data.gridOptions.data = vm.data.packageTransactions;
                    vm.func.clearParcelData();
                }

            }, function onError(reason) {
                popupService.successErrorPopUp("Error Occured: " + reason.data && reason.data.ExceptionMessage + ". Please try again later or contact your system administrator.", "ERROR");
            });
        }

        vm.func.clearParcelData = function () {
            vm.data.Parcel.Length = 0;
            vm.data.Parcel.Breadth = 0;
            vm.data.Parcel.Height = 0;
            vm.data.Parcel.Weight = 0;
            vm.data.Parcel.Quantity = 1;
            vm.data.Parcel.PackageType = "";
            vm.data.Parcel.Cost = 0;
            vm.data.Parcel.TotalPrice = 0;
            vm.data.Parcel.Message = "OK";
        }

        vm.func.showParcelPriceList = function () {
            popupService.showParcelPriceList(vm.data.parcelData, vm.func.saveSuccessOrFailure);
        }

        vm.func.deleteTransaction = function (row) {
            var confirmMessage = "Are you sure you want to delete the package transaction with Id: " + row.entity.Id;
            popupService.confirmPopUp(confirmMessage, row.entity, vm.func.confirmDeletion);
        }

        vm.func.confirmDeletion = function (rowData) {
            vm.data.packageTransactions.splice(vm.data.packageTransactions.indexOf(rowData), 1);
            popupService.successErrorPopUp("Package Successfully deleted", "SUCCESS");
            vm.data.gridOptions.data = vm.data.packageTransactions;
        }

        init();
    }
})();