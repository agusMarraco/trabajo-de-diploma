namespace TrabajoDeCampo.Pantallas.Seguridad
{
    partial class AltaModificacionUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AltaModificacionUsuario));
            this.nombrelbl = new System.Windows.Forms.Label();
            this.nombre = new System.Windows.Forms.TextBox();
            this.apellido = new System.Windows.Forms.TextBox();
            this.apellidolbl = new System.Windows.Forms.Label();
            this.dni = new System.Windows.Forms.TextBox();
            this.dnilbl = new System.Windows.Forms.Label();
            this.direccion = new System.Windows.Forms.TextBox();
            this.direccionlbl = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.TextBox();
            this.emaillbl = new System.Windows.Forms.Label();
            this.alias = new System.Windows.Forms.TextBox();
            this.aliaslbl = new System.Windows.Forms.Label();
            this.telefonolbl = new System.Windows.Forms.Label();
            this.telefono = new System.Windows.Forms.MaskedTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dgfamilias = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.dgfamiliapatente = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgpatentes = new System.Windows.Forms.DataGridView();
            this.desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asignada = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.negada = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.dgfamilias)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgfamiliapatente)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgpatentes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nombrelbl
            // 
            this.nombrelbl.AutoSize = true;
            this.nombrelbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombrelbl.Location = new System.Drawing.Point(7, 13);
            this.nombrelbl.Name = "nombrelbl";
            this.nombrelbl.Size = new System.Drawing.Size(53, 16);
            this.nombrelbl.TabIndex = 0;
            this.nombrelbl.Tag = "com.td.nombre";
            this.nombrelbl.Text = "Nombre";
            // 
            // nombre
            // 
            this.nombre.Location = new System.Drawing.Point(84, 9);
            this.nombre.MaxLength = 20;
            this.nombre.Name = "nombre";
            this.nombre.ShortcutsEnabled = false;
            this.nombre.Size = new System.Drawing.Size(296, 20);
            this.nombre.TabIndex = 0;
            // 
            // apellido
            // 
            this.apellido.Location = new System.Drawing.Point(84, 35);
            this.apellido.MaxLength = 20;
            this.apellido.Name = "apellido";
            this.apellido.ShortcutsEnabled = false;
            this.apellido.Size = new System.Drawing.Size(296, 20);
            this.apellido.TabIndex = 1;
            // 
            // apellidolbl
            // 
            this.apellidolbl.AutoSize = true;
            this.apellidolbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apellidolbl.Location = new System.Drawing.Point(7, 40);
            this.apellidolbl.Name = "apellidolbl";
            this.apellidolbl.Size = new System.Drawing.Size(54, 16);
            this.apellidolbl.TabIndex = 2;
            this.apellidolbl.Tag = "com.td.apellido";
            this.apellidolbl.Text = "Apellido";
            // 
            // dni
            // 
            this.dni.Location = new System.Drawing.Point(84, 61);
            this.dni.MaxLength = 9;
            this.dni.Name = "dni";
            this.dni.ShortcutsEnabled = false;
            this.dni.Size = new System.Drawing.Size(296, 20);
            this.dni.TabIndex = 2;
            // 
            // dnilbl
            // 
            this.dnilbl.AutoSize = true;
            this.dnilbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dnilbl.Location = new System.Drawing.Point(7, 65);
            this.dnilbl.Name = "dnilbl";
            this.dnilbl.Size = new System.Drawing.Size(41, 16);
            this.dnilbl.TabIndex = 4;
            this.dnilbl.Tag = "com.td.d.n.i.";
            this.dnilbl.Text = "D.N.I.";
            // 
            // direccion
            // 
            this.direccion.Location = new System.Drawing.Point(84, 87);
            this.direccion.MaxLength = 50;
            this.direccion.Name = "direccion";
            this.direccion.ShortcutsEnabled = false;
            this.direccion.Size = new System.Drawing.Size(296, 20);
            this.direccion.TabIndex = 3;
            // 
            // direccionlbl
            // 
            this.direccionlbl.AutoSize = true;
            this.direccionlbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.direccionlbl.Location = new System.Drawing.Point(7, 91);
            this.direccionlbl.Name = "direccionlbl";
            this.direccionlbl.Size = new System.Drawing.Size(62, 16);
            this.direccionlbl.TabIndex = 6;
            this.direccionlbl.Tag = "com.td.dirección";
            this.direccionlbl.Text = "Dirección";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(466, 36);
            this.email.MaxLength = 50;
            this.email.Name = "email";
            this.email.ShortcutsEnabled = false;
            this.email.Size = new System.Drawing.Size(300, 20);
            this.email.TabIndex = 6;
            // 
            // emaillbl
            // 
            this.emaillbl.AutoSize = true;
            this.emaillbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.emaillbl.Location = new System.Drawing.Point(408, 40);
            this.emaillbl.Name = "emaillbl";
            this.emaillbl.Size = new System.Drawing.Size(41, 16);
            this.emaillbl.TabIndex = 8;
            this.emaillbl.Tag = "com.td.mail";
            this.emaillbl.Text = "Email";
            // 
            // alias
            // 
            this.alias.Location = new System.Drawing.Point(466, 9);
            this.alias.MaxLength = 50;
            this.alias.Name = "alias";
            this.alias.ShortcutsEnabled = false;
            this.alias.Size = new System.Drawing.Size(300, 20);
            this.alias.TabIndex = 5;
            // 
            // aliaslbl
            // 
            this.aliaslbl.AutoSize = true;
            this.aliaslbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aliaslbl.Location = new System.Drawing.Point(408, 13);
            this.aliaslbl.Name = "aliaslbl";
            this.aliaslbl.Size = new System.Drawing.Size(37, 16);
            this.aliaslbl.TabIndex = 10;
            this.aliaslbl.Tag = "com.td.alias";
            this.aliaslbl.Text = "Alias";
            // 
            // telefonolbl
            // 
            this.telefonolbl.AutoSize = true;
            this.telefonolbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.telefonolbl.Location = new System.Drawing.Point(7, 114);
            this.telefonolbl.Name = "telefonolbl";
            this.telefonolbl.Size = new System.Drawing.Size(55, 16);
            this.telefonolbl.TabIndex = 13;
            this.telefonolbl.Tag = "com.td.teléfono";
            this.telefonolbl.Text = "Teléfono";
            // 
            // telefono
            // 
            this.telefono.Location = new System.Drawing.Point(84, 110);
            this.telefono.Mask = "00-0000-0000";
            this.telefono.Name = "telefono";
            this.telefono.ShortcutsEnabled = false;
            this.telefono.Size = new System.Drawing.Size(296, 20);
            this.telefono.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 453);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 16;
            this.button1.Tag = "com.td.guardar";
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(677, 453);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 23);
            this.button2.TabIndex = 21;
            this.button2.Tag = "com.td.cancelar";
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dgfamilias
            // 
            this.dgfamilias.AllowUserToAddRows = false;
            this.dgfamilias.AllowUserToDeleteRows = false;
            this.dgfamilias.AllowUserToResizeColumns = false;
            this.dgfamilias.AllowUserToResizeRows = false;
            this.dgfamilias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgfamilias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgfamilias.Location = new System.Drawing.Point(6, 40);
            this.dgfamilias.Name = "dgfamilias";
            this.dgfamilias.ShowEditingIcon = false;
            this.dgfamilias.Size = new System.Drawing.Size(360, 230);
            this.dgfamilias.TabIndex = 17;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "MyProperty";
            this.Column1.HeaderText = "FAMILIA";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "_ck";
            this.Column2.HeaderText = "ASIGNADA";
            this.Column2.Name = "Column2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 16);
            this.label7.TabIndex = 19;
            this.label7.Tag = "com.td.familias";
            this.label7.Text = "Familias";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 16);
            this.label9.TabIndex = 20;
            this.label9.Tag = "com.td.patentes";
            this.label9.Text = "Patentes";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(10, 136);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 302);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.dgfamiliapatente);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.dgfamilias);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Tag = "com.td.familias";
            this.tabPage1.Text = "Familias";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(394, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 16);
            this.label11.TabIndex = 21;
            this.label11.Tag = "com.td.patentes.incluidas";
            this.label11.Text = "Patentes Incluidas";
            // 
            // dgfamiliapatente
            // 
            this.dgfamiliapatente.AllowUserToAddRows = false;
            this.dgfamiliapatente.AllowUserToDeleteRows = false;
            this.dgfamiliapatente.AllowUserToResizeColumns = false;
            this.dgfamiliapatente.AllowUserToResizeRows = false;
            this.dgfamiliapatente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgfamiliapatente.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5});
            this.dgfamiliapatente.Location = new System.Drawing.Point(397, 40);
            this.dgfamiliapatente.Name = "dgfamiliapatente";
            this.dgfamiliapatente.ShowEditingIcon = false;
            this.dgfamiliapatente.Size = new System.Drawing.Size(349, 230);
            this.dgfamiliapatente.TabIndex = 20;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "PATENTE";
            this.Column5.Name = "Column5";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgpatentes);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 276);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = "com.td.patentes";
            this.tabPage2.Text = "Patentes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgpatentes
            // 
            this.dgpatentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgpatentes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.desc,
            this.asignada,
            this.negada});
            this.dgpatentes.Location = new System.Drawing.Point(9, 47);
            this.dgpatentes.Name = "dgpatentes";
            this.dgpatentes.ShowCellErrors = false;
            this.dgpatentes.ShowCellToolTips = false;
            this.dgpatentes.ShowEditingIcon = false;
            this.dgpatentes.ShowRowErrors = false;
            this.dgpatentes.Size = new System.Drawing.Size(737, 211);
            this.dgpatentes.TabIndex = 21;
            // 
            // desc
            // 
            this.desc.HeaderText = "desc";
            this.desc.Name = "desc";
            this.desc.ReadOnly = true;
            // 
            // asignada
            // 
            this.asignada.HeaderText = "asignada";
            this.asignada.Name = "asignada";
            // 
            // negada
            // 
            this.negada.HeaderText = "negada";
            this.negada.Name = "negada";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(411, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 59);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "com.td.idioma";
            this.groupBox1.Text = "Idioma";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(55, 32);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(148, 21);
            this.comboBox1.TabIndex = 7;
            // 
            // AltaModificacionUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 492);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.telefono);
            this.Controls.Add(this.telefonolbl);
            this.Controls.Add(this.alias);
            this.Controls.Add(this.aliaslbl);
            this.Controls.Add(this.email);
            this.Controls.Add(this.emaillbl);
            this.Controls.Add(this.direccion);
            this.Controls.Add(this.direccionlbl);
            this.Controls.Add(this.dni);
            this.Controls.Add(this.dnilbl);
            this.Controls.Add(this.apellido);
            this.Controls.Add(this.apellidolbl);
            this.Controls.Add(this.nombre);
            this.Controls.Add(this.nombrelbl);
            this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AltaModificacionUsuario";
            this.helpProvider1.SetShowHelp(this, true);
            this.Tag = "com.td.usuario";
            this.Text = "Usuario";
            this.Load += new System.EventHandler(this.AltaModificacionUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgfamilias)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgfamiliapatente)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgpatentes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nombrelbl;
        private System.Windows.Forms.TextBox nombre;
        private System.Windows.Forms.TextBox apellido;
        private System.Windows.Forms.Label apellidolbl;
        private System.Windows.Forms.TextBox dni;
        private System.Windows.Forms.Label dnilbl;
        private System.Windows.Forms.TextBox direccion;
        private System.Windows.Forms.Label direccionlbl;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.Label emaillbl;
        private System.Windows.Forms.TextBox alias;
        private System.Windows.Forms.Label aliaslbl;
        private System.Windows.Forms.Label telefonolbl;
        private System.Windows.Forms.MaskedTextBox telefono;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dgfamilias;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgfamiliapatente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dgpatentes;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn asignada;
        private System.Windows.Forms.DataGridViewCheckBoxColumn negada;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}