<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="MasterPage.master" EnableEventValidation = "false"  CodeFile="Incidencias.aspx.cs" Inherits="_Default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- page content -->
        <div class="right_col" role="main">
          <div class="">
            <div class="page-title">
              <div class="title_left">
                <h3>Tables <small>Some examples to get you started</small></h3>
              </div>
            </div>

            <div class="row">



             

              <div class="clearfix"></div>

              <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Table design <small>Custom design</small></h2>
                    <ul class="nav navbar-right panel_toolbox">
                      <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                      </li>
                      <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false"><i class="fa fa-wrench"></i></a>
                        <ul class="dropdown-menu" role="menu">
                          <li><a href="#">Settings 1</a>
                          </li>
                          <li><a href="#">Settings 2</a>
                          </li>
                        </ul>
                      </li>
                      <li><a class="close-link"><i class="fa fa-close"></i></a>
                      </li>
                    </ul>
                    <div class="clearfix"></div>
                  </div>

                 

    <asp:UpdatePanel ID="Up_Incidencias" runat="server">
     <ContentTemplate>
     <asp:ScriptManager ID="ScriptManager2" runat="server">
     </asp:ScriptManager>
        <div class="x_content">
        <div class="table-responsive">
            <br />
            <Button onserverclick="Descargar" runat="server" class="btn btn-success"><i class="fa fa-download"></i> Excel</Button>
            <table id="tableddl" runat="server">
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


             <asp:GridView runat="server" ID="gvIncidents"  AutoGenerateColumns="False" class="table table-striped jambo_table bulk_action" AllowPaging="True"  EmptyDataText="No se han encontrado registros para mostrar"  OnPageIndexChanging="PageIndexChanging">
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
                </asp:GridView>
                    </div>
							
						
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
       </div>
       </div>
     </ContentTemplate>
    </asp:UpdatePanel>
        <!-- /page content -->
    </div>
    </div>
</asp:Content>
