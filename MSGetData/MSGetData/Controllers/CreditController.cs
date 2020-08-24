using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSGetData.Services;

namespace MSGetData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreditController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        public CreditController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Credit.ListAll().ToList());
        }
    }
}