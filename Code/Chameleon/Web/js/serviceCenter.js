var serviceSwiper = null;
$(function () {
  initServiceSwiper();
  // tab切换
  layui.element.on("tab(serviceTabBrief)", function (data) {
    if (data.index == 1) {
      serviceSwiper.init();
      serviceSwiper.autoplay.start();
    } else {
      serviceSwiper.autoplay.stop();
    }
  });

  // 点击切换nav
  $('.service-nav-item').on('click', function() {
    $(this).addClass('active').siblings().removeClass('active');
    var index = $(this).attr('data-index');
    $(this).parent().next().find('.nav-content-item').eq(index).addClass('active').siblings().removeClass('active');
  })
});

// 初始化轮播
function initServiceSwiper() {
  serviceSwiper = new Swiper(".OrderSwiper", {
    autoplay: {
      delay: 3000,
      disableOnInteraction: false
    },
    speed: 1000,
    init: false,
    loop: false,
    centeredSlides: true,
    slidesPerView: 2,
    initialSlide: 1,
    pagination: {
      el: ".swiper-pagination",
      clickable :true,
    },
    navigation: {
      prevEl: ".swiper-button-prev",
      nextEl: ".swiper-button-next",
    },
    on: {
      transitionStart: function (swiper) {
        $(".pretend_progress_active").animate(
          {
            width: 128 * (serviceSwiper.activeIndex + 1),
          },
          300
        );
      }
    },
    breakpoints: {
      800: {
        slidesPerView: 1,
      },
    },
  });
}