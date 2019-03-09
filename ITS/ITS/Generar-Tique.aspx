<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="MasterPage.master" CodeFile="Generar-Tique.aspx.cs" Inherits="_Default" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:UpdatePanel ID="Up_generate" runat="server">

        <ContentTemplate>
             <asp:ScriptManager ID="ScriptManager2" runat="server">
        
         </asp:ScriptManager>
        <div class="right_col" role="main">
          <div class="">
            <div class="page-title">
              <div class="title_left">
                <h3>Proporcione información del problema</h3>
              </div>

            </div>

            <div class="clearfix"></div>

            <div class="row">
              <div class="auto-style1">
                <div class="x_panel">
                  <div class="x_title">
                    <h2>Formulario</h2>
                  <div class="clearfix"></div>
                  </div>

                    <!-- FORMULARIO-->
                    <div class="x_content">
                    <br />
                     <div  class="form-horizontal form-label-left">
                      <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12">Titulo <span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                          <asp:TextBox ID="txtTitulo" class="form-control col-md-7 col-xs-12" placeholder="Nombre breve para identificar o exponer de qué se trata" required="required"  runat="server"></asp:TextBox>  
 
                        </div>
                      </div>
                      <div class="form-group">
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="last-name">Tipo de incidencia<span class="required"></span>
                        </label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                          <asp:DropDownList ID="ddlCategoria" class="form-control" AppendDataBoundItems="true" runat="server">
                          <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                          </asp:DropDownList>
                        </div>
                      </div>
                      <div class="form-group">
                        <label for="middle-name" class="control-label col-md-3 col-sm-3 col-xs-12">Area donde presenta</label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                          <asp:DropDownList ID="ddlArea" class="form-control" AppendDataBoundItems="true" runat="server" >
                          <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                          </asp:DropDownList>
                        </div>
                      </div>
                      <div class="form-group">
                        <label for="middle-name" class="control-label col-md-3 col-sm-3 col-xs-12">Nivel de Prioridad</label>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                          <asp:DropDownList ID="ddlPrioridad" class="form-control" AppendDataBoundItems="true"  runat="server">
                          <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                          </asp:DropDownList>
                        </div>
                      </div>


                        <br />
                         <h4>Descripcion</h4>
                  
                  <textarea id="Descripcion" required="required"  runat="server" class="style-descripcion" ></textarea>
                  <br />

                  <div class="ln_solid"></div>

                
                    <label class="control-label col-md-3 col-sm-3 col-xs-12">Resizable Text area</label>
                   
                      <textarea class="resizable_textarea form-control" placeholder="This text area automatically resizes its height as you fill in more text courtesy of autosize-master it out..."></textarea>

                      <div class="ln_solid"></div>
                      <div class="form-group">
                        <div class="col-md-6 col-sm-6 col-xs-12 col-md-offset-3">
						  <button class="btn btn-primary" type="reset">Reset</button>
                          <asp:Button ID="btnSend"  class="btn btn-success" runat="server" type="button"  Text="Guardar" OnClick="btnSend_Click" />  
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
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">

        .style-descripcion{
        min-height:250px; 
        min-width:1250px;  
        background-color:#fff;
        border-collapse:separate;
        border:1px solid #ccc;
        padding:4px;box-sizing:content-box;box-shadow:rgba(0,0,0,.07451) 0 1px 1px 0 inset;overflow:scroll;
        outline:0;border-radius:3px

        }
    </style>
</asp:Content>

