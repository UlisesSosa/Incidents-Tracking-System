<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="GenerarTicket.aspx.cs" Inherits="ReportBug" Title="Report for Defect" %>

<!DOCTYPE html>

<html>

    <title>ReporBug</title>
</head>
</head>

<center>
<table>
<tr>
<td class="auto-style1">
 <td>
<h4>BUG TRACKING SYSTEM: CREAR NUEVO COMPONENTE</h4> 
</td>
</tr>
</table>
<table >
<tr><td style="font-size: 12px; font-family: Verdana" class="auto-style3">Nombre del Bug:</td>
<td align="left"><asp:TextBox ID="txtNameBug" ReadOnly="false"  runat="server"></asp:TextBox></td>
<td style="font-size: 12px; font-family: Verdana">Prioridad:</td>
<td align="left"><asp:DropDownList ID="ddlPrioridad" runat="server" AppendDataBoundItems="true" AutoPostBack="True">
    <asp:ListItem>Seleccionar</asp:ListItem>
    </asp:DropDownList></td>
</tr>
<tr><td class="auto-style1">Tipo de incidencia</td>
<td align="left"><asp:DropDownList ID="ddlCategoria" AppendDataBoundItems="true" runat="server" AutoPostBack="True">
    <asp:ListItem>Seleccionar</asp:ListItem>
    </asp:DropDownList></td>
<td style="font-size: 12px; font-family: Verdana">Estado:</td>
<td align="left"><asp:DropDownList ID="ddlEstado" enabled="True" runat="server" AutoPostBack="True">
    <asp:ListItem>Seleccionar</asp:ListItem>
    </asp:DropDownList></td>
</tr>
<tr><td class="auto-style1">Area de la incidencia</td>
<td align="left"><asp:DropDownList ID="ddlArea" AppendDataBoundItems="true" runat="server" AutoPostBack="True">
    <asp:ListItem>Seleccionar</asp:ListItem>
    </asp:DropDownList></td>
</tr>

<tr>
<td style="font-size: 12px; font-family: Verdana" class="auto-style3">
    Descripción:</td>
<td align="left" colspan="3"><asp:TextBox ID="txtDescription" Height="200"  Width="416px"  runat="server"  TextMode="MultiLine"></asp:TextBox>   </td>
</tr>
<tr>
<td class="auto-style3" ><asp:Button ID="btnSend" runat="server"  Text="Guardar" OnClick="btnSend_Click" />  
<asp:Button ID="btnClear" runat="server"  Text="Limpiar" OnClick="btnClear_Click" />   </td>
</tr>
</table>  
<table>
<tr>
 <td class="auto-style4"></td>
<td><asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label> </td>
</tr>
    <tr>
        <td class="auto-style4"></td>
        <td colspan="2">
            <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
        </td>
    </tr>
</table>
</center> 
