namespace API.Modelos
{
    public class Endereco
    {
        public Guid Id { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }

        public Guid ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
