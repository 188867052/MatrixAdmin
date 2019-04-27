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

        gridSearch: function (searchUrl, e, successPointer) {
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
            //var onSuccess = $.proxy(this._onRefreshing, this);
            $.post(searchUrl, data, function (response) {
                core.setData(response);
                successPointer();
                //successPointer(response);
            });
        },

        // Private Methods

        _onSearchSuccess: function () {
            this.setData(response);
            successPointer();
        },

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