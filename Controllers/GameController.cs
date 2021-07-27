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
    public class GameController : ControllerBase
    {
        // GET: api/Game
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<ExpandoObject> Get()
        {
            Database db = new Database();
            db.Open();
            string query = "select a.id, a.api_id, a.season, a.week, a.season_type, ";
            query += "a.start_date, a.neutral_site, a.home_id, b.school as home_team, ";
            query += "a.home_points, a.away_id, c.school as away_team, a.away_points, ";
            query += "a.spread, a.formatted_spread ";
            query += "from game a, team b, team c ";
            query += "where a.home_id = b.api_id and a.away_id = c.api_id";
            List<ExpandoObject> games = db.Select(query);
            db.Close();

            return games;
        }

        // GET: api/Game/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetGame")]
        public List<ExpandoObject> Get(int id)
        {
            Database db = new Database();
            db.Open();
            string query = "select a.id, a.api_id, a.season, a.week, a.season_type, ";
            query += "a.start_date, a.neutral_site, a.home_id, b.school as home_team, ";
            query += "a.home_points, a.away_id, c.school as away_team, a.away_points, ";
            query += "a.spread, a.formatted_spread ";
            query += "from game a, team b, team c ";
            query += "where a.home_id = b.api_id and a.away_id = c.api_id and a.id = @id";

            var values = new Dictionary<string, object>()
                {
                    {"@id", id},
                };
            List<ExpandoObject> games = db.Select(query, values);

            db.Close();

            return games;
        }

        // POST: api/Game
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Game/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Game/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
