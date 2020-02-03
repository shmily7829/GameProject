//註解 
/*
多行註解
*/
/*
Ctrl + / 
選取範圍註解
*/
//顯示訊息方塊 alert("HAHAHAA")


//建立一個名為 myName的變數(let)儲存一資料 "Shiya"
//建立變數但沒有給值 -> undefined
//var
let myName = "Shiya";
//建立一名為myID的常數
const myAge = 25;
console.log("我的年紀是"+ myAge);
//常數不可賦予新值
//myID = "0000000";
console.log("helloooo" + myName);
myName = "YOHOHO";
//const intro = "HI,我叫" + myName + "今年" + myAge + "歲";
//`${變數名字}`  可在字串內直接顯示變數內容
const intro = `Hi,我叫${myName}今年${myAge}歲`;
console.log(intro);