
$(document).ready(function (){
    $("#myHidden").hide();
})

$('#ShowRegisterPage').on('click', function (ev) {
    ev.preventDefault();
    $("#myHidden").slideToggle('slow');

    if ($("#myHidden").is(':visible')) {
        $("html, body").animate({ scrollTop: $("#myHidden").offset().top });
    }

});

window.onload = function (){
    initMap();
}

var marker = null;

function initMap() {
    var myCenter = new google.maps.LatLng(44.468520, 26.080513);
    var mapProp = { center: myCenter, zoom: 12, mapTypeId: google.maps.MapTypeId.ROADMAP };
    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

    google.maps.event.addListener(map, 'click', function (event) {
        var lat = event.latLng.lat();
        var lng = event.latLng.lng();


        document.getElementById("Latitude").value = lat.toFixed(7);
        document.getElementById("Longitude").value = lng.toFixed(7);


        addMarker(lat, lng, map);
    });

}

function addMarker(lat, lng, map) {
    if (marker !== null) {
        marker.setMap(null);
    }
    marker = new google.maps.Marker({
        position: new google.maps.LatLng(lat, lng), 
        map: map
    });

}

