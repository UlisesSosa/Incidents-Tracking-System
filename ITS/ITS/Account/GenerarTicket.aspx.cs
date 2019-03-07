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

    // SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-DDBTHB5L\NWERYDER;Initial Catalog=BTrackingS;Integrated Security=true;");
    private string connectionString = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //login session
            if (Session["username"] == null )
                Response.Redirect("~/login.aspx");

            //
            ddlEstado.Enabled = false;
            FillProj_SelectedIndexChanged();
            FillPrioridad_SelectedIndexChanged();
            FillEstado_SelectedIndexChanged();
            FillArea_SelectedIndexChanged();
        }
    }

    //llenara los datos de la columna de proyecto para el dropdownlist
    protected void FillProj_SelectedIndexChanged()
    {
        ddlCategoria.Items.Clear();
        ddlCategoria.Items.Add("Seleccionar");

        
            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand cmd = new SqlCommand("ddlCategoria", sqlCon);
            SqlDataReader reader;


            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = reader["Categoria"].ToString();
                newItem.Value = reader["IdCategoria"].ToString();
                ddlCategoria.Items.Add(newItem);
            }
            reader.Close();
            sqlCon.Close();
    
        

    }

    //llera los datos de las columnas de prioridad para el dropdownlist
    protected void FillPrioridad_SelectedIndexChanged()
    {
        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand SqlCmd = new SqlCommand("ddlPrioridad", sqlCon);
        ddlPrioridad.DataSource = SqlCmd.ExecuteReader();
        ddlPrioridad.DataTextField = "NamePrioridad";
        ddlPrioridad.DataValueField = "IdPrioridad";
        ddlPrioridad.DataBind();
        sqlCon.Close();



    }

    //llera los datos de las columnas de Estado para el dropdownlist
    protected void FillEstado_SelectedIndexChanged()
    {

        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand sqlCmd = new SqlCommand("ddlEstado", sqlCon);
        ddlEstado.DataSource = sqlCmd.ExecuteReader();
        ddlEstado.DataTextField = "NameEstado";
        ddlEstado.DataValueField = "IdEstados";
        ddlEstado.DataBind();
        sqlCon.Close();


    }

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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }

    //Limpiara los campos de edicion al usuario
    public void Clear()
    {
        txtNameBug.Text = txtDescription.Text = "";
        lblSuccessMessage.Text = lblErrorMessage.Text = "";
        ddlPrioridad.SelectedIndex = 0;
        ddlCategoria.SelectedIndex = 0;
        ddlArea.SelectedIndex = 0;
    }


    //Enviara un nuevo reporte
    protected void btnSend_Click(object sender, EventArgs e)
    {
        lblSuccessMessage.Text = "";
        lblErrorMessage.Text = "";



        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand sqlCmd = new SqlCommand("TicketCreate", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.AddWithValue("@Area", ddlArea.SelectedItem.Value);
        sqlCmd.Parameters.AddWithValue("@Creador", Session["IdUser"].ToString());
        sqlCmd.Parameters.AddWithValue("@Titulo", txtNameBug.Text.Trim());
        sqlCmd.Parameters.AddWithValue("@Categoria", ddlCategoria.SelectedItem.Value);
        sqlCmd.Parameters.AddWithValue("@Estado", ddlEstado.SelectedItem.Value);
        sqlCmd.Parameters.AddWithValue("@Prioridad", ddlPrioridad.SelectedItem.Value);
        sqlCmd.Parameters.AddWithValue("@Descripcion", txtDescription.Text);
        sqlCmd.Parameters.AddWithValue("@CDate", DateTime.Now);


        sqlCmd.ExecuteNonQuery();
        sqlCon.Close();
        Clear();
        lblSuccessMessage.Text = "Guardado exitosamente";
    }

    }

