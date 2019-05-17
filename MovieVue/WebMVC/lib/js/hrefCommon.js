//跳转到首页
$('#pageIndexli').on('click', function () {
    window.location.href = '/HomePage/Index';
    window.event.returnValue = false;
});

//跳转到电影页
$('#movieli').on('click', function () {
    window.location.href = '/Movie/Index';
    window.event.returnValue = false;
});
//跳转到新闻页
$('#newsli').on('click', function () {
    window.location.href = '/News/Index';
    window.event.returnValue = false;
});
//跳转到成员页
$('#userli').on('click', function () {
    window.location.href = '/User/Index';
    window.event.returnValue = false;
});
//跳转到评论页
$('#commentli').on('click', function () {
    window.location.href = '/Comment/Index';
    window.event.returnValue = false;
});
//跳转到反馈页
$('#emailli').on('click', function () {
    window.location.href = '/Mail/Index';
    window.event.returnValue = false;
});
