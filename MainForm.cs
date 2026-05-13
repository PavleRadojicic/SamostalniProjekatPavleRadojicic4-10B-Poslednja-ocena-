using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace FolklorApp
{
    public class MainForm : Form
    {
        private int korisnikId;
        private string korisnikIme;

        private Panel headerPanel;
        private TabControl tabControl;
        private TabPage tabAnsambl, tabIgrac, tabKoreografija;

        // Ansambl controls
        private DataGridView dgvAnsambl;
        private TextBox txtANaziv, txtAGrad, txtAGodina, txtATip;
        private ComboBox cmbAKorisnik;
        private Button btnADodaj, txtAIzmeni, btnAObrisi, btnAOsvezi;
        private Label lblAStatus;

        // Igrac controls
        private DataGridView dgvIgrac;
        private TextBox txtIIme, txtIPrezime, txtIPozicija;
        private DateTimePicker dtpIRodjenje, dtpIPridruzivanje;
        private ComboBox cmbIPol, cmbIAnsambl;
        private Button btnIDodaj, btnIIzmeni, btnIObrisi, btnIOsvezi;
        private Label lblIStatus;

        // Koreografija controls
        private DataGridView dgvKoreografija;
        private TextBox txtKNaziv, txtKTrajanje, txtKStil;
        private DateTimePicker dtpKPremijera;
        private ComboBox cmbKAnsambl;
        private Button btnKDodaj, btnKIzmeni, btnKObrisi, btnKOsvezi;
        private Label lblKStatus;

        public MainForm(int korisnikId, string korisnikIme)
        {
            this.korisnikId = korisnikId;
            this.korisnikIme = korisnikIme;
            InitializeComponent();
            OsveziBaze();
        }

        private void InitializeComponent()
        {
            this.Text = "Folklor - Upravljanje";
            this.Size = new Size(1050, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 245, 240);

            // Header
            headerPanel = new Panel();
            headerPanel.Size = new Size(1050, 55);
            headerPanel.Location = new Point(0, 0);
            headerPanel.BackColor = Color.FromArgb(139, 69, 19);
            this.Controls.Add(headerPanel);

            Label lblAppName = new Label();
            lblAppName.Text = "FOLKLOR";
            lblAppName.Font = new Font("Georgia", 18, FontStyle.Bold);
            lblAppName.ForeColor = Color.FromArgb(255, 230, 180);
            lblAppName.AutoSize = true;
            lblAppName.Location = new Point(20, 12);
            headerPanel.Controls.Add(lblAppName);

            Label lblUser = new Label();
            lblUser.Text = "Prijavljeni korisnik: " + korisnikIme;
            lblUser.Font = new Font("Segoe UI", 9);
            lblUser.ForeColor = Color.FromArgb(240, 200, 150);
            lblUser.AutoSize = true;
            lblUser.Location = new Point(600, 18);
            headerPanel.Controls.Add(lblUser);

            Button btnOdjava = new Button();
            btnOdjava.Text = "Odjava";
            btnOdjava.Size = new Size(80, 30);
            btnOdjava.Location = new Point(940, 12);
            btnOdjava.Font = new Font("Segoe UI", 8);
            btnOdjava.BackColor = Color.FromArgb(180, 80, 10);
            btnOdjava.ForeColor = Color.White;
            btnOdjava.FlatStyle = FlatStyle.Flat;
            btnOdjava.FlatAppearance.BorderSize = 0;
            btnOdjava.Cursor = Cursors.Hand;
            btnOdjava.Click += (s, e) => { this.Close(); };
            headerPanel.Controls.Add(btnOdjava);

            // TabControl
            tabControl = new TabControl();
            tabControl.Size = new Size(1030, 590);
            tabControl.Location = new Point(10, 65);
            tabControl.Font = new Font("Segoe UI", 9);
            this.Controls.Add(tabControl);

            tabAnsambl = new TabPage("  Ansambli  ");
            tabIgrac = new TabPage("  Igrači  ");
            tabKoreografija = new TabPage("  Koreografije  ");

            tabControl.TabPages.Add(tabAnsambl);
            tabControl.TabPages.Add(tabIgrac);
            tabControl.TabPages.Add(tabKoreografija);

            InitAnsamblTab();
            InitIgracTab();
            InitKoreografijaTab();
        }

        // ===================== ANSAMBL TAB =====================
        private void InitAnsamblTab()
        {
            tabAnsambl.BackColor = Color.FromArgb(245, 245, 240);

            dgvAnsambl = new DataGridView();
            dgvAnsambl.Size = new Size(630, 520);
            dgvAnsambl.Location = new Point(10, 10);
            dgvAnsambl.ReadOnly = true;
            dgvAnsambl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnsambl.MultiSelect = false;
            dgvAnsambl.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAnsambl.RowHeadersVisible = false;
            dgvAnsambl.BackgroundColor = Color.White;
            dgvAnsambl.BorderStyle = BorderStyle.None;
            dgvAnsambl.SelectionChanged += DgvAnsambl_SelectionChanged;
            tabAnsambl.Controls.Add(dgvAnsambl);

            Panel formPanel = new Panel();
            formPanel.Size = new Size(355, 520);
            formPanel.Location = new Point(655, 10);
            formPanel.BackColor = Color.White;
            formPanel.BorderStyle = BorderStyle.FixedSingle;
            tabAnsambl.Controls.Add(formPanel);

            Label lblForm = new Label();
            lblForm.Text = "Podaci o ansamblu";
            lblForm.Font = new Font("Georgia", 11, FontStyle.Bold);
            lblForm.ForeColor = Color.FromArgb(100, 50, 10);
            lblForm.AutoSize = true;
            lblForm.Location = new Point(15, 15);
            formPanel.Controls.Add(lblForm);

            string[] lbs = { "Naziv", "Grad", "Godina osnivanja", "Tip" };
            TextBox[] txts = new TextBox[4];
            int y = 50;
            for (int i = 0; i < lbs.Length; i++)
            {
                Label l = new Label(); l.Text = lbs[i]; l.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                l.ForeColor = Color.FromArgb(80, 40, 0); l.AutoSize = true; l.Location = new Point(15, y);
                formPanel.Controls.Add(l);
                TextBox t = new TextBox(); t.Size = new Size(310, 24); t.Location = new Point(15, y + 18);
                t.Font = new Font("Segoe UI", 9); t.BorderStyle = BorderStyle.FixedSingle;
                formPanel.Controls.Add(t);
                txts[i] = t; y += 55;
            }
            txtANaziv = txts[0]; txtAGrad = txts[1]; txtAGodina = txts[2]; txtATip = txts[3];

            Label lKor = new Label(); lKor.Text = "Korisnik"; lKor.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            lKor.ForeColor = Color.FromArgb(80, 40, 0); lKor.AutoSize = true; lKor.Location = new Point(15, y);
            formPanel.Controls.Add(lKor);
            cmbAKorisnik = new ComboBox(); cmbAKorisnik.Size = new Size(310, 24);
            cmbAKorisnik.Location = new Point(15, y + 18); cmbAKorisnik.Font = new Font("Segoe UI", 9);
            cmbAKorisnik.DropDownStyle = ComboBoxStyle.DropDownList;
            formPanel.Controls.Add(cmbAKorisnik);
            y += 55;

            lblAStatus = new Label(); lblAStatus.Text = ""; lblAStatus.Font = new Font("Segoe UI", 8);
            lblAStatus.ForeColor = Color.Red; lblAStatus.AutoSize = true; lblAStatus.Location = new Point(15, y);
            formPanel.Controls.Add(lblAStatus);

            btnADodaj = MakeButton("Dodaj", new Point(15, y + 20), Color.FromArgb(139, 69, 19));
            txtAIzmeni = MakeButton("Izmeni", new Point(120, y + 20), Color.FromArgb(180, 120, 40));
            btnAObrisi = MakeButton("Obriši", new Point(225, y + 20), Color.FromArgb(180, 60, 60));
            btnADodaj.Size = btnAObrisi.Size = txtAIzmeni.Size = new Size(95, 34);
            formPanel.Controls.Add(btnADodaj); formPanel.Controls.Add(txtAIzmeni); formPanel.Controls.Add(btnAObrisi);

            btnAOsvezi = MakeButton("↻ Osveži", new Point(15, y + 64), Color.FromArgb(80, 120, 80));
            btnAOsvezi.Size = new Size(130, 30);
            formPanel.Controls.Add(btnAOsvezi);

            btnADodaj.Click += BtnADodaj_Click;
            txtAIzmeni.Click += BtnAIzmeni_Click;
            btnAObrisi.Click += BtnAObrisi_Click;
            btnAOsvezi.Click += (s, e) => OsveziAnsamble();
        }

        // ===================== IGRAC TAB =====================
        private void InitIgracTab()
        {
            tabIgrac.BackColor = Color.FromArgb(245, 245, 240);

            dgvIgrac = new DataGridView();
            dgvIgrac.Size = new Size(630, 520);
            dgvIgrac.Location = new Point(10, 10);
            StyleGrid(dgvIgrac);
            dgvIgrac.SelectionChanged += DgvIgrac_SelectionChanged;
            tabIgrac.Controls.Add(dgvIgrac);

            Panel formPanel = new Panel();
            formPanel.Size = new Size(355, 520);
            formPanel.Location = new Point(655, 10);
            formPanel.BackColor = Color.White;
            formPanel.BorderStyle = BorderStyle.FixedSingle;
            tabIgrac.Controls.Add(formPanel);

            Label lblForm = new Label();
            lblForm.Text = "Podaci o igraču";
            lblForm.Font = new Font("Georgia", 11, FontStyle.Bold);
            lblForm.ForeColor = Color.FromArgb(100, 50, 10);
            lblForm.AutoSize = true;
            lblForm.Location = new Point(15, 15);
            formPanel.Controls.Add(lblForm);

            int y = 50;
            // Ime
            AddLabel(formPanel, "Ime", 15, y);
            txtIIme = AddTextBox(formPanel, 15, y + 18);
            y += 55;

            // Prezime
            AddLabel(formPanel, "Prezime", 15, y);
            txtIPrezime = AddTextBox(formPanel, 15, y + 18);
            y += 55;

            // Datum rodjenja
            AddLabel(formPanel, "Datum rođenja", 15, y);
            dtpIRodjenje = new DateTimePicker();
            dtpIRodjenje.Size = new Size(310, 24); dtpIRodjenje.Location = new Point(15, y + 18);
            dtpIRodjenje.Format = DateTimePickerFormat.Short;
            formPanel.Controls.Add(dtpIRodjenje);
            y += 55;

            // Pol
            AddLabel(formPanel, "Pol", 15, y);
            cmbIPol = new ComboBox();
            cmbIPol.Size = new Size(310, 24); cmbIPol.Location = new Point(15, y + 18);
            cmbIPol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIPol.Items.AddRange(new object[] { "M - Muški", "Z - Ženski" });
            cmbIPol.Font = new Font("Segoe UI", 9);
            formPanel.Controls.Add(cmbIPol);
            y += 55;

            // Pozicija
            AddLabel(formPanel, "Pozicija", 15, y);
            txtIPozicija = AddTextBox(formPanel, 15, y + 18);
            y += 55;

            // Datum pridruzivanja
            AddLabel(formPanel, "Datum pridruživanja", 15, y);
            dtpIPridruzivanje = new DateTimePicker();
            dtpIPridruzivanje.Size = new Size(310, 24); dtpIPridruzivanje.Location = new Point(15, y + 18);
            dtpIPridruzivanje.Format = DateTimePickerFormat.Short;
            formPanel.Controls.Add(dtpIPridruzivanje);
            y += 55;

            // Ansambl
            AddLabel(formPanel, "Ansambl", 15, y);
            cmbIAnsambl = new ComboBox();
            cmbIAnsambl.Size = new Size(310, 24); cmbIAnsambl.Location = new Point(15, y + 18);
            cmbIAnsambl.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIAnsambl.Font = new Font("Segoe UI", 9);
            formPanel.Controls.Add(cmbIAnsambl);
            y += 55;

            lblIStatus = new Label(); lblIStatus.Text = ""; lblIStatus.Font = new Font("Segoe UI", 8);
            lblIStatus.ForeColor = Color.Red; lblIStatus.AutoSize = true; lblIStatus.Location = new Point(15, y);
            formPanel.Controls.Add(lblIStatus);

            btnIDodaj = MakeButton("Dodaj", new Point(15, y + 20), Color.FromArgb(139, 69, 19));
            btnIIzmeni = MakeButton("Izmeni", new Point(120, y + 20), Color.FromArgb(180, 120, 40));
            btnIObrisi = MakeButton("Obriši", new Point(225, y + 20), Color.FromArgb(180, 60, 60));
            btnIDodaj.Size = btnIIzmeni.Size = btnIObrisi.Size = new Size(95, 34);
            formPanel.Controls.Add(btnIDodaj); formPanel.Controls.Add(btnIIzmeni); formPanel.Controls.Add(btnIObrisi);

            btnIOsvezi = MakeButton("↻ Osveži", new Point(15, y + 64), Color.FromArgb(80, 120, 80));
            btnIOsvezi.Size = new Size(130, 30);
            formPanel.Controls.Add(btnIOsvezi);

            btnIDodaj.Click += BtnIDodaj_Click;
            btnIIzmeni.Click += BtnIIzmeni_Click;
            btnIObrisi.Click += BtnIObrisi_Click;
            btnIOsvezi.Click += (s, e) => OsveziIgrace();
        }

        // ===================== KOREOGRAFIJA TAB =====================
        private void InitKoreografijaTab()
        {
            tabKoreografija.BackColor = Color.FromArgb(245, 245, 240);

            dgvKoreografija = new DataGridView();
            dgvKoreografija.Size = new Size(630, 520);
            dgvKoreografija.Location = new Point(10, 10);
            StyleGrid(dgvKoreografija);
            dgvKoreografija.SelectionChanged += DgvKoreografija_SelectionChanged;
            tabKoreografija.Controls.Add(dgvKoreografija);

            Panel formPanel = new Panel();
            formPanel.Size = new Size(355, 520);
            formPanel.Location = new Point(655, 10);
            formPanel.BackColor = Color.White;
            formPanel.BorderStyle = BorderStyle.FixedSingle;
            tabKoreografija.Controls.Add(formPanel);

            Label lblForm = new Label();
            lblForm.Text = "Podaci o koreografiji";
            lblForm.Font = new Font("Georgia", 11, FontStyle.Bold);
            lblForm.ForeColor = Color.FromArgb(100, 50, 10);
            lblForm.AutoSize = true;
            lblForm.Location = new Point(15, 15);
            formPanel.Controls.Add(lblForm);

            int y = 50;

            AddLabel(formPanel, "Naziv", 15, y);
            txtKNaziv = AddTextBox(formPanel, 15, y + 18); y += 55;

            AddLabel(formPanel, "Trajanje (minuti)", 15, y);
            txtKTrajanje = AddTextBox(formPanel, 15, y + 18); y += 55;

            AddLabel(formPanel, "Stil", 15, y);
            txtKStil = AddTextBox(formPanel, 15, y + 18); y += 55;

            AddLabel(formPanel, "Datum premijere", 15, y);
            dtpKPremijera = new DateTimePicker();
            dtpKPremijera.Size = new Size(310, 24); dtpKPremijera.Location = new Point(15, y + 18);
            dtpKPremijera.Format = DateTimePickerFormat.Short;
            formPanel.Controls.Add(dtpKPremijera); y += 55;

            AddLabel(formPanel, "Ansambl", 15, y);
            cmbKAnsambl = new ComboBox();
            cmbKAnsambl.Size = new Size(310, 24); cmbKAnsambl.Location = new Point(15, y + 18);
            cmbKAnsambl.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKAnsambl.Font = new Font("Segoe UI", 9);
            formPanel.Controls.Add(cmbKAnsambl); y += 55;

            lblKStatus = new Label(); lblKStatus.Text = ""; lblKStatus.Font = new Font("Segoe UI", 8);
            lblKStatus.ForeColor = Color.Red; lblKStatus.AutoSize = true; lblKStatus.Location = new Point(15, y);
            formPanel.Controls.Add(lblKStatus);

            btnKDodaj = MakeButton("Dodaj", new Point(15, y + 20), Color.FromArgb(139, 69, 19));
            btnKIzmeni = MakeButton("Izmeni", new Point(120, y + 20), Color.FromArgb(180, 120, 40));
            btnKObrisi = MakeButton("Obriši", new Point(225, y + 20), Color.FromArgb(180, 60, 60));
            btnKDodaj.Size = btnKIzmeni.Size = btnKObrisi.Size = new Size(95, 34);
            formPanel.Controls.Add(btnKDodaj); formPanel.Controls.Add(btnKIzmeni); formPanel.Controls.Add(btnKObrisi);

            btnKOsvezi = MakeButton("↻ Osveži", new Point(15, y + 64), Color.FromArgb(80, 120, 80));
            btnKOsvezi.Size = new Size(130, 30);
            formPanel.Controls.Add(btnKOsvezi);

            btnKDodaj.Click += BtnKDodaj_Click;
            btnKIzmeni.Click += BtnKIzmeni_Click;
            btnKObrisi.Click += BtnKObrisi_Click;
            btnKOsvezi.Click += (s, e) => OsveziKoreografije();
        }

        // ===================== HELPERS =====================
        private void StyleGrid(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
        }

        private Button MakeButton(string text, Point loc, Color color)
        {
            Button b = new Button();
            b.Text = text; b.Location = loc;
            b.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            b.BackColor = color; b.ForeColor = Color.White;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.Cursor = Cursors.Hand;
            return b;
        }

        private void AddLabel(Panel p, string text, int x, int y)
        {
            Label l = new Label(); l.Text = text;
            l.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            l.ForeColor = Color.FromArgb(80, 40, 0);
            l.AutoSize = true; l.Location = new Point(x, y);
            p.Controls.Add(l);
        }

        private TextBox AddTextBox(Panel p, int x, int y)
        {
            TextBox t = new TextBox();
            t.Size = new Size(310, 24); t.Location = new Point(x, y);
            t.Font = new Font("Segoe UI", 9); t.BorderStyle = BorderStyle.FixedSingle;
            p.Controls.Add(t);
            return t;
        }

        // ===================== DATA LOADING =====================
        private void OsveziBaze()
        {
            OsveziAnsamble(); OsveziIgrace(); OsveziKoreografije();
            PopuniKorisnikeCombo();
        }

        private void PopuniKorisnikeCombo()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT korisnik_id, ime + ' ' + prezime AS naziv FROM korisnik", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmbAKorisnik.DataSource = dt;
                    cmbAKorisnik.DisplayMember = "naziv";
                    cmbAKorisnik.ValueMember = "korisnik_id";
                }
            }
            catch { }
        }

        private void PopuniAnsamblCombo(ComboBox cmb)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT ansambl_id, naziv FROM ansambl", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cmb.DataSource = dt;
                    cmb.DisplayMember = "naziv";
                    cmb.ValueMember = "ansambl_id";
                }
            }
            catch { }
        }

        private void OsveziAnsamble()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT a.ansambl_id AS ID, a.naziv AS Naziv, a.grad AS Grad,
                                   a.godina_osnivanja AS Godina, a.tip AS Tip,
                                   k.ime + ' ' + k.prezime AS Korisnik
                                   FROM ansambl a LEFT JOIN korisnik k ON a.korisnik_id = k.korisnik_id";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvAnsambl.DataSource = dt;
                }
                PopuniAnsamblCombo(cmbIAnsambl);
                PopuniAnsamblCombo(cmbKAnsambl);
            }
            catch (Exception ex) { MessageBox.Show("Greška: " + ex.Message); }
        }

        private void OsveziIgrace()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT i.igrac_id AS ID, i.ime AS Ime, i.prezime AS Prezime,
                                   i.datum_rodjenja AS Rodjenje, i.pol AS Pol, i.pozicija AS Pozicija,
                                   i.datum_pridruzivanja AS Pridruzivanje, a.naziv AS Ansambl
                                   FROM igrac i LEFT JOIN ansambl a ON i.ansambl_id = a.ansambl_id";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvIgrac.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Greška: " + ex.Message); }
        }

        private void OsveziKoreografije()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"SELECT k.koreografija_id AS ID, k.naziv AS Naziv, k.trajanje AS Trajanje,
                                   k.stil AS Stil, k.datum_premijere AS Premijera, a.naziv AS Ansambl
                                   FROM koreografija k LEFT JOIN ansambl a ON k.ansambl_id = a.ansambl_id";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKoreografija.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Greška: " + ex.Message); }
        }

        // ===================== ANSAMBL EVENTS =====================
        private void DgvAnsambl_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAnsambl.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgvAnsambl.SelectedRows[0];
            txtANaziv.Text = row.Cells["Naziv"].Value?.ToString();
            txtAGrad.Text = row.Cells["Grad"].Value?.ToString();
            txtAGodina.Text = row.Cells["Godina"].Value?.ToString();
            txtATip.Text = row.Cells["Tip"].Value?.ToString();
        }

        private void BtnADodaj_Click(object sender, EventArgs e)
        {
            if (!ValidateAnsambl()) return;
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("InsertAnsambl", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@naziv", txtANaziv.Text);
                        cmd.Parameters.AddWithValue("@grad", txtAGrad.Text);
                        cmd.Parameters.AddWithValue("@godina_osnivanja", int.Parse(txtAGodina.Text));
                        cmd.Parameters.AddWithValue("@tip", txtATip.Text);
                        cmd.Parameters.AddWithValue("@korisnik_id", (int)cmbAKorisnik.SelectedValue);
                        cmd.ExecuteNonQuery();
                    }
                }
                OsveziAnsamble();
                lblAStatus.ForeColor = Color.Green;
                lblAStatus.Text = "Ansambl dodat!";
                OcistiAnsamblFormu();
            }
            catch (Exception ex) { lblAStatus.ForeColor = Color.Red; lblAStatus.Text = "Greška: " + ex.Message; }
        }

        private void BtnAIzmeni_Click(object sender, EventArgs e)
        {
            if (dgvAnsambl.SelectedRows.Count == 0) { lblAStatus.Text = "Izaberite ansambl."; return; }
            if (!ValidateAnsambl()) return;
            int id = (int)dgvAnsambl.SelectedRows[0].Cells["ID"].Value;
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdateAnsambl", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ansambl_id", id);
                        cmd.Parameters.AddWithValue("@naziv", txtANaziv.Text);
                        cmd.Parameters.AddWithValue("@grad", txtAGrad.Text);
                        cmd.Parameters.AddWithValue("@godina_osnivanja", int.Parse(txtAGodina.Text));
                        cmd.Parameters.AddWithValue("@tip", txtATip.Text);
                        cmd.Parameters.AddWithValue("@korisnik_id", (int)cmbAKorisnik.SelectedValue);
                        cmd.ExecuteNonQuery();
                    }
                }
                OsveziAnsamble();
                lblAStatus.ForeColor = Color.Green;
                lblAStatus.Text = "Ansambl izmenjen!";
            }
            catch (Exception ex) { lblAStatus.ForeColor = Color.Red; lblAStatus.Text = "Greška: " + ex.Message; }
        }

        private void BtnAObrisi_Click(object sender, EventArgs e)
        {
            if (dgvAnsambl.SelectedRows.Count == 0) { lblAStatus.Text = "Izaberite ansambl."; return; }
            if (MessageBox.Show("Obrisati izabrani ansambl?", "Potvrda",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            int id = (int)dgvAnsambl.SelectedRows[0].Cells["ID"].Value;
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteAnsambl", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ansambl_id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                OsveziAnsamble();
                lblAStatus.ForeColor = Color.Green;
                lblAStatus.Text = "Ansambl obrisan!";
                OcistiAnsamblFormu();
            }
            catch (Exception ex) { lblAStatus.ForeColor = Color.Red; lblAStatus.Text = "Greška: " + ex.Message; }
        }

        private bool ValidateAnsambl()
        {
            if (string.IsNullOrWhiteSpace(txtANaziv.Text)) { lblAStatus.Text = "Naziv je obavezan."; return false; }
            if (!int.TryParse(txtAGodina.Text, out _)) { lblAStatus.Text = "Godina mora biti broj."; return false; }
            lblAStatus.Text = "";
            return true;
        }

        private void OcistiAnsamblFormu()
        {
            txtANaziv.Clear(); txtAGrad.Clear(); txtAGodina.Clear(); txtATip.Clear();
        }

        // ===================== IGRAC EVENTS =====================
        private void DgvIgrac_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvIgrac.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgvIgrac.SelectedRows[0];
            txtIIme.Text = row.Cells["Ime"].Value?.ToString();
            txtIPrezime.Text = row.Cells["Prezime"].Value?.ToString();
            if (row.Cells["Rodjenje"].Value != DBNull.Value && row.Cells["Rodjenje"].Value != null)
                dtpIRodjenje.Value = (DateTime)row.Cells["Rodjenje"].Value;
            string pol = row.Cells["Pol"].Value?.ToString();
            cmbIPol.SelectedIndex = pol == "M" ? 0 : 1;
            txtIPozicija.Text = row.Cells["Pozicija"].Value?.ToString();
            if (row.Cells["Pridruzivanje"].Value != DBNull.Value && row.Cells["Pridruzivanje"].Value != null)
                dtpIPridruzivanje.Value = (DateTime)row.Cells["Pridruzivanje"].Value;
        }

        private void BtnIDodaj_Click(object sender, EventArgs e)
        {
            if (!ValidateIgrac()) return;
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("InsertIgrac", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ime", txtIIme.Text);
                        cmd.Parameters.AddWithValue("@prezime", txtIPrezime.Text);
                        cmd.Parameters.AddWithValue("@datum_rodjenja", dtpIRodjenje.Value.Date);
                        cmd.Parameters.AddWithValue("@pol", cmbIPol.SelectedIndex == 0 ? "M" : "Z");
                        cmd.Parameters.AddWithValue("@pozicija", txtIPozicija.Text);
                        cmd.Parameters.AddWithValue("@datum_pridruzivanja", dtpIPridruzivanje.Value.Date);
                        cmd.Parameters.AddWithValue("@ansambl_id", (int)cmbIAnsambl.SelectedValue);
                        cmd.ExecuteNonQuery();
                    }
                }
                OsveziIgrace();
                lblIStatus.ForeColor = Color.Green;
                lblIStatus.Text = "Igrač dodat!";
            }
            catch (Exception ex) { lblIStatus.ForeColor = Color.Red; lblIStatus.Text = "Greška: " + ex.Message; }
        }

        private void BtnIIzmeni_Click(object sender, EventArgs e)
        {
            if (dgvIgrac.SelectedRows.Count == 0) { lblIStatus.Text = "Izaberite igrača."; return; }
            if (!ValidateIgrac()) return;
            int id = (int)dgvIgrac.SelectedRows[0].Cells["ID"].Value;
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdateIgrac", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@igrac_id", id);
                        cmd.Parameters.AddWithValue("@ime", txtIIme.Text);
                        cmd.Parameters.AddWithValue("@prezime", txtIPrezime.Text);
                        cmd.Parameters.AddWithValue("@datum_rodjenja", dtpIRodjenje.Value.Date);
                        cmd.Parameters.AddWithValue("@pol", cmbIPol.SelectedIndex == 0 ? "M" : "Z");
                        cmd.Parameters.AddWithValue("@pozicija", txtIPozicija.Text);
                        cmd.Parameters.AddWithValue("@datum_pridruzivanja", dtpIPridruzivanje.Value.Date);
                        cmd.Parameters.AddWithValue("@ansambl_id", (int)cmbIAnsambl.SelectedValue);
                        cmd.ExecuteNonQuery();
                    }
                }
                OsveziIgrace();
                lblIStatus.ForeColor = Color.Green;
                lblIStatus.Text = "Igrač izmenjen!";
            }
            catch (Exception ex) { lblIStatus.ForeColor = Color.Red; lblIStatus.Text = "Greška: " + ex.Message; }
        }

        private void BtnIObrisi_Click(object sender, EventArgs e)
        {
            if (dgvIgrac.SelectedRows.Count == 0) { lblIStatus.Text = "Izaberite igrača."; return; }
            if (MessageBox.Show("Obrisati izabranog igrača?", "Potvrda",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            int id = (int)dgvIgrac.SelectedRows[0].Cells["ID"].Value;
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteIgrac", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@igrac_id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                OsveziIgrace();
                lblIStatus.ForeColor = Color.Green;
                lblIStatus.Text = "Igrač obrisan!";
            }
            catch (Exception ex) { lblIStatus.ForeColor = Color.Red; lblIStatus.Text = "Greška: " + ex.Message; }
        }

        private bool ValidateIgrac()
        {
            if (string.IsNullOrWhiteSpace(txtIIme.Text)) { lblIStatus.Text = "Ime je obavezno."; return false; }
            if (string.IsNullOrWhiteSpace(txtIPrezime.Text)) { lblIStatus.Text = "Prezime je obavezno."; return false; }
            if (cmbIPol.SelectedIndex < 0) { lblIStatus.Text = "Izaberite pol."; return false; }
            if (cmbIAnsambl.SelectedIndex < 0) { lblIStatus.Text = "Izaberite ansambl."; return false; }
            lblIStatus.Text = "";
            return true;
        }

        // ===================== KOREOGRAFIJA EVENTS =====================
        private void DgvKoreografija_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKoreografija.SelectedRows.Count == 0) return;
            DataGridViewRow row = dgvKoreografija.SelectedRows[0];
            txtKNaziv.Text = row.Cells["Naziv"].Value?.ToString();
            txtKTrajanje.Text = row.Cells["Trajanje"].Value?.ToString();
            txtKStil.Text = row.Cells["Stil"].Value?.ToString();
            if (row.Cells["Premijera"].Value != DBNull.Value && row.Cells["Premijera"].Value != null)
                dtpKPremijera.Value = (DateTime)row.Cells["Premijera"].Value;
        }

        private void BtnKDodaj_Click(object sender, EventArgs e)
        {
            if (!ValidateKoreografija()) return;
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("InsertKoreografija", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@naziv", txtKNaziv.Text);
                        cmd.Parameters.AddWithValue("@trajanje", int.Parse(txtKTrajanje.Text));
                        cmd.Parameters.AddWithValue("@stil", txtKStil.Text);
                        cmd.Parameters.AddWithValue("@datum_premijere", dtpKPremijera.Value.Date);
                        cmd.Parameters.AddWithValue("@ansambl_id", (int)cmbKAnsambl.SelectedValue);
                        cmd.ExecuteNonQuery();
                    }
                }
                OsveziKoreografije();
                lblKStatus.ForeColor = Color.Green;
                lblKStatus.Text = "Koreografija dodata!";
            }
            catch (Exception ex) { lblKStatus.ForeColor = Color.Red; lblKStatus.Text = "Greška: " + ex.Message; }
        }

        private void BtnKIzmeni_Click(object sender, EventArgs e)
        {
            if (dgvKoreografija.SelectedRows.Count == 0) { lblKStatus.Text = "Izaberite koreografiju."; return; }
            if (!ValidateKoreografija()) return;
            int id = (int)dgvKoreografija.SelectedRows[0].Cells["ID"].Value;
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdateKoreografija", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@koreografija_id", id);
                        cmd.Parameters.AddWithValue("@naziv", txtKNaziv.Text);
                        cmd.Parameters.AddWithValue("@trajanje", int.Parse(txtKTrajanje.Text));
                        cmd.Parameters.AddWithValue("@stil", txtKStil.Text);
                        cmd.Parameters.AddWithValue("@datum_premijere", dtpKPremijera.Value.Date);
                        cmd.Parameters.AddWithValue("@ansambl_id", (int)cmbKAnsambl.SelectedValue);
                        cmd.ExecuteNonQuery();
                    }
                }
                OsveziKoreografije();
                lblKStatus.ForeColor = Color.Green;
                lblKStatus.Text = "Koreografija izmenjena!";
            }
            catch (Exception ex) { lblKStatus.ForeColor = Color.Red; lblKStatus.Text = "Greška: " + ex.Message; }
        }

        private void BtnKObrisi_Click(object sender, EventArgs e)
        {
            if (dgvKoreografija.SelectedRows.Count == 0) { lblKStatus.Text = "Izaberite koreografiju."; return; }
            if (MessageBox.Show("Obrisati izabranu koreografiju?", "Potvrda",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            int id = (int)dgvKoreografija.SelectedRows[0].Cells["ID"].Value;
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteKoreografija", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@koreografija_id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                OsveziKoreografije();
                lblKStatus.ForeColor = Color.Green;
                lblKStatus.Text = "Koreografija obrisana!";
            }
            catch (Exception ex) { lblKStatus.ForeColor = Color.Red; lblKStatus.Text = "Greška: " + ex.Message; }
        }

        private bool ValidateKoreografija()
        {
            if (string.IsNullOrWhiteSpace(txtKNaziv.Text)) { lblKStatus.Text = "Naziv je obavezan."; return false; }
            if (!int.TryParse(txtKTrajanje.Text, out _)) { lblKStatus.Text = "Trajanje mora biti broj."; return false; }
            if (cmbKAnsambl.SelectedIndex < 0) { lblKStatus.Text = "Izaberite ansambl."; return false; }
            lblKStatus.Text = "";
            return true;
        }
    }
}
