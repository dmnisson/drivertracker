System.register(["@angular/platform-browser-dynamic", "./app.module.js"], function (exports_1, context_1) {
    "use strict";
    var platform_browser_dynamic_1, app_module_js_1;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (platform_browser_dynamic_1_1) {
                platform_browser_dynamic_1 = platform_browser_dynamic_1_1;
            },
            function (app_module_js_1_1) {
                app_module_js_1 = app_module_js_1_1;
            }
        ],
        execute: function () {
            platform_browser_dynamic_1.platformBrowserDynamic().bootstrapModule(app_module_js_1.AppModule);
        }
    };
});
