function loadPage(pageId) {
    // Hide all pages
    var pages = document.getElementsByClassName("page");
    for (var i = 0; i < pages.length; i++) {
        pages[i].style.display = "none";
    }

    // Show the selected page
    var page = document.getElementById(pageId);
    if (page) {
        page.style.display = "block";
    }
}