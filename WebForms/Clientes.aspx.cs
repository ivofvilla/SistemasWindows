using System;
using System.Collections.Generic;
using System.Web.UI;
using WebForms.Modelos;
using WebForms;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace WebForms
{
    public partial class Clientes : Page
    {
        private const string URL = "https://localhost:5001/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarClientes();
            }
        }

        private void CarregarClientes()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("api/cliente").Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var clientes = JsonConvert.DeserializeObject<List<Cliente>>(json);
                    gvClientes.DataSource = clientes;
                    gvClientes.DataBind();
                }
                else
                {
                    throw new Exception("Erro ao carregar clientes: " + response.StatusCode);
                }
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClienteFormulario.aspx");
        }

        protected void gvClientes_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            string cpf = gvClientes.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("ClienteFormulario.aspx?cpf=" + cpf);
        }

        protected void gvClientes_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            string cpf = gvClientes.DataKeys[e.RowIndex].Value.ToString();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.DeleteAsync($"api/cliente/{cpf}").Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        var msg = response.Content.ReadAsStringAsync().Result;
                        throw new ApplicationException($"Erro ao excluir cliente: {response.StatusCode} - {msg}");
                    }
                }

                CarregarClientes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao excluir cliente: " + ex.Message);
            }
        }
    }
}
