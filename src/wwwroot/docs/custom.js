(function () {
    return;
    var interval = setInterval(function () {
        if (document.dom) {

        }
        var logo = document.querySelector('.topbar-wrapper > a')[0];
        logo.href = "https://www.focus-bc.com";

        var logoImg = document.querySelector('.topbar-wrapper > a > img');
        logoImg.alt = "Focus BC";
        logoImg.src = "logo.png";
        logoImg.width = 496;
    })
})();