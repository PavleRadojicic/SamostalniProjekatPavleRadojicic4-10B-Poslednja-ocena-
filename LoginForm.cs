using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace FolklorApp
{
    public class LoginForm : Form
    {
        private Panel leftPanel;
        private Panel rightPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblEmail;
        private Label lblLozinka;
        private TextBox txtEmail;
        private TextBox txtLozinka;
        private Button btnLogin;
        private Button btnRegistracija;
        private Label lblGreska;
        private PictureBox logoBox;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Folklor - Prijava";
            this.Size = new Size(800, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 245, 240);

            // ---- LEFT PANEL (dekorativni) ----
            leftPanel = new Panel();
            leftPanel.Size = new Size(320, 520);
            leftPanel.Location = new Point(0, 0);
            leftPanel.BackColor = Color.FromArgb(139, 69, 19);
            leftPanel.Paint += LeftPanel_Paint;
            this.Controls.Add(leftPanel);

            lblTitle = new Label();
            lblTitle.Text = "FOLKLOR";
            lblTitle.Font = new Font("Georgia", 28, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(255, 230, 180);
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(300, 50);
            lblTitle.Location = new Point(10, 180);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            leftPanel.Controls.Add(lblTitle);

            lblSubtitle = new Label();
            lblSubtitle.Text = "Upravljanje ansamblima,\nigračima i koreografijama";
            lblSubtitle.Font = new Font("Georgia", 10, FontStyle.Italic);
            lblSubtitle.ForeColor = Color.FromArgb(240, 200, 150);
            lblSubtitle.AutoSize = false;
            lblSubtitle.Size = new Size(280, 60);
            lblSubtitle.Location = new Point(20, 240);
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            leftPanel.Controls.Add(lblSubtitle);

            // ---- RIGHT PANEL (forma) ----
            rightPanel = new Panel();
            rightPanel.Size = new Size(470, 520);
            rightPanel.Location = new Point(320, 0);
            rightPanel.BackColor = Color.FromArgb(245, 245, 240);
            this.Controls.Add(rightPanel);

            Label lblWelcome = new Label();
            lblWelcome.Text = "Dobrodošli";
            lblWelcome.Font = new Font("Georgia", 20, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(100, 50, 10);
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(60, 70);
            rightPanel.Controls.Add(lblWelcome);

            Label lblLogin = new Label();
            lblLogin.Text = "Prijavite se na vaš nalog";
            lblLogin.Font = new Font("Segoe UI", 9);
            lblLogin.ForeColor = Color.Gray;
            lblLogin.AutoSize = true;
            lblLogin.Location = new Point(62, 108);
            rightPanel.Controls.Add(lblLogin);

            // Email
            lblEmail = new Label();
            lblEmail.Text = "E-mail adresa";
            lblEmail.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(80, 40, 0);
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(60, 160);
            rightPanel.Controls.Add(lblEmail);

            txtEmail = new TextBox();
            txtEmail.Size = new Size(340, 30);
            txtEmail.Location = new Point(60, 182);
            txtEmail.Font = new Font("Segoe UI", 10);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.BackColor = Color.White;
            rightPanel.Controls.Add(txtEmail);

            // Lozinka
            lblLozinka = new Label();
            lblLozinka.Text = "Lozinka";
            lblLozinka.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblLozinka.ForeColor = Color.FromArgb(80, 40, 0);
            lblLozinka.AutoSize = true;
            lblLozinka.Location = new Point(60, 230);
            rightPanel.Controls.Add(lblLozinka);

            txtLozinka = new TextBox();
            txtLozinka.Size = new Size(340, 30);
            txtLozinka.Location = new Point(60, 252);
            txtLozinka.Font = new Font("Segoe UI", 10);
            txtLozinka.PasswordChar = '●';
            txtLozinka.BorderStyle = BorderStyle.FixedSingle;
            txtLozinka.BackColor = Color.White;
            txtLozinka.KeyPress += TxtLozinka_KeyPress;
            rightPanel.Controls.Add(txtLozinka);

            // Greška label
            lblGreska = new Label();
            lblGreska.Text = "";
            lblGreska.Font = new Font("Segoe UI", 8);
            lblGreska.ForeColor = Color.Red;
            lblGreska.AutoSize = true;
            lblGreska.Location = new Point(60, 292);
            rightPanel.Controls.Add(lblGreska);

            // Dugme Prijava
            btnLogin = new Button();
            btnLogin.Text = "PRIJAVI SE";
            btnLogin.Size = new Size(340, 42);
            btnLogin.Location = new Point(60, 315);
            btnLogin.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLogin.BackColor = Color.FromArgb(139, 69, 19);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += BtnLogin_Click;
            rightPanel.Controls.Add(btnLogin);

            // Separator
            Label lblOr = new Label();
            lblOr.Text = "— ili —";
            lblOr.Font = new Font("Segoe UI", 8);
            lblOr.ForeColor = Color.Gray;
            lblOr.AutoSize = true;
            lblOr.Location = new Point(175, 372);
            rightPanel.Controls.Add(lblOr);

            // Dugme Registracija
            btnRegistracija = new Button();
            btnRegistracija.Text = "Registrujte se";
            btnRegistracija.Size = new Size(340, 36);
            btnRegistracija.Location = new Point(60, 392);
            btnRegistracija.Font = new Font("Segoe UI", 9);
            btnRegistracija.BackColor = Color.FromArgb(245, 245, 240);
            btnRegistracija.ForeColor = Color.FromArgb(139, 69, 19);
            btnRegistracija.FlatStyle = FlatStyle.Flat;
            btnRegistracija.FlatAppearance.BorderColor = Color.FromArgb(139, 69, 19);
            btnRegistracija.FlatAppearance.BorderSize = 1;
            btnRegistracija.Cursor = Cursors.Hand;
            btnRegistracija.Click += BtnRegistracija_Click;
            rightPanel.Controls.Add(btnRegistracija);

            Label lblVersion = new Label();
            lblVersion.Text = "v1.0 © 2025 Pavle Radojičić";
            lblVersion.Font = new Font("Segoe UI", 7);
            lblVersion.ForeColor = Color.LightGray;
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(150, 470);
            rightPanel.Controls.Add(lblVersion);
        }

        private void LeftPanel_Paint(object sender, PaintEventArgs e)
        {
            // Dekorativni šareni elementi
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.FromArgb(60, 255, 200, 100), 2);
            for (int i = 0; i < 8; i++)
            {
                g.DrawEllipse(pen,
                    50 + i * 5, 300 + i * 10,
                    200 - i * 10, 180 - i * 10);
            }
            pen.Dispose();

            // Dekorativna linija
            using (Pen p2 = new Pen(Color.FromArgb(80, 255, 220, 150), 1))
            {
                for (int y = 0; y < 520; y += 20)
                    g.DrawLine(p2, 0, y, 320, y);
            }
        }

        private void TxtLozinka_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                BtnLogin_Click(sender, e);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string lozinka = txtLozinka.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(lozinka))
            {
                lblGreska.Text = "Unesite e-mail i lozinku.";
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT korisnik_id, ime, prezime FROM korisnik WHERE email = @email AND lozinka = @lozinka";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@lozinka", lozinka);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int korisnikId = (int)reader["korisnik_id"];
                                string ime = reader["ime"].ToString();
                                string prezime = reader["prezime"].ToString();
                                this.Hide();
                                MainForm mainForm = new MainForm(korisnikId, ime + " " + prezime);
                                mainForm.FormClosed += (s, args) => this.Close();
                                mainForm.Show();
                            }
                            else
                            {
                                lblGreska.Text = "Pogrešan e-mail ili lozinka.";
                                txtLozinka.Clear();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri povezivanju sa bazom:\n" + ex.Message,
                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegistracija_Click(object sender, EventArgs e)
        {
            RegistracijaForm regForm = new RegistracijaForm();
            regForm.ShowDialog();
        }
    }
}
