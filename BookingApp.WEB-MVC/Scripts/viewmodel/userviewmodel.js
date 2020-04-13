/// <reference path='../jquery-3.4.1.js' />
/// <reference path="../knockout-3.5.1.js" />
/// <reference path="../model/Person.js" />


define(['jquery', 'knockout'], function ($, ko) {


    var sayHelloJquery = function () {
        $(function () {
            $('#test').val('hello from jquery');
            //console.log($);
        });
    };

    var greeting = ko.observable('');

    var sayHelloKnockout = function (person) {
        $(function () {
            greeting('hello from knockout, ' + person.name());
            //console.log(ko);
        });
    };


    return {
        sayHelloJquery: sayHelloJquery,
        sayHelloKnockout: sayHelloKnockout,
        greeting: greeting
    };

});
