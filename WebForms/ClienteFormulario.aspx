<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClienteFormulario.aspx.cs" Inherits="WebForms.ClienteFormulario" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cadastro de Cliente</title>
    <style>
        body {
            font-family: Arial;
        }

        table.formulario {
            width: 100%;
            max-width: 1000px;
            margin: auto;
            border-collapse: separate;
            border-spacing: 10px;
        }

        .form-section {
            font-weight: bold;
            font-size: 18px;
            margin-top: 20px;
            padding-bottom: 10px;
            border-bottom: 1px solid #ccc;
        }

        .form-label {
            font-weight: bold;
            width: 160px;
        }

        .form-input {
            width: 100%;
        }

        .form-button {
            padding: 10px 30px;
            background-color: #b4ce00;
            color: white;
            border: none;
            font-weight: bold;
            font-size: 16px;
            cursor: pointer;
        }

        .error {
            color: red;
            font-size: small;
        }

        .search-btn {
            background-color: #007bff;
            color: white;
            padding: 7px 15px;
            font-weight: bold;
            border: none;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="formulario">

            <tr>
                <td colspan="4" class="form-section">Cliente</td>
            </tr>

            <tr>
                <td class="form-label">CPF *</td>
                <td>
                    <asp:TextBox ID="txtCPF" runat="server" CssClass="form-input" />
                    <asp:RequiredFieldValidator ID="rfvCPF" runat="server" ControlToValidate="txtCPF" ErrorMessage="*" CssClass="error" />
                </td>
                <td class="form-label">Nome *</td>
                <td>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-input" />
                    <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome" ErrorMessage="*" CssClass="error" />
                </td>
            </tr>

            <tr>
                <td class="form-label">RG</td>
                <td><asp:TextBox ID="txtRG" runat="server" CssClass="form-input" /></td>

                <td class="form-label">Data Expedição</td>
                <td><asp:TextBox ID="txtDataExpedicao" runat="server" CssClass="form-input" /></td>
            </tr>

            <tr>
                <td class="form-label">Órgão Expedição</td>
                <td><asp:TextBox ID="txtOrgaoExpedicao" runat="server" CssClass="form-input" /></td>

                <td class="form-label">UF Expedição</td>
                <td>
                    <asp:DropDownList ID="ddlUFExpedicao" runat="server" CssClass="form-input" />
                    <asp:RequiredFieldValidator ID="rfvUFExpedicao" runat="server" ControlToValidate="ddlUFExpedicao" InitialValue="" ErrorMessage="*" CssClass="error" />
                </td>
            </tr>

            <tr>
                <td class="form-label">Data de Nascimento *</td>
                <td>
                    <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="form-input" />
                    <asp:RequiredFieldValidator ID="rfvDataNascimento" runat="server" ControlToValidate="txtDataNascimento" ErrorMessage="*" CssClass="error" />
                </td>

                <td class="form-label">Sexo *</td>
                <td>
                    <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-input" />
                    <asp:RequiredFieldValidator ID="rfvSexo" runat="server" ControlToValidate="ddlSexo" InitialValue="" ErrorMessage="*" CssClass="error" />
                </td>
            </tr>

            <tr>
                <td class="form-label">Estado Civil *</td>
                <td>
                    <asp:DropDownList ID="ddlEstadoCivil" runat="server" CssClass="form-input" />
                    <asp:RequiredFieldValidator ID="rfvEstadoCivil" runat="server" ControlToValidate="ddlEstadoCivil" InitialValue="" ErrorMessage="*" CssClass="error" />
                </td>
                <td></td><td></td>
            </tr>

            <tr>
                <td colspan="4" class="form-section">Endereço</td>
            </tr>

            <tr>
                <td class="form-label">CEP *</td>
                <td>
                    <asp:TextBox ID="txtCEP" runat="server" CssClass="form-input" />
                    <asp:RequiredFieldValidator ID="rfvCEP" runat="server" ControlToValidate="txtCEP" ErrorMessage="*" CssClass="error" />
                </td>

                <td colspan="2">
                </td>
            </tr>

            <tr>
                <td class="form-label">Rua</td>
                <td><asp:TextBox ID="txtLogradouro" runat="server" CssClass="form-input" /></td>

                <td class="form-label">Número *</td>
                <td>
                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-input" />
                    <asp:RequiredFieldValidator ID="rfvNumero" runat="server" ControlToValidate="txtNumero" ErrorMessage="*" CssClass="error" />
                </td>
            </tr>

            <tr>
                <td class="form-label">Complemento</td>
                <td><asp:TextBox ID="txtComplemento" runat="server" CssClass="form-input" /></td>

                <td class="form-label">Bairro *</td>
                <td>
                    <asp:TextBox ID="txtBairro" runat="server" CssClass="form-input" />
                    <asp:RequiredFieldValidator ID="rfvBairro" runat="server" ControlToValidate="txtBairro" ErrorMessage="*" CssClass="error" />
                </td>
            </tr>

            <tr>
                <td class="form-label">Cidade *</td>
                <td>
                    <asp:TextBox ID="txtCidade" runat="server" CssClass="form-input" />
                    <asp:RequiredFieldValidator ID="rfvCidade" runat="server" ControlToValidate="txtCidade" ErrorMessage="*" CssClass="error" />
                </td>

                <td class="form-label">UF *</td>
                <td>
                    <asp:DropDownList ID="ddlUFEndereco" runat="server" CssClass="form-input" />
                    <asp:RequiredFieldValidator ID="rfvUFEndereco" runat="server" ControlToValidate="ddlUFEndereco" InitialValue="" ErrorMessage="*" CssClass="error" />
                </td>
            </tr>

            <tr>
                <td colspan="4" style="text-align:center; padding-top:20px;">
                    <asp:Button ID="btnSalvar" runat="server" Text="Atualizar" CssClass="form-button" OnClick="btnSalvar_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
