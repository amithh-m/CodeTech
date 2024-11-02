using BussinessLayer.BussinessLogic.Interface;
using CodeTech.Model;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace CodeTech.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]              
    public class LoginController : Controller
    {
        private readonly Login_in _bhj_log;
        private readonly ApplicationDbContext context;

        public LoginController(Login_in bhj_log, ApplicationDbContext context)
        {
            _bhj_log = bhj_log;
            this.context = context;
        }

        ResultMsg rslt = new ResultMsg();
        bool status;
        string msg;

        //Generate OTP (Phone/Email)
        [HttpGet("generateOtp")]
        public ActionResult GenerateOTP()
        {
            var otp = _bhj_log.GenerateOtp();
            HttpContext.Session.SetInt32("OTP", otp);
            HttpContext.Session.SetString("OTPGenerateDate", DateTime.UtcNow.ToString());
            return Ok(new { otp });
        }
        //Verify Email(120second)
        [HttpPost("verifyOtp")]
        public ActionResult VerifyOTP(int enteredOtp)
        {
            var sOtp = HttpContext.Session.GetInt32("OTP");
            var date = HttpContext.Session.GetString("OTPGenerateDate");

            DateTime? generateDate = null;
            if (date != null)
            {
                generateDate = DateTime.Parse(date); 
            }

            bool isValid = _bhj_log.VerifyOtp(enteredOtp, sOtp, generateDate); 

            if (!isValid)
            {
                return BadRequest("Invalid OTP or OTP has expired.");
            }

            return Ok("OTP verified successfully.");
        }
        //Login
        [HttpGet]
        [Route("Login/getLogin/{IC}/{password}")]
        public ActionResult getLogin(int IC, string password)
        {
            var jj = _bhj_log.getLogin(IC, password);
            return Ok(jj);


        }

    }
}

