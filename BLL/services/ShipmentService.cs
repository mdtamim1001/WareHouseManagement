using System;
using System.Collections.Generic;
using System.Linq;
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
            var obj = mapper.Map<Shipment>(dto);
            return DAL.DataAccessFactory.ShipmentData().Create(obj);
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
