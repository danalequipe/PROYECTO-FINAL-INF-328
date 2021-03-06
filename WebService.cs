﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Descripción breve de WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hola a todos";
    }
    [WebMethod]
    public DataSet Consulta_Identidad_Docente(int CI, string NOMBRE)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = "server=DESKTOP-JJMELD8;user=sa;pwd=1234567;database=PROYECTOFINAL";
        SqlDataAdapter ada = new SqlDataAdapter();

        ada.SelectCommand = new SqlCommand();
        ada.SelectCommand.Connection = con;
        ada.SelectCommand.CommandText = "select * from Docente where CIDOC=@CI and NOMDOC=@NOMBRE";
        ada.SelectCommand.CommandType = CommandType.Text;
        ada.SelectCommand.Parameters.Add("@CI", SqlDbType.VarChar, 20).Value = CI;
        ada.SelectCommand.Parameters.Add("@NOMBRE", SqlDbType.VarChar, 40).Value = NOMBRE;
        DataSet ds = new DataSet();
        ada.Fill(ds);
        return ds;
    }
    [WebMethod]
    public DataSet Consulta_Identidad_Estudiante(int CI, string NOMBRE)
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = "server=DESKTOP-JJMELD8;user=sa;pwd=1234567;database=PROYECTOFINAL";
        SqlDataAdapter ada = new SqlDataAdapter();

        ada.SelectCommand = new SqlCommand();
        ada.SelectCommand.Connection = con;
        ada.SelectCommand.CommandText = "select * from Estudiante where CI=@CI and NOMEST=@NOMBRE";
        ada.SelectCommand.CommandType = CommandType.Text;
        ada.SelectCommand.Parameters.Add("@CI",SqlDbType.VarChar, 20).Value = CI;
        ada.SelectCommand.Parameters.Add("@NOMBRE", SqlDbType.VarChar, 40).Value = NOMBRE;
        DataSet ds = new DataSet();
        ada.Fill(ds);
        
        return ds;


    }
    // ESTOS DOS FUNCIONES A CONTINUACION ME DEVUELVE UN MENSAJE SI EL ESTUDIANTE ESTA INSCRITO COMO ESTUDIANTE EN LA INSTITUCION
    [WebMethod]
    public string CONSULTA_IDENTIFICACION_ESTUDIANTE(int CI, String NOMBRE)
    {
        string mensaje = "";
        SqlConnection con = new SqlConnection();
        con.ConnectionString = "server=DESKTOP-JJMELD8;user=sa;pwd=1234567;database=PROYECTOFINAL";
        List<string> estudiantes = new List<string>();
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Estudiante WHERE CI = @CI and NOMEST=@NOMBRE", con);
            cmd.Parameters.AddWithValue("@CI", CI);
            cmd.Parameters.AddWithValue("@NOMBRE", NOMBRE);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                mensaje = "ESTUDIANTE INSCRITO";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string ci = dt.Rows[i]["CI"].ToString();
                    string sigla = dt.Rows[i]["NOMEST"].ToString();

                    estudiantes.Add(ci);
                    estudiantes.Add(sigla);

                }
            }
            else
            {
                mensaje = "ESTUDIANTE NO INSCRITO";
            }
            con.Close();
        }
        return mensaje;
    }
    [WebMethod]
    public string CONSULTA_IDENTIFICACION_DOCENTE(int CI, String NOMBRE)
    {
        string mensaje = "";
        SqlConnection con = new SqlConnection();
        con.ConnectionString = "server=DESKTOP-JJMELD8;user=sa;pwd=1234567;database=PROYECTOFINAL";
        List<string> estudiantes = new List<string>();
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Docente WHERE CIDOC = @CI and NOMDOC=@NOMBRE", con);
            cmd.Parameters.AddWithValue("@CI", CI);
            cmd.Parameters.AddWithValue("@NOMBRE", NOMBRE);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                mensaje = "DOCENTE REGISTRADO";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string ci = dt.Rows[i]["CIDOC"].ToString();
                    string sigla = dt.Rows[i]["NOMDOC"].ToString();

                    estudiantes.Add(ci);
                    estudiantes.Add(sigla);

                }
            }
            else
            {
                mensaje = "DOCENTE NO REGISTRADO";
            }
            con.Close();
        }
        return mensaje;
    }
}
