(function () {
    'use strict';

    window.Index = function () {
        this._currentPage = 1;
    };

    window.Index.prototype = {

        // Private Fields

        _searchUrl: null,
        _currentPage: null,
        _count: null,
        _pageSize: null,

        // Public Properties

        // Public Methods

        search: function (e) {
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
            $.post(this._searchUrl, data, function (response) {
                index.setData(response);
            });
        },

        // Private Methods

        setData: function (response) {
            $(".widget-content")[0].innerHTML = response.data;
            $(".pager").replaceWith(response.pager);
            index._pageSize = response.pageSize;
            index._currentPage = response.currentPage;
            $(".page-link").on('click', function () { index.search(event.currentTarget); });
        },

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