export function showLoading() {
    document
        .getElementById("loadingOverlay")
        .classList.remove("d-none");
}

export function hideLoading() {
    document
        .getElementById("loadingOverlay")
        .classList.add("d-none");
}