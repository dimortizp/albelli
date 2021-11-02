using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using Core.UseCases;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Api.Models;
using AutoMapper;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IRequestHandler<IEnumerable<ProductType>> _getAllProductTypesHandler;
        private readonly IRequestHandler<string, ProductType> _getProductTypeHandler;
        private readonly IMapper _mapper;

        public ProductTypeController(
            IRequestHandler<IEnumerable<ProductType>> getAllProductTypesHandler,
            IRequestHandler<string, ProductType> getProductTypeHandler, 
            IMapper mapper)
        {
            _getAllProductTypesHandler = getAllProductTypesHandler ?? throw new ArgumentNullException(nameof(getAllProductTypesHandler));
            _getProductTypeHandler = getProductTypeHandler ?? throw new ArgumentNullException(nameof(getProductTypeHandler));
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductTypeResponse>))]
        public async Task<IActionResult> Get()
        {
            var productTypes = (await _getAllProductTypesHandler.HandleAsync())
                .Select(x => _mapper.Map<ProductTypeResponse>(x))
                .ToList();

            if (productTypes.Count == 0)
            {
                return NoContent();
            }

            return Ok(productTypes);
        }
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductTypeResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var productType = await _getProductTypeHandler.HandleAsync(name);

                if (productType == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ProductTypeResponse>(productType));
            }catch(ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
