
document.addEventListener('click', function (event) {
    const header = event.target.closest('th[data-field]');

    if (!header)
        return;

    const field = header.dataset.field;
    const parameter = `OrderBy${field}`;

    const params = new URLSearchParams(window.location.search);

    const currentOrder = params.get(parameter);

    let nextOrder;

    switch (currentOrder) {
        case 'asc':
            nextOrder = 'desc';
            break;

        case 'desc':
            nextOrder = null;
            break;

        default:
            nextOrder = 'asc';
            break;
    }

    // Atualiza ou remove a ordenação da coluna.
    if (nextOrder)
        params.set(parameter, nextOrder);
    else
        params.delete(parameter);

    // Retorna para a primeira página.
    params.set('page', '1');

    // Atualiza a URL sem recarregar.
    const url = new URL(window.location.href);
    url.search = params.toString();

    window.history.replaceState(null, '', url);

    // Atualiza o estado visual do cabeçalho.
    header.dataset.order = nextOrder ?? '';
});

document.getElementById('applyTableChanges')?.addEventListener('click', function () {
    window.location.reload();
});
