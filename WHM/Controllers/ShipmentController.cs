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
    public class ShipmentController : ApiController
    {
        [HttpGet]
        [Route("api/shipments")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = ShipmentService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpGet]
        [Route("api/shipments/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = ShipmentService.Get(id);
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
        [Route("api/shipments")]
        public HttpResponseMessage Create(ShipmentDTO dto)
        {
            try
            {
                var result = ShipmentService.Create(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.Created, new { Message = "Shipment created." });

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Creation failed." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpPut]
        [Route("api/shipments/{id}")]
        public HttpResponseMessage Update(int id, ShipmentDTO dto)
        {
            try
            {
                if (id != dto.Id)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "ID mismatch." });

                var result = ShipmentService.Update(dto);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Shipment updated." });

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Update failed." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/shipments/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = ShipmentService.Delete(id);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Shipment deleted." });

                return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Shipment not found." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }

        [HttpPatch]
        [Route("api/shipments/{id}")]
        public HttpResponseMessage Patch(int id, JObject patchData)
        {
            try
            {
                var existing = ShipmentService.Get(id);
                if (existing == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Shipment not found." });

                if (patchData["Name"] != null)
                    existing.Name = patchData["Name"].ToString();

                if (patchData["Destination"] != null)
                    existing.Destination = patchData["Destination"].ToString();

                if (patchData["Direction"] != null)
                    existing.Direction = patchData["Direction"].ToString();

                if (patchData["Quantity"] != null)
                    existing.Quantity = (int)patchData["Quantity"];

                if (patchData["ReleaseDate"] != null)
                    existing.ReleaseDate = (DateTime)patchData["ReleaseDate"];

                if (patchData["ReachDate"] != null)
                    existing.ReachDate = (DateTime)patchData["ReachDate"];

                if (patchData["Status"] != null)
                    existing.Status = patchData["Status"].ToString();

                var result = ShipmentService.Update(existing);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Shipment patched." });

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Patch failed." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }
    }
}
