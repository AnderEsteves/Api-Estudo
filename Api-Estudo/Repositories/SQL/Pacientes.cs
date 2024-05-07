using Api_Estudo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api_Estudo.Repositories.SQL
{
    public class Pacientes
    {

        private readonly SqlConnection conn;
        private readonly SqlCommand cmd;

        public Pacientes(string connectionString) {

            conn = new SqlConnection(connectionString);

            cmd = new SqlCommand(){
                Connection = conn
            };
        
        }


        public List<Models.Paciente> Get()
        {

            List<Models.Paciente> pacientes =new List<Models.Paciente>();

            using(this.conn)
            {
                this.conn.Open();

                using (this.cmd)
                {
                    cmd.CommandText = "select codigo, nome, data_nascimento from paciente;";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Models.Paciente paciente = new Models.Paciente();

                            paciente.Codigo = (int)dr["codigo"];
                            paciente.Nome = dr["nome"].ToString();
                            paciente.DataNascimento = (DateTime)dr["data_nascimento"];

                            pacientes.Add(paciente);
                        }
                    }
                }
            }

            return pacientes;
        }

        public Models.Paciente Get(int id)
        {
            Models.Paciente paciente = null;

            using (this.conn)
            {
                this.conn.Open();

                using (this.cmd)
                {
                    cmd.CommandText = "select codigo, nome, data_nascimento from paciente where codigo = @codigo;";
                    cmd.Parameters.Add(new SqlParameter("@codigo", System.Data.SqlDbType.Int)).Value = id;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            paciente = new Models.Paciente();

                            paciente.Codigo = (int)dr["codigo"];
                            paciente.Nome = dr["nome"].ToString();
                            paciente.DataNascimento = (DateTime)dr["data_nascimento"];

                        }
                    }
                }
            }

            return paciente;
        }
    }
}