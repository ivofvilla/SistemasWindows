using MVC.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using System;

namespace MVC.Controllers
{
    public class ClienteController : Controller
    {
        private readonly string URL = "https://localhost:5001/api/cliente";

        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(URL).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                var clientes = JsonConvert.DeserializeObject<List<ClienteViewModel>>(content);
                return View(clientes);
            }
        }

        public ActionResult Create(string cpf)
        {
            PopularViewBags();

            if (!string.IsNullOrEmpty(cpf))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    var response = client.GetAsync($"api/cliente/{cpf}").Result;
                    var content = response.Content.ReadAsStringAsync().Result;

                    dynamic json = JsonConvert.DeserializeObject<dynamic>(content);

                    var cliente = new ClienteViewModel
                    {
                        CPF = json.cpf,
                        Nome = json.nome,
                        RG = json.rg,
                        DataNascimento = json.dataNascimento,
                        DataExpedicao = json.dataExpedicao,
                        OrgaoExpedicao = json.orgaoExpedicao,
                        UF = json.uf,
                        Sexo = json.sexo,
                        EstadoCivil = json.estadoCivil,

                        CEP = json.endereco.cep,
                        Logradouro = json.endereco.logradouro,
                        Numero = json.endereco.numero,
                        Complemento = json.endereco.complemento,
                        Bairro = json.endereco.bairro,
                        Cidade = json.endereco.cidade,
                        EnderecoUF = json.endereco.uf
                    };

                    ViewBag.IsEdit = true;
                    return View("Formulario", cliente);
                }
            }

            ViewBag.IsEdit = false;
            return View("Formulario", new ClienteViewModel());
        }

        [HttpPost]
        public ActionResult Save(ClienteViewModel model)
        {
            PopularViewBags();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;

                if (Request.Form["IsEdit"] == "true")
                {
                    response = client.PutAsync($"api/cliente/{model.CPF}", content).Result;
                }
                else
                {
                    response = client.PostAsync("api/cliente", content).Result;
                }

                if (!response.IsSuccessStatusCode)
                {
                    var msg = response.Content.ReadAsStringAsync().Result;
                    throw new ApplicationException($"Erro: {msg}");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string cpf)
        {
            using (var client = new HttpClient())
            {
                var response = client.DeleteAsync($"{URL}/{cpf}").Result;
            }

            return RedirectToAction("Index");
        }

        private void PopularViewBags()
        {
            ViewBag.UFs = new List<string> { "AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO", "MA",
                                             "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN",
                                             "RS", "RO", "RR", "SC", "SP", "SE", "TO" };

            ViewBag.Sexos = new List<string> { "Masculino", "Feminino" };

            ViewBag.EstadosCivis = new List<string> { "Solteiro(a)", "Casado(a)", "Divorciado(a)", "Viúvo(a)", "União Estável" };
        }
    }
}
