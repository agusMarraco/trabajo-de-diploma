﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoDeCampo.Pantallas.Seguridad
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void docenteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void recalcularDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListaDeUsuarios usuarios = new ListaDeUsuarios();
            usuarios.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void restaurarBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListarFamilias familias = new ListarFamilias();
            familias.Show();
        }

        private void respaldarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Respaldo_Base_de_Datos respaldo = new Respaldo_Base_de_Datos();
            respaldo.Show();
        }

        private void restaurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restaurar_Backup restore = new Restaurar_Backup();
            restore.Show();
        }

        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarContraseña contra = new CambiarContraseña();
            contra.Show();
        }

        private void bitácoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitácora bitacora = new Bitácora();
            bitacora.Show();

        }
       
        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administración.Cursos cursos = new Administración.Cursos();
            cursos.Show();
        }

        private void horariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administración.Horarios horarios = new Administración.Horarios();
            horarios.Show();

        }

        private void materiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Administración.Materias().Show();
        }

        private void promociónDeAlumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Administración.PromocionDeAlumnos().Show();
        }

        private void alumnosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Alumnos.Alumnos().Show();
        }

        private void amonestacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Alumnos.Amonestaciones().Show();
        }

        private void inasistenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Alumnos.Inasistencias().Show();
        }

        private void tutoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Alumnos.Tutores().Show();
        }
    }
}