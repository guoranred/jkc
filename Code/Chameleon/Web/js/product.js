$(function(){
  $('.type-list div').hover(function() {
    $(this).addClass('active').siblings().removeClass('active');
    var pIndex = $(this).attr('data-index');
    var pTotal = $(this).attr('data-total');
    var str = '';
    for(var i = 0; i < Number(pTotal); i++) {
      str += '<div class="layui-col-md4"><div class="product-item"><img src="./images/product/p'+pIndex+'-'+(i+1)+'.png" alt=""></div></div>';
    }
    $('#product-list').html(str);
  }, function() {})
})