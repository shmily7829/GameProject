console.log("[intro.js]");
//設計一個名為craetIntro的方法
//方法將接收傳遞進來的event參數
//方法: 一系列的動作
function creatIntro(event){
    //console.log(event);
    //取消瀏覽器預設的表單重整行為
    event.preventDefault();
    console.log("表單已送出");
    //取得名字
    //docment 瀏覽器提供的資料 可取得特定物件元素 代表整份HTML
    //getElementById 取得標籤的方法
    //.value 使用者輸入的值
    const name = document.getElementById("nameInput").value;
    const phone = document.getElementById("phoneInput").value;
    console.dir(name);
    //產生自我介紹
    const str = `hi,我是${name}電話是${phone}`;
    //顯示在畫面上
    const report = document.getElementById("report");
    console.dir(report);
    report.innerText = str;

    //瀏覽器提供的物件模型
    //https://ithelp.ithome.com.tw/articles/10191666
}