// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// setTimeout => şu kadar saniye sonra çalıştır!

$(function () {
    setTimeout(() => {
        $("div.alert.notification").fadeOut("slow")
    }, 2000);
});