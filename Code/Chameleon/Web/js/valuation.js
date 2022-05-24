$(function() {
  init();
})

function init() {
  plate3d.createCode();
  // 校验数字框
  $('.filelist-3d').on('keyup', '.snum-input', function(e){
    var value = $(e.currentTarget).val();
    if(value.length==1){
      value = value.replace(/[^0-9]/g,'');
    } else {
      value = value.replace(/\D/g,'');
    }
    $(e.currentTarget).val(value);
  })
  // 初始化自动计价表单
  plate3d.getMetalAutoForm();
  // 初始化上传
  plate3d.initUpload();
  // 切换计价类型
  $('.valuation-type-item').on('click', function() {
    var index = $(this)[0].dataset.index;
    $(this).addClass('active').siblings().removeClass('active');
    // $('.valuation-'+index).show().siblings().hide();
  })
  
  // 表单校验
  layui.form.verify({
    // 钣金人工报价
    productFilePath: function(value, item){
      if (!value) {
        return '请上传文件';
      }
    },
    orderName: function(value, item){
      if (!value.trim()) {
        return '请输入产品名称';
      }
    },
    productNum: function(value, item){
      if (!value.trim()) {
        return '请输入产品数量';
      }
      if (!/^[0-9]*$/.test(value)) {
        return '产品数量为整数'
      }
    },
    materialName: function(value, item){
      if (!value.trim()) {
        return '请选择材料名称';
      }
    },
    surfaceProcess: function(value, item){
      if (!value.trim() && !layui.form.val('metalPeopleForm').surfaceOther.trim()) {
        return '请选择或填写后处理';
      }
    },
    surfaceOther: function(value, item){
      if (!value.trim() && !layui.form.val('metalPeopleForm').surfaceProcess) {
        return '请选择或填写后处理';
      }
    },
    // 地址
    province: function(value, item){
      if (value == '-') {
        return '请选择省';
      }
    },
    city: function(value, item){
      if (value == '-') {
        return '请选择市';
      }
    },
    area: function(value, item){
      if (value == '-') {
        return '请选择区';
      }
    },
    detailAddress: function(value, item){
      if (!value.trim()) {
        return '请输入详细地址';
      }
    },
    receiveName: function(value, item){
      if (!value.trim()) {
        return '请输入收货人姓名';
      }
    },
    customPhone: function(value, item){
      if (!value.trim()) {
        return '请输入手机号码';
      }
      if (value && !/^1\d{10}$/.test(value)) {
        return '手机号格式错误';
      }
    },
  })
  // 颜色材料后处理属性选择
  $('.filelist-3d').on('click', '.form-check-item', function(e) {
    e.stopPropagation();
    $(this).addClass('active').siblings().removeClass('active');
    var type = $(this).attr('type');
    if (type == 'color') {
      var text = $(this).text();
      var materialStr = '';
      var arr = plate3d.dataArr.filter(function(item) {
        return item.key == text;
      })
      $.each(arr[0].materialList, function(index, item) {
        if (index == 0) {
          materialStr += '<div class="form-check-item active" materialId="'+item.materialId+'" type="material">'+item.name+'</div>';
        } else {
          materialStr += '<div class="form-check-item" materialId="'+item.materialId+'" type="material">'+item.name+'</div>';
        }
      })
      $(this).parents('.file-form').find('.material-wrap').html(materialStr);
      $(this).parent().prev().val(text);
      $(this).parents('.layui-form-item').next().find('input').val(arr[0].materialList[0].materialId);
      $(this).parents('.file-form').find('.material-advantage').text(arr[0].materialList[0].excellence);
      $(this).parents('.file-form').find('.material-single-price').text(arr[0].materialList[0].price);
    }
    if (type == 'material') {
      var materialId = $(this).attr('materialid');
      $(this).parents('.file-form').find('.material-advantage').text(plate3d.dataArrMap[materialId].excellence);
      $(this).parents('.file-form').find('.material-single-price').text(plate3d.dataArrMap[materialId].price);
      $(this).parent().prev().val(materialId);
    }
    if (type == 'surface') {
      if ($(this).text() != '喷漆') {
        $(this).parent().prev().val($(this).text());
      }
    }
  })

  // 查看3d解析弹窗
  $('.filelist-3d').on('click', '.file-view', function(e) {
    e.stopPropagation();
    var analysisUrl = $(this).attr('analysisurl');
    layer.open({
      type: 2,
      resize: false,
      area:["980px","90%"],
      content: [analysisUrl, 'no'] //这里content是一个URL，如果你不想让iframe出现滚动条，你还可以content: ['http://sentsin.com', 'no']
    }); 
  })
  
  layui.form.val('painting-form', {
    color1: '',
    color2: '',
    color3: '',
    color4: '',
    color5: '',
  })
  
  painting.init();


  // 选择地址
  $('.address-list').on('click', '.address-list-item', function() {
    $(this).addClass('active').siblings().removeClass('active');
  })
  
  // 增加收货地址
  $('.add-address-btn').on('click', function() {
    $('.add-address-dialog').show();
    // 获取省份
    getProvince();
  })
  
  var form = layui.form;
  //创建保存地址
  form.on('submit(personalAddressForm)', function(data){
    var params = data.field;
    
    var province = params.province.split('-');
    var city = params.city.split('-');
    var area = params.area.split('-');
    var isDefault = false;
    if (params.isDefault) {
      isDefault = true;
    }
    var obj = {
      recipient: params.receiveName,
      phoneNumber: params.phone,
      provinceCode: province[0],
      provinceName: province[1],
      cityCode: city[0],
      cityName: city[1],
      countyCode: area[0],
      countyName: area[1],
      detailAddress: params.detailAddress,
      isDefault: isDefault
    }
    ajaxPost({
      url: '/api/addresses',
      params: obj,
      success: function(res) {
        layer.msg('新增成功', {icon: 1,anim: 5,time: 2000});
        $('.add-address-dialog').hide();
        getAddressList();
        refreshForm();
      },
      error: function(err) {
        console.log('失败',err);
      }
    })
    return false;
  });
  // 监听省份切换
  form.on('select(province)', function(data) {
    var code = data.value.split('-')[0];
    var id = '';
    $.each(plate3d.addressProvinceList, function(index, item) {
      if (code == item.code) {
        id = item.id;
      }
    })
    getCity(id);
  })
  // 监听市切换
  form.on('select(city)', function(data) {
    var code = data.value.split('-')[0];
    var id = '';
    $.each(plate3d.addressCityList, function(index, item) {
      if (code == item.code) {
        id = item.id;
      }
    })
    getArea(id);
  })
}

// 喷漆相关
var painting = {
  numMap: {
    1: '一',
    2: '二',
    3: '三',
    4: '四',
    5: '五',
  },
  currentPainting: null,
  init: function () {
    var that = this;
    // 喷漆radio监听
    layui.form.on('radio(painting)', function(data){
      var colorArr = ['金色', '银色', '黑色', '白色', '灰色']
      if (colorArr.indexOf(data.value) > -1) {
        $('input[name=color1]').prop('checked', false);
        layui.form.val('painting-form', {
          colordata1: ''
        })
        layui.form.render();
        var length = $('.painting-form .layui-form-item').length;
        for(var i = length - 1;i > 1;i--) {
          that.remove(i);
        }
        $('.PTColor').css({ 'background': 'transparent' });
      } else {
        $('input[name=metalColor]').prop('checked', false);
        layui.form.render();
      }
    });
    // 监听输入框粘贴
    $('.painting-form').on('paste', '.color', function () {
      var that = this
      setTimeout(function () {
        var text = $(that).val()
        var num = text.indexOf(",");
        if (text.indexOf("rgb") > 0) {
            num = num + 1
            var bg = text.substring(num)
            var inputVal = text.substring(0, num - 1)
            $(that).parent("div").find(".PTColor").css({ "background": bg });
            $(that).val("PANTONE " + inputVal)
        }
        return false
      }, 0)
    })
  },
  add: function () {
    var that = this;
    if (layui.form.val('painting-form').metalColor) {
      layer.msg('已选择常用色不可选其他色，请更换后再进行添加', {icon: 2, time:2000});
      return;
    }
    var length = $('.painting-form .layui-form-item').length;
    if (length == 5) {
      $('.add-more-painting').hide();
    }
    $('.can-add-num').text(5-length);
    var str = '<div class="layui-form-item choose-more">'+
                '<label class="layui-form-label tl color-title">第'+that.numMap[length]+'种颜色</label>'+
                '<div class="layui-input-block">'+
                  '<div class="layui-inline" style="margin-right: 0;">'+
                    '<input type="radio" class="painting-radio" name="color'+length+'" value="哑光" title="哑光" lay-filter="painting">'+
                    '<input type="radio" class="painting-radio" name="color'+length+'" value="高光" title="高光" lay-filter="painting" checked>'+
                  '</div>'+
                    '<div class="layui-inline choose-color-input">'+
                    '<label class="layui-form-label">潘通色值</label>'+
                    '<div class="layui-input-inline">'+
                      '<div class="PTColor"></div>'+
                      '<input type="text" class="layui-input color" autocomplete="off" name="colordata'+length+'">'+
                      '<span class="layui-btn color-search-btn" onclick="painting.openColor('+length+')">查询</span>'+
                      '<img class="delete-img" src="./images/icon-close2.png" alt="" onclick="painting.delete('+length+')">'+
                    '</div>'+
                  '</div>'+
                '</div>'+
              '</div>';
    $('.painting-form').append(str);
    layui.form.render();
  },
  openColor: function(index) {
    //哑光https://www.jiepei.com/ThreeDPrint/ThreeDColor1
    //高光https://www.jiepei.com/ThreeDPrint/ThreeDColor2
    if (layui.form.val('painting-form')['color'+index] == '哑光') {
      window.open('https://www.jiepei.com/ThreeDPrint/ThreeDColor1');
    } else {
      window.open('https://www.jiepei.com/ThreeDPrint/ThreeDColor2');
    }
  },
  delete: function (index) {
    var that = this;
    that.remove(index);
    that.refresh();
  },
  remove: function (index) {
    var that = this;
    var length = $('.painting-form .layui-form-item').length;
    if (length == 6) {
      $('.add-more-painting').show();
    }
    $('.painting-form .layui-form-item').eq(index).remove();
    that.refreshNum();
  },
  refreshNum: function() {
    var length = $('.painting-form .layui-form-item').length;
    $('.can-add-num').text(6-length);
  },
  refresh: function () {
    var that = this;
    $.each($('.painting-form .layui-form-item'), function (index, item) {
      if (index > 1) {
        $(item).find('.color-title').text('第'+that.numMap[index]+'种颜色');
        $(item).find('.painting-radio').attr('name','color'+index+'');
        $(item).find('.color').attr('name','colordata'+index+'');
        $(item).find('.delete-img').attr('onclick','painting.delete('+index+')');
        $(item).find('.color-search-btn').attr('onclick','painting.openColor('+index+')');
      }
    })
    layui.form.render();
  },
  // 显示喷漆弹窗
  showPainting: function(e) {
    var that = this;
    that.currentPainting = $(e);
    $('.choose-painting-container').show();
  },
  // 关闭喷漆弹窗
  closeChoosePainting: function() {
    var that = this;
    // 初始化
    var length = $('.painting-form .layui-form-item').length;
    for(var i = length - 1;i > 1;i--) {
      that.remove(i);
    }
    $('input[name=metalColor]').prop('checked', false);
    layui.form.val('painting-form', {
      color1: '高光',
      colordata1: ''
    })
    layui.form.render();
    $('.PTColor').css({ 'background': 'transparent' });
    if (!/^喷漆--/.test(that.currentPainting.parent().prev().val())) {
      that.currentPainting.removeClass('active');
      that.currentPainting.prev().addClass('active');
      that.currentPainting.parent().prev().val('原色');
    }
    $('.choose-painting-container').hide();
  },
  confirmPainting: function() {
    var that = this;
    var formData = layui.form.val('painting-form');
    if (formData.color1 && !formData.colordata1
      || formData.color2 && !formData.colordata2
      || formData.color3 && !formData.colordata3
      || formData.color4 && !formData.colordata4
      || formData.color5 && !formData.colordata5) {
        layer.msg('请填写潘通色值或选择常用色', {icon: 2, time:2000});
        return;
      }
    var str = JSON.stringify(layui.form.val('painting-form'));
    that.currentPainting.parent().prev().val('喷漆--'+str);
    that.closeChoosePainting();
  }
}

// 3d计价相关
var plate3d = {
  // 地址省市
  addressProvinceList: [],
  addressCityList: [],
  // 地址列表
  addressList: [],
  dataArr: [],//表单选项数据
  dataArrMap: {},//数据键值对
  formMap: {},//动态表单的键值对
  timer: null,
  loading: null,
  unionfabCode: '',
  fileList: [
    // {
    //   picture: 'https://test-jpfile1.oss-cn-shenzhen.aliyuncs.com/upload/threed/2021/8/3/20210803320928808110.PNG',
    //   analysisUrl: '',
    //   filepath: 'https://test-jpfile1.oss-cn-shenzhen.aliyuncs.com/upload/threed/2021/8/3/2021080309322423925318347v83ucp9.stl',
    //   filename: '2021080309322423925318347v83ucp9.stl',
    //   md5: 'NGYzM2JhNjc5ZWRlMTdkYjcyNTE3ODc0ZGYyOWE2MjQ=',
    //   size: '',
    //   volume: '',
    //   unionfabCode: '',
    //   unionfabFileId: '',
    //   materialList: []
    // }
  ],//文件列表
  // 获取自动计价表单数据
  getMetalAutoForm: function() {
    var that = this;
    ajaxGet({
      url: '/api/materials/channelmateriallist',
      params: {
        channelId: channelId,
        orderType: 8,//3d
      },
      noLoading: true,
      success: function(res) {
        var arr = [];
        $.each(res, function(i,item) {
          that.dataArrMap[item.materialId] = item;
          if (arr.length > 0) {
            var num = 0;
            $.each(arr, function(j, aItem) {
              if (aItem.key == item.color) {
                arr[j].materialList.push(item);
                num++;
              }
            })
            if (num == 0) {
              arr.push({
                key: item.color,
                materialList: [item]
              })
            }
          } else {
            arr.push({
              key: item.color,
              materialList: [item]
            })
          }
        })
        that.dataArr = arr;
      },
      error: function(err) {
        console.log('失败',err);
      }
    })
  },
  // 添加文件到列表
  addFileBox: function() {
    var that = this;
    var index = $('.file-item').length;
    var str = '<div class="file-item active">'+
                '<div class="file-item-head clearfix">'+
                  '<div class="file-info fl">'+
                    '<div class="file-view fl" analysisurl="'+that.fileList[index].analysisUrl+'">'+
                      '<img src="'+that.fileList[index].picture+'" alt="">'+
                      '<img src="./images/valuation/file-review.png" class="file-view-icon" alt="">'+
                    '</div>'+
                    '<div class="file-info-wrap fl">'+
                      '<div class="file-name-wrap"><span class="file-name ellipse" title="'+that.fileList[index].filename+'">'+that.fileList[index].filename+'</span><span class="file-delete" onclick="plate3d.deleteFile('+index+',event)"><i class="layui-icon layui-icon-delete layui-font-20"></i><span>删除</span></span></div>'+
                      '<p>尺寸：<span class="file-size">'+that.fileList[index].size+'</span> mm</p>'+
                      '<p>体积：<span class="file-volume">'+that.fileList[index].volume+'</span> mm³</p>'+
                    '</div>'+
                  '</div>'+
                  '<div class="file-material-list fl">'+
                  '</div>'+
                '</div>'+
                '<div class="template-wrap">'+
                  '<div class="file-form-wrap">'+
                  '</div>'+
                  '<div class="add-material-wrap">'+
                    '<div class="add-material-btn" onclick="plate3d.addMaterial(this)">添加打印材料</div>'+
                  '</div>'+
                  '<div class="save-material">'+
                    '<span class="layui-btn" onclick="plate3d.handleSaveMaterial(\'fileform'+index+'\')">保存参数</span>'+
                  '</div>'+
                '</div>'+
              '</div>';
    $('.filelist-3d').append(str);
    $('.filelist-wrap-3d').show();
    that.paramRender(index);
  },
  // 渲染计价表单
  paramRender: function (currentIndex) {
    var that = this;
    var colorStr = '';
    $.each(that.dataArr, function(index, item) {
      if (index == 0) {
        colorStr += '<div class="form-check-item active" type="color">'+item.key+'</div>';
      } else {
        colorStr += '<div class="form-check-item" type="color">'+item.key+'</div>';
      }
    })
    var materialStr = '';
    $.each(that.dataArr[0].materialList, function(index, item) {
      if (index == 0) {
        materialStr += '<div class="form-check-item active" materialId="'+item.materialId+'" type="material">'+item.name+'</div>';
      } else {
        materialStr += '<div class="form-check-item" materialId="'+item.materialId+'" type="material">'+item.name+'</div>';
      }
    })
    var excellenceStr = that.dataArr[0].materialList[0].excellence;
    var index = $('.file-item').eq(currentIndex).find('.file-form').length;
    var str = '<div class="file-form layui-form" lay-filter="fileform'+currentIndex+'-'+(index+1)+'">'+
                '<div class="layui-form-item">'+
                  '<label class="layui-form-label">'+
                    '<span>单价：</span>'+
                  '</label>'+
                  '<div class="layui-input-block" style="width: 200px;">'+
                    '<p class="material-single-price">'+that.dataArr[0].materialList[0].price+'</p>'+
                  '</div>'+
                '</div>'+
                '<div class="layui-form-item">'+
                  '<label class="layui-form-label">'+
                    '<span>数量：</span>'+
                  '</label>'+
                  '<div class="layui-input-block" style="width: 200px;">'+
                    '<input type="text" maxlength="6" name="num" lay-verify="required" placeholder="请输入数量" autocomplete="off" class="layui-input snum-input">'+
                  '</div>'+
                '</div>'+
                '<div class="layui-form-item">'+
                  '<label class="layui-form-label">'+
                    '<span>选择颜色：</span>'+
                  '</label>'+
                  '<div class="layui-input-block">'+
                    '<input type="text" name="color" value="'+that.dataArr[0].key+'" hidden>'+
                    '<div class="clearfix">'+
                    colorStr+
                    '</div>'+
                  '</div>'+
                '</div>'+
                '<div class="layui-form-item">'+
                  '<label class="layui-form-label">'+
                    '<span>选择材料：</span>'+
                  '</label>'+
                  '<div class="layui-input-block">'+
                    '<input type="text" name="material" value="'+that.dataArr[0].materialList[0].materialId+'" hidden>'+
                    '<div class="clearfix material-wrap">'+
                    materialStr+
                    '</div>'+
                  '</div>'+
                '</div>'+
                '<div class="layui-form-item">'+
                  '<label class="layui-form-label">'+
                    '<span>材料优点：</span>'+
                  '</label>'+
                  '<div class="layui-input-block">'+
                    '<p class="material-advantage">'+excellenceStr+'</p>'+
                  '</div>'+
                '</div>'+
                '<div class="layui-form-item">'+
                  '<label class="layui-form-label">'+
                    '<span>后处理：</span>'+
                  '</label>'+
                  '<div class="layui-input-block">'+
                    '<input type="text" name="surface" value="原色" hidden>'+
                    '<div class="clearfix">'+
                      '<div class="form-check-item active" type="surface">原色</div>'+
                      '<div class="form-check-item" type="surface" onclick="painting.showPainting(this)">喷漆</div>'+
                      '<div class="form-check-item" type="surface">电镀</div>'+
                    '</div>'+
                  '</div>'+
                '</div>'+
              '</div>';
    $('.file-item').eq(currentIndex).find('.file-form-wrap').append(str);
  },
  // 表格渲染
  tableRender: function (currentIndex) {
    var that = this;
    var list = that.fileList[currentIndex].materialList;
    var str = '';
    $.each(list, function(index, item) {
      var current = that.dataArrMap[item.material];
      var materialName = current.name;
      var price = current.price;
      var deleteBtn = '';
      if (index > 0) {
        deleteBtn = '<span id="dropdown'+currentIndex+'-'+(index+1)+'" class="dropdown-class">'+
                      '<i class="layui-icon layui-icon-down layui-font-12"></i>'+
                    '</span>';
      }
      str +=  '<div class="file-material-item">'+
                '<h5 class="file-material-title">材料'+(index+1)+'</h5>'+
                '<div class="file-material-item-wrap clearfix">'+
                  '<div class="file-material-table fl">'+
                    '<table>'+
                      '<colgroup>'+
                        '<col style="width: 80px;">'+
                        '<col style="width: 135px;">'+
                        '<col style="width: 80px;">'+
                        '<col style="width: 135px;">'+
                      '</colgroup>'+
                      '<tbody>'+
                        '<tr><td>颜色</td><td>'+item.color+'</td><td>材料</td><td>'+materialName+'</td></tr>'+
                        '<tr><td>后处理</td><td>'+item.surface+'</td><td>单价</td><td>'+item.singlePrice+'</td></tr>'+
                      '</tbody>'+
                    '</table>'+
                  '</div>'+
                  '<div class="file-material-num fl tc">'+
                  '<h5>数量</h5>'+
                  '<p class="file-num">'+item.num+'</p>'+
                  '</div>'+
                  '<div class="file-material-date fl tc">'+
                  '<h5>交期</h5>'+
                  '<p><span class="file-delivery"></span>天</p>'+
                  '</div>'+
                  '<div class="file-material-price fl tc">'+
                    '<h5>总价</h5>'+
                    '<p>￥<span class="file-discount-price">'+price+'</span></p>'+
                    // '<p class="file-single-price-wrap">￥<span class="file-single-price">185.2</span></p>'+
                  '</div>'+
                  '<div class="file-material-action fl">'+
                    '<span class="file-edit-btn" onclick="plate3d.editMaterial('+currentIndex+', '+index+')"><img src="./images/valuation/icon-edit.png" alt="">编辑</span>'+
                    deleteBtn+
                  '</div>'+
                '</div>'+
              '</div>';
    })
    $('.file-item').eq(currentIndex).find('.file-material-list').html(str);
    // 初始化下拉菜单
    $.each(list, function(i, item) {
      layui.dropdown.render({
        elem: '#dropdown'+currentIndex+'-'+(i+1),
        trigger: 'hover',
        data: [{
          title: '删除',
          id: 'delete'
        }],
        click: function(data, othis){
          if (data.id == 'delete') {
            layer.confirm('确认删除该材料?', {icon: 0, title:'提示'}, function(index){
              that.deleteMaterial(currentIndex, i);
              layer.close(index);
            });
          }
        }
      });
    })
  },
  // 添加材料
  addMaterial: function(e) {
    var index = $(e).parent().prev().find('.file-form').attr('lay-filter').split('-')[0].replace('fileform', '');
    plate3d.paramRender(index);
  },
  // 编辑材料
  editMaterial: function(index, subIndex) {
    if ($('.template-wrap:visible').length > 0) {
      layer.msg('您有未保存的参数', {icon: 2, time:2000});
      return;
    }
    $('.file-item').eq(index).addClass('active');
    $('.file-item').eq(index).find('.template-wrap').show();
    $('.file-item').eq(index).find('.file-form').eq(subIndex).show().siblings().hide();
    $('.file-item').eq(index).find('.file-form').eq(subIndex).css('border-bottom', 'none');
  },
  // 删除材料
  deleteMaterial: function(index, subIndex) {
    var that = this;
    $('.file-item').eq(index).find('.file-material-item').eq(subIndex).remove();
    $('.file-item').eq(index).find('.file-form').eq(subIndex).remove();
    that.fileList[index].materialList.splice(subIndex, 1);
    // 删除重新计价
    that.calcPrice();
    that.updateMaterialNo(index);
  },
  // 删除文件
  deleteFile: function(i, e) {
    e.stopPropagation();
    var that = this;
    layer.confirm('确认删除该文件?', function(index){
      $('.file-item').eq(i).remove();
      that.fileList.splice(i, 1);
      if (that.fileList.length == 0) {
        $('.filelist-wrap-3d').hide();
      } else {
        that.updateFileNo(i);
      }
      layer.close(index);
    }); 
  },
  // 更新文件序号
  updateFileNo: function(i) {
    var that = this;
    $.each(that.fileList, function(index, item) {
      $('.file-item').eq(index).find('.file-delete').attr('onclick', 'plate3d.deleteFile('+index+',event)');
      $('.file-item').eq(index).find('.save-material').children().attr('onclick', 'plate3d.handleSaveMaterial(\'fileform'+index+'\')');
      $.each(item.materialList, function (subIndex, subItem) {
        $('.file-item').eq(index).find('.file-form').eq(subIndex).attr('lay-filter', 'fileform'+index+'-'+(subIndex+1));
      })
      
      that.tableRender(index);
    })
    that.calcPrice();
    // 底部文件数，材料数
    that.renderBottomNum();
  },
  // 更新材料序号
  updateMaterialNo: function(i) {
    var that = this;
    $.each(that.fileList[i].materialList, function(index, item) {
      $('.file-item').eq(i).find('.file-form').eq(index).attr('lay-filter', 'fileform'+i+'-'+(index+1));
      $('.file-item').eq(i).find('.save-material').children().attr('onclick', 'plate3d.handleSaveMaterial(\'fileform'+i+'\')');
    })
    that.tableRender(i);
    // 底部文件数，材料数
    that.renderBottomNum();
  },
  // 保存材料
  handleSaveMaterial: function(formName) {
    var index = formName.replace('fileform', '');
    var list = $('.file-item').eq(index).find('.file-form');
    var num = 0;
    var arr = [];
    $.each(list, function(i, item) {
      var surfaceJson = '';
      var formData = layui.form.val(formName+'-'+(i+1));
      if (formData.num > 0) {
        if (/^喷漆/.test(formData.surface)) {
          surfaceJson = formData.surface.split('--')[1];
          formData.surface = formData.surface.split('--')[0]
        }
        arr.push({
          color: formData.color,
          material: formData.material,
          num: formData.num,
          surface: formData.surface,
          surfaceJson: surfaceJson,
          singlePrice: $('.file-item').eq(index).find('.material-single-price').eq(i).text()
        });
      } else {
        num++;
      }
    })
    var that = this;
    if (num > 0) {
      layer.msg('请输入材料数量', {icon: 2,anim: 5,time: 2000});
      return;
    }
    this.fileList[index].materialList = arr;
    this.tableRender(index);
    $('.file-item').eq(index).find('.template-wrap').hide();
    $('.file-item').eq(index).find('.file-material-list').show();
    $('.file-item').eq(index).removeClass('active');
    // 底部文件数，材料数
    that.renderBottomNum();
    // 触发计价
    that.calcPrice();
  },
  // 渲染底部文件，材料个数
  renderBottomNum: function() {
    var that = this;
    var materialNum = 0;
    $.each(that.fileList, function(index, item) {
      materialNum += item.materialList.length;
    })
    $('.file-totalnum').text(that.fileList.length);
    $('.material-totalnum').text(materialNum);
  },
  // 计价
  calcPrice: function() {
    var that = this;
    var params = [];
    $.each(that.fileList, function(index, item) {
      $.each(item.materialList,function(i,mItem) {
        var surfaceJson = '';
        if (mItem.surface == '喷漆') {
          var json = JSON.parse(mItem.surfaceJson);
          if (json.metalColor) {
            surfaceJson = JSON.stringify({
              color1: json.metalColor,
              colordata1: ''
            })
          } else {
            delete json.metalColor;
            surfaceJson = JSON.stringify(json);
          }
        }
        mItem.surfaceJson = surfaceJson;
        params.push({
          channelId: channelId,
          materialId: mItem.material,
          num: mItem.num,
          handleMethod: mItem.surface,//后处理
          handleMethodDesc: surfaceJson,
          volume: item.volume
        })
      })
    })
    ajaxPost({
      url: '/api/materials/calculation-3dmaterial',
      params: params,
      success: function(res) {
        var totalPrice = 0;
        $.each(res, function(index, item) {
          $('.file-material-item-wrap').eq(index).find('.file-discount-price').text(item.price);
          $('.file-material-item-wrap').eq(index).find('.file-delivery').text(item.delivery);
          totalPrice += item.price;
        })
        $('.total-discount-price').text(Math.round(totalPrice*100)/100);
      },
      error: function(err) {
        console.log('失败',err);
      }
    })
  },
  // 显示选择地址
  showAddress: function() {
    var that = this;
    if (that.fileList.length == 0) {
      layer.msg('请上传文件', {icon: 2, time:2000});
      return;
    }
    if ($('.template-wrap:visible').length > 0) {
      layer.msg('您有未保存的参数', {icon: 2, time:2000});
      return;
    }
    var token = localStorage.getItem('token');
    if (!token) {
      showLogin();
      return;
    }
    $('.choose-address-container').show();
    // 获取地址列表
    getAddressList(function () {
      $('.address-list-item').hide();
      $('.address-list-item').eq(0).show();
    });
  },
  // 展开地址
  openAddressList: function() {
    $('.address-list-item').show();
    $('.open-address-list').hide();
  },
  // 创建订单
  createOrder: function() {
    var that = this;
    var addressIndex = $('.address-list .active').attr('data-index');
    var addressData = that.addressList[addressIndex];
    var boms = [];
    $.each(that.fileList, function(index, item) {
      $.each(item.materialList, function(i, mItem) {
        var deliveryDays = $('.file-item').eq(index).find('.file-delivery').eq(i).text() || 0;
        boms.push({
          fileName: item.filename,
          filePath: item.filepath,
          thumbnail: item.picture,
          color: mItem.color,
          materialId: mItem.material, 
          materialName: that.dataArrMap[mItem.material].name,
          count: mItem.num,
          volume: item.volume,
          size: item.size,
          supportVolume: 0,
          handleMethod: mItem.surface,
          handleMethodDesc: mItem.surfaceJson,
          deliveryDays: Number(deliveryDays),
          supplierFileId: item.unionfabFileId,
          supplierPreViewId: 'modelFileId='+item.unionfabFileId+'&code='+item.unionfabCode,
          fileMD5: item.md5,
          supplierOrderCode: item.unionfabCode
        })
      })
    })
    var params = {
      orderType: 8,
      channelId: channelId,
      deliveryInfo: {
        weight: 0,
        receiverName: addressData.recipient,
        receiverCompany: '',
        provinceCode: addressData.provinceCode,
        provinceName: addressData.provinceName,
        cityCode: addressData.cityCode,
        cityName: addressData.cityName,
        countyCode: addressData.countyCode,
        countyName: addressData.countyName,
        receiverAddress: addressData.provinceName+' '+addressData.cityName+' '+addressData.countyName+' '+addressData.detailAddress,
        receiverTel: addressData.phoneNumber,
        orderContactName: '',
        orderContactMobile: '',
        orderContactQQ: ''
      },
      extraproperties: {
        subOrderThreeDItemDtos: boms
      }
    }
    ajaxPost({
      url: '/api/orders/order',
      params: params,
      success: function(res) {
        $('.choose-address-container').hide();
        $('.order-success-dialog').show();
      },
      error: function(res) {
        console.log('失败',res);
      }
    })
  },
  // 计价上传
  initUpload: function() {
    var that = this;
    var host = '';
    var key = '';
    var loading = null;
    //实例化一个上传plupload上传对象
    var uploader = new plupload.Uploader({
      runtimes: 'html5,flash,silverlight,html4',
      browse_button : 'upload-3d', //触发文件选择对话框的按钮，为那个元素id
      drop_element: ['upload-3d'],//拖拽上传范围
      url : 'http://oss.aliyuncs.com', //服务器端的上传页面地址
      // 上传文件类型限制
      filters: {
        mime_types: [
          { title : "格式", extensions : "stl,stp,step" },
        ],
        // 文件大小
        max_file_size: '64m',
        // 允许选取重复文件
        prevent_duplicates: false
      },
      multi_selection: true
    });    

    uploader.init();

    uploader.bind('FilesAdded',function(uploader,files){

      if (that.fileList.length + files.length > 10 ) {
        uploader.splice(0,files.length)
        layer.msg('文件总数量不能超过10个', {icon: 2, time:2000});
        return;
      }
      if ($('.template-wrap:visible').length > 0) {
        uploader.splice(0,files.length)
        layer.msg('您有未保存的参数', {icon: 2, time:2000});
        return;
      }
      
      // $('.no-upload').hide();
      // $('.in-upload').show();
      // $('.file-upload-progress').hide();
      // $('.file-upload-analysis').show();
      // that.getMD5(files[0].getNative());
      // return;
      ajaxGet({
        url: '/api/aliyun-oss/policy-token',
        params: {},
        success: function(res) {
          $('.file-name-3d').text(files[0].name);
          host = res.host;
          var policyBase64 = res.policy;
          var accessid = res.accessId;
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
          $('.no-upload').hide();
          $('.in-upload').show();
          $('.file-upload-progress').show();
          $('.file-upload-analysis').hide();
          loading = layer.load(2, {
            shade: [0.5, '#fff']
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
      if (info.code == -600) {
        layer.msg('选择的文件过大，不能超过64M', {icon: 2,anim: 5,time: 2000});
      }
      if (info.code == -601) {
        layer.msg('选择的文件类型不符合要求', {icon: 2,anim: 5,time: 2000});
      }
    })
    // 文件上传成功
    uploader.bind("FileUploaded", function(uploader, file) {
      $('.file-upload-progress').hide();
      $('.file-upload-analysis').show();
      var filepath = host + key + file.name;
      var filename = file.name;
      
      // 获取md5
      that.getMD5(file.getNative(), filepath, filename);
    })
    // 所有文件上传结束
    uploader.bind('UploadComplete',function(uploader,files){
      layer.close(loading);
    });
    // 文件上传进度
    uploader.bind('UploadProgress',function(uploader,file){
      $('.upload-progress-num').text(file.percent);
    });
  },
  // 获取文件md5
  getMD5: function(file, filepath, filename) {
    
    var that = this;
    var fileReader = new FileReader();
    //文件分割方法（注意兼容性）
    blobSlice = File.prototype.mozSlice || File.prototype.webkitSlice || File.prototype.slice;

    //文件每块分割2M，计算分割详情
    chunkSize = 2097152,
    chunks = Math.ceil(file.size / chunkSize),
    currentChunk = 0,

    //创建md5对象（基于SparkMD5）
    spark = new SparkMD5();

    //每块文件读取完毕之后的处理
    fileReader.onload = function(e) {
      //每块交由sparkMD5进行计算
      spark.appendBinary(e.target.result);
      currentChunk++;

      //如果文件处理完成计算MD5，如果还有分片继续处理
      if (currentChunk < chunks) {
        loadNext();
      } else {
        // 触发文件解析
        var md5 = spark.end();
        console.info("计算的Hash", spark.end());
        that.createFile(filepath, filename, md5);
      }
    };

    //处理单片文件的上传
    function loadNext() {
      var start = currentChunk * chunkSize, end = start + chunkSize >= file.size ? file.size : start + chunkSize;

      fileReader.readAsBinaryString(blobSlice.call(file, start, end));
    }

    loadNext();
  },
  // 优联-生成订单号
  createCode: function() {
    var that = this;
    ajaxPut({
      url: '/api/suppliers/unionfab/order/create-code',
      noLoading: true,
      success: function(res) {
        that.unionfabCode = res.data;
      },
      error: function(err) {
        console.log('失败',err);
      }
    })
  },
  // 优联-创建文件
  createFile: function(filepath, filename, md5) {
    var that = this;
    ajaxPost({
      url: '/api/suppliers/unionfab/'+that.unionfabCode+'/files',
      params: {
        url: filepath,
        name: filename,
        md5: md5
      },
      success: function(res) {
        var unionfabFileId = res.data.fileId;
        var num = 0;
        that.timer = setInterval(function() {
          if (num >= 99) {
            clearInterval(that.timer);
            return;
          }
          num += 9;
          $('.analysis-progress-num').text(num);
        }, 100)
        that.loading = layer.load(2, {
          shade: [0.5, '#fff']
        });
        that.getFileInfo(filepath, filename, md5, unionfabFileId);
      },
      error: function(err) {
        console.log('失败',err);
      }
    })
  },
  // 优联-获取文件信息
  getFileInfo: function(filepath, filename, md5, unionfabFileId) {
    var that = this;
    ajaxGet({
      url: '/api/suppliers/unionfab/file/'+that.unionfabCode+'/'+unionfabFileId,
      params: {},
      noLoading: true,
      success: function(res) {
        if (res.data && res.data.attr.thumbnail) {
          $('.no-upload').show();
          $('.in-upload').hide();
          var data = res.data.attr;
          layer.close(that.loading);
          clearInterval(that.timer);
          $('.analysis-progress-num').text(100);
          that.fileList.push({
            picture: data.thumbnail,
            analysisUrl: analysisUrl3D+'modelFileId='+unionfabFileId+'&code='+that.unionfabCode,
            filepath: filepath,
            filename: filename,
            md5: md5,
            size: parseFloat(data.sizeX).toFixed(2)+' * '+parseFloat(data.sizeY).toFixed(2)+' * '+parseFloat(data.sizeZ).toFixed(2),
            volume: data.volume,
            unionfabCode: that.unionfabCode,
            unionfabFileId: unionfabFileId,
            materialList: []
          })
          // 添加文件到列表
          that.addFileBox();
        } else {
          that.loading = layer.load(2, {
            shade: [0.5, '#fff']
          });
          setTimeout(function(){
            $('.no-upload').hide();
            $('.in-upload').show();
            $('.file-upload-progress').hide();
            $('.file-upload-analysis').show();
            that.getFileInfo(filepath, filename, md5, unionfabFileId);
          },200)
        }
      },
      error: function(err) {
        console.log('失败',err);
        clearInterval(that.timer);
      }
    })

  }
}

// 获取地址列表
function getAddressList(fn) {
  ajaxGet({
    url: '/api/addresses',
    params: {},
    success: function(res) {
      plate3d.addressList = res.items;
      var str = '';
      var firstStr = ''
      $.each(res.items, function(index, item) {
        if (item.isDefault) {
          firstStr = '<div class="address-list-item active" data-index="'+index+'">'+
                        '<i class="radio-img"></i>'+
                        '<span class="name-phone">'+item.recipient+' '+phoneFormat344(item.phoneNumber)+' </span>'+
                        '<span class="detail-address">'+item.provinceName+' '+item.cityName+' '+item.countyName+' '+item.detailAddress+'</span>'+
                      '</div>';
        } else {
          str += '<div class="address-list-item" data-index="'+index+'">'+
                    '<i class="radio-img"></i>'+
                    '<span class="name-phone">'+item.recipient+' '+phoneFormat344(item.phoneNumber)+' </span>'+
                    '<span class="detail-address">'+item.provinceName+' '+item.cityName+' '+item.countyName+' '+item.detailAddress+'</span>'+
                  '</div>';
        }
      })
      $('#address-list').html(firstStr+str);
      fn&&fn();
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}

// 刷新表单，详情填充
function refreshForm() {
  var params = {
    province: '-',
    city: '-',
    area: '-',
    detailAddress: '',
    receiveName: '',
    phone: '',
    areaCode: '',
    tel: '',
    extNumber: '',
    email: '',
    isDefault: 'on'
  }
  layui.form.val('addressForm', params);
}

// 获取省份
function getProvince() {
  ajaxGet({
    url: '/api/addresses/administrative-divisions',
    params: {},
    noLoading: true,
    success: function(res) {
      if (res) {
        plate3d.addressProvinceList = res;
        var addressProvince = '<option value="-">--省--</option>';
        $.each(res, function(index, item){
          addressProvince += '<option value="'+item.code+'-'+item.name+'">'+item.name+'</option>';
        })
        $('.addressProvince').html(addressProvince);
        // 动态表单需要重新渲染
        layui.form.render();
      }
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}

// 获取市
function getCity(id, fn) {
  ajaxGet({
    url: '/api/addresses/administrative-divisions/'+id,
    params: {},
    noLoading: true,
    success: function(res) {
      if (res) {
        plate3d.addressCityList = res;
        var addressCity = '<option value="-">--市--</option>';
        $.each(res, function(index, item){
          addressCity += '<option value="'+item.code+'-'+item.name+'">'+item.name+'</option>';
        })
        $('.addressCity').html(addressCity);
        // 动态表单需要重新渲染
        layui.form.render();
        if (fn) {
          fn();
        }
      }
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}

// 获取区
function getArea(id, fn) {
  ajaxGet({
    url: '/api/addresses/administrative-divisions/'+id,
    params: {},
    noLoading: true,
    success: function(res) {
      if (res) {
        var addressArea = '<option value="-">--区--</option>';
        $.each(res, function(index, item){
          addressArea += '<option value="'+item.code+'-'+item.name+'">'+item.name+'</option>';
        })
        $('.addressArea').html(addressArea);
        // 动态表单需要重新渲染
        layui.form.render();
        if (fn) {
          fn();
        }
      }
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}


// 关闭地址选择弹窗
function closeChooseAddress() {
  $('.choose-address-container').hide();
}
// 关闭新增地址弹窗
function closeAddAddress() {
  $('.add-address-dialog').hide();
}
// 关闭成功弹窗
function closeSuccess() {
  $('.order-success-dialog').hide();
}
