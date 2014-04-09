app.controller('loginController', function ($scope) {

    var msgErrorDefault;
    $scope.WaitFor = false;
    $scope.m = { username:"", password: "" };
    $scope.idModal = "#modalLogin";

    var resetLogin = function () {
        $scope.WaitFor = false;
        $scope.$apply();
    };


    var onError = function () {
        $scope.MsgError = msgErrorDefault;
        resetLogin();
    };

    $scope.$on('onLinkLogin', function () {
        $scope.clear();
        $($scope.idModal).modal('show');
    });

    var onSuccess = function (jResp) {
        try {
            if (jResp.HasError) {
                $scope.MsgError = jResp.Message;
                $scope.$apply();
            }
            else {
                //Redirect
                window.location.replace(jResp.UrlToGo);
                return false;
            }
        } catch (e) {
            $scope.MsgError = e;
        }

        resetLogin();
        return false;
    };

    $scope.clear = function () {
        $scope.MsgError = "";
        $scope.m.username = "";
        $scope.m.password = "";
    };

    $scope.login = function (formId, msgError) {

        msgErrorDefault = msgError;
        if ($(formId).valid() == false) {
            return false;
        }

        $scope.WaitFor = true;

        $.post('/Account/Login', $(formId).serialize())
            .success(onSuccess)
            .error(onError);
        return false;
    };
});

