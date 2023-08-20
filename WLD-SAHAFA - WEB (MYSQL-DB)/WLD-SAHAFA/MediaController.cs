using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;

namespace WLD_SAHAFA
{
    public class MediaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetMediaFiles()
        {
            var imageFiles = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/photos")).Select(Path.GetFileName);
            var videoFiles = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/videos")).Select(Path.GetFileName);

            return Json(new { images = imageFiles, videos = videoFiles });
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}