<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="WebForms.Clientes" %>
<!DOCTYPE html>
<head runat="server">
    <title>Lista de Clientes</title>
    <style>
        .container {
            max-width: 1000px;
            margin: auto;
            font-family: Arial;
        }

        .header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-top: 20px;
        }

        .header h2 {
            margin: 0;
        }

        .btn {
            padding: 6px 14px;
            background-color: #337ab7;
            color: white;
            border: none;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #23527c;
        }

        .grid {
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="header">
                <h2>Clientes Cadastrados</h2>
                <asp:Button ID="btnNovo" runat="server" CssClass="btn" Text="Novo Cliente" OnClick="btnNovo_Click" />
            </div>

            <div class="grid">
                <asp:GridView ID="gvClientes" runat="server" AutoGenerateColumns="False" DataKeyNames="CPF"
                    OnRowEditing="gvClientes_RowEditing"
                    OnRowDeleting="gvClientes_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="CPF" HeaderText="CPF" ReadOnly="True" />
                        <asp:BoundField DataField="Nome" HeaderText="Nome" />
                        <asp:BoundField DataField="DataNascimento" HeaderText="Nascimento" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                        <asp:BoundField DataField="EstadoCivil" HeaderText="Estado Civil" />

                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
