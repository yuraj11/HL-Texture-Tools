using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HLTools;

namespace HLTextureTools
{
    public partial class EditForm : Form
    {
        SpriteLoader sprLoader;

        public EditForm()
        {
            InitializeComponent();
        }

        public EditForm(SpriteLoader spriteLoader)
        {
            InitializeComponent();
            this.sprLoader = spriteLoader;

            switch (spriteLoader.SpriteHeader.Type)
            {
                case SprType.VP_PARALLEL_UPRIGHT:
                    radioButton1.Checked = true;
                    break;
                case SprType.FACING_UPRIGHT:
                    radioButton2.Checked = true;
                    break;
                case SprType.VP_PARALLEL:
                    radioButton3.Checked = true;
                    break;
                case SprType.ORIENTED:
                    radioButton4.Checked = true;
                    break;
                case SprType.VP_PARALLEL_ORIENTED:
                    radioButton5.Checked = true;
                    break;
                default:
                    radioButton1.Checked = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SprType newSprType = SprType.VP_PARALLEL_UPRIGHT;
            if (radioButton1.Checked)
            {
                newSprType = SprType.VP_PARALLEL_UPRIGHT;
            }
            else if (radioButton2.Checked)
            {
                newSprType = SprType.FACING_UPRIGHT;
            }
            else if (radioButton3.Checked)
            {
                newSprType = SprType.VP_PARALLEL;
            }
            else if (radioButton4.Checked)
            {
                newSprType = SprType.ORIENTED;
            }
            else if (radioButton5.Checked)
            {
                newSprType = SprType.VP_PARALLEL_ORIENTED;
            }
            //Save sprite
            try
            {
                sprLoader.FixSpriteType(newSprType);

                //Close window
                Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Close window
            Close();
        }
    }
}
