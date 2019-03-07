using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

public partial class admin_LogViewer : System.Web.UI.Page
{
    private string connectionString = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;



    //se encargara de arrojar los datos cuando se seleccione/cambie un [ListItem] y otros elementos.
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            
            FillAplicacionList();
            
            lstForm.Enabled = false;
            lstMethod.Enabled = false;

            lstForm.Items.Clear();
            lstForm.Items.Add("Seleccionar");
            lstMethod.Items.Clear();
            lstMethod.Items.Add("Seleccionar");


        }
    }

    //En este metodo se crean los item del Dropdownlist Aplicaciones
    private void FillAplicacionList()
    {
        //Si agregamos la siguiente linea, borrara los items existentes actuales del dropdownlist.
        lstAplicacion.Items.Clear();

        //Creamos un item nuevo.
        lstAplicacion.Items.Add("Seleccionar");

        //hace el llamado de la conexion y se crea sentencias para identificar el numero de columnas, se creara...
        //un lisitem por cada fila que exista. Se mostrara unicamente El Dato de la columna Nombre como vista de las mismas.
        string selectSQL = "SELECT ID, Nombre_Aplicacion FROM configuracion";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(selectSQL, con);
        SqlDataReader reader;

        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = reader["Nombre_Aplicacion"] + "";
                newItem.Value = reader["ID"].ToString();
                lstAplicacion.Items.Add(newItem);
            }
            reader.Close();
        }


        catch (Exception err)
        {
            lblresults1.Text = "Error al leer las columnas de la tabla. ";
            lblresults1.Text += err.Message;
        }
        finally
        {
            con.Close();
        }

    


    }

    //crea una cadena de conexion con los datos de la fila seleccionada.
    public void Test()
    {
        
        string _Servidor = "";
        string BD = "";
        string Usuario = "";
        string Contraseña = "";


        /* CAMBIARLO POR PARAMETROS, SOLO FALTA CREARLO EN SQL
       SqlConnection conn = new SqlConnection(connectionString);
       SqlCommand comm = new SqlCommand("VISUALIZAR", conn);
       comm.CommandType = CommandType.StoredProcedure;
       comm.Parameters.AddWithValue("@ap", lstAplicacion.SelectedValue);
       //comm.Parameters.AddWithValue("@Nombre_BD", lstAplicacion.SelectedValue);
       */

        string SqlConexion;
        SqlConexion = "SELECT * FROM Configuracion ";
        SqlConexion += "WHERE ID='" + lstAplicacion.SelectedItem.Value + "'";

        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(SqlConexion, con);
        SqlDataReader dr;

        con.Open();
        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                _Servidor = dr["Servidor"].ToString();
                BD = dr["Nombre_BD"].ToString();
                Usuario = dr["Usuario"].ToString();
                Contraseña = dr["Contraseña"].ToString();
            }
        }
        con.Close();
        
        //Conexion Local
        //string constr = "Data Source ="+_ip+"; Initial Catalog ="+BD+"; Integrated Security = SSPI";

        string Conexion = "Data Source=" + _Servidor + "; Initial Catalog=" + BD + "; User Id=" + Usuario + "; Password=" + Contraseña;
        Session["conexion"] = Conexion.ToString();


       
        


    }

    //En este metodo se crean los item del Dropdownlist Form
    private void FillFormList()
    {
        Test();
        //Si agregamos la siguiente linea, borrara los items existentes actuales del dropdownlist.

        lstForm.Items.Clear();
        lstForm.Items.Add("Seleccionar");
       
        string SQLForm = "SELECT Distinct (Form) from ErrorLog"; ;
        //Tambien puede funcionar esto; "SELECT MAX(ID), Form FROM ErrorLog group by Form";
        
        SqlConnection con2 = new SqlConnection((string)Session["conexion"]);

        SqlCommand cmd = new SqlCommand(SQLForm, con2);
        SqlDataReader reader;

        try
        {
            con2.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = reader["Form"] + "";
                newItem.Value = reader["Form"].ToString();
                lstForm.Items.Add(newItem);

            }
            reader.Close();

        }


        catch (Exception err)
        {
            lblresults1.Text = "Error al leer las columnas de la tabla. ";
            lblresults1.Text += err.Message;
        }
        finally
        {
            con2.Close();
        }

       
    }
    
    //En este metodo se crean los item del Dropdownlist Method
    private void FillMethodList()

    {
        lstMethod.Items.Clear();
        lstMethod.Items.Add("Seleccionar");

        Test();
        string SQLMethod = "SELECT Distinct (Method) from ErrorLog";
        //Tambien puede funcionar esto; "SELECT MAX(ID), Form Method ErrorLog group by Method";
        SqlConnection con3 = new SqlConnection((string)Session["conexion"]);
        SqlCommand cmd = new SqlCommand(SQLMethod, con3);
        SqlDataReader reader;

        try
        {
            con3.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = reader["Method"] + "";
                newItem.Value = reader["Method"].ToString();
                lstMethod.Items.Add(newItem);
            }
            reader.Close();
        }



        catch (Exception err)
        {
            lblresults3.Text = "Error al leer las columnas de la tabla. ";
            lblresults3.Text += err.Message;
        }
        finally
        {
            con3.Close();
        }
    }

   

    //En este metodo estan las condiciones del filtro y ademas la ejecucion de la tabla
    protected void lstAplicacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        //Condiciones para habilitar listas

        if (lstAplicacion.SelectedIndex > 0)

        {
            lstForm.Enabled = true;
            lstMethod.Enabled = true;
            Test();
            FillFormList();
            FillMethodList();


        }
        else
        {
            
            lstForm.Enabled = false;
            lstMethod.Enabled = false;
            //Volver a poner las listas en default
            
            lstForm.SelectedIndex = 0;
            lstMethod.SelectedIndex = 0;

            lstForm.Items.Clear();
            lstForm.Items.Add("Seleccionar");
            lstMethod.Items.Clear();
            lstMethod.Items.Add("Seleccionar");

        }


        



        //Condiciones para el filtrado de datos en tabla [CONSULTA SQL A APLICAR]
        {
            string SQLTable;
            SQLTable = "SELECT  * FROM ErrorLog ";

              


            //TABLA
          

            using (SqlConnection sqlCon = new SqlConnection((string)Session["conexion"]))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter(SQLTable, sqlCon);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                GridView.DataSource = dtbl;
                GridView.DataBind();
                sqlCon.Close();

            }
        }

    }



    protected void Condiciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        { 
        string SQLTable;

        if (lstAplicacion.SelectedIndex > 0 && lstForm.SelectedIndex > 0 && lstMethod.SelectedIndex > 0)

        {

            SQLTable = "SELECT  * FROM ErrorLog ";
            SQLTable += "WHERE FORM='" + lstForm.SelectedItem.Value + "'";
            SQLTable += " AND METHOD='" + lstMethod.SelectedItem.Value + "'";



        }

        else if (lstAplicacion.SelectedIndex > 0 && lstForm.SelectedIndex > 0 && lstMethod.SelectedIndex == 0)

        {

            SQLTable = "SELECT  * FROM ErrorLog ";
            SQLTable += "WHERE FORM='" + lstForm.SelectedItem.Value + "'";



        }

        else if (lstAplicacion.SelectedIndex > 0 && lstForm.SelectedIndex == 0 && lstMethod.SelectedIndex > 0)

        {
            SQLTable = "SELECT  * FROM ErrorLog ";
            SQLTable += "WHERE METHOD='" + lstMethod.SelectedItem.Value + "'";


        }

        else if (lstAplicacion.SelectedIndex > 0 && lstForm.SelectedIndex == 0 && lstMethod.SelectedIndex == 0)
        {
            SQLTable = "SELECT  * FROM ErrorLog ";


        }

        else
        {
            return;
        }

        using (SqlConnection sqlCon = new SqlConnection((string)Session["conexion"]))
        {
            sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter(SQLTable, sqlCon);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            GridView.DataSource = dtbl;
            GridView.DataBind();
            sqlCon.Close();

        }
    }
  }
}