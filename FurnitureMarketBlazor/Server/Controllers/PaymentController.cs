﻿using Microsoft.AspNetCore.Authorization;

namespace FurnitureMarketBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServiceServer _paymentService;

        public PaymentController(IPaymentServiceServer paymentService) => _paymentService = paymentService;


        [HttpPost("checkout"), Authorize]
        public async Task<ActionResult<string>> CreateCheckoutSession()
        {
            var session = await _paymentService.CreateCheckoutSession();
            return Ok(session.Url);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> FulfillOrder()
        {
            var response = await _paymentService.FulfillOrder(Request);
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
    }
}
