/// <reference path="../knockout-3.5.1.js" />


define(['knockout'], function (ko) {

    var ctor = function (name) {
        this.name = ko.observable(name);
    };

    return ctor;
});