


// Set the date we're counting down to
//var countDownDate = new Date("Jan 5, 2021 15:37:25").getTime();
var dd = document.getElementById("count-down").textContent;
var countDownDate = new Date( dd+ "15:37:25").getTime();

// Update the count down every 1 second
var x = setInterval(function () {

    // Get today's date and time
    var now = new Date().getTime();

    // Find the distance between now and the count down date
    var distance = countDownDate - now;

    // Time calculations for days, hours, minutes and seconds
    var days = Math.floor(distance / (1000 * 60 * 60 * 24));
    var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
    var seconds = Math.floor((distance % (1000 * 60)) / 1000);

    // Output the result in an element with id="demo"
    document.getElementById("project-timer").innerHTML = days + "d " + hours + "h "
        + minutes + "m " + seconds + "s ";

    // If the count down is over, write some text 
    if (distance < 0) {
        clearInterval(x);
        document.getElementById("project-timer").innerHTML = "EXPIRED";
    }
}, 1000);

//======================= PROJECT IMAGE MODALS ==========================
// Get the modal
var modal = document.getElementById(".myModal");

// Get the image and insert it inside the modal - use its "alt" text as a caption
var img = document.getElementById(".myImg");
var modalImg = document.getElementById(".img01");
var captionText = document.getElementById(".caption");
img.onclick = function () {
    modal.style.display = "block";
    modalImg.src = this.src;
    captionText.innerHTML = this.alt;
}

// Get the <span> element that closes the modal
var span = document.getElementsByClassName(".close")[0];

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}