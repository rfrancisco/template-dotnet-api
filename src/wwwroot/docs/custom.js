(function () {
    return;
    var interval = setInterval(function () {
        if (document.dom) {

        }
        var logo = document.querySelector('.topbar-wrapper > a')[0];
        logo.href = "https://www.projectName.com";

        var logoImg = document.querySelector('.topbar-wrapper > a > img');
        logoImg.alt = "ProjectName";
        logoImg.src = "logo.png";
        logoImg.width = 496;
    })
})();