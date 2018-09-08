$(document).ajaxError(function (xhr, props) {
    if (props.status === 401) {
        window.location.href = '@Url.Action("Login","Account")';
    }
});  