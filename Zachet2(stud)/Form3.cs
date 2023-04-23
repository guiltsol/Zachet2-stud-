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
    public partial class Form3 : Form
    {
        DataTable dt;
        string filterField = "Фамилия";
        public Form3()
        {
            InitializeComponent();
        }
       
       

        private void Form3_Load_1(object sender, EventArgs e)
        {
            // открываем файл на считывание
            StreamReader file = new StreamReader(@"C:\Users\Администратор\source\repos\Zachet2(stud)\student.txt");
            // таблица данных
            dt = new DataTable();
            // добавляем столбцы
            dt.Columns.Add("Фамилия");
            dt.Columns.Add("Имя");
            dt.Columns.Add("Группа");
            dt.Columns.Add("Экзамен");
            dt.Columns.Add("Курсовая работа");

            // считываем файл

            string[] values; //
            string newline; // считанная строка и файла
                            // считываем до конца файла
            while ((newline = file.ReadLine()) != null)
            {
                DataRow dr = dt.NewRow(); // строки таблицы
                values = newline.Split(' '); // строку разбиваем на части(lastname,firstname и т.д.), используя разделить пробел Split(' ')
                for (int i = 0; i < values.Length; i++)
                {
                    dr[i] = values[i]; // присваиваем ячейкам строки
                }

                dt.Rows.Add(dr); // строку добавляем в таблицу
            }
            file.Close();
            // таблицу данных dt используем как DataSource для dataGridView1
            dataGridView1.DataSource = dt;
            // устанавливаем автоматическую ширину столбцов
            dataGridView1.AutoResizeColumns();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            // если checkBox1 выбран будем рассчитывать средний балл
            if (checkBox1.Checked)
            {
                // добавляем новый столбец
                dt.Columns.Add("Средний балл");
               // dataGridView1.Columns.Add("Средний балл", "Средний балл");
                // и рассчитаем средний балл
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double exam = Convert.ToDouble(dt.Rows[i]["Экзамен"]);
                    double coursework = Convert.ToDouble(dt.Rows[i]["Курсовая работа"]);
                    dt.Rows[i]["Средний балл"] = (exam + coursework) / 2;
                }
                dataGridView1.DataSource = dt;
            }
            else
            // если checkBox1 не выбран - удаляем столбец
            {
                dt.Columns.Remove("Средний балл");
                //dataGridView1.Columns.Remove("Средний балл");
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            // фильтрация данных
            dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", filterField,
            textBox1.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }
            ExcelWorkBook.SaveAs(@"C:\Users\Администратор\source\repos\Zachet2(stud)\tab.xlsx");
            ExcelWorkBook.Close(true);
            ExcelApp.Quit();
            MessageBox.Show("Excel file created , you can find the file C:/Users/Администратор/source/repos/Zachet2(stud)/tab.xlsx");

        }
    }
}

