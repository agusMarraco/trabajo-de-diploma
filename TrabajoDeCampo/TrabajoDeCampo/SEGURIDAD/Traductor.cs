using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoDeCampo.SEGURIDAD
{
    public abstract class Traductor
    {
        public abstract void process(List<String> tags,Form formToTranslate, Dictionary<String, String> traduciones);
    }
    //levanta las keys
    public class TraductorIterador : Traductor
    {

        public override void process(List<String> tags,Form formToTranslate, Dictionary<String, String> traduciones)
        {
            //llamada de primer nivel, empiezo a recorrer el arbol
            foreach (Control control in formToTranslate.Controls)
            {
                if (control.Tag != null && control.Tag.ToString() != "")
                {
                    tags.Add(control.Tag.ToString());
                }
                if (control.GetType() == typeof(MenuStrip))
                {
                    iterateControls(tags, ((MenuStrip)control).Items);
                }

                else if (control.GetType() == typeof(GroupBox))
                {
                    iterateControls(tags, ((GroupBox)control).Controls);
                }

                else if (control.GetType() == typeof(DataGridView))
                {
                    iterateControls(tags, ((DataGridView)control).Columns);
                }

                else
                {
                    iterateControls(tags, control.Controls);
                }
            }
        }


        //iteracion especifica de controles
        private void iterateControls(List<String> tags, IList controles)
        {
            if (controles.Count > 0)
            {
                foreach (var item in controles)
                {

                    if (item.GetType() == typeof(ToolStripMenuItem))
                    {

                        if (((ToolStripMenuItem)item).Tag != null)
                        {
                            tags.Add(((ToolStripMenuItem)item).Tag.ToString());
                        }
                        iterateControls(tags, ((ToolStripMenuItem)item).DropDownItems);
                    }
                    else if (item.GetType() == typeof(ToolStripItem))
                    {
                        if (((ToolStripItem)item).Tag != null)
                        {
                            tags.Add(((ToolStripItem)item).Tag.ToString());
                        }
                    }
                    else if (item.GetType() == typeof(DataGridViewTextBoxColumn) ||
                        item.GetType() == typeof(DataGridViewButtonColumn))
                    {
                        if (((DataGridViewColumn)item).Tag != null)
                        {
                            tags.Add(((DataGridViewColumn)item).Tag.ToString());
                        }
                    }
                    else if (item.GetType() == typeof(Button))
                    {
                        if (((Button)item).Tag != null)
                        {
                            tags.Add(((Button)item).Tag.ToString());
                        }
                    }
                    else if (item.GetType() == typeof(RadioButton))
                    {
                        if (((RadioButton)item).Tag != null)
                        {
                            tags.Add(((RadioButton)item).Tag.ToString());
                        }
                    }
                    else if (item.GetType() == typeof(Label))
                    {
                        if (((Label)item).Tag != null)
                        {
                            tags.Add(((Label)item).Tag.ToString());
                        }
                    }
                }

            }

        }
    }
    //traduce
    public class TraductorReal : Traductor
    {
        public override void process(List<String> tags, Form formToTranslate, Dictionary<String, String> traduciones)
        {
            foreach (KeyValuePair<String, String> item in traduciones)
            { 
                //llamada de primer nivel, empiezo a recorrer el arbol
                foreach (Control control in formToTranslate.Controls)
                {
                    if (control.Tag != null && control.Tag.ToString() == item.Key)
                    {
                        control.Text = item.Value;
                    }
                    if (control.GetType() == typeof(MenuStrip))
                    {
                        iterateControls(item, ((MenuStrip)control).Items);
                    }

                    else if (control.GetType() == typeof(GroupBox))
                    {
                        iterateControls(item, ((GroupBox)control).Controls);
                    }

                    else if (control.GetType() == typeof(DataGridView))
                    {
                        iterateControls(item, ((DataGridView)control).Columns);
                    }

                    else
                    {
                        iterateControls(item, control.Controls);
                    }
                }

            }

        }

        //iteracion especifica de controles

        private void iterateControls(KeyValuePair<String,String> kvPair,  IList controles)
        {
            if (controles.Count > 0)
            {
                foreach (var item in controles)
                {

                    if (item.GetType() == typeof(ToolStripMenuItem))
                    {

                        if (((ToolStripMenuItem)item).Tag != null && ((ToolStripMenuItem)item).Tag.ToString() == kvPair.Key)
                        {
                            ((ToolStripMenuItem)item).Text = kvPair.Value;
                        }
                        iterateControls(kvPair, ((ToolStripMenuItem)item).DropDownItems);
                    }
                    else if (item.GetType() == typeof(ToolStripItem))
                    {
                        if (((ToolStripItem)item).Tag != null && ((ToolStripItem)item).Tag.ToString() == kvPair.Key)
                        {
                            ((ToolStripItem)item).Text = kvPair.Value;
                        }
                    }
                    else if (item.GetType() == typeof(DataGridViewTextBoxColumn) ||
                        item.GetType() == typeof(DataGridViewButtonColumn))
                    {
                        if (((DataGridViewColumn)item).Tag != null && ((DataGridViewColumn)item).Tag.ToString() == kvPair.Key)
                        {
                            ((DataGridViewColumn)item).HeaderText = kvPair.Value;
                        }
                    }
                    else if (item.GetType() == typeof(Button))
                    {
                        if (((Button)item).Tag != null && ((Button)item).Tag.ToString() == kvPair.Key)
                        {
                            ((Button)item).Text = kvPair.Value;
                        }
                    }
                    else if (item.GetType() == typeof(RadioButton))
                    {
                        if (((RadioButton)item).Tag != null && ((RadioButton)item).Tag.ToString() == kvPair.Key)
                        {
                            ((RadioButton)item).Text = kvPair.Value;
                        }
                    }
                    else if (item.GetType() == typeof(Label))
                    {
                        if (((Label)item).Tag != null && ((Label)item).Tag.ToString() == kvPair.Key)
                        {
                            ((Label)item).Text = kvPair.Value;
                        }
                    }
                }

            }

        }
    }


}





////init traducciones;
//string codigoIdioma = TrabajoDeCampo.Properties.Settings.Default.Idioma;
//List<String> codigos = new List<string>();
//iterateControls(codigos, formToTranslate.Controls, formToTranslate);
//Dictionary<String, String> traducciones = traerTraducciones(codigos, codigoIdioma);
//            foreach (KeyValuePair<String, String> item in traducciones)
//            {
//                foreach (Control control in formToTranslate.Controls)
//                {
//                    if (control.Tag != null && control.Tag.ToString() == item.Key)
//                    {
//                        control.Text = item.Value;
//                    }
//                    if (control.GetType() == typeof(MenuStrip))
//                    {
//                        foreach (ToolStripMenuItem menuItem in ((MenuStrip) control).Items)
//                        {
//                            if (menuItem.Tag != null && menuItem.Tag.ToString() == item.Key)
//                            {
//                                menuItem.Text = item.Value;

//                            }
//                            foreach (ToolStripItem subItem in menuItem.DropDownItems)
//                            {
//                                if (subItem.Tag != null && subItem.Tag.ToString() == item.Key)
//                                    subItem.Text = item.Value;

//                                foreach (ToolStripItem sideSubItem in ((ToolStripMenuItem) subItem).DropDownItems)
//                                {
//                                    if (sideSubItem.Tag != null && sideSubItem.Tag.ToString() == item.Key)
//                                        sideSubItem.Text = item.Value;

//                                }
//                            }

//                        }
//                    }
//                }
