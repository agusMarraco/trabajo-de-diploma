namespace TrabajoDeCampo.Pantallas.Administración
{
    partial class AltaModificacionCurso
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
            this.txtCapacidad = new System.Windows.Forms.TextBox();
            this.txtLetra = new System.Windows.Forms.TextBox();
            this.comboNiveles = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.rbTurnoMañana = new System.Windows.Forms.RadioButton();
            this.rbTurnoTarde = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCapacidad
            // 
            this.txtCapacidad.Location = new System.Drawing.Point(82, 23);
            this.txtCapacidad.MaxLength = 2;
            this.txtCapacidad.Name = "txtCapacidad";
            this.txtCapacidad.Size = new System.Drawing.Size(44, 20);
            this.txtCapacidad.TabIndex = 1;
            // 
            // txtLetra
            // 
            this.txtLetra.Location = new System.Drawing.Point(199, 23);
            this.txtLetra.MaxLength = 1;
            this.txtLetra.Name = "txtLetra";
            this.txtLetra.Size = new System.Drawing.Size(49, 20);
            this.txtLetra.TabIndex = 2;
            // 
            // comboNiveles
            // 
            this.comboNiveles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboNiveles.FormattingEnabled = true;
            this.comboNiveles.Location = new System.Drawing.Point(324, 22);
            this.comboNiveles.Name = "comboNiveles";
            this.comboNiveles.Size = new System.Drawing.Size(161, 21);
            this.comboNiveles.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 4;
            this.label1.Tag = "com.td.nivel";
            this.label1.Text = "Nivel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 5;
            this.label2.Tag = "com.td.letra";
            this.label2.Text = "Letra";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Tag = "com.td.capacidad";
            this.label3.Text = "Capacidad";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Tag = "com.td.guardar";
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbTurnoMañana
            // 
            this.rbTurnoMañana.AutoSize = true;
            this.rbTurnoMañana.Location = new System.Drawing.Point(6, 29);
            this.rbTurnoMañana.Name = "rbTurnoMañana";
            this.rbTurnoMañana.Size = new System.Drawing.Size(64, 17);
            this.rbTurnoMañana.TabIndex = 9;
            this.rbTurnoMañana.TabStop = true;
            this.rbTurnoMañana.Tag = "com.td.mañana";
            this.rbTurnoMañana.Text = "Mañana";
            this.rbTurnoMañana.UseVisualStyleBackColor = true;
            // 
            // rbTurnoTarde
            // 
            this.rbTurnoTarde.AutoSize = true;
            this.rbTurnoTarde.Location = new System.Drawing.Point(6, 64);
            this.rbTurnoTarde.Name = "rbTurnoTarde";
            this.rbTurnoTarde.Size = new System.Drawing.Size(53, 17);
            this.rbTurnoTarde.TabIndex = 10;
            this.rbTurnoTarde.TabStop = true;
            this.rbTurnoTarde.Tag = "com.td.tarde";
            this.rbTurnoTarde.Text = "Tarde";
            this.rbTurnoTarde.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbTurnoMañana);
            this.groupBox1.Controls.Add(this.rbTurnoTarde);
            this.groupBox1.Location = new System.Drawing.Point(21, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "com.td.turno";
            this.groupBox1.Text = "Turno";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(410, 197);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Tag = "com.td.cancelar";
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AltaModificacionCurso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 232);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboNiveles);
            this.Controls.Add(this.txtLetra);
            this.Controls.Add(this.txtCapacidad);
            this.Name = "AltaModificacionCurso";
            this.Tag = "com.td.curso";
            this.Text = "Curso ";
            this.Load += new System.EventHandler(this.AltaModificacionCurso_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtCapacidad;
        private System.Windows.Forms.TextBox txtLetra;
        private System.Windows.Forms.ComboBox comboNiveles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbTurnoTarde;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton rbTurnoMañana;
    }
}