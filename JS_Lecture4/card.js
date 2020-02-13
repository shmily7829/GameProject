function Card(id, name, cash) {
    this.id = id;
    this.name = name;
    this.balance = cash;
    this.purchaseHistory = {};
    const cashPledge = 100;
    //加值
    this.deposit = function (cash) {
        //把現金的數字加總至餘額
        this.balance += cash;
        console.log("加值成功! ");
        this.showBlance();
    };
    //消費
    this.purchase = function (title, price) {
        //顯示錯誤訊息與餘額資訊
        //扣除餘額的負值不可以超過押金
        if (this.balance - price < -cashPledge) {
            console.log("餘額不足!");
            this.showBlance();
            //停止函數
            return;
        }
        //扣除餘額
        this.balance -= price;
        console.log("消費成功! ");
        console.log(`購買商品${title} -$${price}`);
        this.showBlance();
        //取得當下的時間戳記
        const createdAt = new Date().getTime();
        console.log(createdAt);

        //產生一筆紀錄
        const record = {
            title: title,
            price: price,
            createdAt: createdAt
        }
        //console.log("一筆紀錄: ", record);

        //把紀錄記錄在消費紀錄內
        this.purchaseHistory[createdAt] = record;
    };

    //顯示餘額
    this.showBlance = function () {
        console.log(`餘額: ${this.balance}`);
    };
    //顯示消費紀錄
    this.showHistory = function () {
        //console.log("消費紀錄: ", this.purchaseHistory);
        //把消費紀錄的資料一個一個取出(迴圈)
        //for...in 把該物件中的所有屬性名稱和屬性值都呼叫出來
        for (let key in this.purchaseHistory) {
            //console.log("key: ", key);
            //console.log("record: ", record);
            const record = this.purchaseHistory[key];
            const time = moment(record.createdAt).format("YYYY年MM月DD日 HH:mm:ss");
            const str = `${time} ${record.title} $${record.price}`;
            console.log(str);
        }
    };


}
//產生一張卡片的資料
const card1 = new Card("12345678", "Aily", 100);

//加值卡片
card1.deposit(200);

//消費
//SetTimeout 延遲1秒執行 單位:毫秒 

setTimeout(function () {
    card1.purchase("商品1", 50);
}, 1000)

setTimeout(function () {
    card1.purchase("商品2", 40);
}, 2000)

setTimeout(function () {
    card1.purchase("商品3", 40);
    //消費紀錄
    card1.showHistory();
    //console.log(card1);
}, 3000)

const card2 = new Card("87654321","Tony",50);
console.assert("card02: ",card2);
//物件轉為字串
const str2 =JSON.stringify(card2);
console.log("str2 ",str2); 
//字串轉回物件
console.log(JSON.parse(str2));
