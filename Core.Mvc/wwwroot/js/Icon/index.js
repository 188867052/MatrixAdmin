(function () {
    'use strict';

    window.Index = function () {
    };

    window.Index.prototype = {

        // Private Fields

        _searchUrl: null,

        // Public Properties

        // Public Methods

        search: function (e) {
            var data = new Object;
            data.pageSize = 10;
            data.currentPage = e.innerText;
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