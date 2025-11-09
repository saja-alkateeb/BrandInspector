using System;
using System.Windows.Forms;
using BrandInspector.Services;
using BrandInspector.Models;

namespace BrandInspector.Forms
{
    public partial class LoginForm : Form
    {
        private readonly ApiService _api;
        public string Token { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            _api = new ApiService();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                btnLogin.Enabled = false;
                var token = await _api.LoginAsync(txtUsername.Text, txtPassword.Text);
                Token = token.Access;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }
    }
}
