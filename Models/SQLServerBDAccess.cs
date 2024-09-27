using AgapeaNetCore_V5.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Overwatch.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Overwatch.Models
{
    public class SQLServerBDAccess : IDBAccess
    {

        #region ....propiedades de clase....
        private readonly IConfiguration _config;
        private IOptions<ConfigSQLServer> _configSQLServer;
        private String __cadenaConexion;
        #endregion

        public SQLServerBDAccess(IOptions<ConfigSQLServer> objetoinyOpcConfigSQL,
                                IConfiguration config)
        {
            this._config = config;
            this._configSQLServer = objetoinyOpcConfigSQL;
            this.__cadenaConexion = _config.GetConnectionString("SQLServerConnectionString"); // "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Overwatch;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";//this._configSQLServer.Value.SQLServerConnectionString;
        }

        public List<Heroe> RecuperarListaHeroes()
        {
            //Crear y abrir conexión
            SqlConnection _conexionBD = new SqlConnection(this.__cadenaConexion);
            _conexionBD.Open();

            //Crear comando SQL para recuperar un id y nombre de héroe, configurar su conexión y la peticiónSQL; EJECUTARLO
            SqlCommand _selectHeroes = new SqlCommand();
            _selectHeroes.Connection = _conexionBD;
            _selectHeroes.CommandText = "SELECT IdHeroe, Nombre FROM dbo.Heroe ORDER BY Nombre ASC";

            SqlDataReader _resultado = _selectHeroes.ExecuteReader();


            //Si OK: añadir a la lista ese héroe, cerrar conexión y devolver la lista :)
            List<Heroe> _listaHeroes = new List<Heroe>();

            while (_resultado.Read())
            {
                _listaHeroes.Add(new Heroe

                                 {
                                    IdHeroe = System.Convert.ToInt32(_resultado["idHeroe"]),
                                    Nombre = _resultado["Nombre"].ToString()
                                 }
                );


            }

            _conexionBD.Close();
            return _listaHeroes;
        }



        public bool RegistrarHeroe(Heroe NuevoHeroe)
        {
            try
            {
                //1º conectarnos al servidor y a la BD
                SqlConnection __miconexion = new SqlConnection();
                __miconexion.ConnectionString = this.__cadenaConexion;

                __miconexion.Open();

                //2º lanzar comando INSERT sobre tabla dbo.Clientes
                SqlCommand __insertClientes = new SqlCommand();
                __insertClientes.Connection = __miconexion;

                //construccion query
                string sql = "";

                sql = "INSERT INTO dbo.Heroe ";
                sql += " ( ";
                sql += " Nombre, ";
                sql += " Rol, ";
                sql += " Vida, ";
                sql += " Dano, ";
                sql += " Cura ";
                sql += " ) ";

                sql += " VALUES ";
                sql += " ( ";
                sql += " @Nombre, ";
                sql += " @Rol, ";
                sql += " @Vida, ";
                sql += " @Dano, ";
                sql += " @Cura ";
                sql += " ) ";

                __insertClientes.CommandText = sql;
                __insertClientes.Parameters.AddWithValue("@Nombre", NuevoHeroe.Nombre);
                __insertClientes.Parameters.AddWithValue("@Rol", NuevoHeroe.Rol);
                __insertClientes.Parameters.AddWithValue("@Vida", NuevoHeroe.Vida);
                __insertClientes.Parameters.AddWithValue("@Dano", NuevoHeroe.Dano);
                __insertClientes.Parameters.AddWithValue("@Cura", NuevoHeroe.Cura);

                int __filasafectadas = __insertClientes.ExecuteNonQuery();

                __miconexion.Close();

                if (__filasafectadas > 0)
                {
                    return true;
                } 
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public Heroe RecuperarListaHeroes(int idHeroe)
        {
            SqlConnection __conexionBD = new SqlConnection(this.__cadenaConexion);
            __conexionBD.Open();

            SqlCommand __SelectHeroe = new SqlCommand();
            __SelectHeroe.Connection = __conexionBD;
            __SelectHeroe.CommandText = "SELECT IdHeroe, Nombre, Rol, Vida, Dano, Cura FROM dbo.Heroe WHERE IdHeroe = @IdHeroe;";

            __SelectHeroe.Parameters.AddWithValue("@IdHeroe", idHeroe);

            SqlDataReader __resultado = __SelectHeroe.ExecuteReader();

            Heroe _Heroe = new Heroe();

            while (__resultado.Read())
            {
                _Heroe.IdHeroe = System.Convert.ToInt32(__resultado["IdHeroe"]);
                _Heroe.Nombre = __resultado["Nombre"].ToString();
                _Heroe.Rol = __resultado["Rol"].ToString();
                _Heroe.Vida = System.Convert.ToInt32(__resultado["Vida"]);
                _Heroe.Cura = System.Convert.ToInt32(__resultado["Cura"]);
                _Heroe.Dano = System.Convert.ToInt32(__resultado["Dano"]);
            }


            __conexionBD.Close();
            return _Heroe;
        }
    }
}
