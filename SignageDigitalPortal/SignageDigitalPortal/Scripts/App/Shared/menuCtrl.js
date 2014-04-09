app.controller('menuController', function ($scope, $rootScope) {
    $scope.linkLogin = function () {
        $rootScope.$broadcast('onLinkLogin');
    };
});