$(function () {
  // 显示登录
  $('.have-account').on('click', 'span', function () {
    showLogin();
  })
  $('.reset-success').on('click', '.reset-login', function() {
    showLogin();
  })
  // 返回首页
  $('.back-home').on('click', function() {
    location.href = './index.html';
  })
  // 表单校验
  layui.form.verify({
    resetPhone: function(value, item){
      if (!value) {
        return '请输入手机号';
      }
      if (!/^1\d{10}$/.test(value)) {
        return '手机号格式错误';
      }
    },
    resetPass: function(value, item){
      if (!value) {
        return '请输入密码';
      }
      if (!isPassword(value)) {
        return '密码为6-25位的数字和字母组合';
      }
    },
    resetConfirmPass: function(value, item){
      if (!value) {
        return '请输入确认密码';
      }
      var formData = layui.form.val('resetForm');
      if (value != formData.password) {
        return '两次输入的密码不一致';
      }
    },
    resetVerifyCode: function(value, item){
      if (!value) {
        return '请输入验证码';
      }
    },
  })
  // 修改密码
  layui.form.on('submit(confirmBtn)', function(data){
    var params = data.field;
    ajaxPost({
      url: '/api/members/retrieve-password',
      params: {
        phoneNumber: params.mobile,
        password: params.password,
        confirmPassword: params.confirmPassword,
        validateCode: params.verifyCode,
        channelId: channelId
      },
      success: function(res) {
        $('.reset-password').hide();
        $('.reset-success').show();
      },
      error: function(err) {
        console.log('失败',err);
      }
    })
    return false;
  });
})

// 发送验证码
function sendResetCode() {
  if ($('.reset-code-btn').attr('disabled')) {
    return;
  }
  var data = layui.form.val('resetForm');
  if (!data.mobile) {
    layer.msg('请输入手机号', {icon: 5,anim: 6});
    return false;
  }
  if (data.mobile && !/^1\d{10}$/.test(data.mobile)) {
    layer.msg('手机号格式错误', {icon: 5,anim: 6});
    return false;
  }
  ajaxGet({
    url: '/api/members/validate-code',
    params: {
      channelId: channelId,
      type: 1,
      phoneNumber: data.mobile
    },
    success: function(res) {
      if (res) {
        layer.msg('发送成功', {icon: 1,anim: 5,time: 2000});
        $('.reset-code-btn').attr('disabled',true);
        resetCountDown();
      }
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}

// 验证码计时器
function resetCountDown() {
  var count = 300;
  $('.reset-code-btn').text('重新发送(300s)');
  var timer = setInterval(function() {
    if (count > 0) {
      count--;
      $('.reset-code-btn').text('重新发送('+count+'s)');
    } else {
      $('.reset-code-btn').removeAttr('disabled');
      $('.reset-code-btn').text('重新发送');
      clearInterval(timer);
    }
  }, 1000);
}