$(".like").click(function () {
    var parentElement = $(this).parent().parent().parent().parent().parent().parent().get(0);
    var titleElement = $(parentElement).find(".post-title");
    var title = titleElement.text(); 
    var userData1 = {
        'postCreatorId': $('#postCreatorId').text(),
        'postTitle': title,
        'likeState': true
    };

    console.log(userData1)
    $.ajax({
        type: "POST",
        url: "/ajax/post/addLike",
        data: userData1,
        dataType: "json",
        success: function (data) {
            console.log(data);
            $('#like').removeClass("text-white");            
            $('#like').addClass("text-success");                        
            $('#dislike').removeClass("text-success");
        },
        error: function (erorr) {
            ;
        }
    })
});

$(".dislike").click(function () {



    var userData2 = {
        'postCreatorId': $('#postCreatorId').text(),
        'postTitle': $('.post-title').text(),
        'likeState': false
    };


    console.log(userData2)

    $.ajax({
        type: "POST",
        url: "/ajax/post/addLike",
        data: userData2,
        dataType: "json",
        success: function (data) {
            console.log(data);
            $('#dislike').removeClass("text-white");            
            $('#dislike').addClass("text-success");                        
            $('#like').removeClass("text-success");
        },
        error: function (erorr) {
            ;
        }
    })
});