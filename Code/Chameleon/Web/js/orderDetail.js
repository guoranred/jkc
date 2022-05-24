$(function() {
  // 查看图片
  $('.order-detail-wrap').on('click', '.magnifier-wrap', function() {
    layer.photos({
      photos: {
        data: [{
          src: $(this)[0].dataset.src
        }]
      },
      anim: 0
    }); 
  })
  getOrderDetail();
})

function getOrderDetail() {
  var orderId = getQueryVariable('orderId');
  ajaxGet({
    url: '/api/orders/order/customerorder-3ddetial/'+orderId,
    success: function(res) {
      // 订单号
      $('.order-detail-orderno').text(res.orderNo);
      // 订单状态
      $('.order-statusname').text(res.statusName);
      if (res.checkNoPassReason) {
        $('.order-failreason').show();
        $('.order-failreason-text').text(res.checkNoPassReason);
      }
      // 地址
      var address = (res.deliveryDto && res.deliveryDto[0]) || {};
      $('.receiver-name').text(address.receiverName || '-');
      $('.receiver-tel').text(address.receiverTel || '-');
      $('.receiver-address').text(address.receiverAddress || '-');
      $('.courier-type').text(address.courier || '-');
      // 金额
      var cost = (res.costDto && res.costDto[0]) || {};
      if (JSON.stringify(cost) != '{}') {
        $('.pro-price').text(cost.proMoney.toFixed(2));
        $('.ship-price').text(cost.shipMoney.toFixed(2));
        $('.tax-price').text(cost.taxMoney.toFixed(2));
        $('.discount-price').text(cost.discountMoney.toFixed(2));
        $('.order-total-price').text(res.sellingPrice.toFixed(2));
      }
      // 产品清单
      var productList = res.customer3DOrderExtraBomDtos;
      var str = '';
      $.each(productList, function(index, item) {
        var fileStyle = '';
        if (index == 0 || (item.filePath != productList[index-1].filePath)) {
        } else {
          fileStyle = 'style="visibility: hidden;"';
        }
        var surface = '<p>'+item.handleMethod+'</p>';
        if (item.handleMethod == '喷漆') {
          var surfaceObj = JSON.parse(item.handleMethodDesc);
          $.each([1,1,1,1,1], function(subIndex, subItem) {
            var color = surfaceObj['color'+(subIndex+1)];
            var colordata = surfaceObj['colordata'+(subIndex+1)];
            if (color && colordata) {
              surface += '<p>'+color+'：'+colordata+'</p>'
            }
            if (color && !colordata) {
              surface += '<p>金属色：'+color+'</p>'
            }
          })
        }
        str += '<tr>'+
                  '<td>'+
                    '<div class="fileinfo-wrap" '+fileStyle+'>'+
                      '<div class="magnifier-wrap" data-src="'+item.thumbnail+'">'+
                        '<img src="'+item.thumbnail+'" alt="">'+
                        '<div class="magnifier"><i class="layui-icon layui-icon-search"></i></div>'+
                      '</div>'+
                      '<div>'+
                        '<a href="'+item.filePath+'" target="_blank" class="filename">'+item.fileName+'</a>'+
                        '<p class="fileinfo">尺寸：'+item.size+' mm</p>'+
                        '<p class="fileinfo">体积：'+item.volume+' mm³</p>'+
                      '</div>'+
                    '</div>'+
                  '</td>'+
                  '<td>'+
                    '<p>材料'+(index+1)+'：'+item.materialName+'</p>'+
                    '<p>颜色：'+item.color+'</p>'+
                  '</td>'+
                  '<td>'+surface+'</td>'+
                  '<td align="center">'+item.deliveryDays+'天</td>'+
                  '<td align="center" class="order-price">'+item.price+'</td>'+
                  '<td align="center">'+item.count+'</td>'+
                  '<td align="center" class="order-price total-price">'+item.orginalMoney+'</td>'+
                '</tr>';
      })
      $('#order-product-list').html(str);
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}