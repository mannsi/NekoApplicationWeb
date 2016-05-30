(function () {
    "use strict";
    // Getting the existing module
    angular.module("app-neko")
        .controller("personalPageController", personalPageController)
        .controller("educationPageController", educationPageController);

    function personalPageController($http) {
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

    function educationPageController($http) {
        var vm = this;

        vm.applicantsDegrees = [];
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
        
        vm.initData = function (data) {
            for (var i = 0; i < data.length; i++) {
                var applicantDegrees = data[i];
                for (var j = 0; j < applicantDegrees.Degrees.length; j++) {
                    var degree = applicantDegrees.Degrees[j];
                    degree.DateFinished = new Date(degree.DateFinished);
                }
            }

            angular.copy(data, vm.applicantsDegrees);
        };

        vm.addDegree = function(applicantDegrees) {
            $http.get('/api/degree/new')
                .then(function (response) {
                        response.data.DateFinished = new Date(response.data.DateFinished);
                        applicantDegrees.Degrees.push(response.data);
                        vm.pageModified = true;
                    },
                    function(error) {

                    });
        };

        vm.removeDegree = function(applicantDegrees, degree) {
            var index = -1;
            for (var i = 0; i < applicantDegrees.Degrees.length; i++) {
                if (degree === applicantDegrees.Degrees[i]) {
                    index = i;
                }
            }
            if (index > 0) {
                applicantDegrees.Degrees.splice(index, 1);
                vm.pageModified = true;
            }
        };

        vm.continue = function () {
            $http.post('/api/degree/list', vm.applicantsDegrees)
                .then(function (response) {
                    vm.pageModified = false;
                    window.location.href = 'Starfsferill';
                }, function (error) {
                    alert("Ekki tókst að vista");
                });
        };

        init();
    };
})()