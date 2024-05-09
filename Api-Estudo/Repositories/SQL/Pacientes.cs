using Api_Estudo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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


        public async Task<List<Models.Paciente>> Get()
        {
            List<Models.Paciente> pacientes =new List<Models.Paciente>();

            using(this.conn)
            {
                await this.conn.OpenAsync();

                using (this.cmd)
                {
                    cmd.CommandText = "select codigo, nome, data_nascimento from paciente;";

                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
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

        public async Task<Models.Paciente> Get(int id)
        {
            Models.Paciente paciente = null;

            using (this.conn)
            {
                await this.conn.OpenAsync();

                using (this.cmd)
                {
                    cmd.CommandText = "select codigo, nome, data_nasciment from paciente where codigo = @codigo;";
                    cmd.Parameters.Add(new SqlParameter("@codigo", System.Data.SqlDbType.Int)).Value = id;

                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
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

        public async Task<List<Models.Paciente>> Get(string nome)
        {

            List<Models.Paciente> pacientes = new List<Paciente>();

            using (this.conn)
            {
                await this.conn.OpenAsync();

                using (this.cmd) {

                    cmd.CommandText = "select nome, codigo, data_nascimento from paciente where nome like @nome;";
                    cmd.Parameters.Add("@nome", System.Data.SqlDbType.VarChar).Value = $"%{nome}%";

                    using(SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                           Models.Paciente paciente = new Models.Paciente();

                            paciente.Nome = dr["nome"].ToString();
                            paciente.Codigo = (int) dr["codigo"];
                            paciente.DataNascimento = (DateTime)dr["data_nascimento"];

                            pacientes.Add(paciente);
                        }
                    }
                }
            }

            return pacientes;
        }


        public async Task<bool> Post(Models.Paciente paciente)
        {
            using (this.conn)
            {
                await this.conn.OpenAsync();

                using (this.cmd){

                    cmd.CommandText = "insert into paciente (nome, data_nascimento) values (@nome, @data_nascimento); select convert(int,SCOPE_IDENTITY());";

                    cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = paciente.Nome;
                    cmd.Parameters.Add(new SqlParameter("@data_nascimento", System.Data.SqlDbType.Date)).Value = paciente.DataNascimento;
                    paciente.Codigo = (int) await cmd.ExecuteScalarAsync();

                }
            }

            return paciente.Codigo != 0;
        }


        public async Task<bool> Put(Models.Paciente paciente)
        {

            int linhasAfetadas = 0;

            using (this.conn)
            {
                await this.conn.OpenAsync();

                using (this.cmd)
                {
                    cmd.CommandText = "update paciente set nome = @nome, data_nascimento = @data_nascimento where codigo = @id ";
                   
                    cmd.Parameters.Add(new SqlParameter("@nome", System.Data.SqlDbType.VarChar)).Value = paciente.Nome;
                    cmd.Parameters.Add(new SqlParameter("@data_nascimento", System.Data.SqlDbType.Date)).Value = paciente.DataNascimento;
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = paciente.Codigo;

                    linhasAfetadas = (int) await cmd.ExecuteNonQueryAsync();
                }
            }
                
             return linhasAfetadas != 0;
        } 

        public async Task<bool> Delete(int id)
        {

            int linhasAfetadas = 0;

            using (this.conn) { 
            
                await this.conn.OpenAsync();

                using (this.cmd)
                {
                    cmd.CommandText = "delete paciente where codigo = @id;";
                    cmd.Parameters.Add(new SqlParameter("@id", System.Data.SqlDbType.Int)).Value = id;

                    linhasAfetadas = (int) await cmd.ExecuteNonQueryAsync();
                }
            }

            return linhasAfetadas == 1;
        }

    }
}