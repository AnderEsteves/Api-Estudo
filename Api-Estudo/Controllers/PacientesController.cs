using Api_Estudo.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Management;
using System.Web.UI;


namespace Api_Estudo.Controllers
{
    public class PacientesController : ApiController
    {

        private readonly Repositories.SQL.Pacientes repoPaciente;

       


        public PacientesController() {

            repoPaciente = new Repositories.SQL.Pacientes(Base.GetConnectionString());
        }


        private void Log(Exception ex)
        {
            using (StreamWriter arq = new StreamWriter("C:\\Users\\Álvaro\\Desktop\\log.txt", true))
            {
                arq.WriteLine($"data:\t{DateTime.Now}");
                arq.WriteLine($"mensagem:\t{ex.Message}");
                arq.WriteLine($"stack:\t{ex.StackTrace}\n\n\n");
            }
        }


       // GET: api/Pacientes
       public async Task<IHttpActionResult> Get()
        { 
           return Ok( await repoPaciente.Get());
        }


        // GET: api/Pacientes/5
        public async Task<IHttpActionResult> Get(int id)
        {

            try
            {
                Models.Paciente paciente = await repoPaciente.Get(id);

                if (paciente is null)
                    return NotFound();

                return Ok(paciente);

            }
            catch (Exception ex)
            {

                Log(ex);

                return InternalServerError();

            }

        }



        public async Task<IHttpActionResult> Get(string nome)
        {
            return Ok(await repoPaciente.Get(nome));
        }





        // POST: api/Pacientes
        public async Task<IHttpActionResult> Post([FromBody] Models.Paciente paciente)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await repoPaciente.Post(paciente))
                return InternalServerError();

            return Ok();
        }





        // PUT: api/Pacientes/5
        public async Task<IHttpActionResult> Put(int id, [FromBody] Models.Paciente paciente)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (id != paciente.Codigo)
                return BadRequest("id da requisição é diferente do id do body");


            bool repostaDoRetorno = await repoPaciente.Put(paciente);

           
            if(!repostaDoRetorno)
                return NotFound();


            return Ok(paciente);

        }




        // DELETE: api/Pacientes/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            bool retorno = await repoPaciente.Delete(id);


            if (!retorno)
                return NotFound();


            return Ok();

        }
    }
}
