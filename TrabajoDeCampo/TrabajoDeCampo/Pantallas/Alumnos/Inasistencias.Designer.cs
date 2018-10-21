namespace TrabajoDeCampo.Pantallas.Alumnos
{
    partial class Inasistencias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inasistencias));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.registrar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.media = new System.Windows.Forms.RadioButton();
            this.completa = new System.Windows.Forms.RadioButton();
            this.chJustificado = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.modificar = new System.Windows.Forms.Button();
            this.cancelar = new System.Windows.Forms.Button();
            this.btnExportar = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(12, 289);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(490, 329);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Fecha";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Tipo";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Justificada";
            this.Column3.Name = "Column3";
            // 
            // registrar
            // 
            this.registrar.Location = new System.Drawing.Point(12, 626);
            this.registrar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.registrar.Name = "registrar";
            this.registrar.Size = new System.Drawing.Size(87, 28);
            this.registrar.TabIndex = 1;
            this.registrar.Tag = "com.td.registrar";
            this.registrar.Text = "Registrar";
            this.registrar.UseVisualStyleBackColor = true;
            this.registrar.Click += new System.EventHandler(this.registrar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGuardar);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Location = new System.Drawing.Point(12, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(490, 241);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "com.td.información";
            this.groupBox1.Text = "Información";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(15, 205);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(87, 28);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Tag = "com.td.guardar";
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.media);
            this.groupBox2.Controls.Add(this.completa);
            this.groupBox2.Controls.Add(this.chJustificado);
            this.groupBox2.Location = new System.Drawing.Point(15, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(374, 100);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "com.td.tipo.de.falta";
            this.groupBox2.Text = "Tipo de Falta";
            // 
            // media
            // 
            this.media.AutoSize = true;
            this.media.Location = new System.Drawing.Point(9, 66);
            this.media.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.media.Name = "media";
            this.media.Size = new System.Drawing.Size(61, 20);
            this.media.TabIndex = 6;
            this.media.TabStop = true;
            this.media.Tag = "com.td.media";
            this.media.Text = "Media";
            this.media.UseVisualStyleBackColor = true;
            this.media.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // completa
            // 
            this.completa.AutoSize = true;
            this.completa.Location = new System.Drawing.Point(9, 38);
            this.completa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.completa.Name = "completa";
            this.completa.Size = new System.Drawing.Size(81, 20);
            this.completa.TabIndex = 0;
            this.completa.TabStop = true;
            this.completa.Tag = "com.td.completa";
            this.completa.Text = "Completa";
            this.completa.UseVisualStyleBackColor = true;
            this.completa.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // chJustificado
            // 
            this.chJustificado.AutoSize = true;
            this.chJustificado.Location = new System.Drawing.Point(280, 66);
            this.chJustificado.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chJustificado.Name = "chJustificado";
            this.chJustificado.Size = new System.Drawing.Size(88, 20);
            this.chJustificado.TabIndex = 2;
            this.chJustificado.Tag = "com.td.justificada";
            this.chJustificado.Text = "Justificada";
            this.chJustificado.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 4;
            this.label2.Tag = "com.td.fecha";
            this.label2.Text = "Fecha";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(87, 43);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(302, 22);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // modificar
            // 
            this.modificar.Location = new System.Drawing.Point(210, 626);
            this.modificar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.modificar.Name = "modificar";
            this.modificar.Size = new System.Drawing.Size(87, 28);
            this.modificar.TabIndex = 3;
            this.modificar.Tag = "com.td.modificar";
            this.modificar.Text = "Modificar";
            this.modificar.UseVisualStyleBackColor = true;
            this.modificar.Click += new System.EventHandler(this.modificar_Click);
            // 
            // cancelar
            // 
            this.cancelar.Location = new System.Drawing.Point(415, 626);
            this.cancelar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelar.Name = "cancelar";
            this.cancelar.Size = new System.Drawing.Size(87, 28);
            this.cancelar.TabIndex = 8;
            this.cancelar.Tag = "com.td.cancelar";
            this.cancelar.Text = "Cancelar";
            this.cancelar.UseVisualStyleBackColor = true;
            this.cancelar.Click += new System.EventHandler(this.cancelar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Location = new System.Drawing.Point(27, 253);
            this.btnExportar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(87, 28);
            this.btnExportar.TabIndex = 8;
            this.btnExportar.Tag = "com.td.exportar";
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // Inasistencias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 664);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.cancelar);
            this.Controls.Add(this.modificar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.registrar);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Inasistencias";
            this.helpProvider1.SetShowHelp(this, true);
            this.Tag = "com.td.inasistencias";
            this.Text = "Inasistencias";
            this.Load += new System.EventHandler(this.Inasistencias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button registrar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chJustificado;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.RadioButton completa;
        private System.Windows.Forms.Button modificar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton media;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button cancelar;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}