using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        public IMusicRepo Repo { get; }

        public RequestController(IMusicRepo repo)
        {
            Repo = repo;
        }

        // GET: api/Request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestModel>>> Get()
        {
            List<RequestModel> dispRequests = new List<RequestModel>();

            try
            {
                IEnumerable<Library.PendingRequests> requests = await Repo.GetAllRequests();

                foreach (var item in requests)
                {
                    RequestModel request = new RequestModel
                    {
                        Id = item.Id,
                        Artistid = item.Artistid,
                        Artistname = item.Artistname,
                        Albumid = item.Albumid,
                        Albumname = item.Albumname,
                        Songname = item.Songname
                    };

                    dispRequests.Add(request);
                }

                return dispRequests;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Request/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<RequestModel>> Get(int id)
        {
            try
            {
                Library.PendingRequests request = await Repo.GetRequestById(id);

                return new RequestModel
                {
                    Id = request.Id,
                    Artistid = request.Artistid,
                    Artistname = request.Artistname,
                    Albumid = request.Albumid,
                    Albumname = request.Albumname,
                    Songname = request.Songname
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST: api/Request
        [HttpPost]
        public async Task<string> Post([FromBody] Library.PendingRequests value)
        {
            try
            {
                string addMe = await Repo.AddRequest(value);

                if (!bool.Parse(addMe))
                {
                    return addMe;
                }

                return "Request added!";

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex).ToString();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            try
            {
                string removeMe = await Repo.RemoveRequest(id);

                if (!bool.Parse(removeMe))
                {
                    return removeMe;
                }

                return "Request deleted!";
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex).ToString();
            }
        }
    }
}
