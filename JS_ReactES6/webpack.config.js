//輸出一個打包的配置物件
//因為 webpack 是基於Node構建的, 所以webpack支持所有Node API 和語法
module.exports = {
    mdoe: 'production' //development production 
}
//在 webpack 4.x中,有一個很大的特性, 就是 約定大於配置
//約定, 默認的打包入口路徑是 src --> index.js


//目前不能這樣使用
//export default{}