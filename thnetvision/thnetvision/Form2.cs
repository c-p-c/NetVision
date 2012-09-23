using System;
using System.Drawing;
using System.Windows.Forms;

namespace thnetvision
{
    public partial class Form2 : Form
    {
        public Card[] card = new Card[100];
        public CardDB[] cardDB = new CardDB[5000];
        public int[] cardOrder = new int[50];
        public int cardNum;

        public Form2(CardDB[] cardDB_temp, Card[] card_temp, int[] cardOrder_temp,int cardNum_temp)
        {
            cardDB = cardDB_temp;
            card = card_temp;
            cardOrder = cardOrder_temp;
            cardNum = cardNum_temp;

            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int i;
            for (i = cardNum - 1; i >= 0; i--)
            {
                int temp = card[cardOrder[i]].no;
                listBox1.Items.Add("No." + string.Format("{0,4}",temp) + " " + cardDB[temp].name.ToString());
            }
            listBox1.SelectedIndex = 0;
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(pictureBox1.BackColor);

            if (listBox1.SelectedIndex != -1)
            {
                int id = cardOrder[cardNum - listBox1.SelectedIndex - 1];

                if (cardDB[card[id].no].bmp != null)
                {
                    VisionFunctions.DrawImage_CenterBottom(g, cardDB[card[id].no].bmp, pictureBox1);
                }
                else
                {
                    VisionFunctions.DrawImage_Noimage(g, cardDB[card[id].no], pictureBox1);
                }
            }
        }
    }
}
