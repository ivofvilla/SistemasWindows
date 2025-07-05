using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.UI;
using Newtonsoft.Json;
using WebForms.Modelos;

namespace WebForms
{
    public partial class ClienteFormulario : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopularDropDowns();

                string cpf = Request.QueryString["cpf"];
                if (!string.IsNullOrEmpty(cpf))
                {
                    ViewState["ModoEdicao"] = true;
                    ViewState["CPFOriginal"] = cpf;
                    CarregarClientePorCpf(cpf);
                }
                else
                {
                    ViewState["ModoEdicao"] = false;
                }
            }
        }

        private void PopularDropDowns()
        {
            string[] ufs = new string[] { "AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO", "MA",
                                          "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
                                          "RS", "RO", "RR", "SC", "SP", "SE", "TO" };

            ddlUFExpedicao.DataSource = ufs;
            ddlUFExpedicao.DataBind();
            ddlUFExpedicao.Items.Insert(0, "Selecione");

            ddlUFEndereco.DataSource = ufs;
            ddlUFEndereco.DataBind();
            ddlUFEndereco.Items.Insert(0, "Selecione");

            ddlSexo.Items.Insert(0, "Selecione");
            ddlSexo.Items.Insert(1, "Masculino");
            ddlSexo.Items.Insert(2, "Feminino");

            ddlEstadoCivil.Items.Insert(0, "Selecione");
            ddlEstadoCivil.Items.Insert(1, "Solteiro(a)");
            ddlEstadoCivil.Items.Insert(2, "Casado(a)");
            ddlEstadoCivil.Items.Insert(3, "Divorciado(a)");
            ddlEstadoCivil.Items.Insert(4, "Viúvo(a)");
            ddlEstadoCivil.Items.Insert(5, "União Estável");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            try
            {
                bool edicao = (bool)ViewState["ModoEdicao"];

                if (edicao)
                {
                    AtualizarCliente();
                }
                else
                {
                    SalvarCliente();
                }

                Response.Redirect("Clientes.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao salvar cliente: " + ex.Message);
            }
        }

        private void SalvarCliente()
        {
            var payload = new
            {
                CPF = txtCPF.Text,
                Nome = txtNome.Text,
                RG = txtRG.Text,
                DataExpedicao = DateTime.Parse(txtDataExpedicao.Text),
                OrgaoExpedicao = txtOrgaoExpedicao.Text,
                UF = ddlUFExpedicao.SelectedValue,
                DataNascimento = DateTime.Parse(txtDataNascimento.Text),
                Sexo = ddlSexo.SelectedValue,
                EstadoCivil = ddlEstadoCivil.SelectedValue,
                CEP = txtCEP.Text,
                Logradouro = txtLogradouro.Text,
                Numero = txtNumero.Text,
                Complemento = txtComplemento.Text,
                Bairro = txtBairro.Text,
                Cidade = txtCidade.Text,
                EnderecoUF = ddlUFEndereco.SelectedValue
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("api/cliente", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var msg = response.Content.ReadAsStringAsync().Result;
                    throw new ApplicationException($"Erro ao salvar cliente: {response.StatusCode} - {msg}");
                }
            }
        }
        private void CarregarClientePorCpf(string cpf)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync($"api/cliente/{cpf}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var cliente = JsonConvert.DeserializeObject<Cliente>(json);

                    txtCPF.Text = cliente.CPF;
                    txtNome.Text = cliente.Nome;
                    txtRG.Text = cliente.RG;
                    txtDataExpedicao.Text = cliente.DataExpedicao.ToString("dd-MM-yyyy");
                    txtOrgaoExpedicao.Text = cliente.OrgaoExpedicao;
                    ddlUFExpedicao.SelectedValue = cliente.UF;
                    txtDataNascimento.Text = cliente.DataNascimento.ToString("dd-MM-yyyy");
                    ddlSexo.SelectedValue = cliente.Sexo;
                    ddlEstadoCivil.SelectedValue = cliente.EstadoCivil;

                    if (cliente.Endereco != null)
                    {
                        txtLogradouro.Text = cliente.Endereco.Logradouro;
                        txtCidade.Text = cliente.Endereco.Cidade;
                        ddlUFEndereco.SelectedValue = cliente.Endereco.UF;
                        txtCEP.Text = cliente.Endereco.CEP;
                        txtNumero.Text = cliente.Endereco.Numero;
                        txtComplemento.Text = cliente.Endereco.Complemento;
                        txtBairro.Text = cliente.Endereco.Bairro;
                    }
                }
            }
        }

        private void AtualizarCliente()
        {
            var cpf = ViewState["CPFOriginal"].ToString();

            var cliente = new
            {
                CPF = txtCPF.Text,
                Nome = txtNome.Text,
                RG = txtRG.Text,
                DataExpedicao = DateTime.Parse(txtDataExpedicao.Text),
                OrgaoExpedicao = txtOrgaoExpedicao.Text,
                UF = ddlUFExpedicao.SelectedValue,
                DataNascimento = DateTime.Parse(txtDataNascimento.Text),
                Sexo = ddlSexo.SelectedValue,
                EstadoCivil = ddlEstadoCivil.SelectedValue,
                CEP = txtCEP.Text,
                Logradouro = txtLogradouro.Text,
                Numero = txtNumero.Text,
                Complemento = txtComplemento.Text,
                Bairro = txtBairro.Text,
                Cidade = txtCidade.Text,
                EnderecoUF = ddlUFEndereco.SelectedValue
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(cliente);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PutAsync($"api/cliente/{cpf}", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var msg = response.Content.ReadAsStringAsync().Result;
                    throw new ApplicationException($"Erro ao atualizar cliente: {response.StatusCode} - {msg}");
                }
            }
        }
    }
}
