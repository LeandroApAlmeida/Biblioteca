import { formatToDatetimeLocal } from "./utils.js"


document.addEventListener('DOMContentLoaded', function () {

    document.getElementById("return-date").value = formatToDatetimeLocal(new Date());

});