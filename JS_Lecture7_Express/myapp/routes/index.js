var express = require('express');
var router = express.Router();

/* GET home page. */
//路由
router.get('/', function (req, res, next) {
  //當有人造訪 / 時，透過views/index.ejs的模板渲染HTML作為回應
  // res. == response回應
  //res.locals把資料title遞送給選定的模板
  res.locals.title = "MyHomePage";
  res.locals.subtitle = "subtitle";
  res.render('index');
});

module.exports = router;
