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
    public class PlayerController : ControllerBase
    {
        // GET: api/Player
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<ExpandoObject> Get()
        {
            Database db = new Database();
            db.Open();
            string query = "select * from player";
            List<ExpandoObject> players = db.Select(query);
            db.Close();

            return players;
        }

        // GET: api/Player/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "Get")]
        public List<ExpandoObject> Get(int id)
        {
            Database db = new Database();
            db.Open();
            string query = "select * from player where id = @id";
            var values = new Dictionary<string, object>()
                {
                    {"@id", id},
                };
            List<ExpandoObject> players = db.Select(query, values);
            db.Close();

            return players;
        }

        // POST: api/Player
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Player/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Player/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
