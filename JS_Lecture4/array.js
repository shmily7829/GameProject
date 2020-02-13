const list = [1, 2, 5, 6, 8];
//抓出list內所引為0的資料
//取得陣列的值 陣列[索引]
// console.log(list[0]); //第一筆資料
// console.log(list[1]);
// console.log(list[2]);
// console.log(list[3]);
// console.log(list[4]);

//屬出list的所有索引值
//從0數屬到list.length-1
for (let index = 0; index < list.length; index++) {
    console.log(`第${index + 1}筆數字是${list[index]}`);
}

//把list裡的資料一個一個取出
//陣列.forEach(function(每個在陣列內的資料,資料的索引){})
list.forEach(function(ary,index){
    console.log(`No.${index+1}: ${ary}`);
})

//更新陣列的值
list[0] = 100;
console.log(`list裡面有${list.length}筆資料`);
console.log("list: ", list);//列出陣列所有資料

//產生一個空陣列
//陣列.push(新資料) 把新增的資料放在陣列的最後面
const list2 = [];
list2.push(1);
list2.push(5);
list2.push(8);
//[1,5,8]
//從索引位置開始移除n筆資料,n從start往後數
list2.splice(0,3);
console.log("list2: ",list2);