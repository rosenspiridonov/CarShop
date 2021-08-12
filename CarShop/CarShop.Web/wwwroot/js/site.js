$('.sorting input[type="submit"]').click(function (e) {
    let queryData = window.location.search;
    if (queryData !== '') {
        e.preventDefault();
    } else {
        return;
    }

    queryData = queryData.replace(/&?Sorting=\d&Order=\d/g, '');
    const formQuery = $('.sorting').serialize();
    const queryString = queryData + '&' + formQuery;
    const pathName = window.location.pathname;
    const hostName = window.location.host;

    let url = 'https://' + hostName + pathName + queryString;

    url = url.replaceAll('&&', '&').replace('?&', '?');

    window.location = url;
});

$(document).ready(() => {
    $('.pagination .page-link').each(function () {
        $(this).attr('href', $(this).attr('href') + window.location.search.replace('?', '&').replace(/&page=\d+/g, '').replaceAll('&&', '&'));
    });
});
