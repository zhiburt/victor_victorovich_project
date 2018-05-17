// $('#stars').barrating({
//     theme: 'fontawesome-stars-o',
//     onSelect: function (value, text, event) {
//         if (typeof (event) !== 'undefined') {
//             // rating was selected by a user
//             console.log(event.target);
//             console.log(text);
//             console.log(value);
//             var userData = {
//                 'manualName': $('.blog-title').text(),
//                 'creator': $('.creator-blog').text(),
//                 'rating': value
//             };

//             $.ajax({
//                 type: "POST",
//                 url: "/api/setRating",
//                 data: userData,
//                 dataType: "json",
//                 success: function (data) {
//                     console.log(data);
//                     console.log(typeof (data));
//                     $('#stars').barrating('set', data);
//                 },
//                 error: function (erorr) {
//                     ;
//                 }
//             })
//             $('#stars').barrating('readonly', true);
//         } else {
//             // rating was selected programmatically
//             // by calling `set` method
//         }
//     }
// });

$(document).ready(function () {
    $('img').addClass('img-fluid');
    $('iframe').addClass('embed-responsive-item embed-responsive embed-responsive-16by9');
});

// $("#like").click(function () {

//     var userData1 = {
//         'manualName': $('.blog-title').text(),
//         'creator': $('.creator-blog').text(),
//         'data': true
//     };
//     console.log(userData1)
//     $.ajax({
//         type: "POST",
//         url: "/api/AddLike",
//         data: userData1,
//         dataType: "json",
//         success: function (data) {
//             $('#like').addClass("text-success");
//             $('#dislike').removeClass("text-success");
//         },
//         error: function (erorr) {
//             ;
//         }
//     })
// });

// $("#dislike").click(function () {



//     var userData2 = {
//         'manualName': $('.blog-title').text(),
//         'creator': $('.creator-blog').text(),
//         'data': false
//     };


//     console.log(userData2)

//     $.ajax({
//         type: "POST",
//         url: "/api/AddLike",
//         data: userData2,
//         dataType: "json",
//         success: function (data) {
//             $('#dislike').addClass("text-success");
//             $('#like').removeClass("text-success")
//         },
//         error: function (erorr) {
//             ;
//         }
//     })
// });