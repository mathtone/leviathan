var CookieManagerClass = /** @class */ (function () {
    function CookieManagerClass() {
    }
    CookieManagerClass.prototype.setCookie = function (name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    };
    CookieManagerClass.prototype.getCookie = function (name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var _i = 0, ca_1 = ca; _i < ca_1.length; _i++) {
            var element = ca_1[_i];
            var c = element;
            while (c.charAt(0) === ' ')
                c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) === 0)
                return c.substring(nameEQ.length, c.length);
        }
        return null;
    };
    CookieManagerClass.prototype.eraseCookie = function (name) {
        document.cookie = name + '=; Max-Age=-99999999;';
    };
    return CookieManagerClass;
}());
window.CookieManager = new CookieManagerClass();
export function navigateTo(url) {
    window.location.href = url;
}
window.navigateTo = navigateTo;
