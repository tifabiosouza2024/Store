
$(function () {
    function normalizePath(path) {
        return path.replace(/\/+$/, "").toLowerCase() || "/";
    }

    const currentPath = normalizePath(window.location.pathname);
    const $cashSubmenu = $("#cashSubmenu");
    const $cashToggle = $('[data-bs-target="#cashSubmenu"]');

    let cashMenuHasActiveItem = false;

    $(".sidebar-nav a.nav-link[href]").each(function () {
        const linkUrl = new URL(this.href, window.location.origin);
        const isActive =
            normalizePath(linkUrl.pathname) === currentPath;

        $(this).toggleClass("active", isActive);

        if (isActive && $(this).closest("#cashSubmenu").length) {
            cashMenuHasActiveItem = true;
        }
    });

    if (cashMenuHasActiveItem) {
        // Destaca o botão Caixa e expande o submenu.
        $cashToggle.addClass("active");
        $cashToggle.attr("aria-expanded", "true");
        $cashSubmenu.addClass("show");
    } else {
        $cashToggle.removeClass("active");
        $cashToggle.attr("aria-expanded", "false");
        $cashSubmenu.removeClass("show");
    }
});
