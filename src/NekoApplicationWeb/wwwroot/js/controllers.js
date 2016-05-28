(function () {
    "use strict";
    // Getting the existing module
    angular.module("app-neko")
        .controller("personalPageController", personalPageController);

    function personalPageController($http) {
        var vm = this;

        vm.applicants = [];

        function loadApplicants() {
            vm.applicants = [];

            $http.get("/api/applicants")
                .then(
                function(response) {
                    angular.copy(response.data, vm.applicants);
                },
                function(error) {
                    
                });
        };

        vm.initData = function(data) {
            // Todo pass the model data here so we don't have to start calling the api
        };


        loadApplicants();
    };
})()