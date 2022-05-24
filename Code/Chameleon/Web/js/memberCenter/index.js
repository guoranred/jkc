var currentOrderId = '';
$(function() {
  // 数据初始化
  init();
  $('#memberorder-list').on('click', '.memberorder-img', function() {
    var analysisUrl = $(this).attr('analysisurl');
    layer.open({
      type: 2,
      resize: false,
      area:["980px","90%"],
      content: [analysisUrl, 'no'] //这里content是一个URL，如果你不想让iframe出现滚动条，你还可以content: ['http://sentsin.com', 'no']
    }); 
  })

  // 修改订单上传文件
  $('#memberorder-list').on('click', '.edit-order-file', function() {
    currentOrderId = $(this)[0].dataset.id;
    console.log($('#upload-file-order'))
    $('#upload-file-order').click();
    console.log(123)
  })
  
  // 取消订单
  $('#memberorder-list').on('click', '.cancel-order', function() {
    var id = $(this)[0].dataset.id;
    layer.confirm('确认取消订单?', function(index){
      ajaxPut({
        url: '/api/orders/order/cancel/'+id,
        success: function(res) {
          layer.msg('取消成功', {icon: 1,anim: 5,time: 2000});
          getOrderList();
        },
        error: function(err) {
          console.log('失败',err);
        },
        complete: function() {
          layer.close(index);
        }
      })
    });
    // layer.prompt({
    //   formType: 2,
    //   title: '取消原因',
    //   area: ['400px', '200px'] //自定义文本域宽高
    // }, function(value, index, elem){
    //   ajaxPut({
    //     url: '/api/Client/order/status',
    //     params: {
    //       id: id,
    //       cancelReason: value
    //     },
    //     success: function(res) {
    //       layer.msg('取消成功', {icon: 1,anim: 5,time: 2000});
    //       getOrderList();
    //     },
    //     error: function(err) {
    //       console.log('失败',err);
    //     },
    //     complete: function() {
    //       layer.close(index);
    //     }
    //   })
    // });
  })

  // 确认收货
  $('#memberorder-list').on('click', '.order-confirm-btn', function() {
    var id = $(this)[0].dataset.id;
    layer.confirm('确认已收货?', function(index){
      ajaxPut({
        url: '/api/orders/order/complete/'+id,
        success: function(res) {
          layer.msg('操作成功', {icon: 1,anim: 5,time: 2000});
          getOrderList();
        },
        error: function(err) {
          console.log('失败',err);
        },
        complete: function() {
          layer.close(index);
        }
      })
    });
  })
})

function init() {
  getOrderList();
  // 获取订单统计
  getOrderCollect();
  // 初始化上传
  // initUpload();
}
// 获取订单列表
function getOrderList() {
  ajaxGet({
    url: '/api/orders/order/customerorder-3d',
    params: {
      channelId: channelId,
      orderType: 8,
    },
    success: function(res) {
      // 渲染订单列表
      var str = '<tr style="height: 13px;"></tr>';
      if (res.items && res.items.length > 0) {
        $('.member-empty').hide();
      } else {
        $('.member-empty').show();
        str = '';
      }
      $.each(res.items, function (index, item) {
        // 订单头部颜色
        var tipColor = 'green';
        if (item.status == ORDERSTATUS.received || item.status == ORDERSTATUS.cancel) {
          tipColor = 'grey';
        }
        if (item.status == ORDERSTATUS.tobePaid) {
          tipColor = 'orange';
        }
        str += '<tr style="height: 10px;"></tr>'+
        '<tr class="memberorder-title '+tipColor+'">'+
          '<td>'+timeFormat(item.creationTime)+'</td>'+
          '<td colspan="8">订单号: '+item.orderNo+'</td>'+
          // '<td align="center"><a href="javascript:;">打印合同</a></td>'+
        '</tr>';
        var num = 0;
        $.each(item.customer3DOrderExtraBomDtos, function(i,j) {
          num += j.count;
        })
        $.each(item.customer3DOrderExtraBomDtos, function (subIndex, subItem) {
          // 查看物流按钮判断
          var deliveryBtn = '';
          if (item.status == ORDERSTATUS.tobeReceived) {
            deliveryBtn = 'ondelivery';
          }
          var viewDelivery = '';
          if (subIndex == 0) {
            viewDelivery = '<p class="'+deliveryBtn+'">'+item.statusName+'</p>'+
            '<p><a href="../orderDetail.html?orderId='+item.orderId+'" target="_blank">订单详情</a></p>';
            if (item.status == ORDERSTATUS.tobeReceived || item.status == ORDERSTATUS.received) {
              viewDelivery += '<p><a href="javascript:;">查看物流</a></p>';
            }
          }
          // 修改文件
          var changeStr = '';
          if (item.status == ORDERSTATUS.audit || item.status == ORDERSTATUS.auditFail) {
            // changeStr = '<br><a href="javascript:;" class="apply-bill edit-order-file" style="padding:0;" data-id="'+item.orderId+'">修改文件</a>';
          }
          // 总交期，总报价
          var allDelivery = '';
          var allPrice = '';
          // 订单操作栏判断
          var btnStr = '';
          if (subIndex == 0) {
            if (item.status == ORDERSTATUS.tobePaid) {
              btnStr += '<p><a href="../../pay.html?id='+item.orderId+'&orderNo='+item.orderNo+'&productNum='+num+'&price='+item.sellingPrice+'" class="layui-btn layui-btn-sm order-pay">立即付款</a></p>';
            }
            if (item.status != ORDERSTATUS.paid && item.status != ORDERSTATUS.cancel) {
              btnStr += '<p><a href="javascript:;" class="apply-bill cancel-order" data-id="'+item.orderId+'">取消订单</a></p>';
            }
            if (item.status == ORDERSTATUS.paid) {
              // btnStr += '<a href="javascript:;" class="remind-delivery">提醒卖家发货</a>';
            }
            if (item.status == ORDERSTATUS.tobeReceived) {
              // btnStr += '<p class="delivery-time">还剩9天19时</p>'+
              btnStr += '<p><button class="layui-btn layui-btn-sm order-confirm-btn" data-id="'+item.orderId+'">确认收货</button></p>';
              // '<p><a href="javascript:;" class="apply-bill">申请开票</a></p>';
            }
            if (item.status == ORDERSTATUS.received) {
              // btnStr += '<button	class="layui-btn layui-btn-primary layui-btn-sm">晒单</button>';
            }
            allDelivery = '<p>'+item.deliveryDays+'天</p>';
            allPrice = '<p>'+item.sellingPrice+'元</p>';
          }
          var subClass = '';
          if (subIndex != item.customer3DOrderExtraBomDtos.length - 1) {
            subClass = 'class="memberorder-suborder"'
          }   
          str += '<tr '+subClass+'>'+
            '<td>'+
              '<div class="memberorder-img" analysisurl="'+analysisUrl3D+subItem.supplierPreViewId+'">'+
                '<img src="'+subItem.thumbnail+'" alt="">'+
                '<div class="magnifier"><i class="layui-icon layui-icon-search"></i></div>'+
              '</div>'+
            '</td>'+
            '<td>'+
              '<a href="'+subItem.filePath+'" class="part-name">'+subItem.fileName+'</a>'+
              changeStr+
              '<p class="part-size">尺寸：'+subItem.size+' mm</p>'+
              '<p class="part-size">体积：'+subItem.volume+' mm³</p>'+
            '</td>'+
            '<td>'+
              '<p>'+subItem.materialName+'</p>'+
            '</td>'+
            '<td align="center">'+subItem.handleMethod+'</td>'+
            '<td align="center" class="border-right">'+subItem.count+'个</td>'+
            '<td align="center" class="no-border">'+
              allDelivery+
            '</td>'+
            '<td align="center" class="no-border">'+
              allPrice+
            '</td>'+
            '<td align="center" class="no-border">'+
              viewDelivery+
            '</td>'+
            '<td align="center" class="no-border">'+
              btnStr+
            '</td>'+
          '</tr>';
        })

      });
      $('#memberorder-list').html(str);
    },
    error: function(err) {
      console.log('error:',err);
    }
  })
}

// 计价上传
function initUpload() {
  var host = '';
  var key = '';
  var loading = null;
  //实例化一个上传plupload上传对象
  var uploader = new plupload.Uploader({
    runtimes: 'html5,flash,silverlight,html4',
    browse_button : 'upload-file-order', //触发文件选择对话框的按钮，为那个元素id
    url : 'http://oss.aliyuncs.com', //服务器端的上传页面地址
    // 上传文件类型限制
    filters: {
      mime_types: [
        { title : "格式", extensions : "stl,stp,step" },
      ],
      // 文件大小
      max_file_size: '50M',
      // 不允许选取重复文件
      prevent_duplicates: false
    },
    multi_selection: false
  });

  uploader.init();

  uploader.bind('FilesAdded',function(uploader,files){
    ajaxGet({
      url: '/api/Client/aliyun',
      params: {},
      success: function(res) {
        res = JSON.parse(res);
        host = res.host;
        var policyBase64 = res.policy;
        var accessid = res.accessid;
        var signature = res.signature;
        var callbackbody = res.callback;
        key = res.dir;

        var new_multipart_params = {
          'key': key+'${filename}',
          'policy': policyBase64,
          'OSSAccessKeyId': accessid,
          'success_action_status': '200', //让服务端返回200,不然，默认会返回204
          'callback': callbackbody,
          'signature': signature,
        };

        uploader.setOption({
          'url': host,
          'multipart_params': new_multipart_params
        });
        loading = layer.load(2, {
          shade: [0.5, '#fff'],
          content: '上传中',
          success: function (layero) {
            layero.find('.layui-layer-content').css({
              'paddingTop': '40px',
              'width': '60px',
              'textAlign': 'center',
              'backgroundPositionX': 'center'
            });
          }
        });
        uploader.start();
      },
      error: function(err) {
        console.log('失败',err);
      }
    })
  });
  // 发生错误
  uploader.bind("Error", function(up, info) {
    layer.close(loading);
    if (info.code == -600) {
      layer.msg('选择的文件过大，不能超过50M', {icon: 2,anim: 5,time: 2000});
    }
    if (info.code == -601) {
      layer.msg('选择的文件类型不符合要求', {icon: 2,anim: 5,time: 2000});
    }
  })
  // 文件上传成功
  uploader.bind("FileUploaded", function(up, file, info) {
    layer.close(loading);
    ajaxPut({
      url: '/api/Client/order',
      params: {
        id: currentOrderId,
        productFileName: file.name,
        productFilePath: host + key + file.name
      },
      success: function(res) {
        layer.msg('修改成功', {icon: 1,anim: 5,time: 2000});
        getOrderList();
      },
      error: function(err) {
        console.log('失败',err);
      }
    })
  })
}

// 获取订单统计
function getOrderCollect() {
  ajaxGet({
    url: '/api/orders/order/d3ordercount',
    params: {},
    success: function(res) {
      $('.totalOrders').text(res.total);
      $('.waitHandle').text(res.pendingSum);
      $('.inProcess').text(res.processedSum);
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}