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
    public class WagerController : ControllerBase
    {
        // GET: api/Wager
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<ExpandoObject> Get()
        {
            Database db = new Database();
            db.Open();
            string query = "select a.id, a.season, a.week, a.season_type, ";
            query += "a.start_date, a.neutral_site, a.home_id, b.school as home_team, ";
            query += "a.home_points, a.away_id, c.school as away_team, a.away_points, ";
            query += "a.spread, a.formatted_spread, d.home_wager, d.away_wager, a.playable ";
            query += "from game a ";
            query += "LEFT JOIN team b on a.home_id = b.api_id ";
            query += "LEFT JOIN team c on a.away_id = c.api_id ";
            query += "LEFT JOIN wager d on a.api_id = d.game_api_id ";
            query += "where a.week = 1";
            List<ExpandoObject> wagers = db.Select(query);
            db.Close();

            return wagers;
        }

        // GET: api/Wager/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetWager")]
        public List<ExpandoObject> Get(string id)
        {
            Console.WriteLine("made it to the get with " + id);
            Database db = new Database();
            db.Open();
            string query = "select a.id, a.season, a.week, a.season_type, ";
            query += "a.start_date, a.neutral_site, a.home_id, b.school as home_team, ";
            query += "a.home_points, a.away_id, c.school as away_team, a.away_points, ";
            query += "a.spread, a.formatted_spread, d.home_wager, d.away_wager, a.playable ";
            query += "from game a ";
            query += "LEFT JOIN team b on a.home_id = b.api_id ";
            query += "LEFT JOIN team c on a.away_id = c.api_id ";
            query += "LEFT JOIN wager d on a.api_id = d.game_api_id ";
            query += "where a.week = 1 and d.google_id = @id";
            var values = new Dictionary<string, object>()
                {
                    {"@id", id},
                };
            List<ExpandoObject> wagers = db.Select(query, values);
            db.Close();

            return wagers;
        }

        // POST: api/Wager
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Wager/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Wager/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
