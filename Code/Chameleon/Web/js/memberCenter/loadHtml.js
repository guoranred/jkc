$(function() {
  $('#memberHeader').load('../../memberCenter/memberHeader.html', function() {
    refreshMemberInfo();
    layui.use('dropdown', function(){
      var dropdown = layui.dropdown;
      dropdown.render({
        elem: '#dropdown',
        trigger: 'hover',
        data: [{
          title: '退出登录',
          id: 'logout'
        }],
        click: function(data, othis){
          console.log(data.id)
          if (data.id == 'logout') {
            layer.confirm('确认退出当前账号?', {icon: 0, title:'提示'}, function(index){
              logout();
              location.href = '/';
              layer.close(index);
            });
          }
        }
      });
    });
    
  });
  $('#memberSideBar').load('../../memberCenter/memberSideBar.html', function() {
    var list = $('.sidebar-item');
    var pathname = location.pathname;
    for(var i = 0; i < list.length; i++) {
      if (list[i].dataset.index == pathname) {
        $(list[i]).addClass('active');
      }
    }
    if (pathname == '/memberCenter/') {
      $(list[0]).addClass('active');
    }

    // 展开收起菜单
    $('.sidebar-group').on('click', function() {
      console.log($(this).siblings())
      $(this).siblings().slideToggle();
      $(this).find('.right-icon').toggleClass('layui-icon-down');
      $(this).find('.right-icon').toggleClass('layui-icon-up');
    })
  });
  $('#memberFooter').load('../../memberCenter/memberFooter.html', function() {
    // 底部事件
  });
})

// 刷新会员信息
function refreshMemberInfo() {
  var localMemberInfo = localStorage.getItem('memberInfo');
  var memberName = '';
  var profilePhotoUrl='../images/logo.png';
  if (localMemberInfo) {
    var memberInfo = JSON.parse(localMemberInfo);
    memberName = memberInfo.name;
    if(memberInfo.profilePhotoUrl !=null && memberInfo.profilePhotoUrl !=''){
      profilePhotoUrl=commomUrl+memberInfo.profilePhotoUrl;
    }
  } else {
    memberName = '';
  }
  $('.member-name-text').text(memberName);
  $('.member-info-name').text(memberName);
  $('#memberProfilePhotoUrl').attr('src',profilePhotoUrl);
}
// 退出登录
function logout() {
  localStorage.removeItem('memberInfo');
  localStorage.removeItem('token');
}