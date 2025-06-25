using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTOs;
using BLL.Services;
using Newtonsoft.Json.Linq;

namespace WHM.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("api/products")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = ProductService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpGet]
        [Route("api/products/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = ProductService.Get(id);
                if (data == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpPost]
        [Route("api/products")]
        public HttpResponseMessage Create(ProductToSectionDto dto)
        {
            try
            {
                var result = ProductService.Create(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.Created, new { Message = "Product created." });

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Creation failed." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpPut]
        [Route("api/products/{id}")]
        public HttpResponseMessage Update(int id, ProductDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "ID mismatch." });

                var result = ProductService.Update(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Product updated." });

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Update failed." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/products/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = ProductService.Delete(id);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Product deleted." });

                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Product not found." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpPatch]
        [Route("api/products/{id}")]
        public HttpResponseMessage Patch(int id, JObject patchData)
        {
            try
            {
                var existing = ProductService.Get(id);
                if (existing == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Product not found." });

                // Manually apply only provided fields
                if (patchData["ProductName"] != null)
                    existing.ProductName = patchData["ProductName"].ToString();

                if (patchData["SKU"] != null)
                    existing.SKU = patchData["SKU"].ToString();

                if (patchData["Quantity"] != null)
                    existing.Quantity = (int)patchData["Quantity"];

                if (patchData["ImportDate"] != null)
                    existing.ImportDate = (DateTime)patchData["ImportDate"];

                if (patchData["ExpireDate"] != null)
                    existing.ExpireDate = (DateTime)patchData["ExpireDate"];


                var result = ProductService.Update(existing);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Product patched." });

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Patch failed." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpPost]
        [Route("api/products/expiring")]
        public HttpResponseMessage GetExpiringSoon([FromBody] ExpiryRequestDTO dto)
        {
            try
            {
                var data = ProductService.GetExpiringSoon(dto.Days);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/products/release")]
        public HttpResponseMessage ReleaseProduct([FromBody] ProductReleaseDTO dto)
        {
            try
            {
                var result = ProductService.ReleaseProductFromSection(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Product released." });

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Release failed." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }







    }
}
