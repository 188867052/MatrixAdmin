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
            $("table .dropdown-toggle").on('click',
                function () {
                    window.core.rowContextMenu();
                });
        },

        submit: function () {
            var url = event.currentTarget.dataset.url;
            window.core.submit(url, this._dialogInstance, $.proxy(this.search, this));
        },

        search: function () {
            window.core.setSuccessPointer($.proxy(this._onSuccess, this));
            window.core.gridSearch(this._searchUrl);
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