function toggleRideEditState(event) {
    // get coordinates
    var x = event.clientX; var y = event.clientY;

    // find the element that was clicked on
    var rideBoxes = document.getElementsByClassName("rideWidget");
    var target = null;

    // get screen coordinates (Source: https://plainjs.com/javascript/styles/get-the-position-of-an-element-relative-to-the-document-24/)
    var scrollLeft = window.pageXOffset || document.documentElement.scrollLeft;
    var scrollTop = window.pageYOffset || document.documentElement.scrollTop;

    for (var i = 0; i < rideBoxes.length; i++) {
        var box = rideBoxes[i];
        var rect = box.getBoundingClientRect();

        // check if the click was inside the box
        if (((x >= rect.left + scrollLeft) && (x <= rect.right + scrollLeft))
            && ((y >= rect.top + scrollTop) && (y <= rect.bottom + scrollTop))) {
            target = rideBoxes[i];
            break;
        }
    }

    if (target == null) {
        console.error("Faulty click event. No ride object found.");
        return;
    }

    // if ride is in edit mode, exit out of edit mode
    if (target.classList.contains("active")) {
        // remove active class and show normal view
        target.classList.remove("active");
        target.getElementsByClassName("normal-view")[0].style.display = "block";
        target.getElementsByClassName("edit-view")[0].style.display = "none";
    }
    else {
        // add active class and show edit view
        target.classList.add("active");
        target.getElementsByClassName("normal-view")[0].style.display = "none";
        target.getElementsByClassName("edit-view")[0].style.display = "block";
    }
}

function toggleRouteEditState(event) {
    // get coordinates
    var x = event.clientX; var y = event.clientY;

    // find the element that was clicked on
    var rideBoxes = document.getElementsByClassName("routeWidget");
    var target = null;

    // get screen coordinates (Source: https://plainjs.com/javascript/styles/get-the-position-of-an-element-relative-to-the-document-24/)
    var scrollLeft = window.pageXOffset || document.documentElement.scrollLeft;
    var scrollTop = window.pageYOffset || document.documentElement.scrollTop;

    for (var i = 0; i < rideBoxes.length; i++) {
        var box = rideBoxes[i];
        var rect = box.getBoundingClientRect();

        // check if the click was inside the box
        if (((x >= rect.left + scrollLeft) && (x <= rect.right + scrollLeft))
            && ((y >= rect.top + scrollTop) && (y <= rect.bottom + scrollTop))) {
            target = rideBoxes[i];
            break;
        }
    }

    if (target == null) {
        console.error("Faulty click event. No ride object found.");
        return;
    }

    // if ride is in edit mode, exit out of edit mode
    if (target.classList.contains("active")) {
        // remove active class and show normal view
        target.classList.remove("active");
        target.getElementsByClassName("normal-view")[0].style.display = "block";
        target.getElementsByClassName("edit-view")[0].style.display = "none";
    }
    else {
        // add active class and show edit view
        target.classList.add("active");
        target.getElementsByClassName("normal-view")[0].style.display = "none";
        target.getElementsByClassName("edit-view")[0].style.display = "block";
    }
}

// put each route object in "normal mode"
var rideWidgets = document.getElementsByClassName("routeWidget");
for (var j = 0; j < rideWidgets.length; j++) {
    var box = rideWidgets[j];
    if (box.classList.contains("active")) box.classList.remove("active");

    box.getElementsByClassName("normal-view")[0].style.display = "block";
    box.getElementsByClassName("edit-view")[0].style.display = "none";
}


// put each ride object in "normal mode"
var rideWidgets = document.getElementsByClassName("rideWidget");
for (var j = 0; j < rideWidgets.length; j++) {
    var box = rideWidgets[j];
    if (box.classList.contains("active")) box.classList.remove("active");

    box.getElementsByClassName("normal-view")[0].style.display = "block";
    box.getElementsByClassName("edit-view")[0].style.display = "none";
}







//@*Toggles between the ride and route list.*@
function hideFunction() {
    //@* Change user rider/ driver status *@

		var x = document.getElementById('rideList');
    var y = document.getElementById('routeList');

    if (document.getElementById('viewType').checked) {
        x.style.display = "none";
        y.style.display = "block";

        $.ajax({
            type: "POST",
            url: "ChangeUserDriverStatus",
            data: "{ 'isDriving': 'true' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log(result);
            }
        });
    }
    else {
        x.style.display = "block";
        y.style.display = "none";

        $.ajax({
            type: "POST",
            url: "ChangeUserDriverStatus",
            data: "{ 'isDriving': 'false' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log(result);
            }
        });
    }

    //@*location.href = "@Url.Action("Index", "Driver")";*@
	}

    (function () {
        var x = document.getElementById('rideList');
        var y = document.getElementById('routeList');

        if (document.getElementById('viewType').checked) {
            x.style.display = "none";
            y.style.display = "block";
        }
        else {
            x.style.display = "block";
            y.style.display = "none";
        }
    })();




    function greenBox(buttonClass) {
        var x = buttonClass;
        var y = document.getElementsByClassName(x + "-Check")[0];
        var z = document.getElementsByClassName(x + "-Btn")[0];

        if (y.getAttribute("checked") == "") {
            y.removeAttribute("checked");
            $(z).removeClass("greenBox");
        }
        else {
            y.setAttribute("checked", "");
            $(z).addClass("greenBox");
        }

    }

    function renderGreenBox(buttonClass) {
        var x = buttonClass;
        var y = document.getElementsByClassName(x + "-Check")[0];
        var z = document.getElementsByClassName(x + "-Btn")[0];
        if (y.getAttribute("checked") == "") {
            $(z).addClass("greenBox");
        }
    }