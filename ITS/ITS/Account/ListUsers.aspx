<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListUsers.aspx.cs" Title="Report for Defect" Inherits="admin_BugTracker" %>


<!DOCTYPE html>

<html>

    <title>Registro de usuario</title>
</head>
<body>

    <div>
        <asp:HiddenField ID="EditarUser" runat="server" />
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Nombre"></asp:Label>
                </td>
                <td colspan="2">
                <asp:TextBox ID="txtNombre" runat="server" ></asp:TextBox>
               
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Apellido"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
                   
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Username"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                   
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" type="Password" Text="Password"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtPassword"  runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label>
                </td>
                <td colspan="2">
                 <asp:TextBox ID="txtEmail" type="Email" runat="server"></asp:TextBox>
                   
                </td>
            </tr>
             <tr>
                <td>
                    Area</td>
                <td colspan="2">
                    
                    <asp:DropDownList ID="ddlArea" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>

            </tr>
          <tr>
                <td>
                    Rol</td>
                <td colspan="2">
                    
                    <asp:DropDownList ID="ddlRol" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>

            </tr>
              <tr>
                <td>
                    Estado</td>
                <td colspan="2">
                    
                    <asp:DropDownList ID="ddlEstado" AppendDataBoundItems="true"  runat="server">
                        <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>

            </tr>
            <tr>
                <td>
                    
                </td>
                <td colspan="2">
                    <asp:Button CausesValidation="True" ID="btnSave" runat="server" Text="Guardar" OnClick="btnSave_Click" OnClientClick='return confirm("Los cambios serán guardados. ¿Estás seguro?");'/>
                    <asp:Button ID="btnDelete" runat="server" Text="Eliminar" OnClick="btnDelete_Click"  OnClientClick='return confirm("Este usuario será eliminado. ¿Estás seguro?");' />
                    <asp:Button ID="btnClear" runat="server" Text="Limpiar" OnClick="btnClear_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
                <td colspan="2">
                    <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
                </td>
                <tr>
                <td>
                    
                </td>
                <td colspan="2">
                    <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            </tr>
        </table>
        <br />
        <asp:GridView ID="gvContact" runat="server"  EmptyDataText="No han encontrado registros que mostrar"  AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="NameArea" HeaderText="Area" />
                <asp:BoundField DataField="Rol" HeaderText="Rol" />
                 <asp:BoundField DataField="Estado" HeaderText="Estado" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("IdUser") %>' OnClick="lnk_OnClick">Editar</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</body>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

