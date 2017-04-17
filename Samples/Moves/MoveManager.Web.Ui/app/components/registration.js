define(['knockout'],
    function (ko) {

        ko.components.register('login-cmp', {
            viewModel: { require: 'cmp/login/viewmodel' },
            template:  { require: 'text!cmp/login/view.html' }
        });

        ko.components.register('menu-cmp', {
            viewModel: { require: 'cmp/menu/viewmodel' },
            template:  { require: 'text!cmp/menu/view.html' }
        });
    });