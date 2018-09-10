using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoDeCampo.SEGURIDAD
{
    public abstract class FormUtils
    {
        public abstract void process(List<String> tags, Form formToTranslate, Dictionary<String, String> traduciones,List<Control> controles);
    }
    //levanta las keys
    public class TraductorIterador : FormUtils
    {

        public override void process(List<String> tags, Form formToTranslate, Dictionary<String, String> traduciones, List<Control> controles)
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
                else if (control.GetType() == typeof(TabControl))
                {
                    iterateControls(tags, ((TabControl)control).TabPages);
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
                        item.GetType() == typeof(DataGridViewButtonColumn) || item.GetType() == typeof(DataGridViewCheckBoxColumn))
                    {
                        if (((DataGridViewColumn)item).Tag != null)
                        {
                            tags.Add(((DataGridViewColumn)item).Tag.ToString());
                        }
                    }
                    else if (item.GetType() == typeof(DataGridView) )
                    {
                            iterateControls(tags, ((DataGridView)item).Columns);                        
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
                    else if (item.GetType() == typeof(TabPage))
                    {
                        if (((TabPage)item).Tag != null)
                        {
                            tags.Add(((TabPage)item).Tag.ToString());
                            iterateControls(tags, ((TabPage)item).Controls);
                        }
                    }
                }

            }

        }
    }
    //traduce
    public class TraductorReal : FormUtils
    {
        public override void process(List<String> tags, Form formToTranslate, Dictionary<String, String> traduciones, List<Control> controles)
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
                    else if (control.GetType() == typeof(TabControl))
                    {
                        iterateControls(item, ((TabControl)control).TabPages);
                    }

                    else
                    {
                        iterateControls(item, control.Controls);
                    }
                }

            }

        }

        //iteracion especifica de controles

        private void iterateControls(KeyValuePair<String, String> kvPair, IList controles)
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
                        item.GetType() == typeof(DataGridViewButtonColumn) || item.GetType() == typeof(DataGridViewCheckBoxColumn))
                    {
                        if (((DataGridViewColumn)item).Tag != null && ((DataGridViewColumn)item).Tag.ToString() == kvPair.Key)
                        {
                            ((DataGridViewColumn)item).HeaderText = kvPair.Value;
                        }
                    }
                    else if (item.GetType() == typeof(DataGridView))
                    {
                            iterateControls(kvPair, ((DataGridView)item).Columns);   
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
                    else if (item.GetType() == typeof(TabPage))
                    {
                        if (((TabPage)item).Tag != null)
                        {
                            if(((TabPage)item).Tag.ToString() == kvPair.Key){
                                ((TabPage)item).Text = kvPair.Value;
                            }
                            
                            iterateControls(kvPair, ((TabPage)item).Controls);
                        }
                    }
                }

            }

        }
    }

    public class Desbloqueador : FormUtils
    {
        public override void process(List<string> tags, Form formToTranslate, Dictionary<string, string> traduciones, List<Control> controles)
        {
            throw new NotImplementedException();
        }
    }


    public class Bloqueador : FormUtils
    {
        public override void process(List<string> tags, Form formToTranslate, Dictionary<string, string> traduciones, List<Control> controles)
        {
            throw new NotImplementedException();
        }
    }


}