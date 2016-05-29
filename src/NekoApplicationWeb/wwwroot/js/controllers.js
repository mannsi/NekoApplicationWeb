(function () {
    "use strict";
    // Getting the existing module
    angular.module("app-neko")
        .controller("personalPageController", personalPageController);

    function personalPageController($http, $scope) {
        var vm = this;

        vm.applicants = [];
        vm.pageModified = false;

        function init() {
            $(window).on('beforeunload', function () {
                if (vm.pageModified) {
                    return 'You have unsaved changes. Are you sure you wish to continue ?';
                }
                else {
                    //reset
                    vm.pageModified = false;
                    return;
                }
            });
        }

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
                    vm.pageModified = true;
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

            vm.pageModified = true;
            enumerateApplicants();
        };

        vm.continue = function() {
            $http.post('/api/applicant/list', vm.applicants)
                .then(function (response) {
                    vm.pageModified = false;
                    window.location.href = 'Menntun';
                }, function(error) {
                    alert("Ekki tókst að vista");
                });
        };

        init();
    };
})()