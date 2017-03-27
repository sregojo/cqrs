define(['knockout'], function (ko) {
    return function viewModel() {
        this.user = ko.observable('Usuario');
        this.password = ko.observable('Password');
    };
});