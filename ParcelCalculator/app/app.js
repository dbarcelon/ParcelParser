(function () {
    'use strict'
    angular.module("parcel.parser", ["ui.grid"
                                , 'ui.grid.pagination'
                                , "blockUI"
                                , "ui.bootstrap"
    ]).config(["blockUIConfig", "$compileProvider", function (blockUIConfig, compileProvider) {
        blockUIConfig.autoBlock = false;
    }]);
})();