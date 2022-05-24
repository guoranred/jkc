// var commomUrl = 'http://clientapi.bslzz.com';
// var commomUrl = 'http://115.236.37.105:8363';
var commomUrl = 'https://localhost:44346';
var channelId = '3fa85f64-5717-4562-b3fc-2c963f66afa6';
function ajaxGet(params) {
  ajax({url: params.url, type: 'get', params: params.params, noLoading: params.noLoading, success: params.success, error: params.error, complete: params.complete})
}
function ajaxPost(params) {
  ajax({url: params.url, type: 'post', params: JSON.stringify(params.params), noLoading: params.noLoading, success: params.success, error: params.error, complete: params.complete})
}
function ajaxPut(params) {
  ajax({url: params.url, type: 'put', params: JSON.stringify(params.params), noLoading: params.noLoading, success: params.success, error: params.error, complete: params.complete})
}
function ajaxDelete(params) {
  ajax({url: params.url, type: 'delete', params: JSON.stringify(params.params), noLoading: params.noLoading, success: params.success, error: params.error, complete: params.complete})
}
function ajax(params) {
  if (!params.noLoading) {
    var loading = layer.load(2, {
      shade: [0.5, '#fff']
    });
  }
  $.ajax({
    url: commomUrl + params.url,
    type: params.type,
    contentType: 'application/json',
    headers: {
      'Accept': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    },
    data: params.params,
    success: function (res) {
      if (!params.noLoading) {
        layer.close(loading);
      }
      params.success && params.success(res);
    },
    error: function (res) {
      if (!params.noLoading) {
        layer.close(loading);
      }
      if (res.status == 0) {
        layer.msg('网络超时，请稍后再试', {icon: 2,anim: 5,time: 2000});
        return;
      }
      if (res.status == 401) {
        layer.msg('登录已过期或未登录，请重新登录', {icon: 2,shade: [0.1,'#ffffff'],anim: 5,time: 2000});
        if (location.pathname == '/valuation.html') {
          showLogin();
          return;
        }
        setTimeout(function() {
          localStorage.removeItem('token');
          localStorage.removeItem('memberInfo');
          location.href = '/?redirect='+location.pathname;
        }, 2000);
        return;
      }
      if (res.responseText) {
        var msg = JSON.parse(res.responseText).error.message;
        if (msg == '你的请求无效!' || msg == 'Your request is not valid!') {
					msg = JSON.parse(res.responseText).error.validationErrors[0].message;
				}
        layer.msg(msg, {icon: 2,anim: 5,time: 2000});
      }
      params.error && params.error(res);
    },
    complete:function(res) {
      params.complete && params.complete(res);
    }
  });
}
function ajaxUploadPut(params) {
  if (!params.noLoading) {
    var loading = layer.load(2, {
      shade: [0.5, '#fff']
    });
  }
  $.ajax({
    url: commomUrl + params.url,
    type: 'put',
    contentType:false,
    processData:false,
    headers: {
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    },
    data: params.params,
    success: function (res) {
      if (!params.noLoading) {
        layer.close(loading);
      }
      params.success && params.success(res);
    },
    error: function (res) {
      if (!params.noLoading) {
        layer.close(loading);
      }
      if (res.responseText) {
        var msg = JSON.parse(res.responseText).error.message;
        layer.msg(msg, {icon: 2,anim: 5,time: 2000});
      }
      params.error && params.error(res);
    },
    complete:function(res) {
      params.complete && params.complete(res);
    }
  });
}
function ajaxUploadPost(params) {
  if (!params.noLoading) {
    var loading = layer.load(2, {
      shade: [0.5, '#fff']
    });
  }
  $.ajax({
    url: commomUrl + params.url,
    type: 'post',
    contentType:false,
    processData:false,
    headers: {
      'Authorization': 'Bearer ' + localStorage.getItem('token')
    },
    data: params.params,
    success: function (res) {
      if (!params.noLoading) {
        layer.close(loading);
      }
      params.success && params.success(res);
    },
    error: function (res) {
      if (!params.noLoading) {
        layer.close(loading);
      }
      if (res.responseText) {
        var msg = JSON.parse(res.responseText).error.message;
        layer.msg(msg, {icon: 2,anim: 5,time: 2000});
      }
      params.error && params.error(res);
    },
    complete:function(res) {
      params.complete && params.complete(res);
    }
  });
}