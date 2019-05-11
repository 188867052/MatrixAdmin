(function () {
    'use strict';

    window.Index = function () {
    };

    window.Index.prototype = {

        // Private Fields

        _searchUrl: null,

        // Private Event Delegates  

        _onSuccess: function () {
            this.initialize();
        },

        // Public Properties

        // Public Methods

        initialize: function () {
            $(".page-link").on('click', $.proxy(this.search, this));
        },

        add: function () {
            window.core.dialog();
        },

        edit: function () {
            window.core.editDialog();
        },

        submit: function () {
            var url = event.currentTarget.dataset.url;
            window.core.submit(url, this._dialogInstance, $.proxy(this.search, this));
        },

        search: function () {
            window.core.setSuccessPointer($.proxy(this._onSuccess, this));
            window.core.gridSearch(this._searchUrl);
        }

        // Private Methods
    };
})();