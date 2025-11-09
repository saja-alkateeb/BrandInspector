using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrandInspector.Models;
using BrandInspector.Services;

namespace BrandInspector.Forms
{
    public partial class MainForm : Form
    {
        private readonly ApiService _api = new ApiService();
        private readonly BrandService _brandService;
        private readonly PptxScanner _scanner = new PptxScanner();
        private BrandSettings _settings;
        private string _token;
        private List<ScanResult> _results = new List<ScanResult>();

        public MainForm()
        {
            InitializeComponent();
            _brandService = new BrandService(_api);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            // Show login form
            using (var login = new LoginForm())
            {
                if (login.ShowDialog() != DialogResult.OK)
                {
                    Close();
                    return;
                }

                _token = login.Token;
                _brandService.SetToken(_token);
                _settings = await _brandService.GetBrandSettingsAsync();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "PowerPoint Files|*.pptx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                    txtFilePath.Text = ofd.FileName;
            }
        }

        private async void btnScanFonts_Click(object sender, EventArgs e)
        {
            await RunScan("Fonts");
        }

        private async void btnScanColors_Click(object sender, EventArgs e)
        {
            await RunScan("Colors");
        }

        private async void btnScanSizes_Click(object sender, EventArgs e)
        {
            await RunScan("Sizes");
        }

        private async Task RunScan(string type)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("Please select a .pptx file first.", "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            progressBar.Style = ProgressBarStyle.Marquee;

            try
            {
                _results = await Task.Run(() => _scanner.ScanFile(txtFilePath.Text));
                _scanner.CheckCompliance(_results, _settings, type);

                dgvResults.DataSource = null;
                dgvResults.DataSource = _results;

                PopulateErrors();
                UpdateStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Scan failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar.Style = ProgressBarStyle.Blocks;
            }
        }

        private void PopulateErrors()
        {
            lstErrors.Items.Clear();

            foreach (var r in _results)
            {
                if (r.Compliance != null && r.Compliance.StartsWith("Fail"))
                {
                    lstErrors.Items.Add("Slide " + r.SlideNumber + ": " + r.Compliance);
                }
            }
        }

        private void UpdateStatus()
        {
            int total = _results.Count;
            int nonCompliant = 0;

            foreach (var r in _results)
            {
                if (r.Compliance != null && r.Compliance.StartsWith("Fail"))
                    nonCompliant++;
            }

            lblTotals.Text = "Total: " + total;
            lblNonCompliant.Text = "Non-Compliant: " + nonCompliant;
        }

        private void lstErrors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstErrors.SelectedIndex < 0)
                return;

            string selected = lstErrors.SelectedItem.ToString();
            if (!selected.StartsWith("Slide "))
                return;

            string[] parts = selected.Split(' ');
            int slide;
            if (!int.TryParse(parts[1].Replace(":", ""), out slide))
                return;

            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                object val = row.Cells["SlideNumber"].Value;
                if (val != null && (int)val == slide)
                {
                    row.Selected = true;
                    dgvResults.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }
    }
}
