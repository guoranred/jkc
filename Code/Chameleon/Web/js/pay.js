var intervalTimer = null;
var intervalCount = 0;
$(function() {
  var id = getQueryVariable('id');
  var orderNo = getQueryVariable('orderNo');
  var productNum = getQueryVariable('productNum');
  var price = getQueryVariable('price');
  $('.pay-order-price').text(price);
  $('.pay-order-no').text(orderNo);
  $('.pay-order-num').text(productNum);
  getPayCode(id, PAYTYPE.wx);
  // 初始化tab
  var currentTab = PAYTYPE.wx;
  var element = layui.element;
  element.on('tab(docDemoTabBrief)', function(data){
    var type = data.index + 1;
    if (type == currentTab) {
      return false;
    }
    currentTab = type;
    getPayCode(id, type);
  });
  // 刷新支付码
  $('.scancode-fail').on('click', 'span', function() {
    var type = $(this)[0].dataset.type*1;
    getPayCode(id, type);
  })
})

// 获取支付码
function getPayCode(id, type) {
  ajaxPost({
    url: '/api/pays/pay',
    params: {
      OrderIds: [id],
      PayType: type
    },
    success: function(res) {
      if (type == PAYTYPE.wx) {
        $('.qrcode-wx').attr('src', 'data:image/jpeg;base64,'+res.qrCodeBase64);
        $('.qrcode-wx').show();
      } else if (type == PAYTYPE.ali) {
        $('.qrcode-ali').attr('src', 'data:image/jpeg;base64,'+res.qrCodeBase64);
        $('.qrcode-ali').show();
      }
      if (intervalTimer) {
        clearInterval(intervalTimer)
      }
      var payNo = res.payNo;
      intervalTimer = setInterval(function() {
        getPayResult(payNo)
      }, 3000)
    },
    error: function(err) {
      if (intervalTimer) {
        clearInterval(intervalTimer)
      }
      console.log('失败',err);
    }
  })
}

// 获取支付结果
function getPayResult(PayNo) {
  intervalCount++;
  ajaxGet({
    url: '/api/pays/result',
    params: {
      PayNo: PayNo
    },
    noLoading: true,
    success: function(res) {
      if (res.toLowerCase() == 'true') {
        if (intervalTimer) {
          clearInterval(intervalTimer);
        }
        var orderNo = getQueryVariable('orderNo');
        location.replace('../paySuccess.html?orderNo='+orderNo);
      } else {
        if (intervalCount == 10) {
          if (intervalTimer) {
            clearInterval(intervalTimer);
          }
          intervalCount = 0;
          location.replace('../memberCenter/order3d.html');
        }
      }
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}