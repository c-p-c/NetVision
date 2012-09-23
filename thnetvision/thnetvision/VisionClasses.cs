using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace thnetvision
{
    //============================== 汎用関数 ===============================
    static class VisionFunctions
    {
        static private System.Random rndGenerator = new System.Random();

        // Point加減算
        static public Point addPoint(Point point1, Point point2)
        {
            int x = 0;
            int y = 0;
            Point result = new Point(x, y);
            result.X = point1.X + point2.X;
            result.Y = point1.Y + point2.Y;
            return result;
        }
        static public Point subPoint(Point point1, Point point2)
        {
            int x = 0;
            int y = 0;
            Point result = new Point(x, y);
            result.X = point1.X - point2.X;
            result.Y = point1.Y - point2.Y;
            return result;
        }

        static public void Shuffle(int[] ary) { Shuffle(ary, ary.Length); }
        static public void Shuffle(int[] ary, int ary_length)
        {
            // Fisher-Yatesアルゴリズムでシャッフルする
            // 参考：http://blogs.wankuma.com/episteme/archive/2009/07/13/177448.aspx
            for (int i = ary_length - 1; i >= 0; i--)
            {
                int rnd = rndGenerator.Next(i);
                int tmp = ary[rnd];
                ary[rnd] = ary[i];
                ary[i] = tmp;
            }
        }

        //----------汎用描画関数----------
        // ピクチャボックスの中央下にimgを描画
        static public void DrawImage_CenterBottom(Graphics g, Bitmap img, PictureBox pb)
        {
            Point[] pt = new Point[3];
            double imgW = (double)img.Width;
            double imgH = (double)img.Height;
            double pbW = (double)pb.Width;
            double pbH = (double)pb.Height;

            if (imgH / imgW > pbH / pbW)
            {
                int w = (int)(imgW * (pbH / imgH));
                int h = (int)pbH;
                int temp = (((int)pbW - w) / 2);
                pt[0] = new Point(temp, 0);
                pt[1] = new Point(temp + w, 0);
                pt[2] = new Point(temp, h);
            }
            else
            {
                int w = (int)pbW;
                int h = (int)(imgH * (pbW / imgW));
                int temp = (int)pbH - h;
                pt[0] = new Point(0, temp);
                pt[1] = new Point(w, temp);
                pt[2] = new Point(0, (int)pbH);
            }
            g.DrawImage(img, pt);
        }

        // カード画像がない場合のカードの描画
        static public Bitmap DrawCard(CardDB[] cardDB, Card card, int w, int h)
        {
            Bitmap img = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(img);
            Rectangle area;

            // 枠の描画
            g.FillRectangle(Brushes.White, 0, 0, w, h);
            Pen pen = new Pen(Brushes.Black, 3);
            if (cardDB[card.no].type == "Character") pen.Brush = Brushes.Aquamarine;
            if (cardDB[card.no].type == "Spell") pen.Brush = Brushes.OrangeRed;
            if (cardDB[card.no].type == "Command") pen.Brush = Brushes.Gray;
            g.DrawRectangle(pen, 1, 1, w - 3, h - 3);
            g.DrawRectangle(new Pen(Brushes.Black, 1), 0, 0, w - 1, h - 1);

            // カード名の描画
            area = new Rectangle(3, h / 2, w - 6, h / 2 - 15);
            g.DrawString(cardDB[card.no].name, new Font("MS UI Gothic", 8), Brushes.Black, area);

            // 攻撃力・耐久力の描画
            area = new Rectangle(4, h - 13, w - 8, 10);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Center;
            g.DrawString(cardDB[card.no].attack + " / " + cardDB[card.no].toughness, new Font("MS UI Gothic", 8), Brushes.Black, area, stringFormat);

            // ノード・コストの描画
            area = new Rectangle(4, 5, w - 8, 15);
            g.DrawString("N " + cardDB[card.no].node + "  C " + cardDB[card.no].cost, new Font("MS UI Gothic", 8), Brushes.Black, area, stringFormat);

            g.Dispose();
            return img;
        }

        // カード画像がない場合の拡大画像の描画
        static public void DrawImage_Noimage(Graphics g, CardDB cardDB, PictureBox pb)
        {
            g.DrawString(cardDB.name, new Font("MS UI Gothic", 12, FontStyle.Bold), Brushes.Black, 6, 272);
            g.DrawString("N " + cardDB.node + " C " + cardDB.cost, new Font("MS UI Gothic", 16, FontStyle.Bold), Brushes.Black, 185, 28);
            Rectangle area = new Rectangle(10, 5, pb.Width - 20, 16);
            StringFormat centering = new StringFormat();
            centering.Alignment = StringAlignment.Center;
            centering.LineAlignment = StringAlignment.Center;
            g.DrawString(cardDB.type + " Card", new Font("Century", 11), Brushes.Black, area, centering);
            g.DrawString(string.Format("No.{0:D4}", cardDB.no), new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, 120, 378);
            if (cardDB.type == "Character")
            {
                g.DrawString("GRAZE", new Font("MS UI Gothic", 12, FontStyle.Bold), Brushes.Black, 12, 28);
                g.DrawString(cardDB.graze, new Font("MS UI Gothic", 26, FontStyle.Bold), Brushes.Black, 24, 42);
                g.DrawString(cardDB.cclass, new Font("MS UI Gothic", 10), Brushes.Black, 210, 275);
            }
        }

        // シリアライズ・デシリアライズ
        static public byte[] SetTopByte(byte[] accptBytes, byte addByte)
        {
            int i;
            byte[] resultBytes = new byte[accptBytes.Length + 1];
            resultBytes[0] = addByte;
            for (i = 0; i < accptBytes.Length; i++)
            {
                resultBytes[i + 1] = accptBytes[i];
            }
            return resultBytes;
        }

        static public byte[] DeleteTopByte(byte[] accptBytes)
        {
            int i;
            byte[] resultBytes = new byte[accptBytes.Length - 1];
            for (i = 0; i < accptBytes.Length - 1; i++)
            {
                resultBytes[i] = accptBytes[i + 1];
            }
            return resultBytes;
        }

        static public byte[] Serialize(object obj)
        {
            BinaryFormatter bfSend = new BinaryFormatter();
            MemoryStream msSend = new MemoryStream();
            bfSend.Serialize(msSend, obj);
            return msSend.ToArray();
        }

        static public string DeserializeToString(byte[] accptBytes)
        {
            MemoryStream ms = new MemoryStream(accptBytes);
            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            string accptStr = (string)bf.Deserialize(ms);
            ms.Close();
            return accptStr;
        }
        static public NetvisionCore DeserializeToNetvisionCore(byte[] accptBytes)
        {
            MemoryStream ms = new MemoryStream(accptBytes);
            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            NetvisionCore resultCore = (NetvisionCore)bf.Deserialize(ms);
            ms.Close();
            return resultCore;
        }
        static public Card[] DeserializeToDeck(byte[] accptBytes)
        {
            MemoryStream ms = new MemoryStream(accptBytes);
            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            Card[] resultDeck = (Card[])bf.Deserialize(ms);
            ms.Close();
            return resultDeck;
        }
    }

    //============================== 定数 ==============================
    public enum SECTION : byte
    {
        NONE,
        HOMEFIELD,
        AWAYFIELD,
        HOMEHAND,
        AWAYHAND,
        HOMELIBRARY,
        AWAYLIBRARY,
        HOMEHADES,
        AWAYHADES,
        HOMENODEACTIVE,
        AWAYNODEACTIVE,
        HOMENODESLEEP,
        AWAYNODESLEEP
    }

    //============================== クラス宣言 ==============================
    // CardDB : cardlist.iniの情報,カードの画像データを格納
    public class CardDB
    {
        public int no;
        public string type; //カード種類
        public string graze; //グレイズ
        public string node; //必要ノード
        public string cost; //必要コスト
        public string range; //効果範囲
        public string time; //発動期間
        public string user; //スペカ術者
        public string name; //カード名
        public string cclass; //種族
        public string skill; //キャラクター能力
        public string upkeep; //維持コスト
        public string ability; //効果
        public string attack; //攻撃力
        public string toughness; //耐久力
        public string text; //フレーバーテキスト
        public string illustration; //絵師
        public string fname; //画像パス
        public Bitmap bmp;

        public CardDB()
        {
            no = 0;
            type = "";
            graze = "";
            node = "";
            cost = "";
            range = "";
            time = "";
            user = "";
            name = "";
            cclass = "";
            skill = "";
            upkeep = "";
            ability = "";
            attack = "";
            toughness = "";
            text = "";
            illustration = "";
            fname = "";
        }
    }

    // Card : カードの情報を格納
    [Serializable]
    public class Card
    {
        public int id; // ゲーム中でのカードのid
        public int no; // カードNo.
        public bool open;
        public bool active;
        public bool reverse;
        public bool owner;
        public Point Location;
        public SECTION section;

        public Card(int id, int no)
        {
            this.id = id;
            this.no = no;
            this.Initialize();
        }

        public void Initialize()
        {
            this.open = true;
            this.active = true;
            this.reverse = false;
            this.owner = true;
            this.Location = new Point(0, 0);
            this.section = SECTION.NONE;
        }

        public void setAll(Card setCard)
        {
            this.id = setCard.id;
            this.no = setCard.no;
            this.open = setCard.open;
            this.active = setCard.active;
            this.reverse = setCard.reverse;
            this.owner = setCard.owner;
            this.Location.X = setCard.Location.X;
            this.Location.Y = setCard.Location.Y;
            this.section = setCard.section;
        }
    }

    // NetvisionCore : 場の持つ情報
    [Serializable]
    public class NetvisionCore
    {
        public FieldData home;
        public FieldData away;
        public Card[] cards = new Card[100];

        public NetvisionCore()
        {
            int i;
            for (i = 0; i < 100; i++)
            {
                cards[i] = new Card(i, 1);
            }
            Initialize();
        }

        public void Initialize()
        {
            for (int i = 0; i < 100; i++) cards[i].Initialize();
            home = new FieldData();
            away = new FieldData();
        }

        public void setAll(NetvisionCore setCore)
        {
            int i;
            for (i = 0; i < 100; i++)
            {
                cards[i].setAll(setCore.cards[i]);
            }
            home.setAll(setCore.home);
            away.setAll(setCore.away);
        }

        public NetvisionCore ReverseSide()
        {
            int i;
            NetvisionCore resultCore = new NetvisionCore();
            resultCore.setAll(this);

            resultCore.home.setAll(this.away);
            resultCore.away.setAll(this.home);

            for (i = 0; i < 100; i++)
            {
                switch (this.cards[i].section)
                {
                    case SECTION.HOMEFIELD:
                        resultCore.cards[i].section = SECTION.AWAYFIELD;
                        break;
                    case SECTION.HOMEHADES:
                        resultCore.cards[i].section = SECTION.AWAYHADES;
                        break;
                    case SECTION.HOMEHAND:
                        resultCore.cards[i].section = SECTION.AWAYHAND;
                        break;
                    case SECTION.HOMELIBRARY:
                        resultCore.cards[i].section = SECTION.AWAYLIBRARY;
                        break;
                    case SECTION.HOMENODEACTIVE:
                        resultCore.cards[i].section = SECTION.AWAYNODEACTIVE;
                        break;
                    case SECTION.HOMENODESLEEP:
                        resultCore.cards[i].section = SECTION.AWAYNODESLEEP;
                        break;

                    case SECTION.AWAYFIELD:
                        resultCore.cards[i].section = SECTION.HOMEFIELD;
                        break;
                    case SECTION.AWAYHADES:
                        resultCore.cards[i].section = SECTION.HOMEHADES;
                        break;
                    case SECTION.AWAYHAND:
                        resultCore.cards[i].section = SECTION.HOMEHAND;
                        break;
                    case SECTION.AWAYLIBRARY:
                        resultCore.cards[i].section = SECTION.HOMELIBRARY;
                        break;
                    case SECTION.AWAYNODEACTIVE:
                        resultCore.cards[i].section = SECTION.HOMENODEACTIVE;
                        break;
                    case SECTION.AWAYNODESLEEP:
                        resultCore.cards[i].section = SECTION.HOMENODESLEEP;
                        break;
                }
                resultCore.cards[i].owner = !this.cards[i].owner;
            }
            return resultCore;
        }
    }

    [Serializable]
    public class FieldData
    {
        public int[] fieldOrder = new int[100];
        public int fieldNum;
        public int[] libraryOrder = new int[100];
        public int libraryNum;
        public int[] hadesOrder = new int[100];
        public int hadesNum;
        public int[] handOrder = new int[100];
        public int handNum;
        public int[] nodeActiveOrder = new int[100];
        public int nodeActiveNum;
        public int[] nodeSleepOrder = new int[100];
        public int nodeSleepNum;
        public int life;

        public FieldData()
        {
            Initialize();
        }

        public void Initialize()
        {
            this.libraryNum = 0;
            this.fieldNum = 0;
            this.handNum = 0;
            this.hadesNum = 0;
            this.nodeActiveNum = 0;
            this.nodeSleepNum = 0;
            this.life = 25;
        }

        public void setAll(FieldData setData)
        {
            int i;
            for (i = 0; i < 100; i++)
            {
                this.fieldOrder[i] = setData.fieldOrder[i];
                this.libraryOrder[i] = setData.libraryOrder[i];
                this.hadesOrder[i] = setData.hadesOrder[i];
                this.handOrder[i] = setData.handOrder[i];
                this.nodeActiveOrder[i] = setData.nodeActiveOrder[i];
                this.nodeSleepOrder[i] = setData.nodeSleepOrder[i];
            }
            this.fieldNum = setData.fieldNum;
            this.libraryNum = setData.libraryNum;
            this.hadesNum = setData.hadesNum;
            this.handNum = setData.handNum;
            this.nodeActiveNum = setData.nodeActiveNum;
            this.nodeSleepNum = setData.nodeSleepNum;
            this.life = setData.life;
        }
    }
}
