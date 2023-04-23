using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;


namespace Zachet2_stud_
{
    public partial class Form2 : Form
    {
        string ln;
        string fn;
        static string gr;
        double ex;
        double cw;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            string[] group = { "ПИе22_22", "ПИе21_21", "ПИе20_20", "ПИе19_19" };

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox1.DataSource = group;
            comboBox1.SelectedIndex = 0;
            exam.KeyPress += exam_KeyPress;
            coursework.KeyPress += exam_KeyPress;
            exam.TextChanged += mark_TextChanged;
            coursework.TextChanged += mark_TextChanged;
        }

        static void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            gr = comboBox.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openPicture = new OpenFileDialog();
            openPicture.Filter = "JPG|*.jpg;*.jpeg|PNG|*.png";
            if (openPicture.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openPicture.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void exam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == ',' && (sender as TextBox).Text.Contains(','))
                | (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != ','))
            {
                e.Handled = true;
            }

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (lastname.Text != String.Empty)
            {
                ln = lastname.Text;
            }
            else
            {
                MessageBox.Show("Заполните поле Фамилия");
                lastname.Focus();
                return;
            }
            if (firstname.Text != String.Empty)
            {
                fn = firstname.Text;
            }
            else
            {
                firstname.Focus();
                MessageBox.Show("Заполните поле Имя");
                return;
            }
            if (exam.Text != String.Empty)
            {
                //ex = Convert.ToDouble(exam.Text.Replace(',', '.'));
                ex = Convert.ToDouble(exam.Text);
            }
            else
            {
                exam.Focus();
                MessageBox.Show("Заполните поле Экзамен");
                return;
            }
            if (coursework.Text != String.Empty)
            {
                //cw = Convert.ToDouble(coursework.Text.Replace(',', '.'));
                cw = Convert.ToDouble(coursework.Text);
            }
            else
            {
                coursework.Focus();
                MessageBox.Show("Заполните поле Курсовая работа");
                return;
            }
            Student s = new Student(ln, fn, gr, ex, cw);

            MessageBox.Show(s.Info());

            String fileName = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Title = "Сохранить успеваемость";
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            saveFileDialog.FileName = @"C:\Users\Администратор\source\repos\Zachet2(stud)\student.txt";
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;// Сохранить имя файла
                StreamWriter streamwriter = new StreamWriter(fileName, true, System.Text.Encoding.GetEncoding("utf-8"));
                streamwriter.WriteLine(s.Info());

                streamwriter.Close();

                pictureBox1.Image.Save(@"C:\Users\Администратор\source\repos\Zachet2(stud)\img\" + lastname.Text + ".jpg");

            }

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            double sred_ball = 0;

            if (exam.Text != String.Empty)
            {
                //ex = Convert.ToDouble(exam.Text.Replace(',', '.'));
                ex = Convert.ToDouble(exam.Text);
            }
            else
            {
                exam.Focus();
                MessageBox.Show("Заполните поле Экзамен");
                return;
            }
            if (coursework.Text != String.Empty)
            {
                //cw = Convert.ToDouble(coursework.Text.Replace(',', '.'));
                cw = Convert.ToDouble(coursework.Text);
            }
            else
            {
                coursework.Focus();
                MessageBox.Show("Заполните поле Курсовая работа");
                return;
            }

            sred_ball = (ex + cw) / 2;
            averageball.Text = sred_ball.ToString();
        }


        private void mark_TextChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            averageball.Text = "";
        }
    }
}
