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
        _successPointer: null,

        // Private Event Delegates  

        _onGridSearch: function (response) {
            $(".widget-content")[0].innerHTML = response.data;
            $(".pager").replaceWith(response.pager);
            this._pageSize = response.pageSize;
            this._currentPage = response.currentPage;
            this._successPointer();
        },

        // Public Properties

        // Public Methods

        setSuccessPointer: function (pointer) {
            this._successPointer = pointer;
        },

        gridSearch: function (searchUrl) {
            var data = this.generateFormData();
            var onSuccess = $.proxy(this._onGridSearch, this);
            $.post(searchUrl, data, onSuccess);
        },

        // Private Methods

        getPageIndex: function () {
            var e = event.currentTarget;
            if (e.type === "submit") {
                return 1;
            } else if (e.innerText === "«") {
                return this._currentPage - 1;
            } else if (e.innerText === "»") {
                return this._currentPage + 1;
            }
            return e.innerText;
        },

        generateFormData: function () {
            var data = new Object;
            data.pageIndex = this.getPageIndex();
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