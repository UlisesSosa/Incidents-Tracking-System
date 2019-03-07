<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Incidencias.aspx.cs" Inherits="ReportBug" Title="Report for Defect" %>



  <div>

<center> 
<!------ Include the above in your HEAD tag ---------->
<h2>BUG TRACKING SYSTEM</h2>



    <br />
<div class="container">
  <div class="alert alert-success">
   <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
  </div>
  <div class="alert alert-info">
  <asp:Label ID="lblObservableMessage" runat="server" Text="" ForeColor="blue"></asp:Label> 
  </div>
<div class="alert alert-danger">
   <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
  </div>
<br />
<br />
</center>
      <center>
          <table id="tableddl" runat="server" class="auto-style2">
        <tr>
            <td class="auto-style4">
              Nuevo</td>
            <td class="auto-style11">
                Proyecto:</td>
            <td class="auto-style1">
                Estado:</td>
            <td class="auto-style8">
                Prioridad:</td>
            <td> Resultado: <asp:Label ID="lblDetailsMessage" runat="server" Text="" ForeColor="blue"></asp:Label> </td>
        </tr>
        <tr>
        <td class="auto-style4">
          <a href="ReportBug.aspx">#Bug</a></td>
          
            <td class="auto-style11">
                <asp:DropDownList runat="server" ID="ddlCategoriagvw" AppendDataBoundItems="true" AutoPostBack="true"
                    OnSelectedIndexChanged="Filtro">
                    <asp:ListItem Text="ALL" Value="0" />
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlEstadogvw" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                    OnSelectedIndexChanged="Filtro">
                    <asp:ListItem Text="ALL" Value="0" />
                </asp:DropDownList>
            </td>
            <td class="auto-style9">
                <asp:DropDownList ID="ddlPrioridadgvw" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                    OnSelectedIndexChanged="Filtro">
                    <asp:ListItem Text="ALL" Value="0" />
                </asp:DropDownList>
            </td>
            <td> </td>
        </tr>
    </table>
    <asp:GridView runat="server" ID="gvBugs" AutoGenerateColumns="False" AllowPaging="True"  runat="server"  EmptyDataText="No se han encontrado registros para mostrar"  OnPageIndexChanging="PageIndexChanging">
         <Columns>
             <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkedit" runat="server"  CommandArgument='<%# Eval("ID") %>' OnClick="lnk_Asignar">Ver</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField >
                <asp:BoundField DataField="ID" HeaderText="#" />
                <asp:BoundField DataField="NameArea" HeaderText="Area" />  
                <asp:BoundField DataField="Creador" HeaderText="Reportado por" />
                <asp:BoundField DataField="Titulo" HeaderText="Resumen" />
                <asp:BoundField DataField="Categoria" HeaderText="Tipo" />  
                <asp:BoundField DataField="Asignadoby" HeaderText="Asignado Por" NullDisplayText="Sin Asignar"/>
                <asp:BoundField DataField="AsignadoTo" HeaderText="Asignado Para" NullDisplayText="Sin Asignar"/>
                <asp:BoundField DataField="NameEstado" HeaderText="Estado" />
                <asp:BoundField DataField="NamePrioridad" HeaderText="Prioridad" />
                <asp:BoundField DataField="CreationDate" HeaderText="Reportado el dia" />
                <asp:BoundField DataField="ResolucionDate" HeaderText="Resuelto el dia" NullDisplayText="N/A"/>
                
        </Columns>
    </asp:GridView>
    </center>

    </div>
   </ContentTemplate>
    </asp:UpdatePanel>
