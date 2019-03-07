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

    private int IDBUG
    {
        get { return (int)Session["idBug"]; }
        set { Session["idBug"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //login session
            if (Session["username"] == null)
            Response.Redirect("~/login.aspx");


            //tomo el id del bug  que vino en la url y se lo da a IDBUG
            if (Request.Params["id"] == null)
                this.IDBUG = -1; //si se seelcciono la opcion de nuevo
            else
                this.IDBUG = Convert.ToInt32(Request.Params["id"]);

            //Carga metodos
          
            FillPrioridad();
            FillEstado();
            FillAsignadoTo();
            Asignar(this.IDBUG);

        }
    }



    //Default txt y ddl
    public void Clear()
    {
        txtTitulo.Text = txtCategoria.Text = txtDescription.Text = txtResolucion.Text = "";
        lblSuccessMessage.Text = lblErrorMessage.Text = lblObservableMessage.Text = "";

    }


    //Dropdownlist's seguimiento del Bug
    protected void FillPrioridad()
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
    protected void FillEstado()
    {


     

            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand sqlCmd= new SqlCommand("ddlEstado", sqlCon);
            ddlEstado.DataSource = sqlCmd.ExecuteReader();
            ddlEstado.DataTextField = "NameEstado";
            ddlEstado.DataValueField = "IdEstados";
            ddlEstado.DataBind();
            sqlCon.Close();
        


    }
    protected void FillAsignadoTo()
    {
       
            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("ddlAT", sqlCon);
            ddlAsignadoTo.DataSource = sqlCmd.ExecuteReader();
            ddlAsignadoTo.DataTextField = "Username";
            ddlAsignadoTo.DataValueField = "IdUser";
            ddlAsignadoTo.DataBind();
            sqlCon.Close();
     
        

    }



    //Pondra los campos de la tabla al formulario que muestra los detalles del bug
    protected void Asignar(int id)
    {
        Clear();
        
        lblSuccessMessage.Text = "";
        lblErrorMessage.Text = "";
        txtDescription.Attributes.Add("readonly", "readonly");

      
            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter SqlCmd = new SqlDataAdapter("SelectTicketByID", sqlCon);
            SqlCmd.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlCmd.SelectCommand.Parameters.AddWithValue("@ID", id);


            
            SqlDataReader reader = SqlCmd.SelectCommand.ExecuteReader();
            while (reader.Read())

            {

                //Poner los datos de la tabla a los campos del formulario
                txtSession.Text = Session["username"].ToString();
                txtTitulo.Text = reader["Titulo"].ToString();
                txtDescription.Text = reader["Descripcion"].ToString();
                txtResolucion.Text = reader["Resolución"].ToString();
                ddlEstado.SelectedValue = reader["idEstado"].ToString();
                ddlPrioridad.SelectedValue = reader["idPrioridad"].ToString();

               //Comprobar si hay personal asignado para la incidencia 
                string Personal = reader["AsignadoTo"].ToString();
                if (Personal == "")
                {
                ddlAsignadoTo.SelectedIndex = 0;
                lblErrorMessage.Text = "No hay personal asignado para esta incidencia";
                }
                else
                {
                ddlAsignadoTo.SelectedValue = Personal;
                }

                //Comprobar si el area de la incidencia existe en la bd
                string Area = reader["NameArea"].ToString();
                if (Area == "")
                {
                txtArea.Text = "N/A";
                }
                else
                {
                txtArea.Text = Area;
                }


               //Comprobar si la categoria/tipo de incidencia existe en la bd
                string Categoria =  reader["Categoria"].ToString();
                if (Categoria == "")
                {
                txtCategoria.Text = "N/A";
                }
                else
                {
                txtCategoria.Text = Categoria;
                }

            
            }
            sqlCon.Close();
            
    }
    //Actualizara los datos del bug
    protected void btnSend_Click(object sender, EventArgs e)
    {

        lblSuccessMessage.Text = "";
        lblErrorMessage.Text = "";


        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand sqlCmd = new SqlCommand("TicketUpdate", sqlCon);
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.AddWithValue("@ID", IDBUG);
        sqlCmd.Parameters.AddWithValue("@Estado", ddlEstado.SelectedValue);
        sqlCmd.Parameters.AddWithValue("@Prioridad", ddlPrioridad.SelectedItem.Value);
        sqlCmd.Parameters.AddWithValue("@Resolucion", txtResolucion.Text);
        sqlCmd.Parameters.AddWithValue("@MDate", DateTime.Now);

        //Condicion si no se ha elegido usuarios a asignar para el bug. 

        if (ddlAsignadoTo.SelectedIndex == 0)
        {

            sqlCmd.Parameters.AddWithValue("@AsignadoBy", "");
            sqlCmd.Parameters.AddWithValue("@AsignadoTo", "");

        }

        else if (ddlAsignadoTo.SelectedIndex > 0)

        {
       
              sqlCmd.Parameters.AddWithValue("@AsignadoBy", Session["IdUser"].ToString());
              sqlCmd.Parameters.AddWithValue("@AsignadoTo", ddlAsignadoTo.SelectedValue);
                        

        }

        //Condicion registrar fecha reporte cerrado
        if (ddlEstado.SelectedItem.Text == "Cerrado")

        {
            sqlCmd.Parameters.AddWithValue("@RDate", DateTime.Now);

        }

        else if (ddlEstado.SelectedItem.Text != "Cerrado")

        {

            sqlCmd.Parameters.AddWithValue("@RDate", DBNull.Value);

        }

            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            lblSuccessMessage.Text = "¡Guardado exitosamente!";
            Response.Redirect("Incidencias.aspx");

        
    }
    //Retroceso
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Clear();
        Response.Redirect("Incidencias.aspx");


    }
}







