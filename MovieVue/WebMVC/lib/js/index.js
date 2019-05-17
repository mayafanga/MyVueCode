//调用动画
new WOW().init();
//图片剪切初始化
$('.image-editor').cropit({
    //加载小图片
    smallImage: 'allow',
    // 让图片脱离边框
    freeMove: true,
    //图片加载时的缩放
    initialZoom: 'min'
});

//轮播选择图片
$('.preGloryInfo .glorySlidInfo .select-image-btn').click(function () {
    $('.preGloryInfo .glorySlideInfo .cropit-image-input').click();
    console.log(10);
});
//轮播输出图片
$('.preGloryInfo .glorySlideInfo .export').click(function () {
    var imageData = $('.preGloryInfo .glorySlideInfo .image-editor').cropit('export', {
        type: 'image/jpeg'
    });
    $('.preGloryInfo .againUpLoad img').each(function () {
        if ($(this).attr('src') == '') {
            $(this).attr('src', imageData);
            return false;
        }
    });
});
//重新生成轮播图
$('.preGloryInfo .againUpLoad button').each(function () {
    $(this).on('click', function () {
        $(this).prev().attr('src', '');
    });
});