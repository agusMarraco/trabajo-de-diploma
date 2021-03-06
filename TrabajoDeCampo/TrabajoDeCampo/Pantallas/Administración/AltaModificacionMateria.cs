﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrabajoDeCampo.SEGURIDAD;
using TrabajoDeCampo.SERVICIO;

namespace TrabajoDeCampo.Pantallas.Administración
{
    public partial class AltaModificacionMateria : Form
    {
        private ServicioSeguridad servicioSeguridad;
        private ServicioAdministracion administracion;
        private bool esEdit = false;
        private Materia currentMateria = null;
        private Boolean valido = false;
        private Regex onlyLetters = new Regex("^[a-zA-Z0-9 ñÑ]+$");
        private Dictionary<String, String> traducciones;


        private Materias callerForm = null;
        public AltaModificacionMateria()
        {
            InitializeComponent();

        }

        public AltaModificacionMateria(Materia materia,Materias materiasForm)
        {
            InitializeComponent();
            callerForm = materiasForm;
            if (materia != null)
            {
                esEdit = true;
                currentMateria = materia;
                this.textBox1.Text = materia.nombre;
                this.textBox2.Text = materia.descripcion;
                if(materia.tipo == "TRONCAL")
                {
                    this.rbTroncal.Checked = true;
                }
                else
                {
                    this.rbExtracurricular.Checked = true;
                }
            }
            else
            {
                this.rbTroncal.Checked = true;
            }
        }
        private void AltaModificacionMateria_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.helpProvider1.SetHelpKeyword(this, Properties.Settings.Default.Idioma.Equals("es") ? "MAterias.htm" : "Classes.htm");
            this.helpProvider1.HelpNamespace = Application.StartupPath + @"\\DocumentsDeAyuda.chm";
            this.servicioSeguridad = new ServicioSeguridad();
            this.administracion = new ServicioAdministracion();

            this.textBox1.KeyPress += TextBox1_KeyPress;
            this.textBox2.KeyPress += TextBox2_KeyPress;


            //traduccion
            FormUtils traductor = new TraductorIterador();
            List<String> tags = new List<string>();
            long id = TrabajoDeCampo.Properties.Settings.Default.SessionUser;
            traductor.process(tags, this, null, null);
            tags.Add("com.td.complete.campos");
            tags.Add("com.td.existe.materia");
            tags.Add("com.td.completado");
            traducciones = servicioSeguridad.traerTraducciones(tags, Properties.Settings.Default.Idioma);
            traductor = new TraductorReal();
            traductor.process(null, this, traducciones, null);
            traductor = new TraductorIterador();
        }

        

        //troncal 1 extra 0
        private void button1_Click(object sender, EventArgs e)
        {
            String nombreMateria = this.textBox1.Text;
            String descripcion = this.textBox2.Text;
            Boolean esTroncal = this.rbTroncal.Checked ?  true : false;
            if(String.IsNullOrEmpty(nombreMateria.Trim()) || String.IsNullOrEmpty(descripcion.Trim()) || 
                (!this.rbExtracurricular.Checked && !this.rbTroncal.Checked))
            {
                MessageBox.Show(traducciones["com.td.complete.campos"]);
                return;
            }
            try
            {
                if (!esEdit)
                {
                    Materia mat = new Materia();
                    mat.nombre = nombreMateria;
                    mat.tipo = esTroncal ? "1" : "0";
                    mat.descripcion = descripcion;
                    this.administracion.guardarMateria(mat);
                    MessageBox.Show(traducciones["com.td.completado"], "", MessageBoxButtons.OK);
                    this.textBox1.Text = "";
                    this.rbExtracurricular.Checked = false;
                    this.rbTroncal.Checked = false;
                    this.textBox2.Text = "";
                    callerForm.actualizarLista();
                    this.Close();
                }
                else
                {
                    currentMateria.nombre = nombreMateria;
                    currentMateria.tipo = esTroncal ? "1" : "0";
                    currentMateria.descripcion = descripcion;
                    this.administracion.actualizarMateria(currentMateria);
                    MessageBox.Show(traducciones["com.td.completado"], "", MessageBoxButtons.OK);
                    this.textBox1.Text = "";
                    this.rbExtracurricular.Checked = false;
                    this.rbTroncal.Checked = false;
                    this.textBox2.Text = "";
                    callerForm.actualizarLista();
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                if(ex.Message == "EXISTE")
                {
                    MessageBox.Show(traducciones["com.td.existe.materia"]);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            callerForm.actualizarLista();
            this.Close();
        }



        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!onlyLetters.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!e.KeyChar.Equals('\b'))//tecla borrar
            {
                if (!onlyLetters.IsMatch(e.KeyChar.ToString()))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
