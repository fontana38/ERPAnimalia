VoucherApp.Controller('voucherController.js', 
    function voucherCotroller($scope, voucherService) {
        $scope.Voucher = voucherService.Voucher;
    });
    