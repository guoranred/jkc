$(function () {
    var idArr = [dituContent1, dituContent2];
    var zuobiao = [
       { x: 120.187565, y: 30.332577 },
       { x: 119.474711, y: 30.898805 },

    ]
    var mark = [
        { title: "变色龙总部", content: "浙江省杭州市拱墅区新天地商务中心2幢西楼1201室", point: zuobiao[0].x + "|" + zuobiao[0].y, isOpen: 1, icon: { w: 23, h: 25, l: 46, t: 21, x: 9, lb: 12 } },
        { title: "变色龙生产基地", content: "安徽广德经济开发区太极大道780号", point: zuobiao[1].x + "|" + zuobiao[1].y, isOpen: 1, icon: { w: 23, h: 25, l: 46, t: 21, x: 9, lb: 12 } },

    ]
   
    ditu(idArr[0], mark[0], zuobiao[0].x, zuobiao[0].y);
    $(".address_title .contact_icon").hide();
    $(".address_title .contact_icon").eq(0).show();
    $(".contact_tab li").click(function () {
       var n = $(".contact_tab li").index(this);
       $(".dituContentitem").hide();
       $("#dituContentitem" + (n+1)).html("");
       $(".dituContentitem").eq(n).show();
       ditu(idArr[n], mark[n], zuobiao[n].x, zuobiao[n].y);
    })
})
function ditu(id,mark,x,y) {
    //创建和初始化地图函数：
    function initMap() {
        createMap();//创建地图
        setMapEvent();//设置地图事件
        addMapControl();//向地图添加控件
        addMarker();//向地图中添加marker
    }

    //创建地图函数：
    function createMap() {
        var map = new BMap.Map(id);//在百度地图容器中创建一个地图
        var point = new BMap.Point(x, y);//定义一个中心点坐标
        map.centerAndZoom(point, 18);//设定地图的中心点和坐标并将地图显示在地图容器中
        window.map = map;//将map变量存储在全局
    }

    //地图事件设置函数：
    function setMapEvent() {
        map.enableDragging();//地图拖拽事件
        map.disableScrollWheelZoom();//禁用地图滚轮放大缩小，默认禁用(可不写)
        map.disableDoubleClickZoom();//禁用鼠标双击放大
        map.disableKeyboard();//禁用键盘上下左右键移动地图，默认禁用(可不写)
    }

    //地图控件添加函数：
    function addMapControl() {
    }

    //标注点数组
    var markerArr = [mark];
    //创建marker
    function addMarker() {
        for (var i = 0; i < markerArr.length; i++) {
            var json = markerArr[i];
            var p0 = json.point.split("|")[0];
            var p1 = json.point.split("|")[1];
            var point = new BMap.Point(p0, p1);
            var iconImg = createIcon(json.icon);
            var marker = new BMap.Marker(point, { icon: iconImg });
            var iw = createInfoWindow(i);
            var label = new BMap.Label(json.title, { "offset": new BMap.Size(json.icon.lb - json.icon.x + 10, -20) });
            marker.setLabel(label);
            map.addOverlay(marker);
            label.setStyle({
                borderColor: "#808080",
                color: "#333",
                cursor: "pointer"
            });

            (function () {
                var index = i;
                var _iw = createInfoWindow(i);
                var _marker = marker;
                _marker.addEventListener("click", function () {
                    this.openInfoWindow(_iw);
                });
                _iw.addEventListener("open", function () {
                    _marker.getLabel().hide();
                })
                _iw.addEventListener("close", function () {
                    _marker.getLabel().show();
                })
                label.addEventListener("click", function () {
                    _marker.openInfoWindow(_iw);
                })
                if (!!json.isOpen) {
                    label.hide();
                    _marker.openInfoWindow(_iw);
                }
            })()
        }
    }
    //创建InfoWindow
    function createInfoWindow(i) {
        var json = markerArr[i];
        var iw = new BMap.InfoWindow("<b class='iw_poi_title' title='" + json.title + "'>" + json.title + "</b><div class='iw_poi_content'>" + json.content + "</div>");
        return iw;
    }
    //创建一个Icon
    function createIcon(json) {
        var icon = new BMap.Icon("https://map.baidu.com/image/us_mk_icon.png", new BMap.Size(json.w, json.h), { imageOffset: new BMap.Size(-json.l, -json.t), infoWindowOffset: new BMap.Size(json.lb + 5, 1), offset: new BMap.Size(json.x, json.h) })
        return icon;
    }

    initMap();//创建和初始化地图
}