using System;
using System.Drawing;
using System.Windows.Forms;

namespace thnetvision
{
    public partial class Form3 : Form
    {
        public string name;
        public bool host;
        public int port = 7500;
        public string ip = "localhost";

        public Form3()
        {
            InitializeComponent();

            textBox2.Text = port.ToString();
            textBox4.Text = port.ToString();
            textBox3.Text = ip;

            radioButton1.Checked = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox1.BackColor = Color.White;
            }
            else
            {
                textBox1.BackColor = Color.Pink;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(textBox2.Text, out n))
            {
                textBox2.BackColor = Color.White;
            }
            else
            {
                textBox2.BackColor = Color.Pink;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 0)
            {
                textBox3.BackColor = Color.White;
            }
            else
            {
                textBox3.BackColor = Color.Pink;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(textBox4.Text, out n))
            {
                textBox4.BackColor = Color.White;
            }
            else
            {
                textBox4.BackColor = Color.Pink;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // プレイヤー名
            if (textBox1.Text.Length > 0)
            {
                name = textBox1.Text;
            }
            else
            {
                MessageBox.Show("プレイヤー名を入力してください。");
                return;
            }
            // プロパティ
            if (radioButton1.Checked)
            {
                host = true;
                if (!int.TryParse(textBox2.Text, out port))
                {
                    MessageBox.Show("待受ポート番号を正しく入力してください。");
                    return;
                }
            }
            else
            {
                host = false;
                if (textBox3.Text.Length > 0)
                {
                    ip = textBox3.Text;
                }
                else
                {
                    MessageBox.Show("接続先IPアドレスを入力してください。");
                    return;
                }
                if (!int.TryParse(textBox4.Text, out port))
                {
                    MessageBox.Show("接続先ポート番号を正しく入力してください。");
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
        }
    }
}
