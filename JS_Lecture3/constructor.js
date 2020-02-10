//建構式是一個用來產生物件的函數
//設計一個用來產生使用者的建構式
//建構式的命名第一個字是大寫
//()要傳入的參數
function User(name, age) {
    //this代表即將被建構式產生的物件
    //this.name 物件的屬性name
    //this.age 物件的屬性age
    this.name = name;
    this.age = age;
    //宣告一個追隨清單(物件)用來儲存追隨的對象
    this.followList = {}
    this.intro = function () {
        console.log("[this]", this);
        const str = `hi,我是${this.name}今年${this.age}歲`;
        console.log(str);
    }
    this.follow = function (target) {
        //取得追隨對象
        console.log(`${this.name}的新追隨目標是`,target.name); //this.name = user1(追蹤者) , target.name = user2(被追蹤者)
        //console.log(`[追隨目標的名字]`,target.name);
        //把追隨目標加到我的追隨清單內
        //this.followList.target.name = target;
        this.followList[target.name] = target;

    }
}
//透過建構式產生一個User物件
const user1 = new User("Aily", 20);
//透過建構式產生一個User物件
const user2 = new User("Joyice", 35);
const user3 = new User("Cindy", 18);
//傳遞user2到user1的follow的流程裡
//user1(this) 觸發了 follow 
user1.follow(user2);
user1.follow(user3);


// user1.intro();
// user2.intro();
console.log("user1", user1);
console.log("user2", user2);