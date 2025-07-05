using System.Collections.Generic;
using System.ServiceModel;
using WCF.Modelo;

[ServiceContract]
public interface IClienteService
{
    [OperationContract]
    string InserirCliente(ClienteDTO cliente);

    [OperationContract]
    string AtualizarCliente(ClienteDTO cliente);

    [OperationContract]
    ClienteDTO ObterClientePorCPF(string cpf);

    [OperationContract]
    string ExcluirCliente(string cpf);

    [OperationContract]
    List<ClienteDTO> ListarClientes();
}
