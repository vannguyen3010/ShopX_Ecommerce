function copyToClipboard(text) {
    navigator.clipboard.writeText(text).then(function () {
        alert("Copy thành công!");
    }, function (err) {
        console.error("Error in copying text: ", err);
    });
}