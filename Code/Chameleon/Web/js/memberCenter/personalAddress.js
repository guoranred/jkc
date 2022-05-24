var addressProvinceList = [];
var addressCityList = [];
$(function() {
  // 获取省份
  getProvince();
  // 获取地址列表
  getAddressList();
  var form = layui.form;
  //创建保存地址
  form.on('submit(personalAddressForm)', function(data){
    var params = data.field;
    if (!params.phone && !params.areaCode && !params.tel && !params.extNumber) {
      layer.msg('手机号码、电话号码必须填一项', {icon: 5,anim: 6});
      return false;
    }
    if (params.phone && !/^1\d{10}$/.test(params.phone)) {
      layer.msg('手机号格式错误', {icon: 5,anim: 6});
      return false;
    }
    if ((params.areaCode && !params.tel) || (!params.areaCode && params.tel)) {
      layer.msg('区号、电话号码需填写完整', {icon: 5,anim: 6});
      return false;
    }
    if (params.areaCode && !/^\d{1,4}$/.test(params.areaCode)) {
      layer.msg('区号格式错误', {icon: 5,anim: 6});
      return false;
    }
    if (params.tel && !/^\d{7,8}$/.test(params.tel)) {
      layer.msg('电话号码格式错误', {icon: 5,anim: 6});
      return false;
    }
    if (params.extNumber && !/^\d{1,4}$/.test(params.extNumber)) {
      layer.msg('分机号格式错误', {icon: 5,anim: 6});
      return false;
    }
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
    if (params.id) {
      ajaxPut({
        url: '/api/addresses/'+params.id,
        params: obj,
        success: function(res) {
          layer.msg('修改成功', {icon: 1,anim: 5,time: 2000});
          getAddressList();
          closeEditAddress();
        },
        error: function(err) {
          console.log('失败',err);
        }
      })
    } else {
      ajaxPost({
        url: '/api/addresses',
        params: obj,
        success: function(res) {
          layer.msg('新增成功', {icon: 1,anim: 5,time: 2000});
          getAddressList();
          closeEditAddress();
        },
        error: function(err) {
          console.log('失败',err);
        }
      })
    }
    return false;
  });
  // 表单校验
  form.verify({
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
      if (!value) {
        return '请输入详细地址';
      }
    },
    receiveName: function(value, item){
      if (!value) {
        return '请输入收货人姓名';
      }
    },
  })
  // 监听省份切换
  form.on('select(province)', function(data) {
    var code = data.value.split('-')[0];
    var id = '';
    $.each(addressProvinceList, function(index, item) {
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
    $.each(addressCityList, function(index, item) {
      if (code == item.code) {
        id = item.id;
      }
    })
    getArea(id);
  })
})

function initPage(params) {
  var laypage = layui.laypage;
  laypage.render({
    elem: 'addresspage',
    theme: '#009B77',
    curr: params.curr,
    count: params.count,
    prev: '<i class="layui-icon layui-icon-left"></i>',
    next: '<i class="layui-icon layui-icon-right"></i>',
    layout: ['count', 'prev', 'page', 'next', 'skip'],
    jump: function(obj, first){
      if(!first){
        getAddressList(obj.curr, obj.limit);
      }
    }
  });
}

// 获取地址列表
function getAddressList(data) {
  if (!data) {
    data = {};
  } 
  var skipCount = 0;
  if (data.page) {
    skipCount = (data.page - 1) * data.size;
  }
  var params = {
    skipCount: skipCount,
    maxResultCount: data.size || 10
  };
  ajaxGet({
    url: '/api/addresses',
    params: params,
    success: function(res) {
      if (res.items && res.items.length > 0) {
        $('.member-empty').hide();
      } else {
        $('.member-empty').show();
      }
      var str = '';
      $.each(res.items, function(index, item) {
        var strDefault = '';
        if (item.isDefault) {
          strDefault = '<span class="address-default-tag">默认地址</span>';
        }
        str += '<tr>'+
          '<td>'+item.recipient+'</td>'+
          '<td>'+item.provinceName+item.cityName+item.countyName+item.detailAddress+'</td>'+
          '<td>'+phoneFormat(item.phoneNumber)+'</td>'+
          '<td align="center">'+
            '<a href="javascript:;" class="address-btn" data-id="'+item.id+'" onclick="handleChangeAddress(this)">修改</a>'+
            '<span class="btn-divide">|</span>'+
            '<a href="javascript:;" class="address-btn" data-id="'+item.id+'" onclick="handleDeleteAddress(this)">删除</a>'+
          '</td>'+
          '<td align="center">'+strDefault+'</td>'+
        '</tr>';
      })
      $('#address-list').html(str);
      $('.address-totalnum').text(res.totalCount);
      initPage({curr: 1, count: res.totalCount});
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}

// 新增
function addAddress() {
  $('.address-edit-title-text').text('新增收货地址');
  showEditAddress();
}

// 编辑
function handleChangeAddress(e) {
  $('.address-edit-title-text').text('修改收货地址');
  showEditAddress();
  var id = $(e)[0].dataset.id;
  ajaxGet({
    url: '/api/addresses/'+id,
    success: function(res) {
      // 请求回显市区
      var cityId = '';
      $.each(addressProvinceList, function(index, item) {
        if (res.provinceCode == item.code) {
          cityId = item.id;
        }
      })
      getCity(cityId, function() {
        var areaId = '';
        $.each(addressCityList, function(index, item) {
          if (res.cityCode == item.code) {
            areaId = item.id;
          }
        })
        getArea(areaId, function() {
          refreshForm(res);
        });
      });
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}

// 显示编辑弹窗
function showEditAddress() {
  $('.address-edit-box').show();
}
// 关闭编辑弹窗
function closeEditAddress() {
  $('.address-edit-box').hide();
  refreshForm();
}

// 删除
function handleDeleteAddress(e) {
  var id = $(e)[0].dataset.id;
  layer.confirm('确认删除该地址？', function(index){
    ajaxDelete({
      url: '/api/addresses/'+id,
      success: function(res) {
        layer.msg('删除成功', {icon: 1,anim: 5,time: 2000});
        getAddressList();
        layer.close(index);
      },
      error: function(err) {
        console.log('失败',err);
      }
    })
  });  
}

// 刷新表单，详情填充
function refreshForm(data) {
  var params = null;
  if (data) {
    var isDefault = '';
    if (data.isDefault) {
      isDefault = 'on';
    }
    params = {
      id: data.id,
      province: data.provinceCode+'-'+data.provinceName,
      city: data.cityCode+'-'+data.cityName,
      area: data.countyCode+'-'+data.countyName,
      detailAddress: data.detailAddress,
      receiveName: data.recipient,
      phone: data.phoneNumber,
      areaCode: '',
      tel: '',
      extNumber: '',
      email: '',
      isDefault: isDefault
    }
  } else {
    params = {
      id: '',
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
        addressProvinceList = res;
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
        addressCityList = res;
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