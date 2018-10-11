namespace TrabajoDeCampo.Pantallas.Administración
{
    partial class AsignacionDeMaterias
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
            this.comboNiveles = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgAsignadas = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgMaterias = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.btDesasignar = new System.Windows.Forms.Button();
            this.btAsignar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btGuardar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btExport = new System.Windows.Forms.Button();
            this.btCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgAsignadas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMaterias)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboNiveles
            // 
            this.comboNiveles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboNiveles.FormattingEnabled = true;
            this.comboNiveles.Location = new System.Drawing.Point(54, 36);
            this.comboNiveles.Name = "comboNiveles";
            this.comboNiveles.Size = new System.Drawing.Size(218, 21);
            this.comboNiveles.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Tag = "com.td.niveles";
            this.label1.Text = "Niveles";
            // 
            // dgAsignadas
            // 
            this.dgAsignadas.AllowUserToAddRows = false;
            this.dgAsignadas.AllowUserToDeleteRows = false;
            this.dgAsignadas.AllowUserToOrderColumns = true;
            this.dgAsignadas.AllowUserToResizeColumns = false;
            this.dgAsignadas.AllowUserToResizeRows = false;
            this.dgAsignadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAsignadas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2});
            this.dgAsignadas.Location = new System.Drawing.Point(12, 170);
            this.dgAsignadas.Name = "dgAsignadas";
            this.dgAsignadas.ShowEditingIcon = false;
            this.dgAsignadas.ShowRowErrors = false;
            this.dgAsignadas.Size = new System.Drawing.Size(275, 245);
            this.dgAsignadas.TabIndex = 2;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "MATERIA";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // dgMaterias
            // 
            this.dgMaterias.AllowUserToAddRows = false;
            this.dgMaterias.AllowUserToDeleteRows = false;
            this.dgMaterias.AllowUserToResizeColumns = false;
            this.dgMaterias.AllowUserToResizeRows = false;
            this.dgMaterias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMaterias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgMaterias.Location = new System.Drawing.Point(405, 170);
            this.dgMaterias.Name = "dgMaterias";
            this.dgMaterias.ShowEditingIcon = false;
            this.dgMaterias.ShowRowErrors = false;
            this.dgMaterias.Size = new System.Drawing.Size(275, 245);
            this.dgMaterias.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "MATERIA";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(284, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Tag = "com.td.buscar";
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btDesasignar
            // 
            this.btDesasignar.Enabled = false;
            this.btDesasignar.Location = new System.Drawing.Point(299, 199);
            this.btDesasignar.Name = "btDesasignar";
            this.btDesasignar.Size = new System.Drawing.Size(75, 23);
            this.btDesasignar.TabIndex = 5;
            this.btDesasignar.Tag = "com.td.desasignar";
            this.btDesasignar.Text = "Desasignar";
            this.btDesasignar.UseVisualStyleBackColor = true;
            this.btDesasignar.Click += new System.EventHandler(this.btDesasignar_Click);
            // 
            // btAsignar
            // 
            this.btAsignar.Enabled = false;
            this.btAsignar.Location = new System.Drawing.Point(299, 170);
            this.btAsignar.Name = "btAsignar";
            this.btAsignar.Size = new System.Drawing.Size(75, 23);
            this.btAsignar.TabIndex = 6;
            this.btAsignar.Tag = "com.td.desasignar";
            this.btAsignar.Text = "Asignar";
            this.btAsignar.UseVisualStyleBackColor = true;
            this.btAsignar.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 7;
            this.label2.Tag = "com.td.asignadas";
            this.label2.Text = "Asignadas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(402, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 8;
            this.label3.Tag = "com.td.sin.asignar";
            this.label3.Text = "Sin Asignar";
            // 
            // btGuardar
            // 
            this.btGuardar.Location = new System.Drawing.Point(12, 421);
            this.btGuardar.Name = "btGuardar";
            this.btGuardar.Size = new System.Drawing.Size(75, 23);
            this.btGuardar.TabIndex = 9;
            this.btGuardar.Tag = "com.td.guardar";
            this.btGuardar.Text = "Guardar";
            this.btGuardar.UseVisualStyleBackColor = true;
            this.btGuardar.Click += new System.EventHandler(this.btGuardar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btExport);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboNiveles);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 100);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "com.td.busqueda";
            this.groupBox1.Text = "Búsqueda";
            // 
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(584, 34);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(75, 23);
            this.btExport.TabIndex = 5;
            this.btExport.Tag = "com.td.exportar";
            this.btExport.Text = "Exportar";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(605, 421);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(75, 23);
            this.btCancelar.TabIndex = 11;
            this.btCancelar.Tag = "com.td.cancelar";
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            this.btCancelar.Click += new System.EventHandler(this.btCancelar_Click);
            // 
            // AsignacionDeMaterias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 450);
            this.Controls.Add(this.btCancelar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btGuardar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btAsignar);
            this.Controls.Add(this.btDesasignar);
            this.Controls.Add(this.dgMaterias);
            this.Controls.Add(this.dgAsignadas);
            this.Name = "AsignacionDeMaterias";
            this.Tag = "com.td.asignacion.de.materia";
            this.Text = "Asignación de Materias";
            this.Load += new System.EventHandler(this.AsignacionDeMaterias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgAsignadas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMaterias)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboNiveles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgAsignadas;
        private System.Windows.Forms.DataGridView dgMaterias;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btDesasignar;
        private System.Windows.Forms.Button btAsignar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btGuardar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btExport;
    }
}