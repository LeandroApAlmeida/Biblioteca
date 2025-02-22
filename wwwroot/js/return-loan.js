import { formatToDatetimeLocal } from "./module-datetime.js"


document.addEventListener('DOMContentLoaded', function () {

    document.getElementById("return-date").value = formatToDatetimeLocal(new Date());

});