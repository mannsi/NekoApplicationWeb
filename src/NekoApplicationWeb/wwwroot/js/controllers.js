(function () {
    "use strict";
    // Getting the existing module
    angular.module("app-neko")
        .controller("personalPageController", personalPageController)
        .controller("educationPageController", educationPageController)
        .controller("employmentPageController", employmentPageController)
        .controller("financesPageController", financesPageController)
        .controller("loanPageController", loanPageController)
        .controller("confirmEulaController", confirmEulaController)
        .controller("startPageController", startPageController);
    

    function personalPageController($http) {
        var vm = this;

        vm.personalViewModel = [];
        vm.pageModified = false;
        vm.newSsn = "";

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
        };

        vm.initData = function(data) {
            angular.copy(data, vm.personalViewModel);
            if (data.ShowEula) {
                $("#termsModal").modal({ backdrop: 'static', keyboard: false, show: true });
            }
        };

        vm.removeApplicant = function (applicant) {
            $http.post("/api/applicant/delete", applicant.Id)
                .then(
                function (response) {
                    var index = -1;
                    for (var i = 0; i < vm.personalViewModel.Applicants.length; i++) {
                        if (vm.personalViewModel.Applicants[i] === applicant) {
                            index = i;
                            break;
                        }
                    }

                    if (index >= 0) {
                        vm.personalViewModel.Applicants.splice(index, 1);
                    }

                    vm.pageModified = true;
                },
                function (error) {

                });

            vm.pageModified = true;
        };

        vm.confirmApplicant = function(applicant) {
            window.location.href = "/";
        };

        vm.continue = function() {
            vm.pageModified = false;
            $http.post('/api/applicant/list', vm.personalViewModel.Applicants)
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

        vm.FinancesViewModel = [];
        vm.pageModified = false;

        function init() {
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();
            });

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
            vm.FinancesViewModel = data;
        };

        vm.addOtherIncome = function (incomesViewModel) {
            var selectIdString = "#otherIncomeSelect-" + incomesViewModel.Applicant.Id;
            var selectedOtherIncomeValue = $(selectIdString).val();
            if (selectedOtherIncomeValue === -1) {
                return;
            }
            
            var selectedOtherIncomeString = $(selectIdString + " option:selected").text();

            if (selectedOtherIncomeString !== "") {
                incomesViewModel.OtherIncomes.push({
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

        vm.addAsset = function (assetsViewModel) {
            var selectedAssetValue = $("#assetSelectId").val();
            var selectedAssetString = $("#assetSelectId option:selected").text();

            var isProperty = selectedAssetValue === "0";
            var assetPlaceHolderString = "";
            if (isProperty) {
                assetPlaceHolderString = "Fastanúmer eignar";
            } else {
                assetPlaceHolderString = "Ökutækjanúmer";
            }

            if (selectedAssetString !== "") {
                assetsViewModel.push({
                    AssetType: selectedAssetValue,
                    AssetTypeString: selectedAssetString,
                    AssetPlaceholderText: assetPlaceHolderString,
                    AssetWillBeSold: true
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

        vm.addDebt = function (debtsViewModel) {
            var selectedDebtValue = $("#debtSelectId").val();
            var selectedDebtString = $("#debtSelectId option:selected").text();

            if (selectedDebtString !== "") {
                debtsViewModel.push({
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

            var param = JSON.stringify(vm.FinancesViewModel);
            $http.post('/api/finances/list', param)
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
        vm.bankLoans = [];
        vm.pageModified = false;
        vm.showBankLoansSection = false;
        vm.showRatioNumbers = false;

        vm.greidslubyrdarhlutfall = 0;
        vm.skuldahlutfall = 0;
        vm.vedsetningarhlutfall = 0;

        vm.greidslubyrdarhlutfall_Ok = false;
        vm.skuldahlutfall_Ok = false;

        vm.refreshButtonDisabled = false;

        function init() {
            $(document).ready(function () {
                $("[data-toggle=tooltip]").tooltip();
                
            });

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

        function populateLoans() {
            vm.showBankLoansSection = false;

            if (!needsNekoLoan()) {
                $("#toMuchOwnCapitalId").removeClass("hidden");
                return;
            } else {
                if (!$("#toMuchOwnCapitalId").hasClass("hidden")) {
                    $("#toMuchOwnCapitalId").addClass("hidden");
                }
            }

            $http({
                url: '/api/loan/defaultLoans',
                method: "GET",
                params: {
                    lenderId: vm.loanViewModel.LenderId,
                    buyingPrice: vm.loanViewModel.BuyingPrice,
                    ownCapital: vm.loanViewModel.OwnCapital
                }
            }).then(function (response) {
                if (!response.data.NeedsNekoLoan) {
                    $("#toMuchOwnCapitalId").removeClass("hidden");
                    return;
                } else {
                    if (!$("#toMuchOwnCapitalId").hasClass("hidden")) {
                        $("#toMuchOwnCapitalId").addClass("hidden");
                    }

                    vm.bankLoans = response.data.DefaultLoans;

                    vm.lenderNameThagufall = response.data.LenderNameThagufall;

                    vm.greidslugeta = response.data.Greidslugeta;
                    vm.greidslubyrdarhlutfall = response.data.Greidslubyrdarhlutfall;
                    vm.vedsetningarhlutfall = response.data.Vedsetningarhlutfall;

                    vm.isGreidslugetaOk = response.data.IsGreidslugetaOk;
                    vm.isGreidslubyrdarhlutfallOk = response.data.IsGreidslubyrdarhlutfallOk;

                    vm.lenderLendingRulesBroken = response.data.LenderLendingRulesBroken;
                    vm.lenderLendingRulesBrokenText = response.data.LenderLendingRulesBrokenText;

                    if (response.data !== "") {
                        vm.showRatioNumbers = true;
                        if (!vm.lenderLendingRulesBroken) {
                            vm.showBankLoansSection = true;
                        }
                    }

                    

                    if (vm.lenderLendingRulesBroken) {
                        $("#lenderRuleBrokenId").text(vm.lenderLendingRulesBrokenText);
                        $("#lenderRuleBrokenId").removeClass("hidden");
                    } else {
                        if (!$("#lenderRuleBrokenId").hasClass("hidden")) {
                            $("#lenderRuleBrokenId").addClass("hidden");
                        }
                    }
                }
                
            }, function (error) {

            });
        };

        vm.initData = function (data) {
            vm.loanViewModel = data;
            if (vm.loanViewModel.LenderId) {
                populateLoans();
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
            populateLoans();
        };

        vm.loansTotalPrincipal = function(isNekoLoan) {
            var totalPrincipal = 0;

            for (var i = 0; i < vm.bankLoans.length; i++) {
                var bankLoan = vm.bankLoans[i];

                if (bankLoan.IsNekoLoan === isNekoLoan) {
                    totalPrincipal += bankLoan.Principal;
                }
            }

            return totalPrincipal;
        };

        vm.loansTotalMonthlyPayments = function (isNekoLoan) {
            var totalMonthlyPayment = 0;

            for (var i = 0; i < vm.bankLoans.length; i++) {
                var bankLoan = vm.bankLoans[i];
                if (bankLoan.IsNekoLoan === isNekoLoan) {
                    totalMonthlyPayment += bankLoan.MonthlyPayment;
                }
            }

            return totalMonthlyPayment;
        };

        init();
    };

    function startPageController($http) {
        var vm = this;

        vm.initData = function (model) {
            vm.startPageViewModel = model;
            if (model.ShowEula) {
                $("#termsModal").modal({ backdrop: 'static', keyboard: false , show: true});
            }
        };
    };

    function confirmEulaController($http) {
        var vm = this;

        vm.initData = function (model) {
            vm.EulaUser = model;
        };

        vm.agreeButtonClicked = function () {
            $http.post('/api/application/readEula', vm.EulaUser)
                .then(function (response) {
                    $("#termsModal").modal('hide');
                }, function (error) {

                });
        };

        vm.notAgreeButtonClicked = function () {
            window.location.href = 'http://www.neko.is';
        };
    };

})()