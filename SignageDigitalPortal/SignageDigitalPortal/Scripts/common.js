window.relMouseCoords = function (e) {
    var el = e.target, c = el;
    var scaleX = c.width / c.offsetWidth || 1;
    var scaleY = c.height / c.offsetHeight || 1;

    if (!isNaN(e.offsetX))
        return { x: e.offsetX * scaleX, y: e.offsetY * scaleY };

    var x = e.pageX, y = e.pageY;
    do {
        x -= el.offsetLeft;
        y -= el.offsetTop;
        el = el.offsetParent;
    } while (el);
    return { x: x * scaleX, y: y * scaleY };
};


window.showUpsert = function (id, divScope, urlToGo, jqGridToUse) {
    var scope = angular.element($(divScope)).scope();
    scope.show({ id: id }, urlToGo).
        then(function () { $(jqGridToUse).trigger("reloadGrid"); });

};

window.showConfirmService = function (id, divScope, urlToGo, jqGridToUse) {
    var scope = angular.element($(divScope)).scope();
    scope.doConfirm({ id: id }, urlToGo).
        then(function () { $(jqGridToUse).trigger("reloadGrid"); });
};


window.showConfirmCancelDocument = function (id, folio, divScope, urlToGo, jqGridToUse) {
    var scope = angular.element($(divScope)).scope();
    scope.doCancelDocument({ uuid: id }, urlToGo, folio).
        then(function () { $(jqGridToUse).trigger("reloadGrid"); });
};

window.showObsolete = function (id, divScope, urlToGo, jqGridToUse) {
    var scope = angular.element($(divScope)).scope();
    scope.doObsolete({ id: id }, urlToGo).
        then(function () { $(jqGridToUse).trigger("reloadGrid"); });
};


window.showModalFormDlg = function (divModalid, formId) {
    var dlgCat = $(divModalid);
    dlgCat.modal('show');

    $.validator.unobtrusive.parse(formId);

    $(divModalid).injector().invoke(function ($compile, $rootScope) {
        $compile($(divModalid))($rootScope);
        $rootScope.$apply();
    });

    var scope = angular.element(dlgCat).scope();
    scope.setDlg(dlgCat);

};