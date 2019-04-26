(function () {
    'use strict';

    window.Core = function () {
    };

    window.Core.prototype = {

        // Private Fields

        _searchUrl: null,

        // Public Properties

        // Public Methods

        search: function () {
            var data = new Object;
            var list = $(".custom-control-inline");
            for (var i = 0; i < list.length; i++) {
                var input = list[i].lastElementChild;
                var propertyName = input.getAttribute("name");
                var value = input.value;
                data[propertyName] = value;
            }
            $.post(this._searchUrl, data, function (response) {
                $(".widget-content")[0].innerHTML = response;
            });
        }

        // Private Methods
    };
})();