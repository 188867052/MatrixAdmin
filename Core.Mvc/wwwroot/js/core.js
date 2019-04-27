(function () {
    'use strict';

    window.Core = function () {
        this._currentPage = 1;
    };

    window.Core.prototype = {

        // Private Fields

        _currentPage: null,

        // Public Properties

        // Public Methods

        search: function (searchUrl, e, successPointer) {
            var data = new Object;
            data.pageIndex = this.getPageIndex(e);
            data.pageSize = 10;
            var list = $(".custom-control-inline");
            for (var i = 0; i < list.length; i++) {
                var input = list[i].lastElementChild;
                var propertyName = input.getAttribute("name");
                var value = input.value;
                data[propertyName] = value;
            }
            $.post(searchUrl, data, function (response) {
                successPointer(response);
            });
        },

        // Private Methods

        getPageIndex: function (e) {
            if (e.type === "submit") {
                return 1;
            } else if (e.innerText === "«") {
                return this._currentPage - 1;
            } else if (e.innerText === "»") {
                return this._currentPage + 1;
            }
            return e.innerText;
        }
    };
})();