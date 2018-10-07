namespace TrabajoDeCampo.Pantallas.Seguridad
{
    partial class CambiarContraseña
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
            this.label1 = new System.Windows.Forms.Label();
            this.actual = new System.Windows.Forms.TextBox();
            this.nueva = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nuevaRepetido = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 0;
            this.label1.Tag = "com.td.contraseña";
            this.label1.Text = "Contraseña Actual";
            // 
            // actual
            // 
            this.actual.Location = new System.Drawing.Point(231, 25);
            this.actual.MaxLength = 50;
            this.actual.Name = "actual";
            this.actual.PasswordChar = '*';
            this.actual.Size = new System.Drawing.Size(292, 20);
            this.actual.TabIndex = 1;
            // 
            // nueva
            // 
            this.nueva.Location = new System.Drawing.Point(231, 89);
            this.nueva.MaxLength = 50;
            this.nueva.Name = "nueva";
            this.nueva.PasswordChar = '*';
            this.nueva.Size = new System.Drawing.Size(292, 20);
            this.nueva.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Tag = "com.td.guardar";
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 16);
            this.label2.TabIndex = 4;
            this.label2.Tag = "com.td.reingrese.su.nueva.contraseña";
            this.label2.Text = "Reingrese su nueva contraseña";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 16);
            this.label3.TabIndex = 5;
            this.label3.Tag = "com.td.nueva.contraseña";
            this.label3.Text = "Nueva Contraseña";
            // 
            // nuevaRepetido
            // 
            this.nuevaRepetido.Location = new System.Drawing.Point(231, 123);
            this.nuevaRepetido.MaxLength = 50;
            this.nuevaRepetido.Name = "nuevaRepetido";
            this.nuevaRepetido.PasswordChar = '*';
            this.nuevaRepetido.Size = new System.Drawing.Size(292, 20);
            this.nuevaRepetido.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(448, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Tag = "com.td.cancelar";
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CambiarContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 244);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.nuevaRepetido);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nueva);
            this.Controls.Add(this.actual);
            this.Controls.Add(this.label1);
            this.Name = "CambiarContraseña";
            this.Text = "Cambiar Contraseña";
            this.Load += new System.EventHandler(this.CambiarContraseña_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox actual;
        private System.Windows.Forms.TextBox nueva;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nuevaRepetido;
        private System.Windows.Forms.Button button2;
    }
}