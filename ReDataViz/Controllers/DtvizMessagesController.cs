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
        // Check the old code below for basic references

        readonly ApiContext _context;

        // Build a constructor to get the API reference 

        public DtvizMessagesController(ApiContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public IEnumerable<Models.DtvizMessage> Get()
        {
            return _context.DtvizMessages;
        }

        [HttpGet("{name}")]
        public IEnumerable<Models.DtvizMessage> Get(string name)
        {
            return _context.DtvizMessages.Where(dtmessage => dtmessage.Owner.Equals(name));
        }

        [HttpGet("{postid}")]
        public IEnumerable<Models.DtvizMessage> GetPostById(string id)
        {
            return _context.DtvizMessages.Where(dtmessage => dtmessage.Id.Equals(id));
        }

        [HttpPost]
        public Models.DtvizMessage Post([FromBody] Models.DtvizMessage dtm)
        {
            var dbMessage = _context.DtvizMessages.Add(dtm).Entity;
            _context.SaveChanges();
            return dbMessage;
        }

        [HttpPut("{id}")]
        public ActionResult UpdateDtvizMessage([FromRoute] string id, [FromBody] Models.DtvizMessage dtm)
        {
            var editDtm=_context.Entry(dtm).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(editDtm);
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveDtvizMessage([FromRoute] string id)
        {
            var removeDtm = _context.DtvizMessages.SingleOrDefault(dtm => dtm.Id.Equals(id));
            _context.DtvizMessages.Remove(removeDtm);
            _context.SaveChanges();
            return Ok(removeDtm);
        }
    }
}




//static List<Models.DtvizMessage> dtms = new List<Models.DtvizMessage>
//            {
//                new Models.DtvizMessage
//                {
//                    Owner = "John",
//                    Text = "What a sunny day"
//                },
//                new Models.DtvizMessage
//                {
//                    Owner = "Jane",
//                    Text = "Indeed it is"
//                }
//            };

//[HttpGet]
//public IEnumerable<Models.DtvizMessage> Get()
//{
//    return dtms;
//}

//[HttpGet("{name}")]
//public IEnumerable<Models.DtvizMessage> Get(string name)
//{
//    return dtms.FindAll(dtmessage => dtmessage.Owner.Equals(name));
//}

//[HttpPost]
//public Models.DtvizMessage Post([FromBody] Models.DtvizMessage dtm)
//{
//    dtms.Add(dtm);
//    return dtm;
//}