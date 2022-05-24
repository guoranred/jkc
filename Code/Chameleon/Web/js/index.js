$(function() {
  // banner轮播初始化
  initBannerSwiper();
  // 新闻资讯初始化
  initNewsSwiper();
  // 初始化合作伙伴
  initPartSwiper();
  // 切换产品类别
  $('.product-type-item').on('click', function() {
    var pIndex = $(this).attr('data-index');
    var pTotal = $(this).attr('data-total');
    var pTitle = $(this).attr('data-title');
    var pContent = $(this).attr('data-content');
    $(this).addClass('active').siblings().removeClass('active');
    $('.product-title').text(pTitle);
    $('.product-text').text(pContent);
    $('.product-big-img').attr('src', './images/index/product/big'+pIndex+'-1.png');
    var str = '';
    for(var i = 1; i <= pTotal; i++) {
      var active = '';
      if (i == 1) {
        active = 'active';
      }
      str += '<div class="layui-col-md3 product-item '+active+'" data-index="'+pIndex+'-'+i+'">'+
                '<img src="./images/index/product/small'+pIndex+'-'+i+'.png" alt="">'+
              '</div>';
    }
    $('.product-list').html(str);
  })
  // 产品列表点击类别切换
  $('.product-list').on('click', '.product-item', function() {
    var pIndex = $(this).attr('data-index');
    $(this).addClass('active').siblings().removeClass('active');
    $('.product-big-img').attr('src', './images/index/product/big'+pIndex+'.png');
  })
  // 展开设备图片
  $('.shades-item').on('click', function() {
    $(this).siblings().stop(true).animate({width: 95});
    $(this).stop(true).animate({width: 995});
  })
  // 切换设备简介
  $('.equipment-item').hover(function(){
    var index = $(this).attr('data-index');
    $(this).addClass('active').siblings().removeClass('active');
    $('.video-item'+index).siblings().hide();
    $('.video-item'+index).show();
    $('.video-item'+index)[0].currentTime = 0;
    $('.video-item'+index)[0].play();
    $('.equipment-list-item'+index).parent().find('.equipment-list-item').hide();
    $('.equipment-list-item'+index).show();
  },function(){
    var index = $(this).attr('data-index');
    $('.video-item'+index)[0].pause();
  })
  // 切换案例
  $('.case-types-item').hover(function() {
    $(this).addClass('active').parent().siblings().find('.case-types-item').removeClass('active');
    var index = $(this).attr('data-index');
    var arr = $(this).attr('namestr').split(',');
    var str = '';
    $.each(arr, function(i, item) {
      str += '<div class="layui-col-md4 case-item">'+
                '<div class="case-img-wrap">'+
                  '<img src="./images/index/case/case'+index+'-'+(i+1)+'.png" alt="">'+
                '</div>'+
                '<div class="case-item-name">'+
                  '<h5>'+item+'</h5>'+
                '</div>'+
              '</div>';
    })
    $('.case-box').html(str);
  }, function() {})
})

function initBannerSwiper() {
  // 轮播
  ajaxGet({
    url: '/api/Client/banner-appservice',
    params: {},
    noLoading: true,
    success: function(res) {
      var str = '';
      $.each(res, function(index, item) {
        if (item.redirectUrl) {
          str += '<div class="swiper-slide"><a href="'+item.redirectUrl+'" target="_blank"><img src="'+item.imageUrl+'" /></a></div>';
        } else {
          str += '<div class="swiper-slide"><img src="'+item.imageUrl+'" /></div>';
        }
      })
      $('#banner-swiper').html(str);
      var isMore = res.length > 1;
      var mySwiper = $('.banner-swiper-container').swiper({
        loop: isMore,//无缝轮播
        autoplay: isMore ? 5000 : 0,//自动轮播
        calculateHeight: true,//根据内容计算容器高度
        pagination : '.swiper-pagination',//分页器
        paginationClickable: true,//点击分页器切换
      });
    },
    error: function(err) {
      console.log('error:',err);
    }
  })
}

// 新闻
function initNewsSwiper() {
  var newsSwiper = $('.news-swiper-container').swiper({
    loop: true,
    noSwiping: true,//禁止拖拽
  });
  $('#news-prev-btn').click(function(){
    newsSwiper.swipePrev(); 
  })
  $('#news-next-btn').click(function(){
    newsSwiper.swipeNext(); 
  })
}

// 合作伙伴
function initPartSwiper() {
  var newsSwiper = $('.part-swiper-container').swiper({
    loop: true,
    noSwiping: true,//禁止拖拽
    slidesPerView: 6,//显示六个
  });
  $('#part-prev-btn').click(function(){
    newsSwiper.swipePrev(); 
  })
  $('#part-next-btn').click(function(){
    newsSwiper.swipeNext(); 
  })
}