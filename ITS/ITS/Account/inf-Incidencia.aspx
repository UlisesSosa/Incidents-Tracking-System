<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="inf-Incidencia.aspx.cs" Inherits="ReportBug" Title="Report for Defect" %>


  <div>

<center> 
<!------ Include the above in your HEAD tag ---------->
<h2>BUG TRACKING SYSTEM</h2>
<div id="Inforeport" runat="server"> 
<center> 
<table>
  <tr>
    <td><strong>Nombre del bug: </strong><asp:Label ID="txtTitulo" runat="server"></asp:Label> </td>
    <td><strong>Prioridad: </strong><asp:DropDownList ID="ddlPrioridad" runat="server" AutoPostBack="True">
        </asp:DropDownList>  </td>
  </tr>
    <tr>
    <td class="auto-style1"><strong>Tipo de incidencia: </strong> <asp:Label ID="txtCategoria" runat="server"></asp:Label></td>
    <td class="auto-style1"><strong>Estado: </strong><asp:DropDownList ID="ddlEstado" enabled="True" runat="server" AutoPostBack="True">
        </asp:DropDownList></td>
  </tr>
       <tr>
    <td class="auto-style1"><strong>Area de la incidencia: </strong><asp:Label ID="txtArea" runat="server" ></asp:Label></td>
    <td class="auto-style1">&nbsp;</td>
  </tr>
   <tr>
    <td class="auto-style1"><strong>Asignado por: </strong><asp:Label ID="txtSession" runat="server" ></asp:Label></td>
    <td class="auto-style1"><strong>Asignado para:</strong><asp:DropDownList ID="ddlAsignadoTo" AppendDataBoundItems="true" enabled="True" runat="server" AutoPostBack="True">
      <asp:ListItem Text="Sin asignar" Value="0" />   
    </asp:DropDownList></td>
  </tr>
</table>
<table>
  <tr>
    <td><center><h4>Descripción del bug </h4></center></td>
  </tr>
    <tr>
    <td><asp:TextBox ID="txtDescription" Height="200"  Width="416px"  runat="server"  TextMode="MultiLine"></asp:TextBox> &nbsp; </td>
  </tr>
   <tr>
    <td><center><h4>Resolución</h4></center></td>
  </tr>
  <tr>    
    <td >
        <asp:TextBox ID="txtResolucion" runat="server" Height="200" TextMode="MultiLine" Width="416px"></asp:TextBox>
      </td>
  </tr>
<tr>
<td><asp:Button ID="btnSave" runat="server"  Text="Guardar" OnClick="btnSend_Click" />  
<asp:Button ID="Cancelar" runat="server"  Text="Cancelar" OnClick="btnCancel_Click" />  
</td>
</tr>
</table>
</center>
</div>



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
</center>

    </div>

