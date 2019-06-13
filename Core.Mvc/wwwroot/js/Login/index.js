(function () {
    'use strict';

    window.Index = function () {
    };

    window.Index.prototype = {

        // Private Fields

        _indexUrl: null,
        _authUrl: null,
        _roleUrl: null,

        // Private Event Delegates  

        // Public Properties

        // Public Methods

        initialize: function () {
            $('a[type="submit"]').on('click', $.proxy(this.login, this));
        },

        login: function () {
            var username = $('input[type="text"]')[0].value;
            var password = $('input[type="password"]')[0].value;
            $.get(this._authUrl, { username: username, password: password }, $.proxy(this._onSuccess, this));
        },

        _onSuccess: function (data) {
            if (typeof (data.token) == 'undefined') {
                alert(data.message);
            } else {
                window.token = data.token;
                document.cookie = "token=" + window.token;  
                window.location.href = this._indexUrl;
            }
        },
    };
})();