// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showSuccessToast(message) {
    toastr.success(message, null, { progressBar: true });
}

function showErrorToast(message) {
    toastr.error(message, null, { progressBar: true });
} 