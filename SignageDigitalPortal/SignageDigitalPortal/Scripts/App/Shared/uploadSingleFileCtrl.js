app.controller("fileUploadCtrl", function ($scope) {
    $scope.scpDlgToUse = "";
    $scope.urlToUpload = "";

    $scope.setFiles = function (element, scpDlgToUse) {
        $scope.scpDlgToUse = scpDlgToUse;

        $scope.$apply(function () {
            console.log('files:', element.fidebbles);
            $scope.files = [];
            for (var i = 0; i < element.files.length; i++) {
                $scope.files.push(element.files[i]);
            }
            $scope.progressVisible = false;
            $scope.uploadFile();
        });
    };

    $scope.uploadFile = function() {
        var fd = new FormData();
        var bHasFiles = false;
        for (var i in $scope.files) {
            bHasFiles = true;
            fd.append("uploadedFile", $scope.files[i]);
        }

        if (bHasFiles == false)
            return;

        var xhr = new XMLHttpRequest();
        //xhr.upload.addEventListener("progress", uploadProgress, false)
        xhr.addEventListener("load", uploadComplete, false);
        xhr.addEventListener("error", uploadFailed, false);
        xhr.addEventListener("abort", uploadFailed, false);
        xhr.open("POST", $scope.urlToUpload);
        xhr.send(fd);
    };

    function uploadComplete(evt) {
        //alert(evt.target.responseText);
        try {
            var resp = jQuery.parseJSON(evt.target.responseText);

            if ($scope.scpDlgToUse === "")
                return;

            var scp = angular.element($($scope.scpDlgToUse)).scope();
            scp.$apply(function () {
                if (resp.HasError === true) {
                    scp.MsgInnerError = resp.MsgError;
                    return;
                }

                scp.MsgInnerError = "";
                scp.Preview = resp.Preview;
                scp.Media = resp.Media;
                scp.FileName = resp.FileName;
            });
        } catch(e) {
            var scpE = angular.element($($scope.scpDlgToUse)).scope();
            scpE.$apply(function () {
                scpE.MsgInnerError = "Se sucitó un problema al subir la media. Por favor revise que la media sea correcta y que su tamaño no sobrepase a 1GB.";
            });
        } 
    }


    function uploadFailed() {
        if ($scope.scpDlgToUse === "")
            return;

        var scp = angular.element($($scope.scpDlgToUse)).scope();
        scp.$apply(function () {
            scp.MsgInnerError = "Se sucitó un problema de red. Por favor intente más tarde.";
        });
    }

});