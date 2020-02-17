//產生購物車的建構式
function Cart() {
    //this. === Cart 購物車這個function物件
    // localStorage key
    this.key = 'example-cart';
    // 購物車的資料
    this.data = [];
    // 初始化購物車
    this.initCart = function () {

    }
    // 傳入商品id與數量並新增商品至購物車
    this.addItem = function (pid, amount) {
        //透過pid找回商品資料並建立一個購物車的項目
        let product;
        products.forEach(function (p) {
            //如果該商品的id === pid
            if (p.id === pid) {
                //設定此商品的資料就是product
                product = p;
            }
        });
        console.log("商品", product);
        //建構式: 建立一個購物車項目
        const item = {
            pid: product.id,
            title: product.title,
            price: product.price,
            amount: amount
        };
        //console.log("購物車項目: ",item);
        //把新項目加入購物車
        //.push 加新資料,用於陣列加新資料用
        this.data.push(item);

        //更新畫面
        this.render()
    }
    // 至購物車刪除於購物車內指定索引商品
    this.deleteItem = function (i) {

    }
    // 清空購物車
    this.emptyCart = function () {

    }
    // 更新資料到localStorage
    this.updateDataToStorage = function () {

    }
    // 渲染購物車
    this.render = function () {
        //定義表格內的兩個容器 
        //在變數名稱加上$ 代表可以用jQuery的append
        const $tBody = $("#cartTableBody");
        const $tFoot = $("#cartTableFoot");

        //清空畫面上原有的元素
        //document.getElementById("cartTableBody").innerHTML ="";
        $tBody.empty();

        //預設購物車的總和是零
        let sum = 0;


        //取出購物車的項目
        this.data.forEach(function (item) {
            sum += item.price * item.amount;
            $tBody.append(`
                <tr>
                    <td>${item.title}</td>
                    <td class="text-right">$${item.price}</td>
                    <td class="text-right">${item.amount}</td>
                    <td class="text-right">$${item.price * item.amount}</td>
                </tr>`);
        });

        $tFoot.html(`
        <tr>
        <th>總計</th>
        <td class="text-right" colspan="3">$${sum}</td>
        </tr>`);
    }
}

//產生一個購物車
const cart = new Cart();
//初始化購物車
cart.initCart();

//綁定productRow內部動態產生的form的submit事件
//jQuery$(#目標).delegate(目標內的元素,事件名稱,事件處理器function())
$("#productRow").delegate("form", "submit", function (event) {
    //避免表單重整
    event.preventDefault();
    //this 在此代表被送出的表單
    console.log("被送出的表單是: ", this);
    //取表單所代表的產品id
    //const pid = this.getAttribute("data-product-id");
    //$(this)  === jQuery函數
    const pid = $(this).attr("data-product-id");
    console.log("商品ID", pid);
    //取得該表單輸入填寫的數量
    //const amount = parseInt(document.getElementById((`amountInput${pid}`).val))
    const amount = parseInt($(`#amountInput${pid}`).val());
    console.log("數量", amount);
    //把商品id與數量傳遞至addItem函數
    cart.addItem(pid, amount);

});