using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace pr3
{
    public partial class Form1 : Form
    {
        private MySqlConnection conn;
        private String[] cols;
        public Form1()
        {
            conn = DBUtils.GetDBConnection();
            InitializeComponent();
            //Подключение к БД и заполнение списка таблиц
            try
            {
                conn.Open();
                foreach (String s in DBUtils.getTables(conn))
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
            //Получаем массив из заголовков и значений, заголовки выносим в отдельный массив
            List<String[]> data = DBUtils.LoadData(conn, cb_tables.Text);
            cols = data[0];
            data.RemoveAt(0);

            //Создаем столбцы 
            foreach (string s in cols)
                dataGridView1.Columns.Add(s, s);

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
    }
}
