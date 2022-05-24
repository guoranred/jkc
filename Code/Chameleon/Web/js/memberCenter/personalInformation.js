var photoFile = null;
var provinceList = [];
$(function(){
  init();
  var form = layui.form;
  //监听提交
  form.on('submit(personalBaseForm)', function(data){
    var params = data.field;
    if ($('.member-headimg').attr('src') == '../images/memberCenter/default-userhead.png') {
      layer.msg('请上传头像', {icon: 5,anim: 6});
      return false;
    }
    var province = params.province.split('-');
    var city = params.city.split('-');

    var formData = new FormData();
    formData.append('ProfilePhoto', photoFile);
    formData.append('Name', params.name);
    formData.append('Gender', params.sex);
    formData.append('QQ', params.qq);
    formData.append('ProvinceCode', province[0]);
    formData.append('ProvinceName', province[1]);
    formData.append('CityCode', city[0]);
    formData.append('CityName', city[1]);
    formData.append('ChannelId', channelId);
    ajaxUploadPut({
      url: '/api/members/my-profile',
      params: formData,
      success: function(res) {
        layer.msg('保存成功', {icon: 1,anim: 5,time: 2000});
        hiddenEdit();
        getMemberInfo();
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
    $.each(provinceList, function(index, item) {
      if (code == item.code) {
        id = item.id;
      }
    })
    getCity(id);
  })
  // 表单校验
  form.verify({
    username: function(value, item){
      if (!value) {
        return '请输入昵称';
      }
    },
    sex: function(value, item){
      var verifyName=$(item).attr('name')
      , verifyType=$(item).attr('type')
      ,formElem=$(item).parents('.layui-form')//获取当前所在的form元素，如果存在的话
      ,verifyElem=formElem.find('input[name='+verifyName+']')//获取需要校验的元素
      ,isTrue= verifyElem.is(':checked')//是否命中校验
      ,focusElem = verifyElem.next().find('i.layui-icon');//焦点元素
      if(!isTrue || !value){
        //定位焦点
        focusElem.css(verifyType=='radio'?{"color":"#FF5722"}:{"border-color":"#FF5722"});
        //对非输入框设置焦点
        focusElem.first().attr("tabIndex","1").css("outline","0").blur(function() {
            focusElem.css(verifyType=='radio'?{"color":""}:{"border-color":""});
         }).focus();
        return '请选择性别';
      }
    },
    qq: function(value, item){
      if (!value) {
        return '请输入QQ';
      }
    },
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
  })
})

function init() {
  $('#uploadHeadImg').on('click', function(){
    $('.fileUpload').click();
  })
  // 获取会员信息
  getMemberInfo();
}

// 显示编辑
function handleEdit() {
  $('#uploadHeadImg').css('display','inline-block');
  $('.personal-submit').css('display','inline-block');
  $('.personal-edit').css('display','none');
  $('.layui-input').removeAttr('disabled');
  $('.radio-sex').removeAttr('disabled');
  $('.select-area').removeAttr('disabled');
  layui.form.render();
}

// 编辑完成隐藏
function hiddenEdit() {
  $('#uploadHeadImg').css('display','none');
  $('.personal-submit').css('display','none');
  $('.personal-edit').css('display','inline-block');
  $('.layui-input').attr('disabled', true);
  $('.radio-sex').attr('disabled', true);
  $('.select-area').attr('disabled', true);
  layui.form.render();
}

// 获取会员信息
function getMemberInfo() {
  ajaxGet({
    url: '/api/members/my-profile',
    params: {},
    success: function(res) {
      if (res) {
        localStorage.setItem('memberInfo', JSON.stringify(res));
        // 获取省份
        getProvince(function() {
          if (res.provinceCode) {
            var id = '';
            $.each(provinceList, function(index, item) {
              if (res.provinceCode == item.code) {
                id = item.id;
              }
            })
            getCity(id, function() {
              refreshMemberData(res);
            });
          } else {
            refreshMemberData(res);
          }
        });
      }
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}

// 刷新会员信息
function refreshMemberData(res) {
  layui.form.val('baseInfoForm', {
    name: res.name,
    sex: res.gender,
    qq: res.qq,
    province: (res.provinceCode||'') + '-' + (res.provinceName||''),
    city: (res.cityCode||'') + '-' + (res.cityName||'')
  })
  if (res.profilePhotoUrl) {
    $('.member-headimg').attr('src', commomUrl + res.profilePhotoUrl);
  }
}

// 获取上传文件
function getUploadFile(e) {
  var file = $(e)[0].files[0];
  photoFile = file;
  var reader = new FileReader();
　　reader.readAsDataURL(file);
　　reader.onload = function() {
  　　//console.log(this.result);
  　　imgSrc = this.result;
  　　$('.member-headimg').attr("src", imgSrc);
　　};
}

// 获取省份
function getProvince(fn) {
  ajaxGet({
    url: '/api/addresses/administrative-divisions',
    params: {},
    noLoading: true,
    success: function(res) {
      if (res) {
        provinceList = res;
        var infoProvince = '<option value="-">--省--</option>';
        $.each(res, function(index, item){
          infoProvince += '<option value="'+item.code+'-'+item.name+'">'+item.name+'</option>';
        })
        $('.infoProvince').html(infoProvince);
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

// 获取市
function getCity(id, fn) {
  ajaxGet({
    url: '/api/addresses/administrative-divisions/'+id,
    params: {},
    noLoading: true,
    success: function(res) {
      if (res) {
        var infoCity = '<option value="-">--市--</option>';
        $.each(res, function(index, item){
          infoCity += '<option value="'+item.code+'-'+item.name+'">'+item.name+'</option>';
        })
        $('.infoCity').html(infoCity);
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