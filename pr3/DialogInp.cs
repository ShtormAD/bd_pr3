using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace pr3
{
    public partial class DialogInp : Form
    {
        public List<String> cols;
        public List<String> vals;

        //Конструктор для создания записей
        public DialogInp(String[] cols)
        {
            this.cols = new List<String>();
            this.vals = new List<String>();
            InitializeComponent();
            //Получаем имена колонок и создаем их в гриде
            foreach (String s in cols)
            {
                dataGridView1.Columns.Add(s, s);
                this.cols.Add(s);
            }
            //Удаляем колонку с id, т.к. она будет заполнена автоинкрементом в БД
            this.cols.RemoveAt(0);
            dataGridView1.Columns.RemoveAt(0);
            //Создаем строку для внесения данных
            dataGridView1.Rows.Add();
        }

        //Конструктор для редактирования записей
        public DialogInp(String[] cols, String[] vals)
        {
            this.cols = new List<String>();
            this.vals = new List<String>();
            InitializeComponent();
            //Получаем имена колонок и создаем их в гриде
            foreach (String s in cols)
            {
                dataGridView1.Columns.Add(s, s);
                this.cols.Add(s);
            }
            //Удаляем колонку с id, т.к. она будет заполнена автоинкрементом в БД
            this.cols.RemoveAt(0);
            dataGridView1.Columns.RemoveAt(0);
            //Заполняем поля данными для редактирования, меняем текст на кнопке
            dataGridView1.Rows.Add(vals);
            button1.Text = "Изменить";
        }

        //Кнопка отмены
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        //Применение
        private void button1_Click(object sender, EventArgs e)
        {
            bool cor = true;
            //Читаем все ячейки, проверяем, что все заполнено
            for(int i = 0; i < cols.Count; i++)
            {
                if (dataGridView1.Rows[0].Cells[i].Value != null) vals.Add(dataGridView1.Rows[0].Cells[i].Value.ToString());
                else cor = false;
            }
            if(cor) DialogResult = DialogResult.OK;
            else MessageBox.Show(
                    "Проверьте корректность введенных данных",
                    "Внимание!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
