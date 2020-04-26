$(function () {
    $("#upload-file-btn").off().on("click", function (ev) {

        ev.preventDefault();

        var $imageElement = $("#image")[0];

        if ($imageElement.files.length === 0) {

            alert("select a file");
            return;
        }

        var title = $("#title").val();
        if (!title || $.trim(title) === '') {

            alert("provide a title");
            return;
        }
        var formData = new FormData();
        formData.append("Image", $imageElement.files[0]);
        formData.append("Title", title);

        $.ajax({
            url: '/api/upload',
            method: 'POST',
            data: formData,
            processData: false,
            contentType: false,
        }).done(function (result) {
            if (result && !result.success) {
                alert(result.message);

            }
        }).fail(function () {

        });
    });
});