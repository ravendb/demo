
document.getElementById("collapseSidebar").addEventListener("click", function () {
    var sidebarClasses = document.getElementById("sidebar").classList;

    if (sidebarClasses.contains("small")) {
        sidebarClasses.remove("small");
    } else {
        sidebarClasses.add("small");
    }
});

/*
document.getElementById("runScript").addEventListener("click", function () {
    var sidebarClasses = document.getElementById("results").classList;

    if (sidebarClasses.contains("show")) {
        sidebarClasses.remove("show");
    } else {
        sidebarClasses.add("show");
    }
});*/
