using Api_Estudo.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult Get()
        {
            return Ok(repoPaciente.Get());
        }




        // GET: api/Pacientes/5
        public string Get(int id)
        {
            return "value";
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
