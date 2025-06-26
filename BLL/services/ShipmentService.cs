using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using DAL.Models;

namespace BLL.Services
{
    public class ShipmentService
    {
        public static List<ShipmentDTO> Get()
        {
            var data = DAL.DataAccessFactory.ShipmentData().Read();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Shipment, ShipmentDTO>();
            });

            var mapper = new Mapper(cfg);
            return mapper.Map<List<ShipmentDTO>>(data);
        }

        public static ShipmentDTO Get(int id)
        {
            var data = DAL.DataAccessFactory.ShipmentData().Read(id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Shipment, ShipmentDTO>();
            });

            var mapper = new Mapper(cfg);
            return mapper.Map<ShipmentDTO>(data);
        }

        public static bool Create(ShipmentDTO dto)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<ShipmentDTO, Shipment>();
            });

            var mapper = new Mapper(cfg);
            var shipment = mapper.Map<Shipment>(dto);

            var created = DAL.DataAccessFactory.ShipmentData().Create(shipment);
            if (!created) return false;

            // Send email to fixed recipient
            try
            {
                var message = new MailMessage("tamimyousuf2001@gmail.com", "mdtamim26301@gmail.com");
                message.Subject = $"Shipment Created: {dto.Name}";
                message.Body = $"Your shipment '{dto.Name}' to '{dto.Destination}' has been created.\n\n" +
                               $"Direction: {dto.Direction}\n" +
                               $"Quantity: {dto.Quantity}\n" +
                               $"Release Date: {dto.ReleaseDate:yyyy-MM-dd}\n" +
                               $"Reach Date: {dto.ReachDate:yyyy-MM-dd}\n" +
                               $"Status: {dto.Status}";

                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("tamimyousuf2001@gmail.com", "iqvr frgb ocfc ubbe"),
                    EnableSsl = true
                };

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                // Log or handle the error if needed
                Console.WriteLine("Email sending failed: " + ex.Message);
            }

            return true;
        }





        public static bool Update(ShipmentDTO dto)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<ShipmentDTO, Shipment>();
            });

            var mapper = new Mapper(cfg);
            var obj = mapper.Map<Shipment>(dto);
            return DAL.DataAccessFactory.ShipmentData().Update(obj);
        }

        public static bool Delete(int id)
        {
            return DAL.DataAccessFactory.ShipmentData().Delete(id);
        }
    }
}
