(function () {
    'use strict'

    angular.module("parcel.parser").controller("parcelPriceListPopupController", parcelPriceListPopupController);

    parcelPriceListPopupController.$inject = ["$uibModalInstance", "popupInfo", "$scope"];

    function parcelPriceListPopupController(modalInstance, popupInfo, scope) {
        var vm = this;
        vm.data = {};
        vm.func = {};
        vm.data.popupInfo = popupInfo;

        vm.data.gridParcelListOptions = {
            data: [],
            enablePinning: true,
            columnDefs: [{ field: "Id", width: 80, enablePinning: true, enableFiltering: false },
                        { field: "PackageType", enablePinning: true },
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
            vm.data.gridParcelListOptions.data = vm.data.popupInfo.parcelPriceListData;
        }

        vm.func.cancel = function () {
            modalInstance.close("close");
        }

        init();
    }
})();