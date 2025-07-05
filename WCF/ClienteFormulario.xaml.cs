using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using WCF.Modelo;

namespace WCF
{
    public partial class ClienteForm : Window
    {
        private const string apiUrl = "https://localhost:5001/api/cliente";
        private bool isEdit = false;
        private string originalCPF;

        public ClienteForm(string cpf = null)
        {
            InitializeComponent();
            PopularCombos();

            if (!string.IsNullOrEmpty(cpf))
            {
                isEdit = true;
                originalCPF = cpf;
                CarregarCliente(cpf);
                txtCPF.IsReadOnly = true;
            }
        }

        private void PopularCombos()
        {
            var ufs = new List<string> { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
                                         "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
                                         "RS", "RO", "RR", "SC", "SP", "SE", "TO" };

            cbUF.ItemsSource = ufs;
            cbEnderecoUF.ItemsSource = ufs;

            cbSexo.ItemsSource = new List<string> { "Masculino", "Feminino" };
            cbEstadoCivil.ItemsSource = new List<string> { "Solteiro(a)", "Casado(a)", "Divorciado(a)", "Viúvo(a)", "União Estável" };
        }

        private void CarregarCliente(string cpf)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"{apiUrl}/{cpf}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var cliente = JsonConvert.DeserializeObject<ClienteDTO>(json);

                    txtCPF.Text = cliente.CPF;
                    txtNome.Text = cliente.Nome;
                    txtRG.Text = cliente.RG;
                    txtDataExpedicao.Text = cliente.DataExpedicao.ToString("dd/MM/yyyy");
                    txtOrgaoExpedicao.Text = cliente.OrgaoExpedicao;
                    cbUF.SelectedItem = cliente.UF;
                    txtDataNascimento.Text = cliente.DataNascimento.ToString("dd/MM/yyyy");
                    cbSexo.SelectedItem = cliente.Sexo;
                    cbEstadoCivil.SelectedItem = cliente.EstadoCivil;

                    txtCEP.Text = cliente.Endereco.CEP;
                    txtLogradouro.Text = cliente.Endereco.Logradouro;
                    txtNumero.Text = cliente.Endereco.Numero;
                    txtComplemento.Text = cliente.Endereco.Complemento;
                    txtBairro.Text = cliente.Endereco.Bairro;
                    txtCidade.Text = cliente.Endereco.Cidade;
                    cbEnderecoUF.SelectedItem = cliente.Endereco.UF;
                }
                else
                {
                    MessageBox.Show("Erro ao carregar cliente.");
                }
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = new
            {
                CPF = txtCPF.Text,
                Nome = txtNome.Text,
                RG = txtRG.Text,
                DataExpedicao = Convert.ToDateTime(txtDataExpedicao.Text),
                OrgaoExpedicao = txtOrgaoExpedicao.Text,
                UF = cbUF.Text,
                DataNascimento = Convert.ToDateTime(txtDataNascimento.Text),
                Sexo = cbSexo.Text,
                EstadoCivil = cbEstadoCivil.Text,
                CEP = txtCEP.Text,
                Logradouro = txtLogradouro.Text,
                Numero = txtNumero.Text,
                Complemento = txtComplemento.Text,
                Bairro = txtBairro.Text,
                Cidade = txtCidade.Text,
                EnderecoUF = cbEnderecoUF.Text
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;
                if (isEdit)
                    response = client.PutAsync($"{apiUrl}/{originalCPF}", content).Result;
                else
                    response = client.PostAsync(apiUrl, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cliente salvo com sucesso!");
                    this.Close();
                }
                else
                {
                    var msg = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show("Erro: " + msg);
                }
            }
        }
    }
}
