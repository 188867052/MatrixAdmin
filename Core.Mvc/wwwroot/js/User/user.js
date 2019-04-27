(function () {
    'use strict';

    window.Index = function () {
        this._currentPage = 1;
    };

    window.Index.prototype = {

        // Private Fields

        _searchUrl: null,
        _currentPage: null,
        _pageSize: null,

        // Private Event Delegates  

        _onSuccess: function () {
            this._count = "";
        },

        // Public Properties

        // Public Methods

        search: function (e) {
            window.core.gridSearch(this._searchUrl, e, $.proxy(this._onSuccess, this));
        }

        // Private Methods
    };
})();