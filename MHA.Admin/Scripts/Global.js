﻿function AddCategory() {
    Category = new Object();
    Category.CategoryName = $("#CategoryName").val();
    Category.URL = $("#URL").val();
    Category.IsActive = $("#activeCategory").is(":checked");
    Category.ParentId = $("#ParentId").val();

    $.ajax({
        url: "Add/Category",
        data: Category,
        type: "Post",
        dataType: 'json',
        success: function (response) {
            if (response.Success) {

                bootbox.alert(response.Message, function () {
                    location.reload();
                });
            }
            else {
                bootbox.alert(response.Message, function () {
                    //geri döndüğünde birşey yapılması isteniyorsa buraya yazılır
                });
            }
        }
    })
}


$(document).on("click", "#CategoryDelete", function () {
    var returnId = $(this).attr("data-id");
    var deleteRow = $(this).closest("tr");
    alert(returnId)
    $.ajax({
        url: '/Category/Delete/' + returnId,
        //data: {

        //    id= returnId,
        //}, 
        type: "POST",
        dataType: 'json',
        success: function (response) {

            deleteRow.remove();
        }
    })


});



function EditCategory() {

    Category = new Object();
    Category.CategoryName = $("#CategoryName").val();
    Category.URL = $("#URL").val();
    Category.IsActive = $("#activeCategory").is(":checked");
    Category.ParentId = $("#ParentId").val();
    Category.Id = $("#Id").val();
    $.ajax({
        url: "Edit/Category/",
        data: Category,
        type: "Post",
        dataType: 'json',
        success: function (response) {

            if (response.Success) {

                bootbox.alert(response.Message, function () {
                    location.reload();
                });
            }
            else {
                bootbox.alert(response.Message, function () {
                    //geri döndüğünde birşey yapılması isteniyorsa buraya yazılır
                });
            }
        }
    })

}

