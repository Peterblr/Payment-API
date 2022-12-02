using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment.API.Data;
using Payment.API.Models;
using Payment.API.Repository;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly IPaymentRepository repository;

        public PaymentDetailsController(IPaymentRepository repository)
        {
            this.repository = repository;
        }

        //// POST: api/PaymentDetails
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("CreatePayment")]
        //public async Task<ActionResult<PaymentDetail>> CreatePayment(PaymentDetail paymentDetail)
        //{
        //    await repository.CreatePaymentAsync(paymentDetail);

        //    return CreatedAtAction("GetAllPayments", new { id = paymentDetail.PaymentDetailId }, paymentDetail);
        //}

        //// GET: api/PaymentDetails
        //[HttpGet("GetAllPayments")]
        //public async Task<IEnumerable<PaymentDetail>> GetAllPayments()
        //{
        //    return await repository.GetAllPaymentsAsync();
        //}

        //// GET: api/PaymentDetails/5
        //[HttpGet("GetPayment/{id}")]
        //public async Task<ActionResult<PaymentDetail>> GetPayment(int id)
        //{
        //    var paymentDetail = await repository.GetPaymentAsync(id);

        //    if (paymentDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    return paymentDetail;
        //}

        //// PUT: api/PaymentDetails/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("UpdatePayment/{id}")]
        //public async Task<IActionResult> UpdatePayment(int id, PaymentDetail paymentDetail)
        //{
        //    if (id != paymentDetail.PaymentDetailId)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        await repository.UpdatePaymentAsync(paymentDetail);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        //// DELETE: api/PaymentDetails/5
        //[HttpDelete("DeletePayment/{id}")]
        //public async Task<IActionResult> DeletePayment(int id)
        //{
        //    var paymentDetail = await repository.GetPaymentAsync(id);
        //    if (paymentDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    await repository.DeletePaymentAsync(id);

        //    return NoContent();
        //}



        ///////////////*****************////////////////////


        // GET: api/PaymentDetails/5
        [HttpGet("GetPayment/{id}")]
        public  ActionResult<PaymentDetail> GetPayment(int id)
        {
            var paymentDetail = repository.GetPayment(id);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            if (id != paymentDetail.PaymentDetailId)
            {
                return NotFound();
            }

            return paymentDetail;
        }

        // GET: api/PaymentDetails
        [HttpGet("GetAllPayments")]
        public IEnumerable<PaymentDetail> GetAllPayments()
        {
            return repository.GetAllPayments();
        }

        // POST: api/PaymentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreatePayment")]
        public ActionResult<PaymentDetail> CreatePayment(PaymentDetail paymentDetail)
        {
            repository.CreatePayment(paymentDetail);

            return CreatedAtAction("GetAllPayments", new { id = paymentDetail.PaymentDetailId }, paymentDetail);
        }

        // PUT: api/PaymentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdatePayment/{id}")]
        public IActionResult UpdatePayment(int id, PaymentDetail paymentDetail)
        {
            if (id != paymentDetail.PaymentDetailId)
            {
                return BadRequest();
            }

            try
            {
                repository.UpdatePayment(paymentDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/PaymentDetails/5
        [HttpDelete("DeletePayment/{id}")]
        public IActionResult DeletePayment(int id)
        {
            var paymentDetail = repository.GetPayment(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            if (id != paymentDetail.PaymentDetailId)
            {
                return NotFound();
            }

            repository.DeletePayment(id);

            return NoContent();
        }
    }
}
