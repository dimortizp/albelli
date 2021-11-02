using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.UseCases;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IRequestHandler<IEnumerable<ProductType>> _getProductTypeHandler;

        public ProductTypeController(IRequestHandler<IEnumerable<ProductType>> getProductTypeHandler)
        {
            _getProductTypeHandler = getProductTypeHandler ?? throw new ArgumentNullException(nameof(getProductTypeHandler));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductType>))]
        public async Task<IActionResult> Get()
        {
            return Ok(await _getProductTypeHandler.HandleAsync());
        }
    }
}
