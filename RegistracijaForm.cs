using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace FolklorApp
{
    public class RegistracijaForm : Form
    {
        private TextBox txtIme, txtPrezime, txtEmail, txtLozinka, txtLozinka2;
        private Button btnRegistruj, btnOtkazi;
        private Label lblStatus;

        public RegistracijaForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Registracija novog korisnika";
            this.Size = new Size(440, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 245, 240);

            Label lblTitle = new Label();
            lblTitle.Text = "Novi nalog";
            lblTitle.Font = new Font("Georgia", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(100, 50, 10);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(30, 20);
            this.Controls.Add(lblTitle);

            string[] labels = { "Ime", "Prezime", "E-mail", "Lozinka", "Potvrdite lozinku" };
            Control[] fields = new Control[5];
            int y = 65;
            for (int i = 0; i < labels.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text = labels[i];
                lbl.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                lbl.ForeColor = Color.FromArgb(80, 40, 0);
                lbl.AutoSize = true;
                lbl.Location = new Point(30, y);
                this.Controls.Add(lbl);

                TextBox txt = new TextBox();
                txt.Size = new Size(360, 26);
                txt.Location = new Point(30, y + 20);
                txt.Font = new Font("Segoe UI", 10);
                txt.BorderStyle = BorderStyle.FixedSingle;
                txt.BackColor = Color.White;
                if (i >= 3) txt.PasswordChar = '●';
                this.Controls.Add(txt);
                fields[i] = txt;
                y += 56;
            }

            txtIme = (TextBox)fields[0];
            txtPrezime = (TextBox)fields[1];
            txtEmail = (TextBox)fields[2];
            txtLozinka = (TextBox)fields[3];
            txtLozinka2 = (TextBox)fields[4];

            lblStatus = new Label();
            lblStatus.Text = "";
            lblStatus.Font = new Font("Segoe UI", 8);
            lblStatus.ForeColor = Color.Red;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(30, y);
            this.Controls.Add(lblStatus);

            btnRegistruj = new Button();
            btnRegistruj.Text = "REGISTRUJ SE";
            btnRegistruj.Size = new Size(170, 38);
            btnRegistruj.Location = new Point(30, y + 20);
            btnRegistruj.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnRegistruj.BackColor = Color.FromArgb(139, 69, 19);
            btnRegistruj.ForeColor = Color.White;
            btnRegistruj.FlatStyle = FlatStyle.Flat;
            btnRegistruj.FlatAppearance.BorderSize = 0;
            btnRegistruj.Cursor = Cursors.Hand;
            btnRegistruj.Click += BtnRegistruj_Click;
            this.Controls.Add(btnRegistruj);

            btnOtkazi = new Button();
            btnOtkazi.Text = "Otkaži";
            btnOtkazi.Size = new Size(100, 38);
            btnOtkazi.Location = new Point(215, y + 20);
            btnOtkazi.Font = new Font("Segoe UI", 9);
            btnOtkazi.FlatStyle = FlatStyle.Flat;
            btnOtkazi.Cursor = Cursors.Hand;
            btnOtkazi.Click += (s, e) => this.Close();
            this.Controls.Add(btnOtkazi);

            this.Size = new Size(440, y + 100);
        }

        private void BtnRegistruj_Click(object sender, EventArgs e)
        {
            string ime = txtIme.Text.Trim();
            string prezime = txtPrezime.Text.Trim();
            string email = txtEmail.Text.Trim();
            string lozinka = txtLozinka.Text;
            string lozinka2 = txtLozinka2.Text;

            if (string.IsNullOrEmpty(ime) || string.IsNullOrEmpty(prezime) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(lozinka))
            {
                lblStatus.Text = "Sva polja su obavezna.";
                return;
            }

            if (lozinka != lozinka2)
            {
                lblStatus.Text = "Lozinke se ne poklapaju.";
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("InsertKorisnik", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ime", ime);
                        cmd.Parameters.AddWithValue("@prezime", prezime);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@lozinka", lozinka);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Registracija uspešna! Možete se prijaviti.",
                    "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                lblStatus.Text = "E-mail adresa je već registrovana.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message, "Greška",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
