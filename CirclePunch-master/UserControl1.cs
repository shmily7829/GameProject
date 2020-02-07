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
        private Point[] vBullet;

        private Random random;

        private Graphics wndGraphics; //視窗畫布
        private Graphics backGraphics;//背景頁畫布
        private Bitmap backBmp;//點陣圖

        private float makeMonsterTime; //生怪的時間

        //設定常數
        private const float BALL_SIZE = 25; //圓形半徑
        private const int MAX_ENEMY = 10; //敵人最大數量
        private const int MAX_BULLET = 5; //子彈最大數量
        private const int BULLET_SPEED = 5;
        private const int PLAYER_SPEED = 10;
        private const float MAKE_MONSTER_TIME = 3;

        //設定範圍
        private const int VIEW_W = 1024;
        private const int VIEW_H = 768;

        //運作流程 = UserControl1() → onTimer() → onPaint()
        public UserControl1()
        {
            InitializeComponent();

            makeMonsterTime = MAKE_MONSTER_TIME;

            wndGraphics = CreateGraphics();//建立視窗畫布

            backBmp = new Bitmap(VIEW_W, VIEW_H);//建立點陣圖物件
            backGraphics = Graphics.FromImage(backBmp);//建立背景畫布

            random = new Random();

            //宣告圈圈座標
            player = new Point();
            player.x = 200;
            player.y = 100;

            vMonster = new Point[MAX_ENEMY];
            vBullet = new Point[MAX_BULLET];


            for (int i = 0; i < MAX_ENEMY; i++)
            {
                vMonster[i] = new Point();
                vMonster[i].x = random.Next(0, VIEW_W);
                vMonster[i].y = random.Next(0, VIEW_H);
            }

            //timer1 = 計時器物件
            //timer.Interval 多久響
            //單位千分之一秒
            timer1.Interval = 1000 / 30;
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
                    vBullet[i].x += BULLET_SPEED;

                    //檢查有沒有打到怪
                    for (int m = 0; m < MAX_ENEMY; m++)
                    {
                        if (vMonster[m] != null)
                        {   //取得怪物和子彈的距離< 兩個人的半徑(重疊)
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
            backGraphics.FillRectangle(Brushes.White, 0, 0, VIEW_W, VIEW_H);
            //把物件畫在背A景頁畫布上
            backGraphics.DrawEllipse(Pens.Blue, player.x, player.y, BALL_SIZE * 2, BALL_SIZE * 2);

            for (int i = 0; i < MAX_BULLET; i++)
            {
                if (vBullet[i] != null)
                {
                    backGraphics.DrawEllipse(Pens.Black, vBullet[i].x, vBullet[i].y, BALL_SIZE, BALL_SIZE);
                }
            }
            for (int i = 0; i < MAX_ENEMY; i++)
            {
                if (vMonster[i] != null)
                {
                    backGraphics.DrawEllipse(Pens.Red, vMonster[i].x, vMonster[i].y, BALL_SIZE * 2, BALL_SIZE * 2);
                }
            }
            //把背景頁畫到視窗頁上面
            wndGraphics.DrawImageUnscaled(backBmp, 0, 0);


            //Invalidate();//通知重繪畫面，把背景塗白然後重繪
        }

        //定時呼叫onTimer
        //時間到了就會呼叫onTimer
        private void onTimer(object sender, EventArgs e)
        {
            makeMonsterTime -= 1.0f / 30.0f; // 1/30秒
            if (makeMonsterTime <=0)
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

        private void onKeyPress(object sender, KeyPressEventArgs e)
        {
            //e.KeyChar 按鍵編號
            if (e.KeyChar == 'd')//d100
                player.x += PLAYER_SPEED;
            if (e.KeyChar == 'a')//A97
                player.x -= PLAYER_SPEED;
            if (e.KeyChar == 's')//s115
                player.y += PLAYER_SPEED;
            if (e.KeyChar == 'w')//w119 
                player.y -= PLAYER_SPEED;

            //按下按鍵就發射子彈
            if (e.KeyChar == ' ')
            {
                for (int i = 0; i < MAX_BULLET; i++)
                {
                    if (vBullet[i] == null)
                    {
                        vBullet[i] = new Point();
                        //子彈的座標=玩家座標
                        vBullet[i].x = player.x;
                        vBullet[i].y = player.y;

                        break;
                    }
                }
            }
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
}
