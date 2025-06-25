using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Models;

namespace BLL.Services
{
    public class SectionService
    {
        public static List<SectionDTO> Get(string token)
        {
            if (!AuthService.IsTokenValid(token)) throw new Exception("Unauthorized Access");

            var data = DAL.DataAccessFactory.SectionData().Read();

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Section, SectionDTO>();
            });

            var mapper = new Mapper(cfg);
            return mapper.Map<List<SectionDTO>>(data);
        }


        public static SectionDTO Get(int id)
        {
            var data = DAL.DataAccessFactory.SectionData().Read(id);

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Section, SectionDTO>();
            });

            var mapper = new Mapper(cfg);
            return mapper.Map<SectionDTO>(data);
        }

        public static bool Create(SectionDTO dto)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<SectionDTO, Section>();
            });

            var mapper = new Mapper(cfg);
            var obj = mapper.Map<Section>(dto);
            return DAL.DataAccessFactory.SectionData().Create(obj);
        }

        public static bool Update(SectionDTO dto)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<SectionDTO, Section>();
            });

            var mapper = new Mapper(cfg);
            var obj = mapper.Map<Section>(dto);
            return DAL.DataAccessFactory.SectionData().Update(obj);
        }

        public static bool Delete(int id)
        {
            return DAL.DataAccessFactory.SectionData().Delete(id);
        }

        public static List<SectionProductReportDTO> GetSectionWiseProductReport()
        {
            var sectionProducts = DAL.DataAccessFactory.SectionProductData().Read();
            var sections = DAL.DataAccessFactory.SectionData().Read();

            var report = sections.Select(section => new SectionProductReportDTO
            {
                SectionName = section.Name,
                Products = sectionProducts
                            .Where(sp => sp.SectionID == section.Id)
                            .Select(sp => new ProductInSectionDTO
                            {
                                ProductName = sp.Product.ProductName,
                                SKU = sp.Product.SKU,
                                Quantity = sp.Quantity

                            })
                            .ToList()
            })
            .Where(r => r.Products.Count > 0)
            .ToList();

            return report;
        }

        public static string GetSectionWiseReportAsText()
        {
            var report = GetSectionWiseProductReport();

            var sb = new StringBuilder();

            foreach (var section in report)
            {
                sb.AppendLine($"Section: {section.SectionName}");
                foreach (var product in section.Products)
                {
                    sb.AppendLine($"    Product : {product.ProductName}");
                    sb.AppendLine($"    Quantity: {product.Quantity}");
                    sb.AppendLine($"    SKU: {product.SKU}");
                }
                sb.AppendLine(); // blank line between sections
            }

            return sb.ToString();
        }





    }
}
