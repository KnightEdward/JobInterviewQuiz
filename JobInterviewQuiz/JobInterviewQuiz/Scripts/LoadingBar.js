function pressed(button, loading, content) {
    if (document.getElementById(button).onclick !== true) {
        document.getElementById(loading).style.display = "block";
        document.getElementById(content).style.display = "none";
    }
}