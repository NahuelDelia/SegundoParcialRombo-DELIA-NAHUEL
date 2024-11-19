using SegundoParcialRombo.Entidades;
using SegundoParcialRombo.Datos;
namespace SegundoParcialRombo.Windows
{
    public partial class frmRombos : Form
    {
        private RepositorioRombos? repositorio;
        private List<Rombo>? rombo;
        public frmRombos()
        {
            InitializeComponent();
            repositorio = new RepositorioRombos();
        }

        private void addFila(DataGridViewRow r, DataGridView dgv)
        {
            dgv.Rows.Add(r);
        }

        public void setFila(DataGridViewRow r, Rombo romb)
        {
            r.Cells[0].Value = romb.diagonalMayor;
            r.Cells[1].Value = romb.diagonalMenor;
            r.Cells[2].Value = romb.contorno.ToString();
            r.Cells[3].Value = romb.Lado().ToString("N2");
            r.Cells[4].Value = romb.getArea().ToString("N2");
            r.Cells[5].Value = romb.getPerimetro().ToString("N2");
            r.Tag = romb;
        }

        public DataGridViewRow crearFila(DataGridView dataGrid)
        {
            var r = new DataGridViewRow();
            r.CreateCells(dataGrid);
            return r;
        }

        private void borrarFila(DataGridViewRow r, DataGridView dgvDatos)
        {
            dgvDatos.Rows.Remove(r);
        }


        private void CargarComboContornos(ref ToolStripComboBox tsCboBordes)
        {
            var listaBordes = Enum.GetValues(typeof(Contorno));
            foreach (var item in listaBordes)
            {
                tsCboBordes.Items.Add(item);
            }
            tsCboBordes.DropDownStyle = ComboBoxStyle.DropDownList;
            tsCboBordes.SelectedIndex = 0;

        }


        private void lado09ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void lado90ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {

        }

        private void frmElipses_Load(object sender, EventArgs e)
        {
            CargarComboContornos(ref tsCboContornos);

        }

        private void tsbNuevo_Click_1(object sender, EventArgs e)
        {
            frmRomboAE frm = new frmRomboAE(repositorio) { Text = "Agregar Rombo" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) return;
            Rombo? rombo = frm.getRombo();
            try
            {
                if (!repositorio!.existeRombo(rombo!))
                {
                    repositorio.agregar(rombo!);
                    DataGridViewRow r = crearFila(dgvDatos);
                    setFila(r, rombo!);
                    addFila(r, dgvDatos);
                    MessageBox.Show("Registro añadido con éxito.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    MessageBox.Show("registo existente. Prueba con uno distinto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Algo ha salido mal", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void tsbBorrar_Click_1(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow r = dgvDatos.SelectedRows[0];
            Rombo rombo = (Rombo)r.Tag!;
            DialogResult dr = MessageBox.Show("¿Estás seguro que deseas borrar este registro?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            try
            {
                repositorio!.borrarRombo(rombo);
                borrarFila(r, dgvDatos);
                MessageBox.Show("El registro fue agregado con exito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (Exception)
            {

                MessageBox.Show("Algo ha salido mal. Intentalo denuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void tsbEditar_Click_1(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow r = dgvDatos.SelectedRows[0];
            Rombo? rombo = (Rombo)r.Tag!;
            frmRomboAE frm = new frmRomboAE(repositorio) { Text = "Editsar Rombo." };
            frm.setRombo(rombo);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            try
            {
                rombo = frm.getRombo();
                setFila(r, rombo!);
                MessageBox.Show("Su registro ha sido editado con éxito", "Menaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {

                MessageBox.Show("Parece que algo ha salido0 mal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void tsbSalir_Click_1(object sender, EventArgs e)
        {
            repositorio!.guardar();
            MessageBox.Show("Saliendo del programa", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }

        private void mostrarGrilla()
        {
            cleanGrill(dgvDatos);
            foreach (var i in rombo!)
            {
                var r = crearFila(dgvDatos);
                setFila(r, i);
                addFila(r, dgvDatos);
            }
        }

        private void cleanGrill(DataGridView dgvDatos)
        {
            dgvDatos.Rows.Clear();
        }
        

    }
}
