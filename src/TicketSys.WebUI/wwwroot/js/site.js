
function showSuccessToast(message) {
    toastr.success(message, null, { progressBar: true });
}

function showErrorToast(message) {
    toastr.error(message, null, { progressBar: true });
}

document.addEventListener("DOMContentLoaded", () => {
    const modalEl = document.getElementById('processingModal');
    processingModal = new bootstrap.Modal(modalEl);
});

function showProcessing() {
    processingModal.show();
}


function hideProcessing() {
    processingModal.hide();
}