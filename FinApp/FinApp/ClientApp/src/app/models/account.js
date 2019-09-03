"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var currency_1 = require("./currency");
var image_1 = require("./image");
var Account = /** @class */ (function () {
    function Account() {
        this.image = new image_1.Image();
        this.currency = new currency_1.Currency();
    }
    return Account;
}());
exports.Account = Account;
//# sourceMappingURL=account.js.map