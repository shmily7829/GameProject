//宣告陣列格式的物件
//[{}, {}, {}, ...]
const products = [
    {
        id: '1',
        title: '產品一',
        price: 10,
        img: 'https://picsum.photos/id/999/1200/600',
        tags: ['生活用品', '工具'],
        isAvailable: true
    },
    {
        id: '2',
        title: '產品二',
        price: 60,
        img: 'https://picsum.photos/id/1070/1200/600',
        tags: ['藥妝'],
        isAvailable: true
    },
    {
        id: '3',
        title: '產品三',
        price: 180,
        img: 'https://picsum.photos/id/1071/1200/600',
        tags: ['食品'],
        isAvailable: true
    },
    {
        id: '4',
        title: '產品四',
        price: 220,
        img: 'https://picsum.photos/id/1072/1200/600',
        tags: ['生活用品'],
        isAvailable: true
    },
    {
        id: '5',
        title: '產品五',
        price: 360,
        img: 'https://picsum.photos/id/1073/1200/600',
        tags: ['工具'],
        isAvailable: true
    },
    {
        id: '6',
        title: '產品六',
        price: 360,
        img: 'https://picsum.photos/id/1074/1200/600',
        tags: ['食品'],
        isAvailable: true
    },
    {
        id: '7',
        title: '產品七',
        price: 400,
        img: 'https://picsum.photos/id/1075/1200/600',
        tags: ['生活用品', '工具'],
        isAvailable: true
    },
    {
        id: '8',
        title: '產品八',
        price: 450,
        img: 'https://picsum.photos/id/1076/1200/600',
        tags: ['生活用品', '工具'],
        isAvailable: true
    },
    {
        id: '9',
        title: '產品九',
        price: 520,
        img: 'https://picsum.photos/id/1077/1200/600',
        tags: ['藥妝'],
        isAvailable: true
    }
];

// 設計渲染商品的函數
function renderProducts() {
    //定義要放置的容器ID
    //const containerID = "productRow";
    
    //取得容器
    //const container = document.getElementById(containerID);

    //透過迴圈把每個商品資料取出
    products.forEach(function(product) {
        //console.log(product);
        //把容器加入每個產品的卡片UI
        //container.innerHTML += createProductCardElement(product);
        const card = createProductCardElement(product);

        //把卡片的UI加入至id"productRow"的內部
        //#("css選擇器")
        //.append(資料名稱) 加入新內容
        // $("#productRow").append(card) === jQuery("#productRow").append(card);
        jQuery("#productRow").append(card);
    });

}

// 設計建立單一商品卡片HTML標籤的函數
function createProductCardElement(product) {
    //預設要顯示tags的HTML元素是個空字串
    //因為tags的數量可能會有多個
    let tagsElement = "";
    //把單一商品的tags都取出
    product.tags.forEach(function(tag) {
        //console.log(tag);
        //將資料放到span標籤內,並加入至tagsElement
        tagsElement += `<span class="badge badge-secondary">${tag}</span> `
    });
    //預設disable是空字串
    let disabled = "";
    //如果product.isAvailable 是true的話就可以購買
    //如果是fasle就設成disable
    if (!product.isAvailable) {
        //設定變數值為字串disable
        disabled = "disabled";
    }


    // 產生一個Bootstrap Card的元件
    // https://getbootstrap.com/docs/4.4/components/card/
    const cardElement = `
        <div class="col-md-4">
            <div class="card">
                <img src="${product.img}" class="card-img-top">
                <form data-product-id="${product.id}" class="card-body">
                    <h5 class="card-title">${product.title}</h5>
                    ${tagsElement}
                    <p class="card-text">商品價格: $${product.price}</p>
                    <div class="form-group">
                        <label>購買數量</label>
                        <input id="amountInput${product.id}" ${disabled} required class="form-control" type="number" min="1" max="20">
                    </div>
                    <div class="form-group">
                        <button ${disabled} class="btn btn-primary" type="submit">加入購物車</button>
                    </div>
                </form>
            </div>
        </div>
    `;
    return cardElement;
}

// 渲染商品
renderProducts();