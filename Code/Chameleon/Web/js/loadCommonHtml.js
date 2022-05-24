var timer = null;
$(function() {
  $('#header').load('../header.html', function() {
    // 头部事件
    refreshMemberInfo();
    var form = layui.form;
    form.render();

    var currentPage = window.location.pathname;
    var navarr = $('#nav a');
    $(navarr).each(function() {
      if ($(this)[0].dataset.name == currentPage) {
        $(this).addClass('active');
      }
      if (currentPage == '/') {
        $(navarr).eq(0).addClass('active');
      }
    })

    // 忘记密码添加重定向
    var resetPasswordRedirect = location.pathname;
    $('.forget').attr('href', './resetPassword.html?predirect='+resetPasswordRedirect);

    // 初始化登录后下拉菜单
    var dropdown = layui.dropdown;
    dropdown.render({
      elem: '#dropdown',
      trigger: 'hover',
      data: [{
        title: '会员中心',
        id: 'memberCenter'
      }, {
        title: '退出登录',
        id: 'logout'
      }],
      click: function(data, othis){
        if (data.id == 'memberCenter') {
          location.href = '/memberCenter';
        }
        if (data.id == 'logout') {
          layer.confirm('确认退出当前账号?', {icon: 0, title:'提示'}, function(index){
            logout();
            refreshMemberInfo();
            layer.close(index);
          });
        }
      }
    });

    // 判断是否为重定向，显示登录
    var redirectPath = getQueryVariable('redirect');
    if (redirectPath) {
      showLogin();
    }

    // 切换密码框类型
    $('.icon-eye-close').on('click', function() {
      $(this).hide();
      $(this).siblings().show();
      $(this).parent().prev().children().attr('type','text');
    })
    $('.icon-eye').on('click', function() {
      $(this).hide();
      $(this).siblings().show();
      $(this).parent().prev().children().attr('type','password');
    })
    // 切换登录注册
    $('.change-login-register').on('click', function() {
      $('.login-wrap').toggle();
      $('.register-wrap').toggle();
      initForm();
    })

    // 监听复选框
    form.on('checkbox(clause)', function(data){
      checkAllInput();
    }); 

    // 表单校验
    form.verify({
      phoneNumber: function(value, item){
        if (!value) {
          return '请输入手机号';
        }
      },
      password: function(value, item){
        if (!value) {
          return '请输入密码';
        }
      },
      registerPhone: function(value, item){
        if (!value) {
          return '请输入手机号';
        }
        if (!/^1\d{10}$/.test(value)) {
          return '手机号格式错误';
        }
      },
      registerPass: function(value, item){
        if (!value) {
          return '请输入密码';
        }
        if (!isPassword(value)) {
          return '密码为6-25位的数字和字母组合';
        }
      },
      registerConfirmPass: function(value, item){
        if (!value) {
          return '请输入确认密码';
        }
        var formData = layui.form.val('registerForm');
        if (value != formData.password) {
          return '两次输入的密码不一致';
        }
      },
      registerName: function(value, item){
        if (!value) {
          return '请输入姓名';
        }
        if (!isChineseEn(value)) {
          return '姓名为中文或英文';
        }
      },
      verifyCode: function(value, item){
        if (!value) {
          return '请输入验证码';
        }
      },
    })
    //登录
    form.on('submit(loginFormBtn)', function(data){
      var params = data.field;
      ajaxPost({
        url: '/api/members/token',
        params: {
          phoneNumber: params.phoneNumber,
          password: params.password,
          channelId: channelId
        },
        success: function(res) {
          if (params.remember) {
            // 加密并保存账号密码
            localStorage.setItem('credential_bsl', window.btoa(JSON.stringify(params)));
          } else {
            localStorage.removeItem('credential_bsl');
          }
          localStorage.setItem('token', res.tocken);
          getMemberInfo(redirectPath);
        },
        error: function(err) {
          console.log('失败',err);
        }
      })
      return false;
    });
    // 注册
    form.on('submit(registerFormBtn)', function(data){
      var params = data.field;
      ajaxPost({
        url: '/api/members/register',
        params: {
          channelId: channelId,
          name: params.name,
          phoneNumber: params.username,
          password: params.password,
          confirmPassword: params.confirmPassword,
          gender: params.sex,
          validateCode: params.verifyCode,
          promoCode: params.promoCode
        },
        success: function(res) {
          layer.msg('注册成功', {icon: 1,anim: 5,time: 2000});
          if (timer) {
            clearInterval(timer);
          }
          $('.register-wrap').hide();
          $('.login-wrap').show();
        },
        error: function(err) {
          console.log('失败',err);
        }
      })
      return false;
    });
  });
  $('#footer').load('../footer.html', function() {
    // 底部事件
  });
})

// 登录弹窗
function showLogin() {
  // 获取保存的账号密码
  var data = localStorage.getItem('credential_bsl');
  if (data) {
    var params = JSON.parse(window.atob(data));
    layui.form.val('loginForm', params);
  }
  $('.login-register-box').fadeToggle(200);
  $('.login-wrap').show();
}
// 关闭登录
function closeLogin() {
  $('.login-register-box').fadeToggle(200);
  $('.login-wrap').hide();
  initForm();
}
// 注册弹窗
function showRegister() {
  $('.login-register-box').fadeToggle(200);
  $('.register-wrap').show();
}
// 关闭注册
function closeRegister() {
  $('.login-register-box').fadeToggle(200);
  $('.register-wrap').hide();
  initForm();
}
// 清空表单数据
function initForm() {
  var data = localStorage.getItem('credential_bsl');
  if (!data) {
    layui.form.val('loginForm', {
      phoneNumber: '',
      password: '',
      remember: ''
    });
  }
  layui.form.val('registerForm', {
    username: '',
    password: '',
    confirmPassword: '',
    promoCode: '',
    name: '',
    sex: '男',
    verifyCode: '',
    remember: ''
  });
}

// 验证码计时器
function countDown() {
  var count = 300;
  $('.code-btn').text('重新发送(300s)');
  timer = setInterval(function() {
    if (count > 0) {
      count--;
      $('.code-btn').text('重新发送('+count+'s)');
    } else {
      $('.code-btn').removeAttr('disabled');
      $('.code-btn').text('重新发送');
      clearInterval(timer);
    }
  }, 1000);
}

function checkAllInput(e) {
  var formData = layui.form.val('registerForm');
  if (formData.username && formData.password && formData.confirmPassword && formData.name && formData.verifyCode && formData.clause) {
    $('.register-submit').removeAttr('disabled');
  } else {
    $('.register-submit').attr('disabled',true);
  }
}

// 获取会员信息
function getMemberInfo(redirectPath) {
  ajaxGet({
    url: '/api/members/my-profile',
    params: {},
    success: function(res) {
      if (res) {
        layer.msg('登录成功', {icon: 1, shade: [0.1,'#ffffff'],anim: 5,time: 2000});
        localStorage.setItem('memberInfo', JSON.stringify(res));
        closeLogin();
        refreshMemberInfo();
        if (redirectPath) {
          setTimeout(function() {
            location.href = redirectPath;
          }, 2000);
        }
        var predirect = getQueryVariable('predirect');
        if (predirect) {
          setTimeout(function() {
            location.href = predirect;
          }, 2000);
        }
      }
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}

// 刷新会员信息
function refreshMemberInfo() {
  var data = localStorage.getItem('memberInfo');
  if (data) {
    var params = JSON.parse(data);
    $('.membername').text(params.name);
    $('.login').hide();
    $('.has-login').show();
  } else {
    $('.has-login').hide();
    $('.login').show();
  }
}

// 退出登录
function logout() {
  localStorage.removeItem('memberInfo');
  localStorage.removeItem('token');
  if (location.pathname == '/pay') {
    location.href = '/';
  }
}

// 发送验证码
function sendCode() {
  if ($('.code-btn').attr('disabled')) {
    return;
  }
  var data = layui.form.val('registerForm');
  if (!data.username) {
    layer.msg('请输入手机号', {icon: 5,anim: 6});
    return false;
  }
  if (data.username && !/^1\d{10}$/.test(data.username)) {
    layer.msg('手机号格式错误', {icon: 5,anim: 6});
    return false;
  }
  ajaxGet({
    url: '/api/members/validate-code',
    params: {
      channelId: channelId,
      type: 0,
      phoneNumber: data.username
    },
    success: function(res) {
      if (res) {
        layer.msg('发送成功', {icon: 1,anim: 5,time: 2000});
        $('.code-btn').attr('disabled',true);
        countDown();
      }
    },
    error: function(err) {
      console.log('失败',err);
    }
  })
}