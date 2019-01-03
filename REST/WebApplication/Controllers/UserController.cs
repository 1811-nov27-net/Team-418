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
    public class UserController : ControllerBase
    {
        public IMusicRepo Repo { get; }

        public UserController(IMusicRepo repo)
        {
            Repo = repo;
        }

        public static List<UserModel> Data = new List<UserModel>
        {
            new UserModel
            {
                Id = 1,
                Name = "Jane Doe",
                Admin = false
            },
            new UserModel
            {
                Id = 2,
                Name = "John Doe",
                Admin = true
            }
        };

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            List<UserModel> dispUsers = null;
            // need a get all users method in Repo
            try
            {
                dispUsers = Repo.GetAllUsers().Result.Select(x => new UserModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Admin = x.Admin
                }).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            return dispUsers;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public ActionResult<UserModel> Get(UserModel user)
        {
            UserModel dispUser = null;
            // need a get all users method in Repo
            try
            {
                Library.Users getUser = Repo.GetUserByName(user.Name).Result;
                dispUser = new UserModel
                {
                    Id = getUser.Id,
                    Name = getUser.Name,
                    Admin = getUser.Admin
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

            return dispUser;
        }
       

        // POST: api/User
        [HttpPost]
        public async Task<string> Post([FromBody] UserModel user)
        {
            try
            {
                Library.Users newUser = new Library.Users
                {
                    Name = user.Name,
                    Admin = user.Admin
                };

                string addMe = await Repo.AddUser(newUser);

                if (!bool.Parse(addMe))
                {
                    return addMe;
                }

                return "User added!";
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex).ToString();
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<string> Delete( UserModel user)
        {
            try
            {
                Library.Users getUser = await Repo.GetUserByName(user.Name);

                string deleteMe = await Repo.RemoveUser(getUser.Id);

                if (!bool.Parse(deleteMe))
                {
                    return deleteMe;
                }

                return "User deleted!";
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex).ToString();
            }
        }
    }
}
