using SegundoParcialRombo.Datos;
using SegundoParcialRombo.Entidades;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace SegundoParcialRombo.Windows
{
    public partial class frmRomboAE : Form
    {
        private Rombo? rombo;
        private readonly RepositorioRombos? rep;
        public frmRomboAE(RepositorioRombos? repo)
        {
            InitializeComponent();
            rep = repo;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (rombo != null)
            {
                txtDiagonalMayor.Text = rombo.diagonalMenor.ToString();
                txtDiagonalMenor.Text = rombo.diagonalMenor.ToString();
                switch (rombo.contorno)
                {
                    case Contorno.Solido:
                        rbtSolido.Checked = true;
                        break;
                    case Contorno.Punteado:
                        rbtPunteado.Checked = true;
                        break;
                    case Contorno.Rayado:
                        rbtRayado.Checked = true;
                        break;
                    case Contorno.Doble:
                        rbtDoble.Checked = true;
                        break;
                }
            }
        }
        public Rombo? getRombo()
        {
            return rombo;
        }
        public void setRombo(Rombo rombo)
        {
            this.rombo = rombo;
        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            if (validar())
            {
                if (rombo is null)
                {
                    rombo = new Rombo();
                }
                rombo.diagonalMayor = int.Parse(txtDiagonalMayor.Text);
                rombo.diagonalMenor = int.Parse(txtDiagonalMenor.Text);
                if (rbtSolido.Checked)
                    rombo.contorno = Contorno.Solido;
                else if (rbtPunteado.Checked)
                    rombo.contorno = Contorno.Punteado;
                else if (rbtRayado.Checked)
                    rombo.contorno = Contorno.Rayado;
                else
                    rombo.contorno = Contorno.Doble;
                DialogResult = DialogResult.OK;
            }
        }

        private bool validar()
        {
            bool valido = true;
            errorProvider1.Clear();
            if (!int.TryParse(txtDiagonalMayor.Text, out int Diagmayor) || Diagmayor <= 0)
            {
                valido = false;
                errorProvider1.SetError(txtDiagonalMayor, "Has ingresado mal un parámetro");
            }
            if (!int.TryParse(txtDiagonalMenor.Text, out int diagmenor) || diagmenor <= 0 || diagmenor >= Diagmayor)
            {
                valido = false;
                errorProvider1.SetError(txtDiagonalMenor, "Has ingresado mal un parámetro");
            }
            if (rep!.exist(Diagmayor, diagmenor))
            {
                valido = false;
                errorProvider1.SetError(txtDiagonalMayor, "El rombo ingresado ya existe.");
            }
            return valido;
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
