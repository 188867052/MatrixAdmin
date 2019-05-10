(function () {
    'use strict';

    window.Index = function () {
    };

    window.Index.prototype = {

        // Private Fields

        _searchUrl: null,
        _addUrl: null,
        _editUrl: null,
        _saveUrl: null,
        _deleteUrl: null,
        _dialogInstance: null,
        _recoverUrl: null,

        // Private Event Delegates  

        _onSuccess: function () {
            this.initialize();
        },

        // Public Properties

        // Public Methods

        initialize: function () {
            $(".page-link").on('click', $.proxy(this.search, this));
            $("table .dropdown-toggle").on('click',
                function () {
                    window.core.rowContextMenu();
                });
        },

        search: function () {
            window.core.setSuccessPointer($.proxy(this._onSuccess, this));
            window.core.gridSearch(this._searchUrl);
        },

        add: function () {
            window.core.dialog(this._addUrl);
        },

        edit: function () {
            window.core.editDialog();
        },

        submit: function () {
            window.core.submit(this._saveUrl, this._dialogInstance, $.proxy(this.search, this));
        },

        delete: function () {
            window.core.update($.proxy(this.search, this));
        },

        recover: function () {
            window.core.update($.proxy(this.search, this));
        },

        forbidden: function () {
            window.core.update($.proxy(this.search, this));
        },

        normal: function () {
            window.core.update($.proxy(this.search, this));
        }
        // Private Methods
    };
})();