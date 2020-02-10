//js
        //a && b => a,b都是true才會true
        //a || b => a,b其中有一個true就是true
        //!true => false
        function calc() {
            //取得input
            const numInput = document.getElementById("numInput");
            console.log("[numInput]", numInput);
            //取得input的值並轉為整數 .value + parse
            const num = parseInt(numInput.value);
            console.log("[num]", num);
            const a = 20,
            b = 30;
            //num 是否 >= 而且 num是否 <=30
            //num 是否不在 20 ~ 30之間
            //let condition = !(num >= a && num <= b);
            let conditon = num < a || num > b;
            if (condition) {
                //alert方法 待查
                alert(`${num}介於${a}和${b}之間`)
            } else {
                alert("不符合條件");
            }
        }