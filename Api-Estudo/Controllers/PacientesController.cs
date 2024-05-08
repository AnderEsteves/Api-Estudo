using Api_Estudo.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace Api_Estudo.Controllers
{
    public class PacientesController : ApiController
    {

        private readonly Repositories.SQL.Pacientes repoPaciente;


        public PacientesController() {

            repoPaciente = new Repositories.SQL.Pacientes(Base.GetConnectionString());
        }




        // GET: api/Pacientes
        public async Task<IHttpActionResult> Get()
        {
            return Ok( await repoPaciente.Get());
        }


        // GET: api/Pacientes/5
        public async Task<IHttpActionResult> Get(int id)
        {
            Models.Paciente paciente = await repoPaciente.Get(id);

            if (paciente is null)
                return NotFound();

            return Ok(paciente);
        }



        public async Task<IHttpActionResult> Get(string nome)
        {
            List<Models.Paciente> pacientes = await repoPaciente.Get(nome);

            if (pacientes is null)
                return NotFound();


            return Ok(pacientes);
        }



        // POST: api/Pacientes
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/Pacientes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pacientes/5
        public void Delete(int id)
        {
        }
    }
}
