// 截取时间
function timeSubstr(val, index, num) {
  index = index || 0;
  num = num || 10;
  if (val) {
    return val.substr(index, num);
  }
  return val;
}

// 获取地址栏参数
function getQueryVariable(variable) {
  var query = window.location.search.substring(1);
  var vars = query.split("&");
  for (var i=0;i<vars.length;i++) {
    var pair = vars[i].split("=");
    if(pair[0] == variable){return pair[1];}
  }
  return(false);
}

// 密码正则，字母数字组合
function isPassword(value) {
  const reg = /^(?!([a-zA-Z]+|\d+)$)[a-zA-Z\d]{6,25}$/;
  return reg.test(value);
}

// 中文正则
function isChinese(value) {
  const reg = /^([\u4E00-\uFA29]|[\uE7C7-\uE7F3])+$/;
  return reg.test(value);
}

// 中文，字母
function isChineseEn(value) {
  const reg = /^([\u4E00-\uFA29]|[\uE7C7-\uE7F3]|[a-zA-Z])+$/;
  return reg.test(value);
}

// 手机号隐藏中间四位
function phoneFormat(value) {
  return value.substr(0,3)+'****'+value.substr(7,4);
}

// 手机号344格式空格隔开
function phoneFormat344(value) {
  return value.substr(0,3)+' '+value.substr(3,4)+' '+value.substr(7,4);
}

// 时间格式化
function timeFormat(date, fmt) {
  if (!String.prototype.padStart) {
    String.prototype.padStart = function padStart(targetLength, padString) {
      targetLength = targetLength >> 0; //floor if number or convert non-number to 0;
      padString = String(typeof padString !== 'undefined' ? padString : ' ');
      if (this.length > targetLength) {
        return String(this);
      } else {
        targetLength = targetLength - this.length;
        if (targetLength > padString.length) {
          padString += padString.repeat(targetLength / padString.length); //append to original to ensure we are longer than needed
        }
        return padString.slice(0, targetLength) + String(this);
      }
    };
  }
  if (!fmt) {
    fmt = 'YYYY-mm-dd HH:MM:SS';
  }
  if (date) {
    date = new Date(date);
    let ret;
    const opt = {
        "Y+": date.getFullYear().toString(),        // 年
        "m+": (date.getMonth() + 1).toString(),     // 月
        "d+": date.getDate().toString(),            // 日
        "H+": date.getHours().toString(),           // 时
        "M+": date.getMinutes().toString(),         // 分
        "S+": date.getSeconds().toString()          // 秒
        // 有其他格式化字符需求可以继续添加，必须转化成字符串
    };
    for (let k in opt) {
        ret = new RegExp("(" + k + ")").exec(fmt);
        if (ret) {
            fmt = fmt.replace(ret[1], (ret[1].length == 1) ? (opt[k]) : (opt[k].padStart(ret[1].length, "0")))
        };
    };
    return fmt;
  }
  return date;
}