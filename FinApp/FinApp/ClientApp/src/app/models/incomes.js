"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var transaction_1 = require("./transaction");
var incomeCategory_1 = require("./incomeCategory");
var account_1 = require("./account");
var Incomes = /** @class */ (function () {
    function Incomes() {
        this.incomeCategory = new incomeCategory_1.IncomeCategory();
        this.transaction = new transaction_1.Transaction();
        this.account = new account_1.Account();
    }
    return Incomes;
}());
exports.Incomes = Incomes;
//# sourceMappingURL=incomes.js.map