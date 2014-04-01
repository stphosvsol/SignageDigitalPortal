app.directive('pntInn', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<div><div ng-class="channel.active || active ? \'pointer-rectangle-a\' : \'pointer-rectangle\'" ' +
            'ng-style="{left:infopnt.left, top:infopnt.top, \'index-z\': z}"></div><div ng-class="active ? \'move-pnt\' : \'\'" ' +
            'ng-style="{cursor:(active === false ? \'\' : ( infopnt.move === 0 ? \'e-resize\' : ( infopnt.move === 1 ? \'n-resize\' : \'pointer\')))}" ></div>' +
            '</div>',
        scope: {
            screen: '=',
            channel: '=',
            infopnt: '='
        },
        link: function (scope, element) {
            scope.active = false;
            scope.z = 4;
            var last = undefined;

            element.bind('mousedown', function (event) {
                scope.channel.inuse = true;
                scope.active = true;
                scope.z = 6;
                last = window.relMouseCoords(event);
                scope.$apply();
            });

            element.bind('mouseup', function () {
                scope.active = false;
                scope.z = 4;
                last = undefined;
                scope.channel.inuse = false;
                scope.$apply();
            });

            element.bind('mousemove', function (event) {
                if (last === undefined)
                    return;

                var cord = window.relMouseCoords(event);
                cord = { x: cord.x - 5000, y: cord.y - 5000 };

                switch (scope.infopnt.move) {
                    case 0:
                        var xEnd = scope.channel.x + (cord.x - last.x);
                        var wEnd = scope.channel.w - (scope.infopnt.dir) * (cord.x - last.x) - (scope.infopnt.dir === 1 ? 0 : scope.channel.w);
                        if (xEnd < 0)
                            scope.channel.x = 0;
                        else if (xEnd > scope.screen.w) {
                            if (scope.infopnt.dir === 1)
                                scope.channel.x = scope.screen.w;
                            else
                                scope.channel.w = scope.screen.w - scope.channel.x;
                        }
                        else if (wEnd < 2)
                            scope.channel.w = 2;
                        else {
                            scope.channel.w = wEnd;
                            if (scope.infopnt.dir === 1)
                                scope.channel.x = xEnd;
                        }
                        break;
                    case 1:
                        var yEnd = scope.channel.y + (cord.y - last.y);
                        var hEnd = scope.channel.h - (scope.infopnt.dir) * (cord.y - last.y) - (scope.infopnt.dir === 1 ? 0 : scope.channel.h);
                        if (yEnd < 0)
                            scope.channel.y = 0;
                        else if (yEnd > scope.screen.h) {
                            if (scope.infopnt.dir === 1)
                                scope.channel.y = scope.screen.h;
                            else
                                scope.channel.h = scope.screen.h - scope.channel.y;
                        }
                        else if (hEnd < 2)
                            scope.channel.h = 2;
                        else {
                            scope.channel.h = hEnd;
                            if (scope.infopnt.dir === 1)
                                scope.channel.y = yEnd;
                        }
                        break;
                    case 2:
                        var xD = scope.channel.x + (cord.x - last.x);
                        var wD = scope.channel.w - (scope.infopnt.dir.x) * (cord.x - last.x) - (scope.infopnt.dir.x === 1 ? 0 : scope.channel.w);
                        var yD = scope.channel.y + (cord.y - last.y);
                        var hD = scope.channel.h - (scope.infopnt.dir.y) * (cord.y - last.y) - (scope.infopnt.dir.y === 1 ? 0 : scope.channel.h);
                        
                        if (xD < 0)
                            scope.channel.x = 0;
                        else if (xD > scope.screen.w) {
                            if (scope.infopnt.dir.x === 1)
                                scope.channel.x = scope.screen.w;
                            else
                                scope.channel.w = scope.screen.w - scope.channel.x;
                        }
                        else if (wD < 2)
                            scope.channel.w = 2;
                        else {
                            scope.channel.w = wD;
                            if (scope.infopnt.dir.x === 1)
                                scope.channel.x = xD;
                        }
                        
                        if (yD < 0)
                            scope.channel.y = 0;
                        else if (yD > scope.screen.h) {
                            if (scope.infopnt.dir.y === 1)
                                scope.channel.y = scope.screen.h;
                            else
                                scope.channel.h = scope.screen.h - scope.channel.y;
                        }
                        else if (hD < 2)
                            scope.channel.h = 2;
                        else {
                            scope.channel.h = hD;
                            if (scope.infopnt.dir.y === 1)
                                scope.channel.y = yD;
                        }

                        break;
                    default:
                        return;
                }
                scope.$apply();
            });

        }
    };
});
