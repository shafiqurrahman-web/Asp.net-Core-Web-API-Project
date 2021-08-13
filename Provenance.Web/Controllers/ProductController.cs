using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Provenance.Common.Configurations;
using Provenance.ServiceLayer.Contracts;
using Provenance.ServiceLayer.DTOs.Product;
using Provenance.Web.Modules;
using System;

namespace Provenance.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ILogger<ProductController> _logger;
		private readonly IProductService _productService;
		private readonly IProductHistoryService _productHistoryService;

		public ProductController (
			ILogger<ProductController> logger, 
			IProductService productService,
			IProductHistoryService productHistoryService)
		{
			_logger = logger;
			_productService = productService;
			_productHistoryService = productHistoryService;
		}


		/// <summary>
		/// add a new product
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpPost]
		public IActionResult Post (AddProductDTO inputData)
		{
			return _productService.Add(inputData).ToHttpResponse();
		}
		
		//[Authorize(Policy = Policies.ACTIVE)]
		[HttpGet("{id}")]
		public IActionResult Get (Guid id)
		{
			return _productService.Get(id).ToHttpResponse();
		}
		
		//[Authorize(Policy = Policies.ACTIVE)]
		[HttpGet("{id}/ProductHistory")]
		public IActionResult GetProductHistoryByProductId (Guid id)
		{
			return _productHistoryService.GetProductHistoryByProductId(id).ToHttpResponse();
		}

		/// <summary>
		/// update an exist product
		/// </summary>
		/// <param name="id"></param>
		/// <param name="inputData"></param>
		/// <returns></returns>
		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpPut("{id}")]
		public IActionResult Put (Guid id, UpdateProductDTO inputData)
		{
			return _productService.Update(id, inputData).ToHttpResponse();
		}
		
		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpDelete("{id}")]
		public IActionResult Delete (Guid id)
		{
			return _productService.Delete(id).ToHttpResponse();
		}
		
		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpGet("all")]
		public IActionResult GetAll ()
		{
			return _productService.GetAll().ToHttpResponse();
		}
		
		//[Authorize(Roles = RoleTypes.ADMIN)]
		[HttpGet]
		public IActionResult Get (int page = 1, int pageSize = 10)
		{
			return _productService.GetAll(page, pageSize).ToHttpResponse();
		}


	}
}
