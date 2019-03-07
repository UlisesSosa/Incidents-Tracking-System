<%@ Page Language="C#"  AutoEventWireup="true"  MasterPageFile="MasterPage.master" CodeFile="Incidencias.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


          <div class="right_col" role="main">
          <div class="">
            <div class="page-title">
              <div class="title_left">
                <h3>Projects <small>Listing design</small></h3>
              </div>
            </div>
            
                        <div class="clearfix"></div>

            <div class="row">
              <div class="col-md-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Projects</h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>
                  <div class="x_content">
    <asp:UpdatePanel ID="Up_Incidencias" runat="server">
     <ContentTemplate>
     <asp:ScriptManager ID="ScriptManager2" runat="server">
     </asp:ScriptManager>
         <Button onserverclick="Descargar" runat="server" class="btn btn-success pull-right"><i class="fa fa-download"></i> Excel</Button>

         <div class="btn btn-success pull-right">
        <i class="fa fa-download"></i>
        <asp:Label ID="Label1" runat="server" type="button" Text="Excel" onclick="Descargar();"></asp:Label>
        </div>
            <table id="tableddl" runat="server" class="auto-style2">
        <tr>
            <td>Area</td>
            <td>Categoria</td>
            <td>Prioridad</td>
            <td>Estado</td>
            <td>Resultado<asp:Label ID="lblDetailsMessage" runat="server" Text="" ForeColor="blue"></asp:Label> </td>
        </tr>
        <tr>
           <td> 
                  <asp:DropDownList ID="ddlAreagvw" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                    OnSelectedIndexChanged="Filtro" class="btn btn-default dropdown-toggle">
                    <asp:ListItem Text="ALL" Value="0" />
                </asp:DropDownList>
           </td>
           <td>
                <asp:DropDownList ID="ddlCategoriagvw" runat="server"  AppendDataBoundItems="true" AutoPostBack="true"
                    OnSelectedIndexChanged="Filtro" class="btn btn-default dropdown-toggle">
                    <asp:ListItem Text="ALL" Value="0" />
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlPrioridadgvw" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                    OnSelectedIndexChanged="Filtro" class="btn btn-default dropdown-toggle">
                    <asp:ListItem Text="ALL" Value="0" />
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlEstadogvw" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                    OnSelectedIndexChanged="Filtro" class="btn btn-default dropdown-toggle">
                    <asp:ListItem Text="ALL" Value="0" />
                </asp:DropDownList>
            </td>
             <td> 
               <asp:Button ID="BtnResultados" class="buttonFinish btn btn-default" runat="server" Text=" 0" />
            </td>
        </tr>
                
    </table>


                    <!-- start project list -->
                   

             <asp:GridView runat="server" ID="gvBugs"  AutoGenerateColumns="False" class="table table-striped projects" AllowPaging="True"  EmptyDataText="No se han encontrado registros para mostrar"  OnPageIndexChanging="PageIndexChanging" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
             <Columns>
             <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkedit"  class="btn btn-primary btn-xs"  runat="server"  CommandArgument='<%# Eval("ID") %>' OnClick="lnk_Asignar"><i class="fa fa-edit"> Ver </i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField >
                <asp:BoundField DataField="ID" HeaderText="#ID" />
                <asp:BoundField DataField="Area" HeaderText="Area" />  
                <asp:BoundField DataField="Creador" HeaderText="Reportado por" />
                <asp:BoundField DataField="Titulo" HeaderText="Resumen" />
                <asp:BoundField DataField="Categoria" HeaderText="Categoria" />  
                <asp:BoundField DataField="Asignadoby" HeaderText="Asignado Por" NullDisplayText="Sin Asignar"/>
                <asp:BoundField DataField="AsignadoTo" HeaderText="Asignado Para" NullDisplayText="Sin Asignar"/>
                <asp:TemplateField HeaderText="Prioridad">
                <ItemTemplate>
                <asp:Label ID="lblPrioridad" runat="server" class='<%# Eval("Prioridad").ToString() == "Proyecto" ? "btn btn-info btn-xs" : Eval("Prioridad").ToString() == "Medio" ? "btn btn-warning btn-xs" : "btn btn-danger btn-xs"  %>'  Text='<%#Eval("Prioridad") %>' >&nbsp;  </asp:Label>
                </ItemTemplate>
                </asp:TemplateField >
                <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                <asp:Label ID="lblEstado"   runat="server" class='<%# Eval("Estado").ToString() == "Abierto" ? "btn btn-primary btn-xs" : Eval("Estado").ToString() == "En progreso" ? "btn btn-warning btn-xs" :  Eval("Estado").ToString() == "Resuelto" ? "btn btn-success btn-xs" : "btn btn-dark btn-xs"  %>'  Text='<%#Eval("Estado") %>' >&nbsp;  </asp:Label>
                </ItemTemplate>
                </asp:TemplateField >
                <asp:BoundField DataField="CreationDate" HeaderText="Reportado el dia" />
                <asp:BoundField DataField="ResolucionDate" HeaderText="Resuelto el dia" NullDisplayText="N/A"/>

                
                
        </Columns>
                 <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                 <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                 <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                 <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                 <SortedAscendingCellStyle BackColor="#F7F7F7" />
                 <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                 <SortedDescendingCellStyle BackColor="#E5E5E5" />
                 <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
                    <!-- end project list -->

                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
          </div>
          </div>
</asp:Content>
      
   