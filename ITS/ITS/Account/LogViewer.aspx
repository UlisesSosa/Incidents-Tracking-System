<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="LogViewer.aspx.cs" Inherits="admin_LogViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <title>ErrorLog</title>
<link href= "CSS/StyleSheet.css" rel= "stylesheet"  type="text/css"/>
<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
<link rel="stylesheet" href="https://www.w3schools.com/lib/w3-theme-blue-grey.css"/>
<link rel='stylesheet' href='https://fonts.googleapis.com/css?family=Open+Sans'/>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">

</head>
<body class="w3-theme-l5">
<form id="form1" runat="server">

  <!-- barra superior -->
<div class="w3-top">
 <div class="w3-bar w3-theme-d2 w3-left-align w3-large">
  <a class="w3-bar-item w3-button w3-hide-medium w3-hide-large w3-right w3-padding-large w3-hover-white w3-large w3-theme-d2" href="javascript:void(0);" onclick="openNav()"><i class="fa fa-bars"></i></a>
  <a href="#" class="w3-bar-item w3-button w3-padding-large w3-theme-d4"><i class="fa fa-home w3-margin-right"></i>ErrorLog</a>
  <a href="config.aspx" class="w3-bar-item w3-button w3-hide-small w3-padding-large w3-hover-white" title="configuracion"><i class="fa fa-user"></i></a>
  
 </div>
</div>

  <!-- Page Container -->
<div class="w3-container w3-content" style="max-width:1400px;margin-top:80px"/>    
  <!-- The Grid -->
  <div class="w3-row"/>
    <!-- Left Column -->
    <div class="w3-col m3">
      <!-- Profile -->
      <div class="w3-card w3-round w3-white">
        <div class="w3-container">
         <p class="w3-center"><img src="images/logo.png" class="w3-circle" style="height:106px;width:150px" alt="Avatar"></p>
         <hr/>
         </div>
       <div>
  <asp:label id="Label1"  runat="server" Width="70px" Height="20px"> AP:</asp:label>&nbsp; 
    <asp:dropdownlist id="lstAplicacion" runat="server"  AutoPostBack="True" enabled="true" onselectedindexchanged="lstAplicacion_SelectedIndexChanged">
     
    </asp:dropdownlist>
        <br />
    <asp:label id="lblresults1" runat="server" Width="296px" Height="37px"></asp:label>

  </div>
           <div>
  <asp:label id="Label2"  runat="server" Width="70px" Height="20px"> Form:</asp:label>&nbsp;
    <asp:dropdownlist id="lstForm" runat="server"  AutoPostBack="True" enabled="true" OnSelectedIndexChanged="Condiciones_SelectedIndexChanged" >
     
    </asp:dropdownlist>
        <br />
    <asp:label id="lblresults2" runat="server" Width="296px" Height="37px"></asp:label>

  </div>
           <div>
  <asp:label id="Label3"  runat="server" Width="70px" Height="20px"> Method:</asp:label>&nbsp;
    <asp:dropdownlist id="lstMethod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Condiciones_SelectedIndexChanged">
     
    </asp:dropdownlist>
        <br />
    <asp:label id="lblresults3" runat="server" Width="296px" Height="37px"></asp:label>

  </div>
        </div>
      </div>





         <center>
        <asp:GridView ID="GridView"  runat="server" AutoGenerateColumns="False" EmptyDataText="no hay datos" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField datafield="ID" HeaderText="ID" />
                <asp:BoundField datafield="Message" HeaderText="Message" />
                <asp:BoundField datafield="Form" HeaderText="Form" />
                <asp:BoundField datafield="Method" HeaderText="Method" />
                <asp:BoundField datafield="StackTrace" HeaderText="StackTrace" />
                <asp:BoundField datafield="IP" HeaderText="IP" />
                <asp:BoundField datafield="UserAgent" HeaderText="UserAgent" />
                <asp:BoundField datafield="Date" HeaderText="Date" />
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
           </center>


    </form>


</body>
</html>
