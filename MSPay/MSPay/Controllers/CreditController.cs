using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSPay.Services;
using MSPay.ViewObject;

namespace MSPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CreditController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        public CreditController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Pay/")]
        public IActionResult Pay([FromBody] CreditRequest request)
        {

            var response = _unitOfWork.Credit.Pay(request.IdCredit, request.AmountTotal).FirstOrDefault();

            TransactionRequest transaction = new TransactionRequest();
            if (response == "0000")
            {
                transaction.Code = response;
                transaction.Description = "Operacion Exitosa";
            }
            else
            {
                transaction.Code = "1111";
                transaction.Description = "Error en la operacion";
            }

            return Ok(transaction);
        }
    }
}