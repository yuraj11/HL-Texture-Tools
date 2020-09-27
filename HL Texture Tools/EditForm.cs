using System;
using System.Windows.Forms;
using HLTools;

namespace HLTextureTools
{
    public partial class EditForm : Form
    {
        private readonly SpriteLoader sprLoader;

        public EditForm()
        {
            InitializeComponent();
        }

        public EditForm(SpriteLoader spriteLoader)
        {
            InitializeComponent();
            sprLoader = spriteLoader;

            switch (spriteLoader.SpriteHeader.Type)
            {
                case SprType.VP_PARALLEL_UPRIGHT:
                    radioParallelUpright.Checked = true;
                    break;
                case SprType.FACING_UPRIGHT:
                    radioFacingUpright.Checked = true;
                    break;
                case SprType.VP_PARALLEL:
                    radioParallel.Checked = true;
                    break;
                case SprType.ORIENTED:
                    radioOriented.Checked = true;
                    break;
                case SprType.VP_PARALLEL_ORIENTED:
                    radioParallelOriented.Checked = true;
                    break;
                default:
                    radioParallelUpright.Checked = true;
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SprType newSprType = SprType.VP_PARALLEL_UPRIGHT;
            if (radioParallelUpright.Checked)
            {
                newSprType = SprType.VP_PARALLEL_UPRIGHT;
            }
            else if (radioFacingUpright.Checked)
            {
                newSprType = SprType.FACING_UPRIGHT;
            }
            else if (radioParallel.Checked)
            {
                newSprType = SprType.VP_PARALLEL;
            }
            else if (radioOriented.Checked)
            {
                newSprType = SprType.ORIENTED;
            }
            else if (radioParallelOriented.Checked)
            {
                newSprType = SprType.VP_PARALLEL_ORIENTED;
            }

            //Save sprite
            try
            {
                sprLoader.FixSpriteType(newSprType);
                Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
