function redirectToManage() {
    var beginDate = "1900-01-01";
    var endDate = "2100-12-31";
    var url = `/Session/Manage?beginDate=${beginDate}&endDate=${endDate}`;
    window.location.href = url;
}