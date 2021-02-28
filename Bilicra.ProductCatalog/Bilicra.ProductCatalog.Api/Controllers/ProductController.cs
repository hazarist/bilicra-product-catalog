using Bilicra.ProductCatalog.Business.Interfaces;
using Bilicra.ProductCatalog.Common.Models.ProductModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Bilicra.ProductCatalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var serviceRepsonse = await productService.GetByIdAsync(id);
            return Ok(serviceRepsonse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string name = "")
        {
            var serviceRepsonse = await productService.GetAllAsync(name);
            return Ok(serviceRepsonse);
        }

        [HttpGet("excel")]
        public async Task<IActionResult> GetAsExcel()
        {
            IWorkbook workbook = await productService.ExportExcelAsync();
            MemoryStream tempStream = null;
            MemoryStream stream = null;
            try
            {
                tempStream = new MemoryStream();
                workbook.Write(tempStream);
                var byteArray = tempStream.ToArray();
                stream = new MemoryStream();
                stream.Write(byteArray, 0, byteArray.Length);
                stream.Seek(0, SeekOrigin.Begin);
                var contentType = workbook.GetType() == typeof(XSSFWorkbook) ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" : "application/vnd.ms-excel";
                return File(
                    fileContents: stream.ToArray(),
                    contentType: contentType,
                    fileDownloadName: "products.xlsx");
            }
            finally
            {
                if (tempStream != null) tempStream.Dispose();
                if (stream != null) stream.Dispose();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductPostModel product)
        {
            var serviceRepsonse = await productService.CreateAsync(product);
            return Ok(serviceRepsonse);
        }

        [HttpPost("{id}/upload-photo")]
        public async Task<IActionResult> UploadPhoto([FromForm][Bind("Photo")] ProductPostPhotoModel photo, Guid id)
        {
            var serviceRepsonse = await productService.UploadPhotoAsync(id, photo);
            return Ok(serviceRepsonse);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductPutModel product)
        {
            var serviceRepsonse = await productService.UpdateAsync(product);
            return Ok(serviceRepsonse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var serviceRepsonse = await productService.DeleteAsync(id);
            return Ok(serviceRepsonse);
        }

        [HttpPatch("{id}/confirm")]
        public async Task<IActionResult> PriceConfirm(Guid id)
        {
            var serviceRepsonse = await productService.PriceConfirmPatchAsync(id);
            return Ok(serviceRepsonse);
        }
    }
}
