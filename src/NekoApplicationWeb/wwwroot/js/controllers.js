(function () {
    "use strict";
    // Getting the existing module
    angular.module("app-neko")
        .controller("personalPageController", personalPageController)
        .controller("educationPageController", educationPageController)
        .controller("employmentPageController", employmentPageController)
        .controller("financesPageController", financesPageController)
        .controller("loanPageController", loanPageController);

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
            vm.pageModified = false;
            $http.post('/api/applicant/list', vm.applicants)
                .then(function (response) {
                    window.location.href = 'Menntun';
                }, function(error) {
                    vm.pageModified = true;
                    alert("Ekki tókst að vista. Villa: " + error);
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
            vm.pageModified = false;
            $http.post('/api/degree/list', vm.applicantsDegrees)
                .then(function (response) {
                    window.location.href = 'Starfsferill';
                }, function (error) {
                    vm.pageModified = true;
                    alert("Ekki tókst að vista. Villa: " + error);
                });
        };

        init();
    };

    function employmentPageController($http) {
        var vm = this;

        vm.applicantsEmployment = [];
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
                data[i].From = new Date(data[i].From);
            }

            angular.copy(data, vm.applicantsEmployment);
        };

        vm.continue = function () {
            vm.pageModified = false;
            $http.post('/api/employment/list', vm.applicantsEmployment)
                .then(function (response) {
                    window.location.href = 'Fjarmal';
                }, function (error) {
                    vm.pageModified = true;
                    alert("Ekki tókst að vista. Villa: " + error);
                });
        };

        init();
    };

    function financesPageController($http) {
        var vm = this;

        vm.applicantsFinances = [];
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
            angular.copy(data, vm.applicantsFinances);
        };

        vm.addOtherIncome = function(applicantFinances) {
            var selectedOtherIncomeValue = $("#otherIncomeSelect").val();
            var selectedOtherIncomeString = $("#otherIncomeSelect option:selected").text();

            if (selectedOtherIncomeString !== "") {
                applicantFinances.OtherIcome.push({
                    MonthlyAmount: 0,
                    IncomeType: selectedOtherIncomeValue,
                    IncomeTypeString: selectedOtherIncomeString
                });
            }
            vm.pageModified = true;
        };

        vm.removeOtherIncome = function(incomes, incomeToRemove) {
            for (var i = 0; i < incomes.length; i++) {
                if (incomes[i] === incomeToRemove) {
                    incomes.splice(i, 1);
                    break;
                }
            }
            vm.pageModified = true;
        };

        vm.addAsset = function (applicantFinances) {
            var selectedAssetValue = $("#assetsTypeSelect").val();
            var selectedAssetString = $("#assetsTypeSelect option:selected").text();

            if (selectedAssetString !== "") {
                applicantFinances.Assets.push({
                    AssetType: selectedAssetValue,
                    AssetTypeString: selectedAssetString
                });
            }
            vm.pageModified = true;
        };

        vm.removeAsset = function (assets, assetToRemove) {
            for (var i = 0; i < assets.length; i++) {
                if (assets[i] === assetToRemove) {
                    assets.splice(i, 1);
                    break;
                }
            }
            vm.pageModified = true;
        };

        vm.addDebt = function (applicantFinances) {
            var selectedDebtValue = $("#debtTypeSelect").val();
            var selectedDebtString = $("#debtTypeSelect option:selected").text();

            if (selectedDebtString !== "") {
                applicantFinances.Debts.push({
                    DebtType: selectedDebtValue,
                    DebtTypeString: selectedDebtString
                });
            }
            vm.pageModified = true;
        };

        vm.removeDebt = function (debts, debtToRemove) {
            for (var i = 0; i < debts.length; i++) {
                if (debts[i] === debtToRemove) {
                    debts.splice(i, 1);
                    break;
                }
            }
            vm.pageModified = true;
        };

        vm.continue = function () {
            vm.pageModified = false;
            $http.post('/api/finances/list', vm.applicantsFinances)
                .then(function (response) {
                    window.location.href = 'Lanveiting';
                }, function (error) {
                    vm.pageModified = true;
                    alert("Ekki tókst að vista. Villa: " + error);
                });
        };

        init();
    };

    function loanPageController($http) {
        var vm = this;

        vm.loanViewModel = {};
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
            angular.copy(data, vm.loanViewModel);
        };

        vm.addBankLoan = function () {
            $http.get('/api/loan/new')
                .then(function (response) {
                    vm.loanViewModel.BankLoans.push(response.data);
                    vm.pageModified = true;
                    },
                    function (error) {

                    });
        };

        vm.removeBankLoan = function (bankLoan) {
            for (var i = 0; i < vm.loanViewModel.BankLoans.length; i++) {
                if (bankLoan === vm.loanViewModel.BankLoans[i]) {
                    vm.loanViewModel.BankLoans.splice(i, 1);
                    vm.pageModified = true;
                    break;
                }
            }
        };

        vm.continue = function () {
            vm.pageModified = false;
            $http.post('/api/loan', vm.loanViewModel)
                .then(function (response) {
                    window.location.href = 'Fylgiskjol';
                }, function (error) {
                    vm.pageModified = true;
                    alert("Ekki tókst að vista. Villa: " + error);
                });
        };

        init();
    };

})()