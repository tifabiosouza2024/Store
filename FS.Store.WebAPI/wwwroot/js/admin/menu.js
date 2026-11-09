$(function () {
    function normalizePath(path) {
        return path.replace(/\/+$/, "").toLowerCase() || "/";
    }

    const currentPath = normalizePath(window.location.pathname);

    // Primeiro, identifica os links correspondentes à página atual.
    $(".sidebar-nav a.nav-link[href]").each(function () {
        const linkUrl = new URL(this.href, window.location.origin);

        const isActive =
            normalizePath(linkUrl.pathname) === currentPath;

        $(this).toggleClass("active", isActive);
    });

    // Depois, verifica todos os submenus.
    $(".sidebar-nav .collapse[id]").each(function () {
        const $submenu = $(this);
        const submenuId = this.id;

        const $toggle = $(
            '.sidebar-nav [data-bs-target="#' + submenuId + '"]'
        );

        const hasActiveItem = $submenu.find("a.nav-link.active").length > 0;

        // Destaca o botão principal e expande o submenu.
        $toggle.toggleClass("active", hasActiveItem);
        $toggle.attr("aria-expanded", hasActiveItem ? "true" : "false");

        $submenu.toggleClass("show", hasActiveItem);
    });
});