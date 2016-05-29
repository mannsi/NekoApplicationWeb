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

            $http.get("/api/applicant/list")
                .then(
                function(response) {
                    angular.copy(response.data, vm.applicants);
                    enumerateApplicants();
                },
                function(error) {
                    
                });
        };

        function enumerateApplicants() {
            for (var i = 0; i < vm.applicants.length; i++) {
                if (i === 0) {
                    vm.applicants[i].Legend = "Umsækjandi";
                } else {
                    vm.applicants[i].Legend = "Umsækjandi " + (i + 1);
                }
            }
        };

        vm.initData = function(data) {
            angular.copy(data, vm.applicants);
            enumerateApplicants();
        };

        vm.addApplicant = function () {
            $http.get("/api/applicant/new")
                .then(
                function (response) {
                    vm.applicants.push(response.data);
                    enumerateApplicants();
                },
                function (error) {

                });
        };

        vm.removeApplicant = function (applicant) {
            var index = -1;
            for (var i = 0; i < vm.applicants.length; i++) {
                if (vm.applicants[i] === applicant) {
                    index = i;
                    break;
                }
            }

            if (index >= 0) {
                vm.applicants.splice(index, 1);
            }

            enumerateApplicants();
        };

        vm.continue = function() {
            $http.post('/api/applicant/list', vm.applicants)
                .then(function(response) {
                    window.location.href = 'Menntun';
                }, function(error) {
                    alert("Ekki tókst að vista");
                });
        };
    };
})()