$(function () {
    function normalizePath(path) {
        return path.replace(/\/+$/, "").toLowerCase() || "/";
    }

    function updateSidebar() {
        const currentPath = normalizePath(window.location.pathname);
        const currentHash = window.location.hash.toLowerCase();

        $(".sidebar-nav a.nav-link[href]").each(function () {
            const linkUrl = new URL(this.href, window.location.origin);
            const samePath = normalizePath(linkUrl.pathname) === currentPath;
            const linkHash = linkUrl.hash.toLowerCase();

            const isActive = samePath && (currentHash ? linkHash === currentHash : !linkHash);

            $(this).toggleClass("active", isActive);
        });

        $(".sidebar-nav .collapse[id]").each(function () {
            const $submenu = $(this);
            const submenuId = this.id;
            const $toggle = $('.sidebar-nav [data-bs-target="#' + submenuId + '"]');
            const hasActiveItem = $submenu.find("a.nav-link.active").length > 0;

            $toggle.toggleClass("active", hasActiveItem);
            $toggle.attr("aria-expanded", hasActiveItem ? "true" : "false");
            $submenu.toggleClass("show", hasActiveItem);
        });
    }

    updateSidebar();

    window.addEventListener('hashchange', updateSidebar);
});