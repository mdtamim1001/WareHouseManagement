using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using BLL.DTOs;
using BLL.Services;
using Newtonsoft.Json.Linq;

namespace WHM.Controllers
{
    public class SectionController : ApiController
    {
        [HttpGet]
        [Route("api/sections")]
        public HttpResponseMessage GetAll()
        {
            var token = Request.Headers.Authorization.ToString();
            try
            {

                var data = SectionService.Get(token);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpGet]
        [Route("api/sections/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = SectionService.Get(id);
                if (data == null) return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpPost]
        [Route("api/sections")]
        public HttpResponseMessage Create(SectionDTO dto)
        {
            try
            {
                var result = SectionService.Create(dto);
                if (result) return Request.CreateResponse(HttpStatusCode.Created, new { Message = "Section created." });
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Creation failed." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }


        [HttpPatch]
        [Route("api/sections/{id}")]
        public HttpResponseMessage Patch(int id, JObject patchData)
        {
            try
            {
                var existing = SectionService.Get(id);
                if (existing == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Section not found." });
                }

                // Manually apply each field from the patchData
                if (patchData["Name"] != null)
                    existing.Name = patchData["Name"].ToString();

                if (patchData["MaxQuantity"] != null)
                    existing.MaxQuantity = (int)patchData["MaxQuantity"];

                if (patchData["Quantity"] != null)
                    existing.Quantity = (int)patchData["Quantity"];

                var result = SectionService.Update(existing);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Section patched." });

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Patch failed." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }


        [HttpDelete]
        [Route("api/sections/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = SectionService.Delete(id);
                if (result) return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Section deleted." });
                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Section not found." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpGet]
        [Route("api/sections/report")]
        public HttpResponseMessage SectionWiseReport()
        {
            try
            {
                var data = SectionService.GetSectionWiseProductReport();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/sections/report/download")]
        public HttpResponseMessage DownloadReport()
        {
            try
            {
                var reportText = SectionService.GetSectionWiseReportAsText();

                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(reportText, Encoding.UTF8, "text/plain")
                };

                result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "SectionWiseProductReport.txt"
                };

                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
