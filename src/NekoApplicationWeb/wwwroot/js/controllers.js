(function () {
    "use strict";
    // Getting the existing module
    angular.module("app-neko")
        .controller("personalPageController", personalPageController)
        .controller("educationPageController", educationPageController)
        .controller("employmentPageController", employmentPageController)
        .controller("financesPageController", financesPageController)
        .controller("loanPageController", loanPageController)
        .controller("startPageController", startPageController);

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

        vm.addOtherIncome = function (applicantFinances) {
            var selectIdString = "#otherIncomeSelect-" + applicantFinances.ApplicantSsn;
            var selectedOtherIncomeValue = $(selectIdString).val();
            var selectedOtherIncomeString = $(selectIdString + " option:selected").text();

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
            var selectIdString = "#assetsTypeSelect-" + applicantFinances.ApplicantSsn;
            var selectedAssetValue = $(selectIdString).val();
            var selectedAssetString = $(selectIdString + " option:selected").text();

            var isProperty = selectedAssetValue === "0";
            var assetPlaceHolderString = "";
            if (isProperty) {
                assetPlaceHolderString = "Fastanúmer eignar";
            } else {
                assetPlaceHolderString = "Ökutækjanúmer";
            }

            if (selectedAssetString !== "") {
                applicantFinances.Assets.push({
                    AssetType: selectedAssetValue,
                    AssetTypeString: selectedAssetString,
                    AssetPlaceholderText: assetPlaceHolderString
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
            var selectIdString = "#debtTypeSelect-" + applicantFinances.ApplicantSsn;
            var selectedDebtValue = $(selectIdString).val();
            var selectedDebtString = $(selectIdString + " option:selected").text();

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

    function loanPageController($http, $scope) {
        var vm = this;

        vm.lenders = [];
        vm.loanViewModel = {};
        vm.pageModified = false;
        vm.showBankLoansSection = false;

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

            $http.get('/api/loan/lenders')
                .then(function (response) {
                    vm.lenders = response.data;
                },
                    function (error) {

                    });
        }

        function needsNekoLoan() {
            // If borrower has more than 15% own capital he/she does not need Neko loan
            if (vm.loanViewModel.BuyingPrice * 0.15 < vm.loanViewModel.OwnCapital) {
                return false;
            }
            return true;
        };

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
                    window.location.href = 'Samantekt';
                }, function (error) {
                    vm.pageModified = true;
                    alert("Ekki tókst að vista. Villa: " + error);
                });
        };

        vm.lenderChange = function () {
            vm.showBankLoansSection = false;

            if (!needsNekoLoan()) {
                $("#toMuchOwnCapitalId").removeClass("hidden");
                return;
            } else {
                if (!$("#toMuchOwnCapitalId").hasClass("hidden")) {
                    $("#toMuchOwnCapitalId").addClass("hidden");
                }
            }

            var lenderValue = $("#lenderSelect").val();
            vm.loanViewModel.lenderId = lenderValue;
            var lenderString = $("#lenderSelect" + " option:selected").text();
            vm.loanViewModel.lenderName = lenderString;

            $http({
                url: '/api/loan/defaultLoans',
                method: "GET",
                params: {
                    lenderId: lenderValue,
                    buyingPrice: vm.loanViewModel.BuyingPrice,
                    ownCapital: vm.loanViewModel.OwnCapital
                }
            }).then(function(response) {
                vm.loanViewModel.BankLoans = response.data;
                if (response.data !== "") {
                    vm.showBankLoansSection = true;
                }
            }, function(error) {
                
            });
        };

        vm.loansTotalPrincipal = function(isNekoLoan) {
            var totalPrincipal = 0;

            for (var i = 0; i < vm.loanViewModel.BankLoans.length; i++) {
                var bankLoan = vm.loanViewModel.BankLoans[i];

                if (bankLoan.isNekoLoan === isNekoLoan) {
                    totalPrincipal += bankLoan.principal;
                }
            }

            return totalPrincipal;
        };

        vm.loansTotalMonthlyPayments = function (isNekoLoan) {
            var totalMonthlyPayment = 0;

            for (var i = 0; i < vm.loanViewModel.BankLoans.length; i++) {
                var bankLoan = vm.loanViewModel.BankLoans[i];
                if (bankLoan.isNekoLoan === isNekoLoan) {
                    totalMonthlyPayment += bankLoan.monthlyPayment;
                }
            }

            return totalMonthlyPayment;
        };

        init();
    };

    function startPageController($http) {
        var vm = this;
        vm.ssn = "";
    };

})()