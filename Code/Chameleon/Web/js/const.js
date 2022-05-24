// 订单状态
var ORDERSTATUS = {
  audit: 1,
  cancel: 2,
  auditFail: 4,
  tobePaid: 8,
  paid: 16,
  tobeReceived: 128,
  received: 1024,
}
var ORDERSTATUSARRAY = [
  { label: '待审核', value: ORDERSTATUS.audit },
  { label: '审核不通过', value: ORDERSTATUS.auditFail },
  { label: '待付款', value: ORDERSTATUS.tobePaid },
  { label: '生产中', value: ORDERSTATUS.paid },
  { label: '待收货', value: ORDERSTATUS.tobeReceived },
  { label: '已签收', value: ORDERSTATUS.received },
  { label: '已取消', value: ORDERSTATUS.cancel }
]
// 订单类型
var ORDERTYPE = [
  { label: '钣金', value: 0 },
  { label: 'CNC', value: 1 }
]

// 支付类型
var PAYTYPE = {
  wx: 1,//微信
  ali: 2,//支付宝
}

// 3d解析url
var analysisUrl3D = 'https://make.unionfab.com/weapp/#/model_preview/embedded/?';