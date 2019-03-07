using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ReportBug : System.Web.UI.Page
{

   
    private string connectionString = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //login session
            if (Session["username"] == null)
            Response.Redirect("~/login.aspx");
         
            GvwCategoria();
            GvwEstado();
            GvwPrioridad();
            BindGrid("Filtro", CommandType.Text, new List<SqlParameter>().ToArray());

        }
    }



    //Gridview y filtro
    protected void Filtro(object sender, EventArgs e)
    {
        
            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            if (ddlCategoriagvw.SelectedIndex > 0)
            {
                sqlParameter.Add(new SqlParameter("@Categoria", ddlCategoriagvw.SelectedItem.Value.Trim()));
            }
            if (ddlEstadogvw.SelectedIndex > 0)
            {
                sqlParameter.Add(new SqlParameter("@Estado", ddlEstadogvw.SelectedItem.Value.Trim()));
            }
            if (ddlPrioridadgvw.SelectedIndex > 0)
            {
                sqlParameter.Add(new SqlParameter("@Prioridad", ddlPrioridadgvw.SelectedItem.Value.Trim()));
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
        gvBugs.DataSource = dt;
        gvBugs.DataBind();




        lblDetailsMessage.Text = this.gvBugs.Rows.Count.ToString();
        // lblDetailsMessage.Text += "  Componentes";
    }


    protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
      

            gvBugs.PageIndex = e.NewPageIndex;
            BindGrid("Filtro", CommandType.Text, new List<SqlParameter>().ToArray());

          

    }


    //Dropdownlist's del Gridview
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
            ddlPrioridadgvw.DataTextField = "NamePrioridad";
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
            ddlEstadogvw.DataTextField = "NameEstado";
            ddlEstadogvw.DataValueField = "IdEstados";
            ddlEstadogvw.DataBind();
            sqlCon.Close();
 

    }
    
    //limpiar campos de textos
    public void Clear()
    {

        lblSuccessMessage.Text = lblErrorMessage.Text = lblObservableMessage.Text = lblDetailsMessage.Text = "";

    }


    //Obtiene el id de la fila y redirige a otra pagina.
    protected void lnk_Asignar(object sender, EventArgs e)
    {

        int IDBUG = Convert.ToInt32((sender as LinkButton).CommandArgument);
        Response.Redirect(string.Format("inf-Incidencia.aspx?id={0}", IDBUG));
    }

}







