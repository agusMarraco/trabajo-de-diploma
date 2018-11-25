namespace TrabajoDeCampo.Pantallas.Seguridad
{
    partial class Bitácora
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bitácora));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BIT_FECHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.USU_ALIAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BIT_CRITICIDAD_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BIT_MENSAJE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboUsuarios = new System.Windows.Forms.ComboBox();
            this.chUsuario = new System.Windows.Forms.CheckBox();
            this.chCriticidad = new System.Windows.Forms.CheckBox();
            this.chFecha = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.toDatepicker = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.fromDatepicker = new System.Windows.Forms.DateTimePicker();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BIT_FECHA,
            this.USU_ALIAS,
            this.BIT_CRITICIDAD_ID,
            this.BIT_MENSAJE});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dataGridView1.Location = new System.Drawing.Point(12, 192);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(1044, 292);
            this.dataGridView1.TabIndex = 0;
            // 
            // BIT_FECHA
            // 
            this.BIT_FECHA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BIT_FECHA.HeaderText = "Fecha";
            this.BIT_FECHA.Name = "BIT_FECHA";
            this.BIT_FECHA.ReadOnly = true;
            this.BIT_FECHA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.BIT_FECHA.Width = 200;
            // 
            // USU_ALIAS
            // 
            this.USU_ALIAS.HeaderText = "Alias";
            this.USU_ALIAS.Name = "USU_ALIAS";
            this.USU_ALIAS.ReadOnly = true;
            this.USU_ALIAS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // BIT_CRITICIDAD_ID
            // 
            this.BIT_CRITICIDAD_ID.HeaderText = "Criticidad";
            this.BIT_CRITICIDAD_ID.Name = "BIT_CRITICIDAD_ID";
            this.BIT_CRITICIDAD_ID.ReadOnly = true;
            this.BIT_CRITICIDAD_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // BIT_MENSAJE
            // 
            this.BIT_MENSAJE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BIT_MENSAJE.HeaderText = "Descripción";
            this.BIT_MENSAJE.Name = "BIT_MENSAJE";
            this.BIT_MENSAJE.ReadOnly = true;
            this.BIT_MENSAJE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboUsuarios);
            this.groupBox1.Controls.Add(this.chUsuario);
            this.groupBox1.Controls.Add(this.chCriticidad);
            this.groupBox1.Controls.Add(this.chFecha);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.toDatepicker);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.fromDatepicker);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(1044, 161);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "com.td.búsqueda";
            this.groupBox1.Text = "Búsqueda";
            // 
            // comboUsuarios
            // 
            this.comboUsuarios.FormattingEnabled = true;
            this.comboUsuarios.Location = new System.Drawing.Point(22, 63);
            this.comboUsuarios.Name = "comboUsuarios";
            this.comboUsuarios.Size = new System.Drawing.Size(232, 24);
            this.comboUsuarios.TabIndex = 17;
            // 
            // chUsuario
            // 
            this.chUsuario.AutoSize = true;
            this.chUsuario.Location = new System.Drawing.Point(22, 30);
            this.chUsuario.Name = "chUsuario";
            this.chUsuario.Size = new System.Drawing.Size(56, 20);
            this.chUsuario.TabIndex = 16;
            this.chUsuario.Tag = "com.td.alias";
            this.chUsuario.Text = "Alias";
            this.chUsuario.UseVisualStyleBackColor = true;
            // 
            // chCriticidad
            // 
            this.chCriticidad.AutoSize = true;
            this.chCriticidad.Location = new System.Drawing.Point(278, 33);
            this.chCriticidad.Name = "chCriticidad";
            this.chCriticidad.Size = new System.Drawing.Size(81, 20);
            this.chCriticidad.TabIndex = 15;
            this.chCriticidad.Tag = "com.td.criticidad";
            this.chCriticidad.Text = "Criticidad";
            this.chCriticidad.UseVisualStyleBackColor = true;
            // 
            // chFecha
            // 
            this.chFecha.AutoSize = true;
            this.chFecha.Location = new System.Drawing.Point(418, 33);
            this.chFecha.Name = "chFecha";
            this.chFecha.Size = new System.Drawing.Size(63, 20);
            this.chFecha.TabIndex = 14;
            this.chFecha.Tag = "com.td.fecha";
            this.chFecha.Text = "Fecha";
            this.chFecha.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(934, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 33);
            this.button3.TabIndex = 2;
            this.button3.Tag = "com.td.exportar";
            this.button3.Text = "Exportar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(934, 123);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 31);
            this.button2.TabIndex = 13;
            this.button2.Tag = "com.td.cancelar";
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(669, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 12;
            this.label3.Tag = "com.td.hasta";
            this.label3.Text = "Hasta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(415, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 11;
            this.label2.Tag = "com.td.desde";
            this.label2.Text = "Desde";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(278, 63);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 10;
            // 
            // toDatepicker
            // 
            this.toDatepicker.CustomFormat = "";
            this.toDatepicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.toDatepicker.Location = new System.Drawing.Point(744, 66);
            this.toDatepicker.MaxDate = new System.DateTime(2018, 9, 19, 0, 0, 0, 0);
            this.toDatepicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.toDatepicker.Name = "toDatepicker";
            this.toDatepicker.Size = new System.Drawing.Size(148, 22);
            this.toDatepicker.TabIndex = 6;
            this.toDatepicker.Value = new System.DateTime(2018, 9, 19, 0, 0, 0, 0);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(934, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 31);
            this.button1.TabIndex = 5;
            this.button1.Tag = "com.td.buscar";
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fromDatepicker
            // 
            this.fromDatepicker.CustomFormat = "";
            this.fromDatepicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDatepicker.Location = new System.Drawing.Point(490, 66);
            this.fromDatepicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.fromDatepicker.Name = "fromDatepicker";
            this.fromDatepicker.Size = new System.Drawing.Size(148, 22);
            this.fromDatepicker.TabIndex = 3;
            // 
            // Bitácora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 497);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Bitácora";
            this.helpProvider1.SetShowHelp(this, true);
            this.Tag = "com.td.bitácora";
            this.Text = "Bitácora";
            this.Load += new System.EventHandler(this.Bitácora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker toDatepicker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker fromDatepicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chUsuario;
        private System.Windows.Forms.CheckBox chCriticidad;
        private System.Windows.Forms.CheckBox chFecha;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ComboBox comboUsuarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn BIT_FECHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn USU_ALIAS;
        private System.Windows.Forms.DataGridViewTextBoxColumn BIT_CRITICIDAD_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BIT_MENSAJE;
    }
}