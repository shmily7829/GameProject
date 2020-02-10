function calculatorGPA(event) {
    // 防止瀏覽器表單預設會在送出時重整的行為
    event.preventDefault();
    // TODO: 取得國英數成績
    const chineseScore = getScore("chineseScoreInput");
    const englishScore = getScore("englishScoreInput");
    const mathScore = getScore("mathScoreInput");
    //取得資料值 document.getElementById('englishScoreInput').value;
    //算出總分
    const total = chineseScore + englishScore + mathScore;
    console.log("[total]",total);

    // TODO: 計算出平均成績，四捨五入至個位數
    //Math.round(value) 四捨五入
    //Math.ceil(value) 無條件進位
    //Math.floor(value) 無條件捨去
    const avgScore = Math.round(total / 3);
    console.log("[avg]", avgScore);

    // TODO: 完成GPA計算機邏輯
    let gpa;
    //預設顏色是次要色
    let color = "secondary";
    //如果平均分數 >=90
    if (avgScore>=90){
        //設定GPA是A+
        gpa = "A+";
        color = "success";
    }
    if (avgScore >=80){
        gpa = "A";
    }
    if (avgScore >=70){
        gpa = "B";
    }
    if(avgScore >=60){
        gpa = "C";
    }
    else {
        gpa = "F";
        color = "danger";
    }
    console.log("[GPA]",gpa);

    // TODO: 顯示成績報告於畫面上
    //`string $(變數value)`
    const str = 
    `
    <div class="alert alert-${color}">
    國文:${chineseScore}<br>
    英文:${englishScore}<br>
    數學:${mathScore}<br>
    平均分數:${avgScore}<br>
    GPA:${gpa}
    </div>
    `;
    console.log(str);
    //取得result的div標籤
    const result = document.getElementById("result");
    console.log(result);
    //改變div的內容
    //把上次的舊資料顯示在畫面上 ${result.innerHTML}
    result.innerHTML = `${str} ${result.innerHTML}`;
}

//可從input取得值並轉換成數字類型的方法
//()為欲傳入的資料,可以自己取名字
function getScore(inputId){
    //取得指定id裡面所填的值
    let score = document.getElementById(inputId).value;

    //轉成數字
    score = parseInt(score);
    console.log("[score]",score);
    //把結果輸出
    return score;
}