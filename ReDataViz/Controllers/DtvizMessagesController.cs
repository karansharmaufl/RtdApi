using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReDataViz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DtvizMessagesController : ControllerBase
    {
        static List<Models.DtvizMessage> dtms = new List<Models.DtvizMessage>
            {
                new Models.DtvizMessage
                {
                    Owner = "John Doe",
                    Text = "What a sunny day"
                },
                new Models.DtvizMessage
                {
                    Owner = "Jane Doe",
                    Text = "Indeed it is"
                }
            };


        public IEnumerable<Models.DtvizMessage> Get()
        {
            return dtms;
        }

        [HttpPost]
        public Models.DtvizMessage Post([FromBody] Models.DtvizMessage dtm)
        {
            dtms.Add(dtm);
            return dtm;
        }
    }
}