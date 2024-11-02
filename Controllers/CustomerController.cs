using BussinessLayer.AbstractClass;
using BussinessLayer;
using BussinessLayer.BussinessLogic.Interface;
using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using CodeTech.Model;

namespace CodeTech.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]             
    public class CustomerController : Controller
    {
        private readonly Customer_in _bhj_cus;
        private readonly ApplicationDbContext context;

        public CustomerController(Customer_in bhj_cus, ApplicationDbContext context)
        {
            _bhj_cus = bhj_cus;
            this.context = context;
        }

        ResultMsg rslt = new ResultMsg();
        bool status;
        string msg;

        //Customer Registration
        [HttpPost("register")]
        public ActionResult CustomerRegistration([FromBody] Customer_ab customer_ab)
        {
            try
            {
                var jj = _bhj_cus.customerRegistration(customer_ab);
                if (jj == "Success")
                {
                    msg = rslt.s_msg;
                    status = rslt.s_status;
                }
                else if (jj == "Failed")
                {
                    msg = rslt.err_msg;
                    status = rslt.s_status;
                }
                else
                {
                    msg = jj;
                    status = rslt.err_status;
                }
            }
            catch (Exception ex)
            {
                msg = rslt.err_ct;
            }

            return Ok(new { status, message = msg });
        }
        // delete customer
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
        // view all the details
        [HttpGet("getAll")]
        public List<CustomerTbl> getAll() 
        {
            List<CustomerTbl> jj = new List<CustomerTbl>();
            try
            {

                jj = _bhj_cus.getAll();

            }
            catch (Exception ex)
            {
                var err = ex;
            }

            return jj;

        }
        //get details by a customer
        [HttpGet("get_by_id/{id}")]
        public IActionResult get_by_id(int id)
        {
            var jj = _bhj_cus.get_by_id(id);
            return Ok(jj);


        }
        //Update PIN
        [HttpPut("UpdateKey")]
        public IActionResult UpdateKey([FromBody] Customer_ab customer_ab)  
        {

            try
            {

                var jj = _bhj_cus.UpdateKey(customer_ab);
                if (jj == "Success")
                {
                    msg = rslt.update_msg;
                    status = rslt.s_status;

                }
                else if (jj == "Failed")
                {
                    msg = rslt.err_msg;
                    status = rslt.err_status;
                    return NotFound();
                }
                else
                {
                    msg = jj;
                    status = rslt.err_status;
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                var err_msg = ex;
                msg = rslt.err_ct;
                return NotFound();
            }


            return Ok(new { status = status, message = msg });

        }
    }
}
