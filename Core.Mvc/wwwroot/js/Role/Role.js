(function () {
    'use strict';

    window.Index = function () {
    };

    window.Index.prototype = {

        // Private Fields

        _searchUrl: null,

        // Private Event Delegates  

        _onSuccess: function () {
            //do something
        },

        // Public Properties

        // Public Methods

        search: function (e) {
            window.core.gridSearch(this._searchUrl, e, $.proxy(this._onSuccess, this));
        }

        // Private Methods
    };
})();