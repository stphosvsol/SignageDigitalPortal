app.directive('channelDiv', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<div ng-class="channel.active ? \'move-pointer\' : \'\'">' +
            '<div ng-style="{width:channel.w, height:channel.h, left:channel.x, top:channel.y, \'z-index\':channel.z}" ng-class="channel.active ? \'channel-draw-a\' : \'channel-draw\'">' +
            '<span class="channel-name">{{channel.name}}</span>' +
            '<pnt-inn channel="channel" screen="screen" infopnt="{left:-(rectsize/2), top:-(rectsize/2), move:2, dir:{x:1, y:1}}"></pnt-inn>' +
            '<pnt-inn channel="channel" screen="screen" infopnt="{left:(channel.w/2)-5, top:-(rectsize/2), move:1, dir:1}"></pnt-inn>' +
            '<pnt-inn channel="channel" screen="screen" infopnt="{left:channel.w-6, top:-(rectsize/2), move:2, dir:{x:-1, y:1}}"></pnt-inn>' +
            '<pnt-inn channel="channel" screen="screen" infopnt="{left:-(rectsize/2), top:(channel.h/2)-((rectsize/2) + 1), move:0, dir:1}"></pnt-inn>' +
            '<pnt-inn channel="channel" screen="screen" infopnt="{left:-(rectsize/2), top:channel.h-((rectsize/2) + 2), move:2, dir:{x:1, y:-1}}"></pnt-inn>' +
            '<pnt-inn channel="channel" screen="screen" infopnt="{left:channel.w-((rectsize/2) + 2), top:(channel.h/2)-((rectsize/2) + 1), move:0, dir:-1}"></pnt-inn>' +
            '<pnt-inn channel="channel" screen="screen" infopnt="{left:(channel.w/2)-((rectsize/2) + 2), top:channel.h-((rectsize/2) + 2), move:1, dir:-1}"></pnt-inn>' +
            '<pnt-inn channel="channel" screen="screen" infopnt="{left:channel.w-((rectsize/2) + 2), top:channel.h-((rectsize/2) + 1), move:2, dir:{x:-1, y:-1}}"></pnt-inn>' +
            '</div></div>',
        scope: {
            channel: '=',
            screen: '='
        },
        link: function (scope, element) {
            var last = undefined;
            scope.channel.inuse = false;
            scope.rectsize = 12;

            element.bind('mousedown', function (event) {
                if (scope.channel.inuse === true) return;
                last = window.relMouseCoords(event);
                scope.channel.z = 2;
                scope.channel.active = true;
                scope.$apply();
            });

            element.bind('mouseup', function () {
                if (scope.channel.inuse === true) return;
                last = undefined;
                scope.channel.z = 0;
                scope.channel.active = false;
                scope.$apply();
            });

            element.bind('mousemove', function (event) {
                if (last === undefined)
                    return;
                var cord = window.relMouseCoords(event);
                if (Math.abs(cord.x - last.x) <= last.x) scope.channel.x = scope.channel.x + cord.x - last.x;
                else scope.channel.x = cord.x;
                if (scope.channel.x < 0) scope.channel.x = 0;
                if (scope.channel.x + scope.channel.w > scope.screen.w) scope.channel.x = scope.screen.w - scope.channel.w;

                if (Math.abs(cord.y - last.y) <= last.y) scope.channel.y = scope.channel.y + cord.y - last.y;
                else scope.channel.y = cord.y;
                if (scope.channel.y < 0) scope.channel.y = 0;
                if (scope.channel.y + scope.channel.h > scope.screen.h) scope.channel.y = scope.screen.h - scope.channel.h;

                scope.$apply();
                //console.log('-----: ' + cord.x + "-" + cord.y + " -dif- " + (cord.x - last.x));
                //console.log('antes: ' + scope.channel.x + "-" + scope.channel.y);
                //console.log(scope.channel.x + "-" + last.x + " - " + cord.x);
                //console.log("(" + last.x + ", " + last.y + "),  ");
                //console.log('desp: ' + scope.channel.x + "-" + scope.channel.y);
            });
        }
    };
});
