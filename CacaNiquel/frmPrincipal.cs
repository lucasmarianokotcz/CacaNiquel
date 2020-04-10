using Services;
using System;
using System.Windows.Forms;

namespace CacaNiquel
{
    public partial class frmPrincipal : Form
    {
        #region Variaveis

        private readonly CacaNiqueis cacaNiqueis = new CacaNiqueis();
        private int ticks = 0;

        #endregion

        public frmPrincipal()
        {
            InitializeComponent();
        }

        #region Event Methods

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            if (cacaNiqueis.LoadImagens() != null)
            {
                pictureBox1.Image = pictureBox2.Image = pictureBox3.Image = cacaNiqueis.ImagensLoad[Imagens.Img7];
            }
            else
            {
                MessageBox.Show("Ocorreu um erro ao carregar as imagens.", "Erro ao carregar imagens", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            if (cacaNiqueis.LoadSounds() == null)
            {
                MessageBox.Show("Ocorreu um erro ao carregar os sons.", "Erro ao carregar sons.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            lblMoedasUser.Text = $"{cacaNiqueis.MoedasIniciais} moedas";
        }

        private void txtAposta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) ||
                ((Keys)e.KeyChar == Keys.D0 && string.IsNullOrEmpty(txtAposta.Text)))
            {
                e.Handled = true;
            }
        }

        private void btnRodar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAposta.Text) && Convert.ToInt32(txtAposta.Text) > 0)
            {
                btnRodar.Enabled = false;
                lblRodando.ForeColor = System.Drawing.Color.Blue;
                lblRodando.Text = "Rodando...";

                if (cacaNiqueis.Apostar(Convert.ToInt32(txtAposta.Text)))
                {
                    timer1.Start();

                    txtAposta.Enabled = false;
                    lblRodando.Visible = true;
                    lblErroAposta.Visible = false;
                    lblMoedasUser.Text = cacaNiqueis.MoedasUser + " moedas";
                }
                else
                {
                    lblRodando.Visible = false;
                    lblErroAposta.Visible = true;
                    btnRodar.Enabled = true;
                    try
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(cacaNiqueis.SonsLoad[Sons.Error]);
                        player.Play();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ticks < 20)
            {
                Random imagemAleatoria = new Random();
                int totalImagens = cacaNiqueis.ImagensArray.Count;
                pictureBox1.Image = cacaNiqueis.ImagensArray[imagemAleatoria.Next(0, totalImagens)];
                pictureBox2.Image = cacaNiqueis.ImagensArray[imagemAleatoria.Next(0, totalImagens)];
                pictureBox3.Image = cacaNiqueis.ImagensArray[imagemAleatoria.Next(0, totalImagens)];
                ticks++;
            }
            else
            {
                txtAposta.Enabled = true;
                btnRodar.Enabled = true;
                ticks = 0;
                timer1.Stop();

                if (pictureBox1.Image == pictureBox2.Image && pictureBox1.Image == pictureBox3.Image)
                {
                    try
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(cacaNiqueis.SonsLoad[Sons.Win]);
                        player.Play();
                    }
                    catch (Exception)
                    {

                    }
                    lblRodando.ForeColor = System.Drawing.Color.Green;
                    lblRodando.Text = $"Você ganhou {cacaNiqueis.Ganhou()} moedas!";
                    lblMoedasUser.Text = cacaNiqueis.MoedasAtualizadas() + " moedas";
                }
                else
                {
                    try
                    {
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(cacaNiqueis.SonsLoad[Sons.Lost]);
                        player.Play();
                    }
                    catch (Exception)
                    {

                    }
                    lblRodando.ForeColor = System.Drawing.Color.Red;
                    lblRodando.Text = $"Você perdeu {cacaNiqueis.Aposta} moedas!";
                }
            }
        }

        #endregion
    }
}
