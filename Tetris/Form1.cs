using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        //變數與物件的宣告
        //畫面設定
        private const float REQUIRE_FPS = 60; //預期的畫面更新率(每秒呼叫幾次OnTimer)
        private Graphics wndGraphics; //視窗畫布
        private Graphics backGraphics;//背景頁畫布
        private Bitmap backBmp;//點陣圖
        private const int VIEW_W = 800;
        private const int VIEW_H = 600;

        private const int CELL_NULL = 0;
        private const int CELL_RED = 1;
        private const int CELL_GREEN = 2;
        private const int CELL_BLUE = 3;

        private const int CELL_SIZE = 25;

        private const int CELL_W_COUNT = 10;
        private const int CELL_H_COUNT = 20;
        private int[][] vvCell; //二維的陣列

        private const int MOVE_CELL_W_COUNT = 4;
        private const int MOVE_CELL_H_COUNT = 4;
        private int[][] vvMoveCell; //移動中的方塊
        private int moveCellX; //4*4的左上角的CELL,對應到20*20的哪一個位置
        private int moveCellY;

        //控制移動
        private KeyState keySpace;//空白鍵
        private KeyState keyUp;   //上
        private KeyState keyDown; //下
        private KeyState keyLeft; //左
        private KeyState keyRight;//右

        public Form1()
        {
            InitializeComponent();

            //移動按鍵初始化
            moveCellX = 0;
            moveCellY = 0;

            keySpace = new KeyState(Keys.Space);
            keyUp = new KeyState(Keys.Up);   //上
            keyDown = new KeyState(Keys.Down); //下
            keyLeft = new KeyState(Keys.Left); //左
            keyRight = new KeyState(Keys.Right);

            //timer1 = 計時器物件
            //timer.Interval 多久響
            //單位千分之一秒
            timer1.Interval = 1000 / (int)REQUIRE_FPS; //1/30秒
            timer1.Start();

            //建立背景頁
            wndGraphics = CreateGraphics();//建立視窗畫布
            backBmp = new Bitmap(VIEW_W, VIEW_H);//建立點陣圖物件
            backGraphics = Graphics.FromImage(backBmp);//建立背景畫布

            //20個一維陣列
            //20個一維陣列的參照
            vvCell = new int[CELL_H_COUNT][];

            //i = 直的 , m=衡的
            for (int i = 0; i < CELL_H_COUNT; i++)
            {
                //new 一個大小為10的一維陣列
                //new 10個INT
                vvCell[i] = new int[CELL_W_COUNT];

                for (int m = 0; m < CELL_W_COUNT; m++)
                {
                    //把所有的格子都設為空格(null)
                    vvCell[i][m] = CELL_NULL;
                }
            }

            //4*4
            //4個一維陣列的參照
            vvMoveCell = new int[MOVE_CELL_H_COUNT][];

            //i = 直的 , m=衡的
            for (int i = 0; i < MOVE_CELL_H_COUNT; i++)
            {
                vvMoveCell[i] = new int[MOVE_CELL_W_COUNT];

                for (int m = 0; m < MOVE_CELL_W_COUNT; m++)
                {
                    //把所有的格子都設為空格(null)
                    vvMoveCell[i][m] = CELL_NULL;
                }
            }
            vvMoveCell[1][1] = CELL_RED;
            vvMoveCell[1][2] = CELL_RED;
            vvMoveCell[2][1] = CELL_RED;
            vvMoveCell[2][2] = CELL_RED;
            /*
            //塗方塊顏色 測試用,方便觀察
            vvCell[0][0] = CELL_RED;
            vvCell[0][1] = CELL_GREEN;
            vvCell[0][2] = CELL_BLUE;

            vvCell[CELL_H_CONST - 1][CELL_W_CONST - 1] = CELL_RED;
            vvCell[CELL_H_CONST - 1][CELL_W_CONST - 2] = CELL_GREEN;
            vvCell[CELL_H_CONST - 1][CELL_W_CONST - 3] = CELL_BLUE;
            */
        }

        private void drawGame()
        {
            //畫出畫布
            backGraphics.FillRectangle(Brushes.White, 0, 0, VIEW_W, VIEW_H);

            //畫出範圍黑線
            backGraphics.DrawRectangle(Pens.Black, 0, 0, CELL_SIZE * CELL_W_COUNT, CELL_SIZE * CELL_H_COUNT);

            //畫20*20 根據對應的座標資料 塗方塊顏色
            for (int i = 0; i < CELL_H_COUNT; i++)
            {
                for (int m = 0; m < CELL_W_COUNT; m++)
                {
                    //i = 直的 , m=衡的
                    int tx = m * CELL_SIZE;
                    int ty = i * CELL_SIZE;

                    if (vvCell[i][m] == CELL_RED)
                    {
                        backGraphics.FillRectangle(Brushes.Red, tx, ty, CELL_SIZE, CELL_SIZE);
                    }
                    else if (vvCell[i][m] == CELL_GREEN)
                    {
                        backGraphics.FillRectangle(Brushes.Green, tx, ty, CELL_SIZE, CELL_SIZE);
                    }
                    else if (vvCell[i][m] == CELL_BLUE)
                    {
                        backGraphics.FillRectangle(Brushes.Blue, tx, ty, CELL_SIZE, CELL_SIZE);
                    }
                }
            }
            //畫4*4
            for (int i = 0; i < MOVE_CELL_H_COUNT; i++)
            {
                for (int m = 0; m < MOVE_CELL_W_COUNT; m++)
                {
                    //i = 直的 , m=衡的
                    int tx = (m + moveCellX) * CELL_SIZE;
                    int ty = (i + moveCellY) * CELL_SIZE;

                    if (vvMoveCell[i][m] == CELL_RED)
                    {
                        backGraphics.FillRectangle(Brushes.Red, tx, ty, CELL_SIZE, CELL_SIZE);
                    }
                    else if (vvMoveCell[i][m] == CELL_GREEN)
                    {
                        backGraphics.FillRectangle(Brushes.Green, tx, ty, CELL_SIZE, CELL_SIZE);
                    }
                    else if (vvMoveCell[i][m] == CELL_BLUE)
                    {
                        backGraphics.FillRectangle(Brushes.Blue, tx, ty, CELL_SIZE, CELL_SIZE);
                    }
                }
            }


            //把背景頁畫到視窗頁上面
            wndGraphics.DrawImageUnscaled(backBmp, 0, 0);

            //Invalidate();//通知重繪畫面，把背景塗白然後重繪
        }
        private bool checkOverlap()//檢查4*4k的cell和20*20的cell,實心的部分有沒有重疊
        {
            for (int i = 0; i < MOVE_CELL_H_COUNT; i++)
            {
                for (int m = 0; m < MOVE_CELL_W_COUNT; m++)
                {
                    //i = 直的 , m=衡的 小方塊座標
                    if (vvMoveCell[i][m] != CELL_NULL)
                    {
                        //4*4的座標,轉20*10的座標
                        int tx = (m + moveCellX);
                        int ty = (i + moveCellY);

                        //判斷4*4的實心格有沒有重疊到20*10的實心格
                        if (vvCell[ty][tx] != CELL_NULL)
                                return true;//有重疊
                    }
                }
            }
            return false;//沒重疊
        }
        private bool checkOutside()//檢查有沒有出界
        {
            for (int i = 0; i < MOVE_CELL_H_COUNT; i++)
            {
                for (int m = 0; m < MOVE_CELL_W_COUNT; m++)
                {
                    //i = 直的 , m=衡的 小方塊座標
                    if (vvMoveCell[i][m] != CELL_NULL)
                    {
                        //把小方塊棋盤的座標轉換成大方塊的座標
                        int tx = (m + moveCellX);
                        int ty = (i + moveCellY);
                        if (tx >= CELL_W_COUNT)// 右邊出界
                            return true;
                        if (tx < 0) //左邊出界
                            return true;
                        if (ty >= CELL_H_COUNT) //下面出界
                            return true;
                    }
                }
            }
            return false;
        }
        private void copyMoveCell()
        {
            for (int i = 0; i < MOVE_CELL_H_COUNT; i++)
            {
                for (int m = 0; m < MOVE_CELL_W_COUNT; m++)
                {
                    //i = 直的 , m=衡的 小方塊座標
                    if (vvMoveCell[i][m] != CELL_NULL)
                    {
                        //把小方塊棋盤的座標轉換成大方塊的座標
                        int tx = (m + moveCellX);
                        int ty = (i + moveCellY);

                        vvCell[ty][tx] = vvMoveCell[i][m];

                    }
                }
            }

        }
        private void onTimer(object sender, EventArgs e)
        {
            //on timer 就是主迴圈 main loop             
            keySpace.onTimer();//bPress
            keyUp.onTimer();
            keyDown.onTimer();
            keyLeft.onTimer();
            keyRight.onTimer();

            //記下原本的座標
            int oriX = moveCellX;
            int oriY = moveCellY;

            //操作方塊移動設定 下、右、左

            if (keyLeft.isPress())
                moveCellX--;
            if (keyRight.isPress())
                moveCellX++;
            checkOverlap();

            //檢查4*4內的塗色方塊有沒有出界
            //如果有出界，拉回原位
            if (checkOutside())
            {
                moveCellX = oriX;
                moveCellY = oriY;
            }

            oriX = moveCellX;//記下原本的座標
            oriY = moveCellY;

            if (keyDown.isPress())
            {
                moveCellY++;
                if (checkOutside() || checkOverlap())//撞底了(出界)
                {
                    moveCellX = oriX;//拉回原位
                    moveCellY = oriY;
                    copyMoveCell();
                    moveCellX = 0;
                    moveCellY = 0;
                }
            }
            drawGame();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            keySpace.onKeyDown(e.KeyCode);
            keyUp.onKeyDown(e.KeyCode);
            keyDown.onKeyDown(e.KeyCode);
            keyLeft.onKeyDown(e.KeyCode);
            keyRight.onKeyDown(e.KeyCode);
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            keySpace.onKeyUp(e.KeyCode);
            keyUp.onKeyUp(e.KeyCode);
            keyDown.onKeyUp(e.KeyCode);
            keyLeft.onKeyUp(e.KeyCode);
            keyRight.onKeyUp(e.KeyCode);
        }
    }

    //按鍵狀態
    class KeyState
    {
        //Keys不是一個類別
        private Keys theKey;//存放一個對應的按鍵編號

        int repeat;     //通知幾次
        bool bPress;    //狀態,是否剛剛按下按下
        bool bDown;     //狀態,目前這次是否按著
        bool pPreDown;  //上次onTimer是否壓著

        //建構,傳入一個按鍵編號 把它記下來
        public KeyState(Keys k)
        {
            theKey = k;
            bPress = false; //剛剛壓下去
            bDown = false;
            repeat = 0;
        }

        public void onKeyDown(Keys k)//偵測按鍵
        {
            if (theKey == k)
            {
                /*
                //偵測到同一個按鍵,按下去
                if (bDown == false)//是否原本是放開的
                {
                    bPress = true;//剛剛壓著的通知
                }
                else
                {
                    bPress = false; // 原本就是壓著的,持續壓著的通知
                }
                */
                bDown = true;
                repeat++;
            }
        }
        public void onKeyUp(Keys k)//偵測按鍵
        {
            if (theKey == k)
            {
                //偵測到同一個按鍵,放開
                bDown = false;
                bPress = false;
                pPreDown = false;
                repeat = 0;
            }
        }

        public bool isPress()//回報是否剛剛按下按鍵
        {
            return bPress;
        }

        public void onTimer()//timer通知時呼叫
        {
            //bPress的偵測
            if (bDown == true)//此時是壓著的
            {
                if (pPreDown == false)//上次是放開的
                {
                    bPress = true;
                }
                else//上次是壓著的
                {
                    bPress = false;
                }
            }
            else
            {
                //這次是放開的
                bPress = false;
            }
            pPreDown = bDown;//把這次的狀態記下來,下次就可以用這個狀態
        }

        public bool isDown()//回報是否壓著
        {
            return bDown;
        }
    }
}
