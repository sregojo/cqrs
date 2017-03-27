requirejs.config({
    baseUrl: 'lib',

    paths: {
        app: '../app',
        cmp: '../app/components',
        "jquery": "/jquery-2.2.4",
        "bootstrap": "/bootstrap/js/bootstrap"
    },
    shim: {
        "bootstrap": { "deps": ["jquery"] }
    }
});

requirejs(['app/main']);