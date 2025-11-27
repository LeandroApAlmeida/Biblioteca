
/**
 * Módulo para formatação de datas.
 */


/**
 * Formatar a data como YYYY-MM-DDTHH:MN.
 * 
 * @param {any} refDate data de referência.
 * @returns data formatada.
 */
function formatToDatetimeLocal(refDate) {

    var date = refDate,
    ten = function (i) {
        return (i < 10 ? '0' : '') + i;
    },
    YYYY = date.getFullYear(),
    MM = ten(date.getMonth() + 1),
    DD = ten(date.getDate()),
    HH = ten(date.getHours()),
    MN = ten(date.getMinutes());

    return YYYY + '-' + MM + '-' + DD + 'T' + HH + ':' + MN;

};


export { formatToDatetimeLocal };