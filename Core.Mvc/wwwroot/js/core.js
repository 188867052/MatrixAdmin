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
        _currentTarget: null,

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
            $.get(url,
                function (response) {
                    $(".pagination").replaceWith(response.data);
                    $("#" + response.id).modal("show");
                });
        },

        editDialog: function (url, id) {
            $.get(url, id,
                function (response) {
                    $(".pagination").replaceWith(response.data);
                    $("#" + response.id).modal("show");
                });
        },

        rowContextMenu: function () {
            var url = event.currentTarget.dataset.url;
            var id = event.currentTarget.dataset.id;
            this._currentTarget = event.currentTarget;
            var onSuccess = $.proxy(this.initializeRowContextMenu, this);
            $.get(url, id, onSuccess);
        },

        // Private Methods

        initializeRowContextMenu: function (response) {
            this._currentTarget.nextElementSibling.innerHTML = response;
            $(".dropdown-item").on('click',
                function () {
                    var method = event.currentTarget.dataset.method;
                    var id = event.currentTarget.parentElement.previousElementSibling.dataset.id;
                    eval(method + "('" + id + "')");
                });
        },

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
            var inputList = $(".custom-control-inline input");
            for (var i = 0; i < inputList.length; i++) {
                var input = inputList[i];
                data[input.getAttribute("name")] = input.value;
            }
            var selectList = $(".custom-control-inline select");
            for (var j = 0; j < selectList.length; j++) {
                var select = selectList[j];
                data[select.getAttribute("name")] = select.value;
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