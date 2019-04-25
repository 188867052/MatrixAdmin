(function () {
    'use strict';

    window.Index = function () {
    };

    window.Index.prototype = {

        // Private Fields

        _searchUrl: null,

        // Public Properties

        // Public Methods


        search: function () {
            var data = new Object;
            var list = $(".custom-control-inline");
            for (var i = 0; i < list.length; i++) {
                var propertyName = list[i].getAttribute("name");
                data[propertyName] = "1";
            }
            //data.id = "1";
            //data.message = "Message";
            //data.startTime = "2019-04-25 14:45";
            //data.endTime = "2019-04-25 14:45";
            //data.person = new Object();
            //data.person.name = "Message";
            //data.person.sex = "Message";
            $.post(this._searchUrl, data, function () {
                alert();
            });
        }

        // Private Methods
    };
})();