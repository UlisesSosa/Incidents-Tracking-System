using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Globalization;
using System.Threading;
using System.Security.Cryptography;
using System.Text;

public partial class admin_BugTracker : System.Web.UI.Page


{


    private string connectionString = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //login session
            if (Session["username"] == null)
                Response.Redirect("~/login.aspx");

            btnDelete.Enabled = false;
            FillGridView();
            FillRol_SelectedIndexChanged();
            FillEstado_SelectedIndexChanged();
            FillArea_SelectedIndexChanged();
        }
    }


    //Mostrara lista de usuarios en el Gridview
    void FillGridView()
    {
       
            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SelectUserAll", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            gvContact.DataSource = dtbl;
            gvContact.DataBind();
      
    
        

    }



    protected void FillRol_SelectedIndexChanged()
    {

        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand SqlCmd = new SqlCommand("ddlRol", sqlCon);
        ddlRol.DataSource = SqlCmd.ExecuteReader();
        ddlRol.DataTextField = "Rol";
        ddlRol.DataValueField = "IdRol";
        ddlRol.DataBind();
        sqlCon.Close();

    }

    protected void FillEstado_SelectedIndexChanged()
    {

        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand SqlCmd = new SqlCommand("ddlUsuarioEstado", sqlCon);
        ddlEstado.DataSource = SqlCmd.ExecuteReader();
        ddlEstado.DataTextField = "Estado";
        ddlEstado.DataValueField = "IdEstado";
        ddlEstado.DataBind();
        sqlCon.Close();

    }

    protected void FillArea_SelectedIndexChanged()
    {

        SqlConnection sqlCon = new SqlConnection(connectionString);
        if (sqlCon.State == ConnectionState.Closed)
            sqlCon.Open();
        SqlCommand SqlCmd = new SqlCommand("ddlArea", sqlCon);
        ddlArea.DataSource = SqlCmd.ExecuteReader();
        ddlArea.DataTextField = "NameArea";
        ddlArea.DataValueField = "IdArea";
        ddlArea.DataBind();
        sqlCon.Close();

    }




    //leera las columnas de las tablas de acuerdo al id seleccionado  y las pondra en los campos.
    protected void lnk_OnClick(object sender, EventArgs e)
    {

        try
        {
            int IDUSER = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SelectUserByID", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@IdUser", IDUSER);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            EditarUser.Value = IDUSER.ToString();
            txtNombre.Text = dtbl.Rows[0]["Nombre"].ToString();
            txtApellido.Text = dtbl.Rows[0]["Apellido"].ToString();
            txtUsername.Text = dtbl.Rows[0]["Username"].ToString();
            txtPassword.Text = dtbl.Rows[0]["Password"].ToString();
            txtEmail.Text = dtbl.Rows[0]["Email"].ToString();
            int Rol = Convert.ToInt32(dtbl.Rows[0]["IdRol"].ToString());
            ddlRol.SelectedIndex = Rol;
            int Area = Convert.ToInt32(dtbl.Rows[0]["IdArea"].ToString());
            ddlArea.SelectedIndex = Area;
            int Estado = Convert.ToInt32(dtbl.Rows[0]["IdEstado"].ToString());
            ddlEstado.SelectedIndex = Estado;
           }
        catch 
        {
         
        }


        lblErrorMessage.Text = lblSuccessMessage.Text = "";
        btnSave.Text = "Actualizar";
        btnDelete.Enabled = true;

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    //Limpiara los campos de edicion al usuario
    public void Clear()
    {
        EditarUser.Value = "";
        txtNombre.Text = txtApellido.Text = txtUsername.Text = txtPassword.Text = txtEmail.Text = "";
        lblSuccessMessage.Text = lblErrorMessage.Text = "";
        ddlRol.SelectedIndex = 0;
        btnSave.Text = "Guardar";
        btnDelete.Enabled = false;
    }

    public string ValidarDatos()
    {
        string Resultado = "";
        if (txtNombre.Text == "")
        {
            Resultado = Resultado + " | Nombre |\n";
        }
        if (txtApellido.Text == "")
        {
            Resultado = Resultado + " | Apellido |\n";
        }
        if (txtUsername.Text == "")
        {
            Resultado = Resultado + " | Username |\n";
        }
        if (txtPassword.Text == "")
        {
            Resultado = Resultado + " | Password |\n";
        }
        if (txtEmail.Text == "")
        {
            Resultado = Resultado + " | Email |\n";
        }

        //Condicion si no se elegio un rol en especifico 
        if (ddlRol.SelectedIndex == 0)

        {
            Resultado = Resultado + " | Rol: Defina el tipo de restricción para este registro |\n";
        }

        //Condicion si no se elegio un rol en especifico 
        if (ddlArea.SelectedIndex == 0)

        {
            Resultado = Resultado + " | Area: Defina el Area en el que opera |\n";
        }

        //Condicion si no se elegio un rol en especifico 
        if (ddlEstado.SelectedIndex == 0)

        {
            Resultado = Resultado + " | Rol: Defina el Estado para este registro |\n";
        }

        return Resultado;


    }


    //Modelo encriptacion MD5
    static string Encrypt(string value)
    {
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] data = md5.ComputeHash(utf8.GetBytes(value));
            return Convert.ToBase64String(data);
        }
    }

    //Actualizara los datos del usuario o creara uno nuevo
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblSuccessMessage.Text = "";
        lblErrorMessage.Text = "";
        //Contraseña encriptada
        txtPassword.Text = Encrypt(txtPassword.Text);

       
            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("UserCreateOrUpdate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@IdUser", (EditarUser.Value == "" ? 0 : Convert.ToInt32(EditarUser.Value)));
            sqlCmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@Apellido", txtApellido.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@IdRol", ddlRol.SelectedItem.Value);
            sqlCmd.Parameters.AddWithValue("@IdArea", ddlArea.SelectedItem.Value);
            sqlCmd.Parameters.AddWithValue("@IdEstado", ddlEstado.SelectedItem.Value);
            sqlCmd.Parameters.AddWithValue("@CDate", DateTime.Now);

            //Condicion si hay campos vacios

            string sResultado = ValidarDatos();

            if (sResultado == "")
            {
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                string UserID = EditarUser.Value;
                Clear();
                if (UserID == "")
                
                    lblSuccessMessage.Text = "Guardado exitosamente";
                else
                    lblSuccessMessage.Text = "Actualizado exitosamente";
                FillGridView();
            }
            else
            {
                lblErrorMessage.Text = "Faltan completar campos: \n" + sResultado;
            }

       
        


    }
    

    //Borra usuario
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection sqlCon = new SqlConnection(connectionString);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("UserDeleteByID", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@IdUser", Convert.ToInt32(EditarUser.Value));
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            Clear();
            FillGridView();
            lblSuccessMessage.Text = "Eliminado exitosamente";

        }
        catch (Exception exception1)
        {
            Exception exception = exception1;
            PEDSS.Errorlog.Register(exception.Message, "ListUsers", "btnDelete_Click", exception.StackTrace, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.UserAgent);
        }


    }

}

