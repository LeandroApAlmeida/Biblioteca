import { formatToDatetimeLocal } from "./utils.js"


document.addEventListener('DOMContentLoaded', function () {

    document.getElementById("registration-date").value = formatToDatetimeLocal(new Date());

});
