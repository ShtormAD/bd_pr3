using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace pr3
{
    public partial class Form1 : Form
    {
        private bool id_mode = false;
        private MySqlConnection conn;
        private String[] cols;
        private String[] cols_lang;
        private String[] tables;
        public Form1()
        {
            InitializeComponent();
            //Подключение к БД и заполнение списка таблиц
            try
            {
                conn = DBUtils.GetDBConnection();
                conn.Open();
                tables = DBUtils.getTables(conn);
                foreach (String s in DBUtils.getTablesLang(conn))
                {
                    cb_tables.Items.Add(s);
                    cb_tables.SelectedIndex = 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        //При изменении выбранной таблицы
        private void cb_tables_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_id_mode_change.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            if (cb_tables.Text.Equals("Выдачи"))
                btn_id_mode_change.Visible = true;
            else if (cb_tables.Text.Equals("Сотрудники"))
                textBox1.Visible = true;
            else if (cb_tables.Text.Equals("Студенты"))
                textBox2.Visible = true;
            updateTable();
        }

        //Удаление записи
        private void btn_del_Click(object sender, EventArgs e)
        {
            DBUtils.delRow(conn, int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()), cb_tables.SelectedItem.ToString(), dataGridView1.Columns[0].HeaderText);
            updateTable();
        }

        //Добавление записи
        private void button1_Click(object sender, EventArgs e)
        {
            DialogInp di = new DialogInp(cols);
            if (di.ShowDialog(this) == DialogResult.OK)
            {
                DBUtils.addRow(conn, di.cols.ToArray(), di.vals.ToArray(), cb_tables.SelectedItem.ToString());
                updateTable();
            }
            else
            {
            }
        }

        //Загрузка данных в dataGridView из БД
        private void updateTable()
        {
            //Очищаем грид
            dataGridView1.Rows.Clear();
            int count = this.dataGridView1.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                this.dataGridView1.Columns.RemoveAt(0);
            }

            List<String[]> data;
            //Получаем массив из заголовков и значений
            if (cb_tables.Text.Equals("Выдачи") && id_mode)
            {
                data = DBUtils.LoadExtr(conn);
            } else if(cb_tables.Text.Equals("Сотрудники") && textBox1.Text.Length > 0)
            {
                data = DBUtils.LoadData(conn, tables[cb_tables.SelectedIndex], textBox1.Text);
            }
            else if (cb_tables.Text.Equals("Студенты") && textBox2.Text.Length > 0)
            {
                data = DBUtils.LoadStudCount(conn, textBox2.Text);
            }
            else
            {
                data = DBUtils.LoadData(conn, tables[cb_tables.SelectedIndex]);
            }

            //заголовки выносим в отдельный массив
            cols_lang = data[0];
            data.RemoveAt(0);
            cols = data[0];
            data.RemoveAt(0);

            //Создаем столбцы 
            for(int i = 0; i < cols.Length; i++)
            {
                dataGridView1.Columns.Add(cols[i], cols_lang[i]);
            }

            //Заполняем строки
            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
            dataGridView1.AutoResizeColumns();

            //Выбираем грид и строку в нем
            dataGridView1.Select();
            if (dataGridView1.Rows.Count > 0)
                dataGridView1.Rows[0].Selected = true;

            }

        //Кнопка редактирования
        private void btn_edit_Click(object sender, EventArgs e)
        {
            //Коипруем поля выбранной строки
            String[] vals = new String[cols.Length-1];
            for (int i = 1; i < dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells.Count; i++)
            {
                vals[i-1] = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[i].Value.ToString();
            }

            //Вызываем диалог для редактирования, если все хорошо, проводим запрос
            DialogInp di = new DialogInp(cols, vals);
            if (di.ShowDialog(this) == DialogResult.OK)
            {
                DBUtils.editRow(conn, di.cols.ToArray(), di.vals.ToArray(), cb_tables.SelectedItem.ToString(), dataGridView1.Columns[0].HeaderText, int.Parse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value.ToString()));
                updateTable();
            }
        }

        private void btn_id_mode_change_Click(object sender, EventArgs e)
        {
            id_mode = !id_mode;
            updateTable();
        }

        private void btn_exp_xml_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            String fn = "";

            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    fn = saveFileDialog1.FileName;
                    myStream.Close();
                }
            }

            XmlWriter writer = null;

            try
            {

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = ("\t");
                settings.OmitXmlDeclaration = true;
                settings.Encoding = Encoding.UTF8;
                String tmpstr;
                if (!fn.Equals(""))
                {
                    writer = XmlWriter.Create(fn, settings);
                    writer.WriteStartElement("table");
                    tmpstr = "";
                    int columnidx = dataGridView1.Columns.Count;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        tmpstr += column.HeaderText;
                        if (columnidx > 1)
                            tmpstr += ";";
                        columnidx--;
                    }
                    writer.WriteElementString("header", tmpstr);

                    int rowidx = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        String ElementName = "row" + rowidx.ToString();
                        tmpstr = "";
                        columnidx = dataGridView1.Columns.Count;

                        foreach (DataGridViewCell column in row.Cells)
                        {
                            if (null != column.Value)
                                tmpstr += column.Value.ToString();
                            if (columnidx > 1)
                                tmpstr += ";";
                            columnidx--;
                        }
                        writer.WriteElementString(ElementName, tmpstr);
                        rowidx++;
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        private void btn_exp_html_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            String fn = "";

            saveFileDialog1.Filter = "html files (*.html)|*.html";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    fn = saveFileDialog1.FileName;
                    myStream.Close();
                }
            }

            XmlWriter writer = null;

            try
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = ("\t");
                settings.OmitXmlDeclaration = true;
                settings.Encoding = Encoding.UTF8;

                if (!fn.Equals(""))
                {
                    writer = XmlWriter.Create(fn, settings);
                    // Запись комментария:
                    writer.WriteStartElement("table");
                    writer.WriteStartElement("tbody");
                    // Запись заголовка таблицы HTML:
                    writer.WriteStartElement("tr");
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        writer.WriteElementString("th", column.HeaderText);
                    }
                    writer.WriteEndElement();  // закрытие тега tr
                                               // Запись всех строк:
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        writer.WriteStartElement("tr");
                        foreach (DataGridViewCell column in row.Cells)
                        {
                            if (null != column.Value)
                                writer.WriteElementString("td", column.Value.ToString());
                            else
                                writer.WriteElementString("td", " ");
                        }
                        writer.WriteEndElement();  // закрытие тега tr
                    }
                    writer.WriteEndElement();  // закрытие тега tbody
                    writer.WriteEndElement();  // закрытие тега table
                    writer.Flush();
                }   
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }

        }

        private void btn_exp_excel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга.
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    ExcelApp.Cells[i + 1, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }
            //Вызываем нашу созданную эксельку.
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updateTable();
            textBox1.Select();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            updateTable();
            textBox2.Select();
        }
    }
}
