﻿namespace TrabajoDeCampo.Pantallas.Alumnos
{
    partial class Tutores
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tutores));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.delBtn = new System.Windows.Forms.Button();
            this.modBtn = new System.Windows.Forms.Button();
            this.regiBtn = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre,
            this.apellido,
            this.dni,
            this.email,
            this.Column5});
            this.dataGridView1.Location = new System.Drawing.Point(14, 193);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(740, 346);
            this.dataGridView1.TabIndex = 0;
            // 
            // nombre
            // 
            this.nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // apellido
            // 
            this.apellido.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.apellido.HeaderText = "Apellido";
            this.apellido.Name = "apellido";
            this.apellido.ReadOnly = true;
            this.apellido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dni
            // 
            this.dni.HeaderText = "DNI";
            this.dni.Name = "dni";
            this.dni.ReadOnly = true;
            this.dni.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // email
            // 
            this.email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.email.HeaderText = "Mail";
            this.email.Name = "email";
            this.email.ReadOnly = true;
            this.email.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "Teléfonos";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Location = new System.Drawing.Point(14, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(741, 158);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "com.td.búsqueda";
            this.groupBox1.Text = "Búsqueda";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(641, 122);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(94, 28);
            this.button5.TabIndex = 10;
            this.button5.Tag = "com.td.cancelar";
            this.button5.Text = "Cancelar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 7;
            this.label2.Tag = "com.td.filtrar.por";
            this.label2.Text = "Filtrar por:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(324, 23);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.MaxLength = 30;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(207, 22);
            this.textBox1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(641, 23);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 28);
            this.button1.TabIndex = 5;
            this.button1.Tag = "com.td.buscar";
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(126, 23);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(178, 24);
            this.comboBox2.TabIndex = 3;
            // 
            // delBtn
            // 
            this.delBtn.Location = new System.Drawing.Point(661, 547);
            this.delBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(94, 28);
            this.delBtn.TabIndex = 8;
            this.delBtn.Tag = "com.td.borrar";
            this.delBtn.Text = "Borrar";
            this.delBtn.UseVisualStyleBackColor = true;
            this.delBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // modBtn
            // 
            this.modBtn.Location = new System.Drawing.Point(338, 547);
            this.modBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.modBtn.Name = "modBtn";
            this.modBtn.Size = new System.Drawing.Size(94, 28);
            this.modBtn.TabIndex = 9;
            this.modBtn.Tag = "com.td.modificar";
            this.modBtn.Text = "Modificar";
            this.modBtn.UseVisualStyleBackColor = true;
            this.modBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // regiBtn
            // 
            this.regiBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.regiBtn.Location = new System.Drawing.Point(14, 547);
            this.regiBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.regiBtn.Name = "regiBtn";
            this.regiBtn.Size = new System.Drawing.Size(94, 28);
            this.regiBtn.TabIndex = 10;
            this.regiBtn.Tag = "com.td.registrar";
            this.regiBtn.Text = "Registrar";
            this.regiBtn.UseVisualStyleBackColor = true;
            this.regiBtn.Click += new System.EventHandler(this.button4_Click);
            // 
            // Tutores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 586);
            this.Controls.Add(this.delBtn);
            this.Controls.Add(this.modBtn);
            this.Controls.Add(this.regiBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Tutores";
            this.helpProvider1.SetShowHelp(this, true);
            this.Tag = "com.td.tutores";
            this.Text = "Tutores";
            this.Load += new System.EventHandler(this.Tutores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.Button modBtn;
        private System.Windows.Forms.Button regiBtn;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn apellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn dni;
        private System.Windows.Forms.DataGridViewTextBoxColumn email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}