const express = require('express');
const db = require("../db");//引用db
const router = express.Router();

// 首頁路由
router.get('/', async function (req, res, next) {
  //await 等待回應
  const docs = await db.collection("products").get()//取得DB資料
  // db.collection("products").get()
  //   .then(res => console.log(res))
  //   .catch(err => console.log(err))
  //定義產品清單
  const products = [];
  docs.forEach(doc => {
    console.log("doc.data: ", doc.data());
    //定義產品
    const product = doc.data();
    //在products陣列裡面加入product資料
    products.push(product);
  });
  //傳遞產品清單至index.ejs
  res.locals.products = products;
  // TODO: 取得產品列表
  res.render('index');
});

// API示範頁面路由
router.get('/api-demo', function (req, res, next) {
  res.render('api-demo');
});

// 名言頁面
router.get('/quote/:num', function (req, res, next) {
  console.log("req.params:", req.params);
  //取得路徑上的:num
  const num = parseInt(req.params.num);
  //算num / 3的餘數
  const i = num % 3;
  console.log("餘數是:", i);
  const quoteList = [
    {
      author: "莊子",
      img: 'https://picsum.photos/id/1042/1000/600',
      text: "相濡以沫，不如相忘於江湖。"
    },
    {
      author: "老子",
      img: 'https://picsum.photos/id/1044/1000/600',
      text: "天下皆知美之為美，斯惡矣。皆知善之為善，斯不善矣。"
    },
    {
      author: "孔子",
      img: 'https://picsum.photos/id/112/1000/600',
      text: "知者不惑，仁者不憂，勇者不懼。"
    }
  ];
  // TODO: 根據:num參數選擇渲染的資料
  const quote = quoteList[i];
  console.log("quote: ", quote);
  // 規則：使用/3的餘數為資料取得索引
  // 把quote遞送給ejs模板
  res.locals.quote = quote;
  // 把名言陣列傳遞到ejs模板
  res.locals.list = quoteList;

  // TODO: 渲染相對應的畫面
  //透過views/quote.ejs的檔案組合出html作為回應
  res.render("quote");
});

module.exports = router;
