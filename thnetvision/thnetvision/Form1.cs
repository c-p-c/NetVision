// 参考：http://asuka-diary.at.webry.info/201007/article_9.html

using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace thnetvision
{
    public partial class Form1 : Form
    {
        // 定数
        public const int CARDSIZEX = 77;
        public const int CARDSIZEY = 108;

        // staticでフォームのインスタンスを生成
        private static Form2 fm2;
        private static Form3 fm3;

        // 変数
        private SECTION dragFrom = SECTION.HOMEFIELD;
        private Point diffPoint;
        private int selectedCard = -1;
        public bool dragging;
        // インスタンス
        private NetvisionCore core = new NetvisionCore();
        private CardDB[] cardDB = new CardDB[5000];
        private Bitmap backImg = new Bitmap(@"card\\Library.jpg");
        private Bitmap lifeImg = new Bitmap(@"LifeCounter.jpg");
        private PictureBox[] PB = new PictureBox[100];

        //============================== コンストラクタ ==============================
        public Form1()
        {
            InitializeComponent();
            homeLife.Parent = homeLifePicture;
            homeLife.Left = homeLifePicture.Width - homeLife.Width;
            homeLife.Top = homeLifePicture.Height - homeLife.Height;
        }

        //============================== Form初期化処理 ==============================
        private void Form1_Load(object sender, EventArgs e)
        {
            ReadCardlist();

            for (int i = 0; i < 100; i++)
            {
                core.cards[i] = new Card(i, 1);
                core.cards[i].Location = new Point(0, 0);
            }

            //描画
            for (int i = 0; i < 100; i++)
            {
                // PB[]の初期化
                PB[i] = new PictureBox();
                PB[i].BorderStyle = BorderStyle.None;
                PB[i].Paint += new PaintEventHandler(this.Card_Paint);
                PB[i].MouseDown += new MouseEventHandler(this.Card_MouseDown);
                PB[i].MouseUp += new MouseEventHandler(this.Card_MouseUp);
                PB[i].MouseMove += new MouseEventHandler(this.Card_MouseMove);
                PB[i].ContextMenuStrip = CardMenuStrip1;
                PB[i].Size = new Size(CARDSIZEX, CARDSIZEY);
                PB[i].Tag = i;
                PB[i].Location = core.cards[i].Location;
            }
        }

        //============================== 全て再描画 ==============================
        private delegate void RedrawAll_Delegate();
        private void RedrawAll()
        {
            //----------別スレッドからの呼び出しに対応----------
            if (InvokeRequired)
            {
                Invoke(new RedrawAll_Delegate(RedrawAll));
                return;
            }

            //----------HOME手札の整理----------
            for (int i = 0; i < core.home.handNum; i++)
            {
                //位置
                int x;
                double w;

                if (CARDSIZEX * core.home.handNum <= homeHand.Width - 20)
                {
                    x = CARDSIZEX * core.home.handNum;
                    w = (double)CARDSIZEX;
                }
                else
                {
                    x = homeHand.Width - 20;
                    w = (double)(homeHand.Width - 20 - CARDSIZEX) / (double)(core.home.handNum - 1);
                }
                core.cards[core.home.handOrder[i]].Location = new Point(homeHand.Width / 2 - x / 2 + (int)((double)i * w), 5);
                core.cards[core.home.handOrder[i]].section = SECTION.HOMEHAND; //念のため上書き
                core.cards[core.home.handOrder[i]].active = true;

                //重なり順
                if (core.home.handOrder[i] == selectedCard)
                {
                    for (int j = 0; j < core.home.handNum; j++)
                    {
                        PB[core.home.handOrder[j]].BringToFront();
                        if (core.home.handOrder[j] == selectedCard) break;
                    }
                    for (int j = core.home.handNum - 1; j >= 0; j--)
                    {
                        PB[core.home.handOrder[j]].BringToFront();
                        if (core.home.handOrder[j] == selectedCard) break;
                    }
                }
            }

            //----------AWAY手札の整理----------
            for (int i = 0; i < core.away.handNum; i++)
            {
                //位置
                int x;
                double w;

                if (CARDSIZEX * core.away.handNum <= awayHand.Width - 20)
                {
                    x = CARDSIZEX * core.away.handNum;
                    w = (double)CARDSIZEX;
                }
                else
                {
                    x = awayHand.Width - 20;
                    w = (double)(awayHand.Width - 20 - CARDSIZEX) / (double)(core.away.handNum - 1);
                }
                core.cards[core.away.handOrder[i]].Location = new Point(awayHand.Width / 2 - x / 2 + (int)((double)i * w), 5);
                core.cards[core.away.handOrder[i]].section = SECTION.AWAYHAND; //念のため上書き
                core.cards[core.away.handOrder[i]].active = true;

                //重なり順
                if (core.away.handOrder[i] == selectedCard)
                {
                    for (int j = 0; j < core.away.handNum; j++)
                    {
                        PB[core.away.handOrder[j]].BringToFront();
                        if (core.away.handOrder[j] == selectedCard) break;
                    }
                    for (int j = core.away.handNum - 1; j >= 0; j--)
                    {
                        PB[core.away.handOrder[j]].BringToFront();
                        if (core.away.handOrder[j] == selectedCard) break;
                    }
                }
            }

            //----------カードの描画----------
            for (int i = 0; i < 100; i++)
            {
                switch (core.cards[i].section)
                {
                    case SECTION.NONE:
                        PB[i].Parent = null; PB[i].Visible = false; break;

                    case SECTION.HOMEFIELD:
                        PB[i].Parent = homeField; PB[i].Visible = true; break;
                    case SECTION.HOMEHAND:
                        PB[i].Parent = homeHand; PB[i].Visible = true; break;
                    case SECTION.HOMENODEACTIVE:
                        PB[i].Parent = null; PB[i].Visible = false; break;
                    case SECTION.HOMENODESLEEP:
                        PB[i].Parent = null; PB[i].Visible = false; break;
                    case SECTION.HOMELIBRARY:
                        PB[i].Parent = null; PB[i].Visible = false; break;
                    case SECTION.HOMEHADES:
                        PB[i].Parent = null; PB[i].Visible = false; break;

                    case SECTION.AWAYFIELD:
                        PB[i].Parent = awayField; PB[i].Visible = true; break;
                    case SECTION.AWAYHAND:
                        PB[i].Parent = awayHand; PB[i].Visible = true; break;
                    case SECTION.AWAYNODEACTIVE:
                        PB[i].Parent = null; PB[i].Visible = false; break;
                    case SECTION.AWAYNODESLEEP:
                        PB[i].Parent = null; PB[i].Visible = false; break;
                    case SECTION.AWAYLIBRARY:
                        PB[i].Parent = null; PB[i].Visible = false; break;
                    case SECTION.AWAYHADES:
                        PB[i].Parent = null; PB[i].Visible = false; break;
                }
                PB[i].Location = core.cards[i].Location;
                PB[i].Size = core.cards[i].active ? new Size(CARDSIZEX, CARDSIZEY) : new Size(CARDSIZEY, CARDSIZEX);
                PB[i].Refresh();
            }
            if (selectedCard != -1) PB[selectedCard].BringToFront();
            label10.Text = core.home.handNum.ToString();
            label10.BringToFront();

            homeLibrary.Refresh();
            awayLibrary.Refresh();
            homeHades.Refresh();
            awayHades.Refresh();
            homeNodeActive.Refresh();
            homeNodeSleep.Refresh();
            awayNodeActive.Refresh();
            awayNodeSleep.Refresh();

            cardDetailPB.Refresh();

            homeLife.Value = core.home.life;
            homeLifePicture.Refresh();
            awayLifePicture.Refresh();
        }

        //============================== 再描画処理(Paint) ==============================
        // カード
        private void Card_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.Clear(pb.BackColor);

            Bitmap img;
            Card card = core.cards[(int)(pb.Tag)];

            if (card.section == SECTION.HOMEFIELD
                || card.section == SECTION.HOMEHAND
                || card.section == SECTION.AWAYFIELD
                || card.section == SECTION.AWAYHAND)
            {
                //----------カードイメージ作成----------
                if (card.open || card.section == SECTION.HOMEHAND)
                {
                    if (cardDB[card.no].bmp != null)
                    {
                        img = cardDB[card.no].bmp;
                    }
                    else
                    {
                        img = VisionFunctions.DrawCard(cardDB, card, CARDSIZEX, CARDSIZEY);
                    }
                }
                else
                {
                    img = backImg;
                }

                Bitmap img2;
                Graphics g2;
                //----------アクティブorスリープで描画----------
                if (card.active)
                {
                    img2 = new Bitmap(CARDSIZEX, CARDSIZEY);
                    g2 = Graphics.FromImage(img2);
                    g2.DrawImage(img, 0, 0, pb.Width, pb.Height);
                }
                else
                {
                    img2 = new Bitmap(CARDSIZEY, CARDSIZEX);
                    g2 = Graphics.FromImage(img2);
                    Point[] pt = new Point[3];
                    pt[0] = new Point(pb.Width, 0);
                    pt[1] = new Point(pb.Width, pb.Height);
                    pt[2] = new Point(0, 0);
                    g2.DrawImage(img, pt);
                }

                //----------AWAYの場合上下反転----------
                if (card.section == SECTION.AWAYFIELD || card.section == SECTION.AWAYHAND)
                {
                    img2.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }
                g.DrawImage(img2, 0, 0);

                //----------カード選択枠描画----------
                if (card.id == selectedCard) DrawSelected(pb, g);
            }
        }

        // 山札
        private void homeLibrary_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.Clear(pb.BackColor);

            if (core.home.libraryNum > 0)
            {
                VisionFunctions.DrawImage_CenterBottom(g, backImg, pb);
            }
            //----------カード枚数の描画----------
            Rectangle area = new Rectangle(pb.Width - 1 - 40, pb.Height - 1 - 20, 40, 20);
            g.FillRectangle(Brushes.White, area);
            g.DrawRectangle(Pens.Gray, area);
            StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(core.home.libraryNum.ToString(), new Font("MS UI Gothic", 12, FontStyle.Bold), Brushes.Black, area, stringFormat);
        }

        private void awayLibrary_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.Clear(pb.BackColor);

            Bitmap img = new Bitmap(pb.Width, pb.Height);
            Graphics g2 = Graphics.FromImage(img);

            if (core.away.libraryNum >= 1)
            {
                VisionFunctions.DrawImage_CenterBottom(g2, backImg, pb);
            }
            //----------カード枚数の描画----------
            Rectangle area = new Rectangle(pb.Width - 1 - 40, pb.Height - 1 - 20, 40, 20);
            g2.FillRectangle(Brushes.White, area);
            g2.DrawRectangle(Pens.Gray, area);
            StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g2.DrawString(core.away.libraryNum.ToString(), new Font("MS UI Gothic", 12, FontStyle.Bold), Brushes.Black, area, stringFormat);

            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            g.DrawImage(img, 0, 0);
        }

        // 冥界
        private void homeHades_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.Clear(pb.BackColor);

            if (core.home.hadesNum > 0)
            {
                int id = core.home.hadesOrder[core.home.hadesNum - 1];
                Bitmap img;
                //----------イメージの描画----------
                if (cardDB[core.cards[id].no].bmp != null)
                {
                    img = cardDB[core.cards[id].no].bmp;
                    VisionFunctions.DrawImage_CenterBottom(g, img, homeHades);
                }
                else
                {
                    img = VisionFunctions.DrawCard(cardDB, core.cards[id], homeHades.Width - 2, homeHades.Height - 2);
                    g.DrawImage(img, 0, 0);
                }
                //----------カードの選択枠を描画----------
                if (selectedCard != -1 && selectedCard == id)
                {
                    DrawSelected(homeHades, g);
                }
            }
            //----------カード枚数の描画----------
            Rectangle area = new Rectangle(homeHades.Width - 1 - 40, homeHades.Height - 1 - 20, 40, 20);
            g.FillRectangle(Brushes.White, area);
            g.DrawRectangle(Pens.Gray, area);
            StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(core.home.hadesNum.ToString(), new Font("MS UI Gothic", 12, FontStyle.Bold), Brushes.Black, area, stringFormat);
        }

        private void awayHades_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.Clear(pb.BackColor);

            Bitmap img2 = new Bitmap(pb.Width, pb.Height);
            Graphics g2 = Graphics.FromImage(img2);

            if (core.away.hadesNum > 0)
            {
                int id = core.away.hadesOrder[core.away.hadesNum - 1];
                Bitmap img;
                //----------イメージの描画----------
                if (cardDB[core.cards[id].no].bmp != null)
                {
                    img = cardDB[core.cards[id].no].bmp;
                    VisionFunctions.DrawImage_CenterBottom(g2, img, pb);
                }
                else
                {
                    img = VisionFunctions.DrawCard(cardDB, core.cards[id], pb.Width - 2, pb.Height - 2);
                    g2.DrawImage(img, 0, 0);
                }
                //----------カードの選択枠を描画----------
                if (selectedCard != -1 && selectedCard == id)
                {
                    DrawSelected(pb, g2);
                }
            }
            //-----------カード枚数の描画----------
            Rectangle area = new Rectangle(pb.Width - 1 - 40, pb.Height - 1 - 20, 40, 20);
            g2.FillRectangle(Brushes.White, area);
            g2.DrawRectangle(Pens.Gray, area);
            StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g2.DrawString(core.away.hadesNum.ToString(), new Font("MS UI Gothic", 12, FontStyle.Bold), Brushes.Black, area, stringFormat);

            img2.RotateFlip(RotateFlipType.Rotate180FlipNone);
            g.DrawImage(img2, 0, 0);
        }

        //ノード
        private void homeNodeActive_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.Clear(pb.BackColor);

            if (core.home.nodeActiveNum >= 1)
            {
                g.DrawImage(backImg, 0, 0, pb.Width, pb.Height);
            }
            Rectangle area = new Rectangle(pb.Width - 1 - 40, pb.Height - 1 - 20, 40, 20);
            g.FillRectangle(Brushes.White, area);
            g.DrawRectangle(Pens.Gray, area);
            StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(core.home.nodeActiveNum.ToString(), new Font("MS UI Gothic", 12, FontStyle.Bold), Brushes.Black, area, stringFormat);
        }

        private void homeNodeSleep_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.Clear(pb.BackColor);

            if (core.home.nodeSleepNum >= 1)
            {
                Point[] pt = new Point[3];
                pt[0] = new Point(pb.Width, 0);
                pt[1] = new Point(pb.Width, pb.Height);
                pt[2] = new Point(0, 0);
                g.DrawImage(backImg, pt);
            }
            Rectangle area = new Rectangle(pb.Width - 1 - 40, pb.Height - 1 - 20, 40, 20);
            g.FillRectangle(Brushes.White, area);
            g.DrawRectangle(Pens.Gray, area);
            StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(core.home.nodeSleepNum.ToString(), new Font("MS UI Gothic", 12, FontStyle.Bold), Brushes.Black, area, stringFormat);
        }

        private void awayNodeActive_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.Clear(pb.BackColor);

            Bitmap img = new Bitmap(pb.Width, pb.Height);
            Graphics g2 = Graphics.FromImage(img);

            if (core.away.nodeActiveNum >= 1)
            {
                g2.DrawImage(backImg, 0, 0, pb.Width, pb.Height);
            }
            Rectangle area = new Rectangle(pb.Width - 1 - 40, pb.Height - 1 - 20, 40, 20);
            g2.FillRectangle(Brushes.White, area);
            g2.DrawRectangle(Pens.Gray, area);
            StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g2.DrawString(core.away.nodeActiveNum.ToString(), new Font("MS UI Gothic", 12, FontStyle.Bold), Brushes.Black, area, stringFormat);

            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            g.DrawImage(img, 0, 0);
        }

        private void awayNodeSleep_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.Clear(pb.BackColor);

            Bitmap img = new Bitmap(pb.Width, pb.Height);
            Graphics g2 = Graphics.FromImage(img);

            if (core.away.nodeSleepNum >= 1)
            {
                Point[] pt = new Point[3];
                pt[0] = new Point(pb.Width, 0);
                pt[1] = new Point(pb.Width, pb.Height);
                pt[2] = new Point(0, 0);
                g2.DrawImage(backImg, pt);
            }
            Rectangle area = new Rectangle(pb.Width - 1 - 40, pb.Height - 1 - 20, 40, 20);
            g2.FillRectangle(Brushes.White, area);
            g2.DrawRectangle(Pens.Gray, area);
            StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g2.DrawString(core.away.nodeSleepNum.ToString(), new Font("MS UI Gothic", 12, FontStyle.Bold), Brushes.Black, area, stringFormat);

            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            g.DrawImage(img, 0, 0);
        }

        // カード拡大画面
        private void cardDetailPB_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.Clear(pb.BackColor);

            string temptxt = "";

            if (selectedCard != -1)
            {
                Card card = core.cards[selectedCard];
                if (((card.section == SECTION.HOMEFIELD
                    || card.section == SECTION.AWAYFIELD
                    || card.section == SECTION.AWAYHAND)
                    && card.open)
                    || card.section == SECTION.HOMEHAND
                    || card.section == SECTION.HOMEHADES
                    || card.section == SECTION.AWAYHADES)
                {
                    if (cardDB[card.no].bmp != null)
                    {
                        VisionFunctions.DrawImage_CenterBottom(g, cardDB[card.no].bmp, pb);
                    }
                    else
                    {
                        VisionFunctions.DrawImage_Noimage(g, cardDB[card.no], pb);
                    }

                    if (cardDB[card.no].skill != "" || cardDB[card.no].upkeep != "")
                    {
                        if (cardDB[card.no].skill != "") temptxt += cardDB[card.no].skill;
                        if (cardDB[card.no].upkeep != "") temptxt += "　維持コスト（" + cardDB[card.no].upkeep + "）";
                        temptxt += Environment.NewLine;
                    }
                    temptxt += cardDB[card.no].ability;
                    cardDetailAtkDef.Text = (cardDB[card.no].attack + " / ") + cardDB[card.no].toughness;
                    cardDetailAtkDef.Visible = true;
                }
                else
                {
                    cardDetailAtkDef.Visible = false;
                }
            }
            else
            {
                cardDetailAtkDef.Visible = false;
            }
            cardDetailAbility.Text = temptxt;
            textBox2.Text = temptxt;
        }

        // ライフカウンタ
        private void homeLifePicture_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;
            g.DrawImage(lifeImg, 0, 0, pb.Width, pb.Height);
        }

        private void awayLifePicture_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;

            Bitmap img = new Bitmap(pb.Width, pb.Height);
            Graphics g2 = Graphics.FromImage(img);

            g2.DrawImage(lifeImg, 0, 0, pb.Width, pb.Height);

            Rectangle area = new Rectangle(pb.Width - 47, pb.Height - 20, 47 - 1, 20 - 1);
            g2.FillRectangle(Brushes.White, area);
            g2.DrawRectangle(Pens.Gray, area);
            StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
            area = new Rectangle(pb.Width - 40, pb.Height - 20, 28 - 1, 20 - 1);
            g2.DrawString(core.away.life.ToString(), new Font("MS UI Gothic", 10, FontStyle.Bold), Brushes.Black, area, stringFormat);

            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            g.DrawImage(img, 0, 0);
        }

        // その他
        private void button3_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            Graphics g = e.Graphics;
            Bitmap img = new Bitmap(btn.Width,btn.Height);
            Graphics g2 = Graphics.FromImage(img);
            Rectangle area = new Rectangle(0, 0, btn.Width, btn.Height);
            StringFormat stringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g2.DrawString("除外カード", new Font("MS UI Gothic", 9), Brushes.Black, area, stringFormat);
            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            g.DrawImage(img, 0, 0);
        }

        //----------汎用描画関数----------
        private void DrawSelected(PictureBox pb, Graphics g)
        {
            Pen myPen1 = new Pen(Color.FromArgb(0x22, 0x33, 0xaa), 3);
            g.DrawRectangle(myPen1, 1, 1, pb.Width - 3 - (pb.BorderStyle == BorderStyle.None ? 0 : 2), pb.Height - 3 - (pb.BorderStyle == BorderStyle.None ? 0 : 2));
            SolidBrush myBrush1 = new SolidBrush(Color.FromArgb(0x44, 0x22, 0x33, 0xaa));
            g.FillRectangle(myBrush1, 0, 0, pb.Width, pb.Height);
            myBrush1.Dispose();
            myPen1.Dispose();
        }

        //============================== マウス入出力 ==============================
        //----------ドラッグ開始----------
        // カードをドラッグ
        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                //----------カードクリック - カード選択----------
                selectedCard = (int)(((PictureBox)sender).Tag);
                PB[selectedCard].BringToFront();

                //----------ドラッグ開始----------
                dragFrom = core.cards[selectedCard].section;
                dragging = true;
                diffPoint = new Point(e.X, e.Y);

                RedrawAll();
            }
        }

        // 山札からドラッグ
        private void homeLibrary_MouseDown(object sender, MouseEventArgs e)
        {
            if (core.home.libraryNum > 0)
            {
                selectedCard = core.home.libraryOrder[core.home.libraryNum - 1];
                dragFrom = SECTION.HOMELIBRARY;
                dragging = true;
                diffPoint = new Point(CARDSIZEX / 2, CARDSIZEY / 2);
            }
            RedrawAll();
        }

        private void awayLibrary_MouseDown(object sender, MouseEventArgs e)
        {
            if (core.away.libraryNum > 0)
            {
                selectedCard = core.away.libraryOrder[core.away.libraryNum - 1];
                dragFrom = SECTION.AWAYLIBRARY;
                dragging = true;
                diffPoint = new Point(CARDSIZEX / 2, CARDSIZEY / 2);
            }
            RedrawAll();
        }

        //ノードからドラッグ
        private void homeNodeActive_MouseDown(object sender, MouseEventArgs e)
        {
            if (core.home.nodeActiveNum > 0)
            {
                selectedCard = core.home.nodeActiveOrder[core.home.nodeActiveNum - 1];
                dragFrom = SECTION.HOMENODEACTIVE;
                dragging = true;
                diffPoint = new Point(CARDSIZEX / 2, CARDSIZEY / 2);
            }
            RedrawAll();
        }

        private void homeNodeSleep_MouseDown(object sender, MouseEventArgs e)
        {
            if (core.home.nodeSleepNum > 0)
            {
                selectedCard = core.home.nodeSleepOrder[core.home.nodeSleepNum - 1];
                dragFrom = SECTION.HOMENODESLEEP;
                dragging = true;
                diffPoint = new Point(CARDSIZEX / 2, CARDSIZEY / 2);
            }
            RedrawAll();
        }

        // 冥界からドラッグ
        private void homeHades_MouseDown(object sender, MouseEventArgs e)
        {
            if (core.home.hadesNum > 0)
            {
                selectedCard = core.home.hadesOrder[core.home.hadesNum - 1];
                dragFrom = SECTION.HOMEHADES;
                dragging = true;
                diffPoint = new Point(CARDSIZEX / 2, CARDSIZEY / 2);
            }
            RedrawAll();
        }

        private void awayHades_MouseDown(object sender, MouseEventArgs e)
        {
            if (core.away.hadesNum > 0)
            {
                selectedCard = core.away.hadesOrder[core.away.hadesNum - 1];
                dragFrom = SECTION.AWAYHADES;
                dragging = true;
                diffPoint = new Point(CARDSIZEX / 2, CARDSIZEY / 2);
            }
            RedrawAll();
        }

        //----------ドラッグ中----------
        private void Card_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragging) return;

            int id = (int)(((PictureBox)sender).Tag);

            //x軸移動処理
            int x = core.cards[id].Location.X + e.X - diffPoint.X;
            if (x > PB[id].Parent.Width - 20)
            {
                core.cards[id].Location.X = PB[id].Parent.Width - 20;
            }
            else if (x < 20 - PB[id].Width)
            {
                core.cards[id].Location.X = 20 - PB[id].Width;
            }
            else
            {
                core.cards[id].Location.X = x;
            }

            //y軸移動処理
            int y = core.cards[id].Location.Y + e.Y - diffPoint.Y;
            int temp = core.cards[id].active ? CARDSIZEY - CARDSIZEX : 0;
            if (y > PB[id].Parent.Height - temp - 20)
            {
                core.cards[id].Location.Y = PB[id].Parent.Height - temp - 20;
            }
            else if (temp < 20 - PB[id].Height)
            {
                core.cards[id].Location.Y = 20 - PB[id].Height;
            }
            else
            {
                core.cards[id].Location.Y = y;
            }

            //描画
            RedrawAll();
        }

        //----------ドラッグ終了----------
        private void Card_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                // 他のパネルに移動
                // ----- 隅に行き過ぎないように、移動範囲制限を付けたい・・・
                if (dragging)
                {
                    Point mousePos = System.Windows.Forms.Cursor.Position;

                    // ドラッグ終了情報を変数に
                    dragging = false;

                    // ドラッグ先
                    SECTION dragTo = SECTION.NONE;
                    if (new Rectangle(homeField.PointToScreen(new Point(0, 0)), homeField.Size).Contains(mousePos)) dragTo = SECTION.HOMEFIELD;
                    else if (new Rectangle(homeHand.PointToScreen(new Point(0, 0)), homeHand.Size).Contains(mousePos)) dragTo = SECTION.HOMEHAND;
                    else if (new Rectangle(homeLibrary.PointToScreen(new Point(0, 0)), homeLibrary.Size).Contains(mousePos)) dragTo = SECTION.HOMELIBRARY;
                    else if (new Rectangle(homeNodeActive.PointToScreen(new Point(0, 0)), homeNodeActive.Size).Contains(mousePos)) dragTo = SECTION.HOMENODEACTIVE;
                    else if (new Rectangle(homeNodeSleep.PointToScreen(new Point(0, 0)), homeNodeSleep.Size).Contains(mousePos)) dragTo = SECTION.HOMENODESLEEP;
                    else if (new Rectangle(homeHades.PointToScreen(new Point(0, 0)), homeHades.Size).Contains(mousePos)) dragTo = SECTION.HOMEHADES;

                    else if (new Rectangle(awayField.PointToScreen(new Point(0, 0)), awayField.Size).Contains(mousePos)) dragTo = SECTION.AWAYFIELD;
                    else if (new Rectangle(awayHand.PointToScreen(new Point(0, 0)), awayHand.Size).Contains(mousePos)) dragTo = SECTION.AWAYHAND;
                    else if (new Rectangle(awayLibrary.PointToScreen(new Point(0, 0)), awayLibrary.Size).Contains(mousePos)) dragTo = SECTION.AWAYLIBRARY;
                    else if (new Rectangle(awayNodeActive.PointToScreen(new Point(0, 0)), awayNodeActive.Size).Contains(mousePos)) dragTo = SECTION.AWAYNODEACTIVE;
                    else if (new Rectangle(awayNodeSleep.PointToScreen(new Point(0, 0)), awayNodeSleep.Size).Contains(mousePos)) dragTo = SECTION.AWAYNODESLEEP;
                    else if (new Rectangle(awayHades.PointToScreen(new Point(0, 0)), awayHades.Size).Contains(mousePos)) dragTo = SECTION.AWAYHADES;

                    //----------HOMEドラッグ処理----------
                    if (dragTo == SECTION.HOMEFIELD)
                    {
                        core.cards[selectedCard].section = SECTION.HOMEFIELD;
                        core.cards[selectedCard].Location = VisionFunctions.subPoint(homeField.PointToClient(mousePos), diffPoint);
                        if (dragFrom != SECTION.HOMEFIELD)
                        {
                            core.home.fieldOrder[core.home.fieldNum] = selectedCard;
                            core.home.fieldNum++;
                        }

                        if (dragFrom == SECTION.HOMEHAND)
                        {
                            core.cards[selectedCard].open = true;
                            if (cardDB[core.cards[selectedCard].no].type == "Character")
                            {
                                if (cardDB[core.cards[selectedCard].no].skill.IndexOf("速攻") != -1)
                                {
                                    core.cards[selectedCard].active = true;
                                }
                                else
                                {
                                    core.cards[selectedCard].active = false;
                                }
                            }
                        }

                        if (dragFrom == SECTION.HOMELIBRARY)
                        {
                            core.cards[selectedCard].open = false;
                            core.cards[selectedCard].active = true;
                        }
                    }
                    if (dragTo == SECTION.HOMEHAND)
                    {
                        core.cards[selectedCard].section = SECTION.HOMEHAND;
                        core.cards[selectedCard].active = true;
                        core.cards[selectedCard].open = false;
                        core.cards[selectedCard].Location = VisionFunctions.subPoint(homeHand.PointToClient(mousePos), diffPoint);
                        if (dragFrom != SECTION.HOMEHAND)
                        {
                            core.home.handOrder[core.home.handNum] = selectedCard;
                            core.home.handNum++;
                        }
                    }
                    if (dragTo == SECTION.HOMENODEACTIVE)
                    {
                        core.cards[selectedCard].section = SECTION.HOMENODEACTIVE;
                        if (dragFrom != SECTION.HOMENODEACTIVE)
                        {
                            core.cards[selectedCard].open = false;
                            core.home.nodeActiveOrder[core.home.nodeActiveNum] = selectedCard;
                            core.home.nodeActiveNum++;
                        }
                    }
                    if (dragTo == SECTION.HOMENODESLEEP)
                    {
                        core.cards[selectedCard].section = SECTION.HOMENODESLEEP;
                        if (dragFrom != SECTION.HOMENODESLEEP)
                        {
                            core.cards[selectedCard].open = false;
                            core.home.nodeSleepOrder[core.home.nodeSleepNum] = selectedCard;
                            core.home.nodeSleepNum++;
                        }
                    }
                    if (dragTo == SECTION.HOMELIBRARY)
                    {
                        core.cards[selectedCard].section = SECTION.HOMELIBRARY;
                        if (dragFrom != SECTION.HOMELIBRARY)
                        {
                            core.home.libraryOrder[core.home.libraryNum] = selectedCard;
                            core.home.libraryNum++;
                        }
                    }
                    if ((dragTo == SECTION.HOMEHADES || dragTo == SECTION.AWAYHADES) && core.cards[selectedCard].owner == true)
                    {
                        core.cards[selectedCard].section = SECTION.HOMEHADES;
                        core.cards[selectedCard].active = true;
                        core.cards[selectedCard].open = true;
                        if (dragFrom == SECTION.HOMEHADES)
                        {
                            selectedCard = core.home.hadesOrder[core.home.hadesNum - 1];
                        }
                        else
                        {
                            core.home.hadesOrder[core.home.hadesNum] = selectedCard;
                            core.home.hadesNum++;
                        }
                    }

                    //----------AWAYドラッグ処理-----------
                    if (dragTo == SECTION.AWAYFIELD)
                    {
                        core.cards[selectedCard].section = SECTION.AWAYFIELD;
                        core.cards[selectedCard].Location = VisionFunctions.subPoint(awayField.PointToClient(mousePos), diffPoint);
                        if (dragFrom != SECTION.AWAYFIELD)
                        {
                            core.away.fieldOrder[core.away.fieldNum] = selectedCard;
                            core.away.fieldNum++;
                        }

                        if (dragFrom == SECTION.AWAYHAND)
                        {
                            core.cards[selectedCard].open = true;
                            if (cardDB[core.cards[selectedCard].no].type == "Character")
                            {
                                if (cardDB[core.cards[selectedCard].no].skill.IndexOf("速攻") != -1)
                                {
                                    core.cards[selectedCard].active = true;
                                }
                                else
                                {
                                    core.cards[selectedCard].active = false;
                                }
                            }
                        }
                    }
                    if (dragTo == SECTION.AWAYHAND)
                    {
                        core.cards[selectedCard].section = SECTION.AWAYHAND;
                        core.cards[selectedCard].active = true;
                        core.cards[selectedCard].open = false;
                        core.cards[selectedCard].Location = VisionFunctions.subPoint(awayHand.PointToClient(mousePos), diffPoint);
                        if (dragFrom != SECTION.AWAYHAND)
                        {
                            core.away.handOrder[core.away.handNum] = selectedCard;
                            core.away.handNum++;
                        }
                    }
                    if (dragTo == SECTION.AWAYLIBRARY)
                    {
                        core.cards[selectedCard].section = SECTION.AWAYLIBRARY;
                        if (dragFrom != SECTION.AWAYLIBRARY)
                        {
                            core.away.libraryOrder[core.away.libraryNum] = selectedCard;
                            core.away.libraryNum++;
                        }
                    }
                    if (dragTo == SECTION.AWAYNODEACTIVE)
                    {
                        core.cards[selectedCard].section = SECTION.AWAYNODEACTIVE;
                        if (dragFrom != SECTION.AWAYNODEACTIVE)
                        {
                            core.cards[selectedCard].open = false;
                            core.away.nodeActiveOrder[core.away.nodeActiveNum] = selectedCard;
                            core.away.nodeActiveNum++;
                        }
                    }
                    if (dragTo == SECTION.AWAYNODESLEEP)
                    {
                        core.cards[selectedCard].section = SECTION.AWAYNODESLEEP;
                        if (dragFrom != SECTION.AWAYNODESLEEP)
                        {
                            core.cards[selectedCard].open = false;
                            core.away.nodeSleepOrder[core.away.nodeSleepNum] = selectedCard;
                            core.away.nodeSleepNum++;
                        }
                    }
                    if ((dragTo == SECTION.HOMEHADES || dragTo == SECTION.AWAYHADES) && core.cards[selectedCard].owner == false)
                    {
                        core.cards[selectedCard].section = SECTION.AWAYHADES;
                        core.cards[selectedCard].active = true;
                        core.cards[selectedCard].open = true;
                        if (dragFrom == SECTION.AWAYHADES)
                        {
                            selectedCard = core.away.hadesOrder[core.away.hadesNum - 1];
                        }
                        else
                        {
                            core.away.hadesOrder[core.away.hadesNum] = selectedCard;
                            core.away.hadesNum++;
                        }
                    }

                    //----------～からの処理----------
                    // フィールドからの処理
                    if (dragFrom == SECTION.HOMEFIELD && dragTo != SECTION.HOMEFIELD && dragTo != SECTION.NONE)
                    {
                        int i;
                        for (i = 0; i < core.home.fieldNum; i++)
                        {
                            if (core.home.fieldOrder[i] == selectedCard)
                            {
                                for (; i < core.home.fieldNum - 1; i++)
                                {
                                    core.home.fieldOrder[i] = core.home.fieldOrder[i + 1];
                                }
                                break;
                            }
                        }
                    }
                    if (dragFrom == SECTION.AWAYFIELD && dragTo != SECTION.AWAYFIELD && dragTo != SECTION.NONE)
                    {
                        int i;
                        for (i = 0; i < core.away.fieldNum; i++)
                        {
                            if (core.away.fieldOrder[i] == selectedCard)
                            {
                                for (; i < core.away.fieldNum - 1; i++)
                                {
                                    core.away.fieldOrder[i] = core.away.fieldOrder[i + 1];
                                }
                                break;
                            }
                        }
                    }
                    // 手札からの処理
                    if (dragFrom == SECTION.HOMEHAND && dragTo != SECTION.HOMEHAND && dragTo != SECTION.NONE)
                    {
                        int i;
                        for (i = 0; i < core.home.handNum; i++)
                        {
                            if (core.home.handOrder[i] == selectedCard) break;
                        }
                        for (; i < core.home.handNum - 1; i++)
                        {
                            core.home.handOrder[i] = core.home.handOrder[i + 1];
                        }
                        core.home.handNum--;
                    }
                    if (dragFrom == SECTION.AWAYHAND && dragTo != SECTION.AWAYHAND && dragTo != SECTION.NONE)
                    {
                        int i;
                        for (i = 0; i < core.away.handNum; i++)
                        {
                            if (core.away.handOrder[i] == selectedCard) break;
                        }
                        for (; i < core.away.handNum - 1; i++)
                        {
                            core.away.handOrder[i] = core.away.handOrder[i + 1];
                        }
                        core.away.handNum--;
                    }
                    // ノードからの処理
                    if (dragFrom == SECTION.HOMENODEACTIVE && dragTo != SECTION.NONE && dragTo != SECTION.HOMENODEACTIVE)
                    {
                        core.home.nodeActiveNum--;
                    }
                    if (dragFrom == SECTION.HOMENODESLEEP && dragTo != SECTION.NONE && dragTo != SECTION.HOMENODESLEEP)
                    {
                        core.home.nodeSleepNum--;
                    }
                    // 山札からの処理
                    if (dragFrom == SECTION.HOMELIBRARY && dragTo != SECTION.NONE && dragTo != SECTION.HOMELIBRARY)
                    {
                        core.home.libraryNum--;
                    }
                    if (dragFrom == SECTION.AWAYLIBRARY && dragTo != SECTION.NONE && dragTo != SECTION.AWAYLIBRARY)
                    {
                        core.away.libraryNum--;
                    }
                    // 冥界からの処理
                    if (dragFrom == SECTION.HOMEHADES && dragTo != SECTION.NONE && dragTo != SECTION.HOMEHADES)
                    {
                        core.home.hadesNum--;
                    }
                    if (dragFrom == SECTION.AWAYHADES && dragTo != SECTION.NONE && dragTo != SECTION.AWAYHADES)
                    {
                        core.away.hadesNum--;
                    }

                    //描画
                    RedrawAll();
                }
            }
        }

        // 山札をダブルクリック → 手札へ
        private void homeLibrary_DoubleClick(object sender, EventArgs e)
        {
            if (core.home.libraryNum == 0) return;

            selectedCard = core.home.libraryOrder[core.home.libraryNum - 1];

            core.cards[selectedCard].section = SECTION.HOMEHAND;
            core.cards[selectedCard].open = false;
            core.home.libraryNum--;
            core.home.handOrder[core.home.handNum] = selectedCard;
            core.home.handNum++;

            //描画
            RedrawAll();
        }

        //----------山札を右クリック----------
        private void 場に裏向きカードを1枚出すToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (core.home.libraryNum == 0) return;
            selectedCard = core.home.libraryOrder[core.home.libraryNum - 1];

            core.cards[selectedCard].section = SECTION.HOMEFIELD;
            core.cards[selectedCard].Location = new Point(10,10);
            core.cards[selectedCard].open = false;
            core.home.libraryNum--;

            RedrawAll();
        }

        // フィールドクリック - カード選択解除
        private void field_MouseDown(object sender, MouseEventArgs e)
        {
            selectedCard = -1;
            RedrawAll();
        }

        //============================== ファイル入出力 ==============================
        //----------cardlist.ini読み込み----------
        private void ReadCardlist()
        {
            string fname = @"card\\cardlist.ini";
            if (!System.IO.File.Exists(fname))
            {
                MessageBox.Show("cardlist.iniの読み込みに失敗しました。");
                return;
            }

            string line;
            int now_no = 0;
            System.IO.StreamReader reader;

            try
            {
                reader = new System.IO.StreamReader(fname, Encoding.Default);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();

                if (line.Length == 0) continue; //空白行
                if (line[0] == '#') continue;   //コメントアウト
                if (line[0] == '[')             //[]付きカードナンバー
                {
                    int n;
                    if (int.TryParse(line.Substring(1,line.Length - 2), out n))
                    {
                        //[n]であった場合
                        if (n == 0) continue;
                        now_no = n; //now_noをセット
                        cardDB[n] = new CardDB();
                        cardDB[n].no = n;
                        continue;
                    }
                    else
                    {
                        now_no = 0; //now_noを無効数字に
                        continue;
                    }
                }
                if (now_no == 0) continue;

                switch (line.Substring(0, line.IndexOf("=")))
                {
                    case "Type":
                        cardDB[now_no].type = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "Glaze":
                        cardDB[now_no].graze = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "Node":
                        cardDB[now_no].node = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "Cost":
                        cardDB[now_no].cost = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "Range":
                        cardDB[now_no].range = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "Time":
                        cardDB[now_no].time = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "User":
                        cardDB[now_no].user = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "Name":
                        cardDB[now_no].name = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "Class":
                        cardDB[now_no].cclass = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "Skill":
                        cardDB[now_no].skill = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "Upkeep":
                        cardDB[now_no].upkeep = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "Ability":
                        cardDB[now_no].ability = line.Substring(line.IndexOf("=") + 1);
                        cardDB[now_no].ability = cardDB[now_no].ability.Replace("\\n", Environment.NewLine + Environment.NewLine ); //改行文字を変換
                        break;
                    case "Attack":
                        cardDB[now_no].attack = line.Substring(line.IndexOf("=") + 1);
                        if (cardDB[now_no].attack == "") cardDB[now_no].attack = "-";
                        break;
                    case "Toughness":
                        cardDB[now_no].toughness = line.Substring(line.IndexOf("=") + 1);
                        if (cardDB[now_no].toughness == "") cardDB[now_no].toughness = "-";
                        break;
                    case "Text":
                        cardDB[now_no].text = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "Illustration":
                        cardDB[now_no].illustration = line.Substring(line.IndexOf("=") + 1);
                        break;
                    case "File":
                        cardDB[now_no].fname = line.Substring(line.IndexOf("=") + 1);
                        break;
                }
            }
            reader.Close();
        }

        //----------デッキ読み込み----------
        private bool ReadDeck(string fname)
        {
            Card[] deck = new Card[50];

            int cnt = 0;
            System.IO.StreamReader reader = new System.IO.StreamReader(fname, Encoding.Default);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] field = line.Split(',');

                // fieldにデータが2つ以上ない場合
                if (field.Length < 2) continue;

                int n, no; //n:枚数 no:番号
                if (int.TryParse(field[0], out n) && int.TryParse(field[1], out no))
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (cnt >= 50) break;
                        deck[cnt] = new Card(cnt,no);
                        cnt++;
                    }
                }
                if (cnt > 50) break;
            }
            reader.Close();

            // false
            if (cnt != 50)
            {
                MessageBox.Show("デッキの枚数が50枚ではありません。");
                return false;
            }

            // true
            for (int i = 0; i < 50; i++) core.cards[i].no = deck[i].no;
            core.Initialize();
            selectedCard = -1;
            return true;
        }

        //============================== その他 ==============================
        //----------ボタンクリック----------
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendCore();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < 50; i++)
            {
                core.cards[i].section = SECTION.HOMEFIELD;
                PB[i].Visible = cardDB[core.cards[i].no].bmp != null ? true : false;
                PB[i].Location = core.cards[i].Location = new Point(CARDSIZEX * (i % 10), 48 * (i / 10));
                PB[i].Parent = homeField;
                PB[i].BringToFront();
            }*/
            for (int i = 0; i < 50; i++)
            {
                Chat_Add(core.home.handOrder[i].ToString());
            }
            RedrawAll();
        }

        //-----------カード右クリック----------
        // スリープにする
        private void スリープにするToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (core.cards[selectedCard].active)
            {
                core.cards[selectedCard].active = false;
                core.cards[selectedCard].Location.Y += CARDSIZEY - CARDSIZEX;
                RedrawAll();
            }
        }
        // アクティブにする
        private void アクティブにするToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!core.cards[selectedCard].active)
            {
                core.cards[selectedCard].active = true;
                core.cards[selectedCard].Location.Y -= CARDSIZEY - CARDSIZEX;
                RedrawAll();
            }
        }
        // 裏向きにする
        private void 裏向きにするToolStripMenuItem_Click(object sender, EventArgs e)
        {
            core.cards[selectedCard].open = false;
            RedrawAll();
        }
        // 表向きにする
        private void 表向きにするToolStripMenuItem_Click(object sender, EventArgs e)
        {
            core.cards[selectedCard].open = true;
            RedrawAll();
        }

        //----------終了時割り込み----------
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*if (MessageBox.Show("対戦中ですが、終了してよろしいですか？", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }*/
        }

        // ライフ変更反映
        private void homeLife_ValueChanged(object sender, EventArgs e)
        {
            core.home.life = (int)homeLife.Value;
        }

        // 冥界をダブルクリック
        private void homeHades_DoubleClick(object sender, EventArgs e)
        {
            fm2 = new Form2(cardDB, core.cards, core.home.hadesOrder, core.home.hadesNum);
            fm2.ShowDialog();
            fm2.Dispose();
            dragging = false;
        }

        //----------メニュー----------
        private void ゲーム開始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card[] sendDeck = new Card[50];
            string fname;
            openFileDialog1.Filter = "テキストファイル (*.txt)|*.txt|deckファイル (*.deck)|*.deck";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            fname = openFileDialog1.FileName;

            // デッキを読み込み
            if (!ReadDeck(fname))
            {
                MessageBox.Show("デッキの読み込みに失敗しました。");
                return;
            }
            for (int i = 0; i < 50; i++)
            {
                sendDeck[i] = new Card(i, 1);
                sendDeck[i].setAll(core.cards[i]);
            }

            fm3 = new Form3();
            if (fm3.ShowDialog() == DialogResult.OK)
            {
                bool host = fm3.host;
                int port = fm3.port;
                string ip = fm3.ip;

                if (host)
                {
                    //サーバーを作成
                    if (Program.OpenServer(port) == false)
                    {
                        MessageBox.Show("サーバーの作成に失敗しました。");
                        return;
                    }
                    textBox1.Text += "サーバーを作成しました。" + Environment.NewLine;

                    ip = "localhost";
                }

                //クライアントを作成
                if (Program.OpenClient(ip, port) == false)
                {
                    MessageBox.Show("サーバーへの接続に失敗しました。");
                    return;
                }

                Program.cl.Send(sendDeck, TOPBYTE.DECK);
                selectedCard = -1;
                RedrawAll();
            }
            fm3.Dispose();
        }

        private void デッキを読み込みToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Card[] sendDeck = new Card[50];
            string fname;
            openFileDialog1.Filter = "テキストファイル (*.txt)|*.txt|deckファイル (*.deck)|*.deck";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            fname = openFileDialog1.FileName;

            // デッキを読み込み
            if (!ReadDeck(fname))
            {
                MessageBox.Show("デッキの読み込みに失敗しました。");
                return;
            }

            for (int i = 0; i < 50; i++)
            {
                core.home.libraryOrder[i] = i;
            }
            core.home.libraryNum=50;
            RedrawAll();
        }

        private void SendCore()
        {
            Program.cl.Send(core, TOPBYTE.NETVISIONCORE);
        }

        private void StartGame()
        {
            // cardDBに画像の読み込み
            for (int i = 0; i < 100; i++)
            {
                if (cardDB[i].bmp == null)
                {
                    string fname = @"card\\" + cardDB[i].fname;
                    if (System.IO.File.Exists(fname))
                    {
                        cardDB[i].bmp = new Bitmap(fname);
                    }
                }
            }
        }

        //============================== publicメソッド ==============================
        private delegate void Chat_Add_Delegate(string str);
        public void Chat_Add(string text)
        {
            // 別Threadから処理が飛んできた場合はInvoke
            if (InvokeRequired)
            {
                Invoke(new Chat_Add_Delegate(Chat_Add), text);
                return;
            }
            this.textBox1.Text += text +Environment.NewLine;
        }

        private void Chat_Submit()
        {
            string sendStr = textBox3.Text;
            if (sendStr.Length > 0)
            {
                Program.cl.Send(sendStr, TOPBYTE.STRING);
            }
            textBox3.Text = "";
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Chat_Submit();
            }
        }

        private void Chat_SubmitBtn_Click(object sender, EventArgs e)
        {
            Chat_Submit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();
        }

        public void setNetvisionCore(NetvisionCore setCore)
        {
            core.setAll(setCore);
            RedrawAll();
        }
    }
}
