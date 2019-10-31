using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CVS.ClientV1
{
    public static class Validators
    {
        public static void inputChange(object sender, string valid)
        {
            if (sender is TextBox)
            {
                TextBox textBox = (sender as TextBox);
                int i = 0;
                while (i < textBox.Text.Length)
                {
                    if (!valid.Contains(textBox.Text[i]))
                    {
                        textBox.Text = textBox.Text.Replace(textBox.Text[i].ToString(), "");
                        textBox.Select(i, 0);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public static void ExitPBOnClick(object sender, EventArgs args)
        {
            if (sender is PictureBox)
            {
                try
                {
                    PictureBox aLabel = (sender as PictureBox);
                    Form aForm = aLabel.FindForm();

                    foreach (object obj in aForm.Controls)
                    {
                        if (obj is IDisposable)
                        {
                            (obj as IDisposable).Dispose();
                        }
                    }
                    GC.Collect();

                    aForm.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("ERROR IN CLOSING- Validators Functions");

                }
            }
        }

        public static void MinimizePBOnClick(object sender, EventArgs args)
        {
            if (sender is PictureBox)
            {
                try
                {
                    PictureBox aLabel = (sender as PictureBox);
                    Form aForm = aLabel.FindForm();


                    aForm.WindowState = FormWindowState.Minimized;
                }
                catch (Exception)
                {

                }
            }
        }

        public static void ExitAndMinimizeMouseHover(object sender, EventArgs args)
        {
            if (sender is PictureBox)
            {
                try
                {
                    PictureBox aLabel = (sender as PictureBox);
                    //aLabel.ForeColor = Color.Moccasin;
                    aLabel.BackColor = Color.Khaki;
                }
                catch (Exception)
                {
                }
            }
        }

        public static void ExitAndMinimizeMouseLeave(object sender, EventArgs args)
        {
            if (sender is Label)
            {
                try
                {
                    PictureBox aLabel = (sender as PictureBox);
                    //aLabel.ForeColor = Color.Black;
                    aLabel.BackColor = Color.Transparent;
                }
                catch (Exception)
                {
                }
            }
        }
    }
}