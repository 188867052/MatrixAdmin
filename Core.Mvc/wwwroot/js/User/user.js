(function () {
    'use strict';

    window.Index = function () {
        this._currentPage = 1;
    };

    window.Index.prototype = {

        // Private Fields

        _searchUrl: null,
        _currentPage: null,
        _count: null,
        _pageSize: null,

        // Public Properties

        // Public Methods

        search: function (e) {
            window.core.search(this._searchUrl, e, $.proxy(this.setData, this));
        },

        // Private Methods

        setData: function (response) {
            $(".widget-content")[0].innerHTML = response.data;
            $(".pager").replaceWith(response.pager);
            index._pageSize = response.pageSize;
            index._currentPage = response.currentPage;
            $(".page-link").on('click', function () { index.search(event.currentTarget); });
        }
    };
})();