/******************************************/
/************** Search Box ****************/
/******************************************/

const searchBox = document.querySelector(".search-box");
const navBtnContainer = document.querySelector(".nav-btn-container");
const searchBtn = document.querySelector(".search-btn");
const closeBtn = document.querySelector(".close-btn");

// When the search icon is pressed, add 'active'
// identifiers to reveal the hidden search box
searchBtn.addEventListener("click", () => {
    searchBox.classList.add("active");
    navBtnContainer.classList.add("active");
});

// When the close icon is pressed, remove 'active'
// identifiers to hide the visible search box
closeBtn.addEventListener("click", () => {
    searchBox.classList.remove("active");
    navBtnContainer.classList.remove("active");
});



/******************************************/
/*********** Shorten URL Form *************/
/******************************************/

// Set cursor when hovering over label (to match input box)
document.getElementById('pre-text').onmouseenter = function() {
    $('#pre-text label').css('cursor', 'text');
};
document.getElementById('pre-text').onmouseleave = function() {
    this.style.cursor = 'auto';
};

// Focus on input box & change background when
// label is pressed (to act like a single box)
$('#pre-text label').mousedown(function(event) {
    event.preventDefault();
}).mouseup(function(event) {
    if (!($('#pre-text input').is(":focus"))) {
        $('#pre-text input:text').focus();
        $('#pre-text input').css('background', '#ddd');
        $('#pre-text label').css('background', '#ddd');
    }
});

// Reset box background when the area outside is pressed
$(document).on("click", function(event) {
    // If anywhere else is clicked:
    if (!$(event.target).closest("#pre-text input").length && !$(event.target).closest("#pre-text label").length) {
        // Close the menu
        $('#pre-text input').css('background', '#f1f1f1');
        $('#pre-text label').css('background', '#f1f1f1');
    }
});

// When input box is pressed, change its
// background and the label's background
$('#pre-text input').mousedown(function(event) {
    $('#pre-text input').css('background', '#ddd');
    $('#pre-text label').css('background', '#ddd');
}).mouseup(function(event) {
    event.preventDefault();
});



/******************************************/
/******* Profile Settings Dropdown *********/
/******************************************/

$(document).ready(function(){
    // If profile is clicked:
    $(".dropdown-toggle").click(function(){
        // Toggle open/close menu
        $(".dropdown-content").slideToggle(300, function() {
            // If the menu was closed:
            if (!$(this).is(":visible")) {
                // Remove focus from button
                $(".dropbtn").blur();
            }
        });
    });

    $(document).on("click", function(event) {
        // If anywhere else is clicked:
        if (!$(event.target).closest(".dropbtn").length) {
            // Close the menu
            $(".dropdown-content").slideUp(300);
        }
    });
});