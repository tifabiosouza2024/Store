document.addEventListener('DOMContentLoaded', function () {
    const accountTabs = document.querySelectorAll('#accountTabs [data-bs-toggle="tab"]');

    function activateAccountTab() {
        const target = window.location.hash;
        if (!target) return;

        const tab = document.querySelector(`#accountTabs [data-bs-target="${target}"]`);
        if (tab) bootstrap.Tab.getOrCreateInstance(tab).show();
    }

    accountTabs.forEach(tab => {
        tab.addEventListener('shown.bs.tab', function () {
            const target = this.dataset.bsTarget;

            history.replaceState(null, '', target);
            window.dispatchEvent(new Event('hashchange'));
        });
    });

    activateAccountTab();

    window.addEventListener('hashchange', activateAccountTab);
});