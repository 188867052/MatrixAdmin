(function () {
    'use strict';

    window.Core = function () {
        this._currentPage = 1;
    };

    window.Core.prototype = {

        // Private Fields

        _searchUrl: null,
        _currentPage: null,
        _pageSize: null,

        // Private Event Delegates  

        _onGridSearch: function (response) {
            $(".widget-content")[0].innerHTML = response.data;
            $(".pager").replaceWith(response.pager);
            this._pageSize = response.pageSize;
            this._currentPage = response.currentPage;
            $(".page-link").on('click', function () {

                //TODO
                index.search(event.currentTarget);
            });
        },

        // Public Properties

        // Public Methods

        gridSearch: function (searchUrl, e, successPointer) {
            var data = this.generateFormData(e);
            //var onSuccess = $.proxy(this._onRefreshing, this);
            $.post(searchUrl, data, function (response) {

                //TODO it is better to use proxy
                window.core._onGridSearch(response);
                successPointer();
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
        },

        generateFormData: function (e) {
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
            return data;
        }
    };
})();