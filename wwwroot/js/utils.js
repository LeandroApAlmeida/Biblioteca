function formatToDatetimeLocal(refDate) {
    var date = refDate,
    ten = function (i) {
        return (i < 10 ? '0' : '') + i;
    },
    YYYY = date.getFullYear(),
    MM = ten(date.getMonth() + 1),
    DD = ten(date.getDate()),
    HH = ten(date.getHours()),
    MN = ten(date.getMinutes()),
    SS = ten(date.getSeconds()),
    MS = ten(date.getMilliseconds());
    return YYYY + '-' + MM + '-' + DD + 'T' +
        HH + ':' + MN;
};


export { formatToDatetimeLocal };