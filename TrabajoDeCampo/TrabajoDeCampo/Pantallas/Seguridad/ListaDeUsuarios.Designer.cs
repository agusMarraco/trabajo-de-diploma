namespace TrabajoDeCampo.Pantallas.Seguridad
{
    partial class ListaDeUsuarios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListaDeUsuarios));
            this.gwUsuarios = new System.Windows.Forms.DataGridView();
            this.alias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBloq = new System.Windows.Forms.Button();
            this.btnMod = new System.Windows.Forms.Button();
            this.btnRegen = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.comboFiltro = new System.Windows.Forms.ComboBox();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.gwUsuarios)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gwUsuarios
            // 
            this.gwUsuarios.AllowUserToAddRows = false;
            this.gwUsuarios.AllowUserToDeleteRows = false;
            this.gwUsuarios.AllowUserToOrderColumns = true;
            this.gwUsuarios.AllowUserToResizeColumns = false;
            this.gwUsuarios.AllowUserToResizeRows = false;
            this.gwUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gwUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gwUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gwUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.alias,
            this.apellido,
            this.nombre,
            this.dni,
            this.baja});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gwUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            this.gwUsuarios.Location = new System.Drawing.Point(12, 212);
            this.gwUsuarios.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gwUsuarios.MultiSelect = false;
            this.gwUsuarios.Name = "gwUsuarios";
            this.gwUsuarios.ReadOnly = true;
            this.gwUsuarios.ShowEditingIcon = false;
            this.gwUsuarios.Size = new System.Drawing.Size(704, 196);
            this.gwUsuarios.TabIndex = 0;
            // 
            // alias
            // 
            this.alias.HeaderText = "Alias";
            this.alias.Name = "alias";
            this.alias.ReadOnly = true;
            this.alias.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // apellido
            // 
            this.apellido.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.apellido.HeaderText = "Apellido";
            this.apellido.Name = "apellido";
            this.apellido.ReadOnly = true;
            this.apellido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // nombre
            // 
            this.nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nombre.HeaderText = "Nombre";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dni
            // 
            this.dni.HeaderText = "DNI";
            this.dni.Name = "dni";
            this.dni.ReadOnly = true;
            this.dni.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // baja
            // 
            this.baja.HeaderText = "Bloqueado";
            this.baja.Name = "baja";
            this.baja.ReadOnly = true;
            this.baja.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // btnBloq
            // 
            this.btnBloq.Location = new System.Drawing.Point(238, 416);
            this.btnBloq.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBloq.Name = "btnBloq";
            this.btnBloq.Size = new System.Drawing.Size(105, 28);
            this.btnBloq.TabIndex = 1;
            this.btnBloq.Tag = "com.td.bloquear";
            this.btnBloq.Text = "Bloquear";
            this.btnBloq.UseVisualStyleBackColor = true;
            this.btnBloq.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMod
            // 
            this.btnMod.Location = new System.Drawing.Point(12, 416);
            this.btnMod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMod.Name = "btnMod";
            this.btnMod.Size = new System.Drawing.Size(107, 28);
            this.btnMod.TabIndex = 2;
            this.btnMod.Tag = "com.td.modificar";
            this.btnMod.Text = "Modificar";
            this.btnMod.UseVisualStyleBackColor = true;
            this.btnMod.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnRegen
            // 
            this.btnRegen.Location = new System.Drawing.Point(540, 416);
            this.btnRegen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRegen.Name = "btnRegen";
            this.btnRegen.Size = new System.Drawing.Size(176, 28);
            this.btnRegen.TabIndex = 3;
            this.btnRegen.Tag = "com.td.regenerar.password";
            this.btnRegen.Text = "Regenerar Password";
            this.btnRegen.UseVisualStyleBackColor = true;
            this.btnRegen.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(125, 416);
            this.btnDel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(107, 28);
            this.btnDel.TabIndex = 4;
            this.btnDel.Tag = "com.td.borrar";
            this.btnDel.Text = "Borrar";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(312, 32);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.MaxLength = 50;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(234, 22);
            this.textBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 12;
            this.label1.Tag = "com.td.filtrar.por";
            this.label1.Text = "Filtrar por:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(598, 32);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 28);
            this.button5.TabIndex = 7;
            this.button5.Tag = "com.td.buscar";
            this.button5.Text = "Buscar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(598, 84);
            this.btnCrear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(100, 28);
            this.btnCrear.TabIndex = 8;
            this.btnCrear.Tag = "com.td.crear";
            this.btnCrear.Text = "Crear";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.button6_Click);
            // 
            // comboFiltro
            // 
            this.comboFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFiltro.FormattingEnabled = true;
            this.comboFiltro.Location = new System.Drawing.Point(93, 32);
            this.comboFiltro.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboFiltro.Name = "comboFiltro";
            this.comboFiltro.Size = new System.Drawing.Size(187, 24);
            this.comboFiltro.TabIndex = 5;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(598, 137);
            this.button7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(100, 28);
            this.button7.TabIndex = 15;
            this.button7.Tag = "com.td.cancelar";
            this.button7.Text = "Cancelar";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.comboFiltro);
            this.groupBox1.Controls.Add(this.btnCrear);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(704, 172);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "com.td.búsqueda";
            this.groupBox1.Text = "Búsqueda";
            // 
            // ListaDeUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 457);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnRegen);
            this.Controls.Add(this.btnMod);
            this.Controls.Add(this.btnBloq);
            this.Controls.Add(this.gwUsuarios);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpProvider1.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ListaDeUsuarios";
            this.helpProvider1.SetShowHelp(this, true);
            this.Tag = "com.td.usuarios";
            this.Text = "Usuarios";
            this.Load += new System.EventHandler(this.ListaDeUsuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gwUsuarios)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gwUsuarios;
        private System.Windows.Forms.Button btnBloq;
        private System.Windows.Forms.Button btnMod;
        private System.Windows.Forms.Button btnRegen;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.ComboBox comboFiltro;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn alias;
        private System.Windows.Forms.DataGridViewTextBoxColumn apellido;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn dni;
        private System.Windows.Forms.DataGridViewTextBoxColumn baja;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}