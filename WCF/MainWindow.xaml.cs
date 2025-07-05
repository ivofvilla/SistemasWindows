using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using Newtonsoft.Json;
using WCF.Modelo;

namespace WCF
{
    public partial class MainWindow : Window
    {
        private const string URL = "https://localhost:5001/api/cliente";

        public MainWindow()
        {
            InitializeComponent();
            CarregarClientes();
        }

        private void CarregarClientes()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(URL).Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var clientes = JsonConvert.DeserializeObject<List<ClienteDTO>>(json);
                    dgClientes.ItemsSource = clientes;
                }
                else
                {
                    MessageBox.Show("Erro ao carregar clientes");
                }
            }
        }

        private void BtnNovo_Click(object sender, RoutedEventArgs e)
        {
            var form = new ClienteForm();
            form.ShowDialog();
            CarregarClientes();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = (sender as FrameworkElement)?.DataContext as ClienteDTO;
            if (cliente != null)
            {
                var form = new ClienteForm(cliente.CPF);
                form.ShowDialog();
                CarregarClientes();
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var cliente = (sender as FrameworkElement)?.DataContext as ClienteDTO;
            if (cliente != null)
            {
                var confirm = MessageBox.Show($"Excluir {cliente.Nome}?", "Confirmação", MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    using (var client = new HttpClient())
                    {
                        var response = client.DeleteAsync($"{URL}/{cliente.CPF}").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Cliente excluído");
                            CarregarClientes();
                        }
                        else
                        {
                            MessageBox.Show("Erro ao excluir");
                        }
                    }
                }
            }
        }
    }
}
