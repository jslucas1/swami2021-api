using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using Microsoft.AspNetCore.Cors;

namespace swami2021.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        // GET: api/Team
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<ExpandoObject> Get()
        {
            Database db = new Database();
            db.Open();
            string query = "select * from team";
            List<ExpandoObject> teams = db.Select(query);
            db.Close();

            return teams;
        }

        // GET: api/Team/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetTeams")]
        public List<ExpandoObject> Get(int id)
        {
            Database db = new Database();
            db.Open();
            string query = "select * from team where id = @id";
            var values = new Dictionary<string, object>()
                {
                    {"@id", id},
                };
            List<ExpandoObject> teams = db.Select(query, values);
            db.Close();

            return teams;
        }

        // POST: api/Team
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Team/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Team/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
