(function () {
    'use strict'
    angular.module("parcel.parser").factory("httpCallService", httpCallService);

    httpCallService.$inject = ["$q", "$http", "blockUI"];

    function httpCallService($q, $http, blockUI) {
        var httpCall = function (controllerAction, data, isPost, isCached) {
            var defered = $q.defer();
            var response = {};

            //Block the user interface
            blockUI.start();

            if (!isPost) {
                controllerAction = controllerAction + "?";
                for (var key in data) {
                    controllerAction = controllerAction + key + "=" + data[key];
                }
            }

            var urlconfig = {
                method: isPost ? "POST" : "GET",
                url: "http://localhost/ParcelCalculator.API/api/" + controllerAction,
                cached: isCached ? true : false,
                data: typeof (data) != "undefined" ? data : null,
                param: isPost ? data : null,
                responseType: controllerAction.indexOf("downloadfile") === -1 ? null : 'arraybuffer',
                contentType: 'application/json; charset=utf-8',
                //withCredentials: true
            };

            $http(urlconfig).success(function (data, status, headers, config) {
                response = {
                    data: data,
                    status: status,
                    header: headers,
                    config: config
                }
                sessionStorage.setItem("Parcel-Authentication-Header", headers('Parcel-Authentication-Header'));

                // Unblock the user interface
                blockUI.stop();
                defered.resolve(response);
            }).error(function (data, status, headers, config) {
                response = {
                    data: data,
                    status: status,
                    header: headers,
                    config: config
                };

                // Unblock the user interface
                blockUI.stop();
                defered.reject(response);
            });

            return defered.promise;
        }

        return {
            httpCall: httpCall
        }
    }
})();