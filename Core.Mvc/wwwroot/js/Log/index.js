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
            var data = new Object();
            data.Id = 1;
            data.Message = "Message";
            data.CreateTime = "2019-04-25 14:45";
            $.post(this._searchUrl, data, function () {
                alert();
            });
        }

        // Private Methods
    };
})();