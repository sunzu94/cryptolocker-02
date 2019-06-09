using System;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using PasswordService.Models;

namespace PasswordService.Controllers
{
    public class PasswordsController : ApiController
    {
        // GET: api/Passwords
        public IQueryable<Password> GetPasswords()
        {
            return Dal.GetPasswords().AsQueryable();
        }

        // GET: api/passwords/privatekey/XX-XX-osv
        [Route("api/passwords/privatekey/{userInfo}")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetPrivateKey(string userInfo)
        {
            var match = Dal.GetPasswords().FirstOrDefault(p => p.UserInfo == userInfo);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match.PrivateKey);
        }

        // GET: api/passwords/decrypt/ytergh63478ghj8
        [Route("api/passwords/decrypt/{id}")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetDecryptionKey(string id)
        {
            var match = Dal.GetPassword(id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match.PrivateKey);
        }

        // GET: api/passwords/generate/id
        // {MachineName}:{UserName}:{private key}:{public key}
        [HttpPost]
        [Route("api/passwords/save")]
        [ResponseType(typeof(Password))]
        public IHttpActionResult PostPassword(Password password)
        {
            password.Id = CreatePassword(15);
            password.TimeStamp = DateTime.UtcNow;
            
            var match = Dal.GetPasswords().FirstOrDefault(p => p.UserInfo == password.UserInfo);
            if (match != null)
            {
                return Ok(match);
            }

            Dal.InsertPassword(password);

            return Ok(password);
        }

        // GET: api/Passwords/dfxshdshtrsdhrtsdhx
        [ResponseType(typeof(Password))]
        public IHttpActionResult GetPassword(string id)
        {
            var match = Dal.GetPassword(id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }
        
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}