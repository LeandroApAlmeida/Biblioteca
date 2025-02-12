@if(TempData[Constants.SUCCESS_MESSAGE] != null) {
    < div class= "alert alert-success text-black" role = "alert" >
        @TempData[Constants.SUCCESS_MESSAGE]
    </ div >
}