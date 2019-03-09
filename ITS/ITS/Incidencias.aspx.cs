using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GvwArea();
            GvwCategoria();
            GvwPrioridad();
            GvwEstado();
            BindGrid("Filtro", CommandType.Text, new List<SqlParameter>().ToArray());
        }
    }



    //Gridview y filtro
    protected void Filtro(object sender, EventArgs e)
    {
        List<SqlParameter> sqlParameter = new List<SqlParameter>();
        if (ddlAreagvw.SelectedIndex > 0)
        {
            sqlParameter.Add(new SqlParameter("@Area", ddlAreagvw.SelectedItem.Value.Trim()));
        }
        if (ddlCategoriagvw.SelectedIndex > 0)
        {
            sqlParameter.Add(new SqlParameter("@Categoria", ddlCategoriagvw.SelectedItem.Value.Trim()));
        }
        if (ddlPrioridadgvw.SelectedIndex > 0)
        {
            sqlParameter.Add(new SqlParameter("@Prioridad", ddlPrioridadgvw.SelectedItem.Value.Trim()));
        }
        if (ddlEstadogvw.SelectedIndex > 0)
        {
            sqlParameter.Add(new SqlParameter("@Estado", ddlEstadogvw.SelectedItem.Value.Trim()));
        }
        BindGrid("Filtro", CommandType.StoredProcedure, sqlParameter.ToArray());
    }


    private void BindGrid(string query, CommandType commandType, SqlParameter[] parameters)
    {
        DataTable dt = new DataTable();
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(query, con);
        cmd.CommandType = commandType;
        if (parameters.Length > 0)
        {
            cmd.Parameters.AddRange(parameters);
        }
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(dt);
        gvIncidents.DataSource = dt;
        gvIncidents.DataBind();
        BtnResultados.Text = this.gvIncidents.Rows.Count.ToString();
    }


    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncidents.PageIndex = e.NewPageIndex;
        BindGrid("Filtro", CommandType.Text, new List<SqlParameter>().ToArray());
    }


    //Dropdownlist's del Gridview

    protected void GvwArea()
    {


        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand SqlCmd = new SqlCommand("ddlArea", sqlCon);
        ddlAreagvw.DataSource = SqlCmd.ExecuteReader();
        ddlAreagvw.DataTextField = "Area";
        ddlAreagvw.DataValueField = "IdArea";
        ddlAreagvw.DataBind();
        sqlCon.Close();



    }
    protected void GvwCategoria()
    {


        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand SqlCmd = new SqlCommand("ddlCategoria", sqlCon);
        ddlCategoriagvw.DataSource = SqlCmd.ExecuteReader();
        ddlCategoriagvw.DataTextField = "Categoria";
        ddlCategoriagvw.DataValueField = "IdCategoria";
        ddlCategoriagvw.DataBind();
        sqlCon.Close();



    }
    private void GvwPrioridad()
    {


        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand SqlCmd = new SqlCommand("ddlPrioridad", sqlCon);
        ddlPrioridadgvw.DataSource = SqlCmd.ExecuteReader();
        ddlPrioridadgvw.DataTextField = "Prioridad";
        ddlPrioridadgvw.DataValueField = "IdPrioridad";
        ddlPrioridadgvw.DataBind();
        sqlCon.Close();




    }
    private void GvwEstado()
    {

        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand sqlCmd = new SqlCommand("ddlEstado", sqlCon);
        ddlEstadogvw.DataSource = sqlCmd.ExecuteReader();
        ddlEstadogvw.DataTextField = "Estado";
        ddlEstadogvw.DataValueField = "IdEstado";
        ddlEstadogvw.DataBind();
        sqlCon.Close();


    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void DescargarExcel(object sender, EventArgs e)
    {
 
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Incidencias.xls"));
        Response.ContentType = "application/vnd.ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
        gvIncidents.AllowPaging = false;
        gvIncidents.RenderControl(hw);
        Response.Write(sw.ToString());
        Response.End();

    }

    protected void DescargarPDF(object sender, EventArgs e)
    {

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Vithal_Wadje.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gvIncidents.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        gvIncidents.AllowPaging = true;
        gvIncidents.DataBind();

    }
    

    




    //Obtiene el id de la fila y redirige a otra pagina.
    protected void lnk_Asignar(object sender, EventArgs e)
    {

        int IDBUG = Convert.ToInt32((sender as LinkButton).CommandArgument);
        Response.Redirect(string.Format("inf-Incidencia.aspx?id={0}", IDBUG));
    }
}