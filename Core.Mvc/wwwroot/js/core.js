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
        _leftText: null,
        _rightText: null,

        // Private Event Delegates  

        _onGridSearch: function (response) {
            $(".widget-content")[0].innerHTML = response.data;
            $(".pagination").replaceWith("<p></p>" + response.pager);
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

        dialog: function (url) {
            $.get(url, function (response) {
                $(".pagination").replaceWith(response.data);
                $("#" + response.id).modal("show");
            });
        },

        // Private Methods

        getPageIndex: function () {
            var e = event.currentTarget;
            if (e.type === "submit") {
                return 1;
            } else if (e.innerText === this._leftText) {
                return this._currentPage - 1;
            } else if (e.innerText === this._rightText) {
                return this._currentPage + 1;
            }
            return e.innerText;
        },

        generateFormDataById: function (id) {
            var data = new Object;
            $("#" + id + " input").each(function () {
                var propertyName = this.getAttribute("name");
                data[propertyName] = this.value;
            });
            return data;
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
        },

        submit: function (url, id, onSuccess) {
            var data = this.generateFormDataById(id);
            $.post(url, data);
            $("#" + id).modal("hide");
            onSuccess();
        }
    };
})();