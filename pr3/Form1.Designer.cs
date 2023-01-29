namespace pr3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cb_tables = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.btn_id_mode_change = new System.Windows.Forms.Button();
            this.btn_exp_xml = new System.Windows.Forms.Button();
            this.btn_exp_html = new System.Windows.Forms.Button();
            this.btn_exp_excel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(783, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(147, 17);
            this.toolStripStatusLabel1.Text = "Соединение установлено";
            // 
            // cb_tables
            // 
            this.cb_tables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_tables.FormattingEnabled = true;
            this.cb_tables.Location = new System.Drawing.Point(12, 12);
            this.cb_tables.Name = "cb_tables";
            this.cb_tables.Size = new System.Drawing.Size(153, 21);
            this.cb_tables.TabIndex = 1;
            this.cb_tables.SelectedIndexChanged += new System.EventHandler(this.cb_tables_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(12, 39);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(759, 386);
            this.dataGridView1.TabIndex = 2;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(171, 12);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 3;
            this.btn_add.Text = "Добавить";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(252, 12);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(75, 23);
            this.btn_edit.TabIndex = 4;
            this.btn_edit.Text = "Изменить";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(333, 12);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(75, 23);
            this.btn_del.TabIndex = 5;
            this.btn_del.Text = "Удалить";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // btn_id_mode_change
            // 
            this.btn_id_mode_change.Location = new System.Drawing.Point(696, 12);
            this.btn_id_mode_change.Name = "btn_id_mode_change";
            this.btn_id_mode_change.Size = new System.Drawing.Size(75, 23);
            this.btn_id_mode_change.TabIndex = 6;
            this.btn_id_mode_change.Text = "id <-> текст";
            this.btn_id_mode_change.UseVisualStyleBackColor = true;
            this.btn_id_mode_change.Visible = false;
            this.btn_id_mode_change.Click += new System.EventHandler(this.btn_id_mode_change_Click);
            // 
            // btn_exp_xml
            // 
            this.btn_exp_xml.Location = new System.Drawing.Point(414, 12);
            this.btn_exp_xml.Name = "btn_exp_xml";
            this.btn_exp_xml.Size = new System.Drawing.Size(75, 23);
            this.btn_exp_xml.TabIndex = 7;
            this.btn_exp_xml.Text = "Экспорт xml";
            this.btn_exp_xml.UseVisualStyleBackColor = true;
            this.btn_exp_xml.Click += new System.EventHandler(this.btn_exp_xml_Click);
            // 
            // btn_exp_html
            // 
            this.btn_exp_html.Location = new System.Drawing.Point(495, 12);
            this.btn_exp_html.Name = "btn_exp_html";
            this.btn_exp_html.Size = new System.Drawing.Size(79, 23);
            this.btn_exp_html.TabIndex = 8;
            this.btn_exp_html.Text = "Экспорт html";
            this.btn_exp_html.UseVisualStyleBackColor = true;
            this.btn_exp_html.Click += new System.EventHandler(this.btn_exp_html_Click);
            // 
            // btn_exp_excel
            // 
            this.btn_exp_excel.Location = new System.Drawing.Point(580, 12);
            this.btn_exp_excel.Name = "btn_exp_excel";
            this.btn_exp_excel.Size = new System.Drawing.Size(90, 23);
            this.btn_exp_excel.TabIndex = 9;
            this.btn_exp_excel.Text = "Экспорт Excel";
            this.btn_exp_excel.UseVisualStyleBackColor = true;
            this.btn_exp_excel.Click += new System.EventHandler(this.btn_exp_excel_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(677, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(94, 20);
            this.textBox1.TabIndex = 10;
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(676, 14);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(95, 20);
            this.textBox2.TabIndex = 11;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 450);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_exp_excel);
            this.Controls.Add(this.btn_exp_html);
            this.Controls.Add(this.btn_exp_xml);
            this.Controls.Add(this.btn_id_mode_change);
            this.Controls.Add(this.btn_del);
            this.Controls.Add(this.btn_edit);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cb_tables);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Библиотека";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ComboBox cb_tables;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_id_mode_change;
        private System.Windows.Forms.Button btn_exp_xml;
        private System.Windows.Forms.Button btn_exp_html;
        private System.Windows.Forms.Button btn_exp_excel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}

