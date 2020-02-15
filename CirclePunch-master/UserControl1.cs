using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormCirclePunch
{
    public partial class UserControl1 : UserControl
    {
        //宣告變數
        private Point player;
        private Point[] vMonster;
        //private Point[] vBullet;
        private Bullet[] vBullet;//子彈的座標

        private Random random;

        private Graphics wndGraphics; //視窗畫布
        private Graphics backGraphics;//背景頁畫布
        private Bitmap backBmp;//點陣圖

        private float makeMonsterTime; //生怪的時間
        private float makeBulletTime; //再過多久生子彈的時間

        //設定常數
        private const float BALL_SIZE = 25; //圓形半徑
        private const int MAX_ENEMY = 10; //敵人最大數量
        private const int MAX_BULLET = 10; //子彈最大數量
        private const int PLAYER_SPEED = 10;
        private const float MAKE_MONSTER_TIME = 3; //每3秒生怪
        private const float MAKE_BULLET_TIME = 0.2f; //每0.2秒生子彈
        private const float REQUIRE_FPS = 60; //預期的畫面更新率(每秒呼叫幾次OnTimer)

        private Point mousePos;
        //布林值
        //private bool isSpaceDown;//空白鍵是否按著
        private KeyState keySpace;//空白鍵
        private KeyState keyUp;   //上
        private KeyState keyDown; //下
        private KeyState keyLeft; //左
        private KeyState keyRight;//右

        //設定範圍
        private const int VIEW_W = 800;
        private const int VIEW_H = 600;

        //運作流程 = UserControl1() → onTimer() → onPaint() → drawGame()
        public UserControl1()
        {
            InitializeComponent();

            //isSpaceDown = false;
            keySpace = new KeyState(Keys.Space);
            keyUp = new KeyState(Keys.Up);   //上
            keyDown = new KeyState(Keys.Down); //下
            keyLeft = new KeyState(Keys.Left); //左
            keyRight = new KeyState(Keys.Right);

            makeBulletTime = 0;

            makeMonsterTime = MAKE_MONSTER_TIME;


            wndGraphics = CreateGraphics();//建立視窗畫布

            backBmp = new Bitmap(VIEW_W, VIEW_H);//建立點陣圖物件
            backGraphics = Graphics.FromImage(backBmp);//建立背景畫布

            random = new Random();

            mousePos = new Point();

            //宣告圈圈座標
            player = new Point();
            player.x = 200;
            player.y = 100;

            vMonster = new Point[MAX_ENEMY];
            vBullet = new Bullet[MAX_BULLET];


            for (int i = 0; i < MAX_ENEMY; i++)
            {
                vMonster[i] = new Point();
                vMonster[i].x = random.Next(0, VIEW_W);
                vMonster[i].y = random.Next(0, VIEW_H);
            }

            //timer1 = 計時器物件
            //timer.Interval 多久響
            //單位千分之一秒
            timer1.Interval = 1000 / (int)REQUIRE_FPS; //1/30秒
            timer1.Start();
        }

        private void onPaint(object sender, PaintEventArgs e)
        {
            drawGame();
            ////參數: x,y,w,h
            ////主角
            //e.Graphics.DrawEllipse(Pens.Blue, player.x, player.y, BALL_SIZE * 2, BALL_SIZE * 2);

            ////子彈,按下按鍵 = 不等於null時才會產生子彈
            ////子彈數量小於MAX值時，按下按鍵就會繼續產生子彈
            //for (int i = 0; i < MAX_BULLET; i++)
            //{
            //    if (vBullet[i] != null)
            //    {
            //        e.Graphics.DrawEllipse(Pens.Black, vBullet[i].x, vBullet[i].y, BALL_SIZE, BALL_SIZE);
            //    }
            //}

            ////怪物
            //for (int i = 0; i < MAX_ENEMY; i++)
            //{
            //    if (vMonster[i] != null)
            //    {
            //        e.Graphics.DrawEllipse(Pens.Red, vMonster[i].x, vMonster[i].y, BALL_SIZE * 2, BALL_SIZE * 2);
            //    }
            //}
        }

        private void moveMonster()
        {
            //當兩點距離不等於零，取得下一步的座標
            for (int i = 0; i < MAX_ENEMY; i++)
            {
                //呼叫玩家的取距離的方法
                //傳入怪物的參考(Refrence) 
                // float L = player.getDistance(vMonster[i]);
                // float L = vMonster[i].getDistance(player);

                if (vMonster[i] != null)
                {
                    //把move做成方法來呼叫
                    vMonster[i].move(player);

                    /*
                    //取怪物和玩家的距離,小於兩個圓形半徑
                    if (vMonster[i].getDistance(player) < BALL_SIZE + BALL_SIZE)
                    {
                        //重疊,把怪砍掉
                        vMonster[i] = null;
                    }
                    */
                }
            }

        }
        private void moveBullet_killMonster()
        {
            for (int i = 0; i < MAX_BULLET; i++)
            {
                if (vBullet[i] != null)
                {
                    //vBullet[i].x += BULLET_SPEED;
                    vBullet[i].move();

                    //檢查有沒有打到怪
                    for (int m = 0; m < MAX_ENEMY; m++)
                    {
                        if (vMonster[m] != null)
                        {
                            //vMonster:Point
                            //vBullet:Bullet
                            //getDistance(可以傳入子類別的參數)
                            //取得怪物和子彈的距離< 兩個人的半徑(重疊)
                            //就把怪和子彈砍掉
                            if (vMonster[m].getDistance(vBullet[i]) < BALL_SIZE + BALL_SIZE)
                            {
                                vBullet[i] = null;
                                vMonster[m] = null;
                                break;
                            }
                        }
                    }
                    //第二次檢查, 如果子彈沒打到怪
                    if (vBullet[i] != null)
                    {

                        if (vBullet[i].x > VIEW_W)
                        {
                            vBullet[i] = null;
                        }
                        else if (vBullet[i].y > VIEW_H)
                        {
                            vBullet[i] = null;
                        }
                        else if (vBullet[i].x < 0)
                        {
                            vBullet[i] = null;
                        }
                        else if (vBullet[i].y < 0)
                        {
                            vBullet[i] = null;
                        }

                    }
                }
                //if (L > 5)
                //{
                //    tx = vMonster[i].x + (player.x - vMonster[i].x) * 5 / L;
                //    ty = vMonster[i].y + (player.y - vMonster[i].y) * 5 / L;
                //    vMonster[i].x = tx;
                //    vMonster[i].y = ty;
                //}
                //else
                //{
                //    tx = player.x;
                //    ty = player.y;
                //}
            }

        }

        private void drawGame()
        {
            //畫出畫布
            backGraphics.FillRectangle(Brushes.White, 0, 0, VIEW_W, VIEW_H);

            //把物件畫在背景頁畫布上
            backGraphics.DrawEllipse(Pens.Blue, player.x, player.y, BALL_SIZE * 2, BALL_SIZE * 2);

            //畫出子彈
            //int total = MAX_BULLET; //計算子彈的總數
            //for (int i = 0; i < MAX_BULLET; i++)
            //{
            //    if (vBullet[i] != null)
            //    {
            //        total--;
            //        backGraphics.DrawEllipse(Pens.Black, vBullet[i].x, vBullet[i].y, BALL_SIZE, BALL_SIZE);
            //    }
            //}

            //畫出子彈
            int total = MAX_BULLET; //計算子彈的總數
            for (int i = 0; i < MAX_BULLET; i++)
            {
                Bullet bullet = vBullet[i];
                if (bullet == null)
                    continue;
                total--;
                backGraphics.DrawEllipse(Pens.Black, bullet.x, bullet.y, BALL_SIZE, BALL_SIZE);

            }

            //顯示子彈的數量
            String str = "子彈數量: " + total;
            backGraphics.DrawString(str, SystemFonts.CaptionFont, Brushes.Black, 0, 0);

            //畫出怪
            total = 0;
            for (int i = 0; i < MAX_ENEMY; i++)
            {

                if (vMonster[i] != null)
                {
                    total++;
                    backGraphics.DrawEllipse(Pens.Red, vMonster[i].x, vMonster[i].y, BALL_SIZE * 2, BALL_SIZE * 2);
                }
            }

            //顯示怪物數量
            str = "怪物數量: " + total;
            backGraphics.DrawString(str, SystemFonts.CaptionFont, Brushes.Black, 0, 20);

            //顯示是否按下Space按鍵
            if (keySpace.isDown())  //同等於 isSpaceDown == true
            {
                backGraphics.DrawString("space down: " + makeBulletTime, SystemFonts.CaptionFont, Brushes.Blue, 0, 40);
            }
            else
            {
                backGraphics.DrawString("space up: " + makeBulletTime, SystemFonts.CaptionFont, Brushes.Red, 0, 40);
            }

            //把背景頁畫到視窗頁上面
            wndGraphics.DrawImageUnscaled(backBmp, 0, 0);


            //Invalidate();//通知重繪畫面，把背景塗白然後重繪
        }

        //定時呼叫onTimer
        //時間到了就會呼叫onTimer
        private void onTimer(object sender, EventArgs e)
        {
            //on timer 就是主迴圈 main loop 
            /*
            keySpace.onTimer();
            keyUp.onTimer();
            keyDown.onTimer();
            keyLeft.onTimer();
            keyRight.onTimer();
            */
            /*
            if (keySpace.isPress())//剛剛壓下去
            {
                //發射子彈
                fireBullet();
            }
            */
            if (keySpace.isDown())//持續壓著
            {
                //按住空白鍵
                makeBulletTime -= 1.0f / REQUIRE_FPS;
                if (makeBulletTime <= 0)
                {
                    //發射子彈的時間到了
                    fireBullet();
                }
            }

            if (keyUp.isDown())
            {
                player.y -= PLAYER_SPEED;
            }
            if (keyDown.isDown())
            {
                player.y += PLAYER_SPEED;
            }
            if (keyLeft.isDown())
            {
                player.x -= PLAYER_SPEED;
            }
            if (keyRight.isDown())
            {
                player.y += PLAYER_SPEED;
            }

            makeMonsterTime -= 1.0f / REQUIRE_FPS; // 1/30秒

            if (makeMonsterTime <= 0)
            {
                //生怪時間到了
                for (int i = 0; i < MAX_ENEMY; i++)
                {
                    if (vMonster[i] == null)
                    {
                        vMonster[i] = new Point();

                        vMonster[i].x = random.Next(0, VIEW_H);
                        vMonster[i].y = random.Next(0, VIEW_W);
                        break;
                    }
                }
                makeMonsterTime = MAKE_MONSTER_TIME;
            }


            moveMonster();

            moveBullet_killMonster();

            drawGame();

        }

        private void fireBullet()
        {
            for (int i = 0; i < MAX_BULLET; i++)
            {
                if (vBullet[i] == null)
                {
                    vBullet[i] = new Bullet(player, mousePos);
                    //子彈的座標=玩家座標
                    vBullet[i].x = player.x;
                    vBullet[i].y = player.y;
                    makeBulletTime = MAKE_BULLET_TIME;
                    break;
                }
            }
        }


        //滑鼠移動的通知
        private void onMouseMove(object sender, MouseEventArgs e)
        {
            mousePos.x = e.X;
            mousePos.y = e.Y;
        }

        private void onKeyPress(object sender, KeyPressEventArgs e)
        {


        }

        //按鍵放開的通知
        private void onKeyUp(object sender, KeyEventArgs e)
        {
            keySpace.onKeyUp(e.KeyCode);
            keyUp.onKeyUp(e.KeyCode);
            keyDown.onKeyUp(e.KeyCode);
            keyLeft.onKeyUp(e.KeyCode);
            keyRight.onKeyUp(e.KeyCode);
            /*
            if (e.KeyCode == Keys.Space)
            {
                isSpaceDown = false;
            }
            */
        }

        //按下某顆按鍵的通知
        private void onKeyDown(object sender, KeyEventArgs e)
        {
            
            //e.KeyChar 按鍵編號
            if (e.KeyCode == Keys.D)//d100
                player.x += PLAYER_SPEED;
            if (e.KeyCode == Keys.A)//A97
                player.x -= PLAYER_SPEED;
            if (e.KeyCode == Keys.S)//s115
                player.y += PLAYER_SPEED;
            if (e.KeyCode == Keys.W)//w119 
                player.y -= PLAYER_SPEED;
            

            keySpace.onKeyDown(e.KeyCode);
            keyUp.onKeyDown(e.KeyCode);
            keyDown.onKeyDown(e.KeyCode);
            keyLeft.onKeyDown(e.KeyCode);
            keyRight.onKeyDown(e.KeyCode);

            //按下按鍵就發射子彈
            /*
            if (e.KeyCode == Keys.Space)
            {
                if (isSpaceDown)
                {
                    //系統通知的連發,不要理他
                    //亦即按住Space鍵只會發一顆子彈
                }
                else
                {
                    //原本放開,剛剛按下去
                    fireBullet();//發射子彈
                }
                isSpaceDown = true;
            }
            */
        }

    }

    //按鍵狀態
    class KeyState
    {
        //Keys不是一個類別
        private Keys theKey;         //存放一個對應的按鍵編號

        bool bPress; //狀態,是否按下
        int repeat; //通知幾次
        bool bDown;  //狀態,是否按著

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
                //偵測到同一個按鍵,按下去
                if (bDown == false)//是否原本是放開的
                {
                    bPress = true;//剛剛壓著的通知
                }
                else
                {
                    bPress = false; // 原本就是壓著的,持續壓著的通知
                }
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
                repeat = 0;
            }
        }
        public bool isPress()//回報是否剛剛按下按鍵
        {
         return bPress;
        }
        /*
        public void onTimer()
        {

        }
        */
        public bool isDown()//回報是否壓著
        {
            return bDown;
        }
    }
    //類別
    //存放座標資料
    class Point
    {
        //宣告x,y座標
        public float x, y;

        //宣告移動速度
        private const int MONSTER_SPEED = 1;

        //x,y == 呼叫者的x,y
        //point對應到傳進來的物件
        //求兩點距離 = (x-vX[0])平方 + (y-vY[0])平方,開根號,squt取開根號
        public float getDistance(Point point)
        {
            float L = (float)Math.Sqrt(
                (x - point.x) * (x - point.x) +
                (y - point.y) * (y - point.y));

            return L;
        }

        public void move(Point target)
        {
            //求兩點距離,呼叫getDistance方法
            float L = getDistance(target);

            //往目標移動
            //x,y == 自己的座標
            //target.x,target.y == 對方的座標
            //tx,ty == 移動後的新座標

            float tx, ty;
            if (L > MONSTER_SPEED)
            {
                tx = x + (target.x - x) * MONSTER_SPEED / L;
                ty = y + (target.y - y) * MONSTER_SPEED / L;

                x = tx;
                y = ty;
            }
            else
            {
                //距離很近,把自己的座標設定成跟對方一樣
                x = target.x;
                y = target.y;
            }
        }
    }
    //繼承
    //加強,修改父類別
    //定義類別時加入:Point
    //Bullet 子類別
    //Point父類別
    //子類別會擁有父類別所有的功能(方法).資料
    class Bullet : Point
    {
        private Point moveDir;//移動的向量
        private const int BULLET_SPEED = 5;

        //建構方法
        //方法名稱和類別名稱相同
        //當new某個物件(實體)的時候
        //會呼叫這個物件(實體)的建構方法
        public Bullet(Point pos, Point mousePos)//建構
        {
            //設定自己(主角)的座標
            x = pos.x;
            y = pos.y;

            //取主角與滑鼠的距離
            float dist = getDistance(mousePos);

            //產生方向的物件
            moveDir = new Point();
            //計算出滑鼠與主角之間的向量
            moveDir.x = (mousePos.x - pos.x) / dist * BULLET_SPEED;
            moveDir.y = (mousePos.y - pos.y) / dist * BULLET_SPEED;


        }

        public void move()
        {
            //base. 使用父類別的move功能
            x += moveDir.x;
            y += moveDir.y;
        }

    }
}
