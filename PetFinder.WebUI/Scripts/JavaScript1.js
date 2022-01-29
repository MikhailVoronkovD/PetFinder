$(document).ready(function () {
    $('input[type="file"]').change(function () {
        var file = this.files; //Files[0] = 1st file
        if (file[0]) {
            var reader = new FileReader();
            reader.readAsDataURL(file[0], 'UTF-8');
            reader.onload = function (event) {
                var result = event.target.result;
                $('.preview').attr('src', event.target.result);

            };
        }
    })

});