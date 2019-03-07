using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{


    private string connectionString = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        //LOGIN, AÑADIR TAMBIEN BORRAR MENSAJES COMO POR EJEMPLO "ENVIADO"

        FillCategoria_SelectedIndexChanged();
        FillArea_SelectedIndexChanged();
        FillPrioridad_SelectedIndexChanged();
    }

    //Lista desplegable Categoria/Tipo
    protected void FillCategoria_SelectedIndexChanged()
    {


        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand SqlCmd = new SqlCommand("ddlCategoria", sqlCon);
        ddlCategoria.DataSource = SqlCmd.ExecuteReader();
        ddlCategoria.DataTextField = "Categoria";
        ddlCategoria.DataValueField = "IdCategoria";
        ddlCategoria.DataBind();
        sqlCon.Close();

    }

    //Lista desplegable Prioridad
    protected void FillPrioridad_SelectedIndexChanged()
    {
        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand SqlCmd = new SqlCommand("ddlPrioridad", sqlCon);
        ddlPrioridad.DataSource = SqlCmd.ExecuteReader();
        ddlPrioridad.DataTextField = "Prioridad";
        ddlPrioridad.DataValueField = "IdPrioridad";
        ddlPrioridad.DataBind();
        sqlCon.Close();



    }

    //Lista desplegable Area
    protected void FillArea_SelectedIndexChanged()
    {

        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand sqlCmd = new SqlCommand("ddlArea", sqlCon);
        ddlArea.DataSource = sqlCmd.ExecuteReader();
        ddlArea.DataTextField = "NameArea";
        ddlArea.DataValueField = "IdArea";
        ddlArea.DataBind();
        sqlCon.Close();


    }

    //Restablece formulario
    public void Clear()
    {
        txtTitulo.Text =  "";
        ddlPrioridad.SelectedIndex = 0;
        ddlCategoria.SelectedIndex = 0;
        ddlArea.SelectedIndex = 0;
    }


    //Se generara un nuevo ticket de incidencias
    protected void btnSend_Click(object sender, EventArgs e)
    {
  

        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand sqlCmd = new SqlCommand("TicketCreate", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.AddWithValue("@Area", ddlArea.SelectedItem.Value);
        sqlCmd.Parameters.AddWithValue("@Creador", 1);
        //sqlCmd.Parameters.AddWithValue("@Creador", Session["IdUser"].ToString());
        sqlCmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
        sqlCmd.Parameters.AddWithValue("@Categoria", ddlCategoria.SelectedItem.Value);
        sqlCmd.Parameters.AddWithValue("@Estado", 1); //El estado abierto tiene el Id 1, cambiarlo si ya no es asi.
        sqlCmd.Parameters.AddWithValue("@Prioridad", ddlPrioridad.SelectedItem.Value);
        sqlCmd.Parameters.AddWithValue("@Descripcion", Descripcion.InnerText);
        sqlCmd.Parameters.AddWithValue("@CDate", DateTime.Now);


        sqlCmd.ExecuteNonQuery();
        sqlCon.Close();
      //PONER AQUI MENSAJE ENVIADO
    }

}
