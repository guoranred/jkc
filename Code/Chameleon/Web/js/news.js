$(function() {
  initPage({curr: 1, count: 20});
})
function initPage(params) {
  var laypage = layui.laypage;
  laypage.render({
    elem: 'newspage',
    theme: '#333333',
    curr: params.curr,
    count: params.count,
    prev: '<i class="layui-icon layui-icon-left"></i>',
    next: '<i class="layui-icon layui-icon-right"></i>',
    layout: ['count', 'prev', 'page', 'next', 'skip'],
    jump: function(obj, first){
      if(!first){
        // getOrderList({page: obj.curr, size: obj.limit});
      }
    }
  });
}