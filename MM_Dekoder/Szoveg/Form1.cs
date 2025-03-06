﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using static System.Windows.Forms.LinkLabel;

namespace Szoveg
{
    public partial class Form1 : Form
    {
        private TextBox txtIP1; 
        private TextBox txtIP2;
        private TextBox txtUzenet;
        private Button btnHozzaad; 
        private Button btnDekod;
        private Button Ki;
        private DataGridView dgvUzenet;
        private DataGridView dgvUzenet1;
        private Label lblIP1;
        private Label lblIP2;
        private Label lblUzenet;
        private Label txtalap;
        private Label egyenialap;
        private int sorszam = 1;
        string[] adat = File.ReadAllLines("messages.log");

        private bool ascending = true;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(925, 750);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lblIP1 = new Label()
            {
                Text = "IP 1:",
                Location = new Point(220, 23),
                AutoSize = true
            };
            
            egyenialap = new Label()
            {
                Text = "Egyénileg megadható:",
                ForeColor = Color.Red,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(20, 45),
                AutoSize = true
            };
            this.Controls.Add(egyenialap);
            
            txtalap = new Label()
            {
                Text = "messages.log TXT alapján feldolgozott verzió:",
                ForeColor = Color.Red,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(20,390),
                AutoSize = true
            };
            this.Controls.Add(txtalap);

            txtIP1 = new TextBox()
            {
                Location = new Point(250, 20),
                Width = 150
            };
            txtIP1.KeyPress += TxtIP_KeyPress;

            lblIP2 = new Label()
            {
                Text = "IP 2:",
                Location = new Point(480, 23),
                AutoSize = true
            };

            txtIP2 = new TextBox()
            {
                Location = new Point(515, 20),
                Width = 150
            };
            txtIP2.KeyPress += TxtIP_KeyPress;

            lblUzenet = new Label()
            {
                Text = "Üzenet:",
                Location = new Point(230, 60),
                AutoSize = true
            };

            txtUzenet = new TextBox()
            {
                Location = new Point(280, 57),
                Width = 370
            };

            btnHozzaad = new Button()
            {
                Text = "Hozzáadás",
                Location = new Point(300, 80),
                Width = 100
            };
            btnHozzaad.Click += BtnHozzaad_Click;

            btnDekod = new Button()
            {
                Text = "Dekódolás",
                Location = new Point(520, 80),
                Width = 100,
            };
            btnDekod.Click += BtnDekodol_Click;

            Ki = new Button()
            {
                Text = "Kilépés",
                Location = new Point(410, 105),
                Width = 100
            };
            Ki.Click += Ki_Click;

            dgvUzenet = new DataGridView()
            {
                Location = new Point(5, 130),
                Size = new Size(900, 250),
                ReadOnly = true,
                AllowUserToAddRows = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            
            dgvUzenet1 = new DataGridView()
            {
                Location = new Point(5, 415),
                Size = new Size(900, 250),
                ReadOnly = true,
                AllowUserToAddRows = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };

            dgvUzenet.Columns.Add("IP1", "IP 1");
            dgvUzenet.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet.Columns.Add("IP2", "IP 2");
            dgvUzenet.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet.Columns.Add("Uzenet", "Üzenet (Bináris)");
            dgvUzenet.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet.Columns.Add("Datum", "Dátum");
            dgvUzenet.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet.Columns.Add("Sorszam", "Sorszám");
            dgvUzenet.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            /*dgvUzenet1.Columns.Add("szemet", "Szemét");
            dgvUzenet1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet1.Columns.Add("Szöveg", "Szöveg");
            dgvUzenet1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet1.Columns.Add("Uzenet", "Üzenet (Bináris)");
            dgvUzenet1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet1.Columns.Add("Datum", "Dátum");
            dgvUzenet1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet1.Columns.Add("Sorszam", "Sorszám");
            dgvUzenet1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;*/

            dgvUzenet1.Columns.Add("IP1", "IP 1");
            dgvUzenet1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet1.Columns.Add("IP2", "IP 2");
            dgvUzenet1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet1.Columns.Add("Uzenet", "Üzenet");
            dgvUzenet1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet1.Columns.Add("Datum", "Dátum");
            dgvUzenet1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvUzenet1.Columns.Add("Sorszam", "Sorszám");
            dgvUzenet1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            this.Controls.Add(lblIP1);
            this.Controls.Add(txtIP1);
            this.Controls.Add(lblIP2);
            this.Controls.Add(txtIP2);
            this.Controls.Add(lblUzenet);
            this.Controls.Add(txtUzenet);
            this.Controls.Add(btnHozzaad);
            this.Controls.Add(btnDekod);
            this.Controls.Add(Ki);
            this.Controls.Add(dgvUzenet);
            this.Controls.Add(dgvUzenet1);

            string UzenetKezdete = "xxx";
            string UzenetVege = ".-+.";
            string FileVege = ".-.-.+.+.";
            string Elvalaszto = ".x.";

            /*foreach (string sor in adat)
            {
                

                string[] adatok = sor.Replace(UzenetKezdete, " ").Replace(UzenetVege, " ").Replace(FileVege, " ").Replace(Elvalaszto, " ").Split(' ');

                string kodoltUzenet = adatok[2];
                string dekodoltUzenet = DecodeMessage(kodoltUzenet);


                Debug.WriteLine(adatok[0]);//, adatok[1], dekodoltUzenet, adatok[3], adatok[4]);
            }*/

            /*foreach (var line in adat)
            {
                var parts = line.Split(new string[] { ".x." }, StringSplitOptions.None);
                if (parts.Length > 1)
                {
                    string uzenet = parts[1];
                    uzenet = uzenet.Replace(UzenetKezdete, "").Replace(UzenetVege, "").Replace(FileVege, "");

                    string[] uzenetReszek = uzenet.Split(new string[] { Elvalaszto }, StringSplitOptions.None);

                    foreach (var resz in uzenetReszek)
                    {
                        if (!string.IsNullOrEmpty(resz))
                        {
                            //string dekodoltUzenet = DecodeMessage(resz);
                            //dgvUzenet1.Rows.Add(parts[0], parts[1], resz);
                        }
                    }
                }
            }*/

            foreach (var line in adat)
            {
                if (line.Contains(UzenetKezdete) && line.Contains(UzenetVege))
                {
                    var parts = line.Split(new string[] { Elvalaszto }, StringSplitOptions.None);
                    if (parts.Length >= 4)
                    {
                        string datum = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //dgvUzenet1.Rows.Add(parts[0], parts[1], ip(parts[2]),datum, sorszam++);

                        string timestamp = parts[0].Substring(UzenetKezdete.Length);//szemét
                        string message = parts[1];
                        string ip1Hex = parts[2];
                        string ip2Hex = parts[3].Substring(0, parts[3].IndexOf(UzenetVege));
                        string ip1 = ip(ip1Hex);
                        string ip2 = ip(ip2Hex);

                        dgvUzenet1.Rows.Add(ip1,ip2,message,datum,sorszam++);
                    }
                }
            }
            dgvUzenet1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(ColumnHeader_Click);

            /*const string uzenetKezdete = "xxx";
            const string uzenetVege = ".-+.";
            const string fileVege = ".-.-.+.+.";
            const string elvalaszto = ".x.";*/

        }
        private void ColumnHeader_Click(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                SortBySorszam();
            }
        }
        private void SortBySorszam()
        {
            var sorok = dgvUzenet1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[4].Value != null).ToList();

            if (ascending)
            {
                sorok = sorok.OrderBy(r => int.Parse(r.Cells[4].Value.ToString())).ToList();
            }
            else
            {
                sorok = sorok.OrderByDescending(r => int.Parse(r.Cells[4].Value.ToString())).ToList();
            }

            dgvUzenet1.Rows.Clear();

            foreach (var sor in sorok)
            {
                dgvUzenet1.Rows.Add(sor.Cells.Cast<DataGridViewCell>().Select(c => c.Value).ToArray());
            }

            ascending = !ascending;
        }
        private void Ki_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string ip(string binary)
        {
            string ipcim = "";

            for (int i = 0; i < 32; i+= 8)
            {
                int ipdeci = Convert.ToInt32(binary.Substring(i, 8), 2);
                ipcim += ipdeci;

                if (i< 24)
                {
                    ipcim += ".";
                }
            }
            return ipcim;
        }
        private void TxtIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt != null)
            {
                if (txt.Text.Length >= 15 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }

                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
            }
        }

        private void BtnHozzaad_Click(object sender, EventArgs e)
        {
            string ip1 = txtIP1.Text;
            string ip2 = txtIP2.Text;
            string uzenet = txtUzenet.Text;
            string kodoltUzenet = EncodeMessage(uzenet);
            string datum = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            dgvUzenet.Rows.Add(ip1, ip2, kodoltUzenet, datum, sorszam);
            sorszam++;
        }

        private void BtnDekodol_Click(object sender, EventArgs e)
        {
            if (dgvUzenet.SelectedRows.Count > 0)
            {
                int rowIndex = dgvUzenet.SelectedRows[0].Index;
                string kodoltUzenet = dgvUzenet.Rows[rowIndex].Cells[2].Value.ToString();
                string dekodoltUzenet = DecodeMessage(kodoltUzenet);

                MessageBox.Show("Dekódolt üzenet: " + dekodoltUzenet, "Üzenet Dekódolva");
            }
        }

        /*private string EncodeMessage(string message)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(message);
            string[] binaryArray = new string[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                binaryArray[i] = Convert.ToString(bytes[i, 2]).PadLeft(8, '0');
            }

            string binaryString = string.Join(" ", binaryArray);
            return binaryString;
        }*/

        private string EncodeMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            string binaryString = string.Join(" ", Array.ConvertAll(bytes, b => Convert.ToString(b, 2).PadLeft(8, '0')));
            return binaryString;
        }

        private string DecodeMessage(string binaryMessage)
        {
            string[] binaryValues = binaryMessage.Split(' ');
            byte[] bytes = new byte[binaryValues.Length];

            for (int i = 0; i < binaryValues.Length; i++)
            {
                bytes[i] = Convert.ToByte(binaryValues[i], 2);
            }

            return Encoding.UTF8.GetString(bytes);
        }
        
    }
}
