/// <reference path="model/Person.js" />
/// <reference path="viewmodel/userviewmodel.js" />
/// <reference path="infrastructure/binder.js" />
/// <reference path="./jquery.unobtrusive-ajax.js" />
/// <reference path="./jquery-ui.js" />
/// <reference path="./bootstrap.js" />

require.config({
    paths: {
        'jquery': './jquery-3.4.1.js',
        'jquryui': './jquery-ui.js',
        'jqueryajax': './jquery.unobtrusive-ajax.js',
        'knockout': './knockout-3.5.1.js',
        'bootstrap': './bootstrap.js',
        'datepicker': './bootstrap-datepicker.js'
    }
});

require(['viewmodel/userviewmodel', 'model/Person', 'infrastructure/binder'], function (userViewModel, Person, binder) {

    binder.apply();
    userViewModel.sayHelloJquery();
    var person = new Person('bob');
    userViewModel.sayHelloKnockout(person);
});