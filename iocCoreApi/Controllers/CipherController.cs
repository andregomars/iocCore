using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using iocCoreApi.Services;

namespace iocCoreApi.Controllers
{
    public class CipherController : ApiController
    {
        [Route("api/Cipher/EncryptedUserID/{userID}")]
        public IHttpActionResult GetEncryptedUserID(int userID)
        {
            string cipher = Utility.EncryptID(userID);
            return Ok(cipher);
        }

        [Route("api/Cipher/DecryptedUserID/{cipher}")]
        public IHttpActionResult GetDecryptedUserID(string cipher)
        {
            string userID = "";
            try
            {
                userID = Utility.DecryptID(cipher);
            }
            catch
            {
                userID = "-1";
            }

            return Ok(userID);
        }
    }
}
