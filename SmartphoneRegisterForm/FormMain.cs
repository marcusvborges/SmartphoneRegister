using SmartphoneRegister.Data;
using SmartphoneRegister.Data.Interface;
using SmatphoneRegister.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartphoneRegisterForm
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("Bem vindo ao formulário!");
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair?", "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ISmartPhoneRepository smartPhoneRepository = new SmartphoneSqlServerRepository();
            smartPhoneRepository.ListarSmartphones();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            ISmartPhoneRepository smartPhoneRepository = new SmartphoneSqlServerRepository();
            try
            {
                smartPhoneRepository.CadastrarSmartphones(new Smartphones(txtMarca.Text, txtModelo.Text, int.Parse(txtValor.Text)));
                MessageBox.Show($"Smartphone {txtMarca.Text} inserido com sucesso!");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            ISmartPhoneRepository smartPhoneRepository = new SmartphoneSqlServerRepository();
            try
            {
                gridSmartphone.DataSource = null;
                gridSmartphone.DataSource = smartPhoneRepository.ListarSmartphones();
                gridSmartphone.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            ISmartPhoneRepository smartPhoneRepository = new SmartphoneSqlServerRepository();
            try
            {
                smartPhoneRepository.AtualizarSmarthpones(smartphones: new Smartphones(txtMarca.Text, txtModelo.Text, int.Parse(txtValor.Text)));
                MessageBox.Show($"Smartphone {txtMarca.Text} atualizado com sucesso!");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            ISmartPhoneRepository smartPhoneRepository = new SmartphoneSqlServerRepository();
            try
            {
                smartPhoneRepository.RemoverSmarthpones(int.Parse(txtId.Text));
                smartPhoneRepository.ListarSmartphones();
                MessageBox.Show($"Smartphone {txtMarca.Text} removido com sucesso!");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
        }


    }
}
