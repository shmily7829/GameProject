//定義一個物件{}
/*
{name:"Andy",
phone:"0912345678"}
屬性(key name):值(value),
屬性(key name):值(value)
多個屬性用逗號區隔
*/
//物件裡面可以再包物件,沒有數量上限
//物件的值可以是一個函數(funtion)
const user1 = {
    name: "andy",
    age: 28,
    phone: "0912345678",
    isAdmin: false,
    address: {
        city: "台北市",
        distrivt: "大安區",
        zip: "106"
    },
    //()傳入的資料
    intro: function (greeting) {
        //在物件函數內使用this
        //this代表這個物件
        console.log("[this]",this);
        const str = `${greeting},我叫${this.name},住在${this.address.city}`;
        console.log(str);
    }
};
console.log("整個視窗的this",this);
// user1使用intro函數
user1.intro("he!!!!");
user1.intro("YOHO");

//更新名字
user1.name = "Jash";
user1["name"] = "Tony";
//新增屬性,兩種寫法皆可
//user1.email = "andy@gmail.com";
user1["email"] = "tony@gmail.com";
//移除屬性
//delete user1.email;
// console.log("[user1]", user1);
//物件.屬性名稱 => 取得值
// console.log(user1.name);
// console.log(user1.age);
// console.log(user1.address.city);
// console.log(user1["address"]["city"]);