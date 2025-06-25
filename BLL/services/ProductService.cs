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
    public class ProductService
    {
        public static List<ProductDTO> Get()
        {
            var data = DAL.DataAccessFactory.ProductData().Read();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Product, ProductDTO>();
            });

            var mapper = new Mapper(cfg);
            return mapper.Map<List<ProductDTO>>(data);
        }

        public static ProductDTO Get(int id)
        {
            var data = DAL.DataAccessFactory.ProductData().Read(id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Product, ProductDTO>();
            });

            var mapper = new Mapper(cfg);
            return mapper.Map<ProductDTO>(data);
        }

        public static bool Create(ProductToSectionDto dto)
        {
            var section = DAL.DataAccessFactory.SectionData().Read(dto.SectionId);
            if (section == null) throw new Exception("Section not found.");

            if (section.Quantity + dto.Quantity > section.MaxQuantity)
            {
                throw new Exception("Cannot store product. Section is over capacity.");
            }

            var product = new Product
            {
                ProductName = dto.ProductName,
                SKU = dto.SKU,
                Quantity = dto.Quantity,
                ImportDate = dto.ImportDate,
                ExpireDate = dto.ExpireDate,
                Addedby = dto.Addedby
            };

            var success = DAL.DataAccessFactory.ProductData().Create(product);
            if (!success) return false;

        
            var createdProduct = DAL.DataAccessFactory.ProductData()
                                 .Read()
                                 .OrderByDescending(p => p.Id)
                                 .FirstOrDefault(p => p.SKU == dto.SKU);

            if (createdProduct == null) return false;

            var sp = new SectionProducts
            {
                ProductID = createdProduct.Id,
                SectionID = dto.SectionId,
                Quantity = dto.Quantity
            };

            DAL.DataAccessFactory.SectionProductData().Create(sp);

            section.Quantity += dto.Quantity;
            DAL.DataAccessFactory.SectionData().Update(section);

            return true;
        }

        public static bool Update(ProductDTO dto)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<ProductDTO, Product>();
            });

            var mapper = new Mapper(cfg);
            var obj = mapper.Map<Product>(dto);
            return DAL.DataAccessFactory.ProductData().Update(obj);
        }

        public static bool Delete(int id)
        {
            return DAL.DataAccessFactory.ProductData().Delete(id);
        }


        public static List<ProductDTO> GetExpiringSoon(int days)
        {
            var today = DateTime.Now;
            var threshold = today.AddDays(days);

            var data = DAL.DataAccessFactory.ProductData()
                          .Read()
                          .Where(p => p.ExpireDate <= threshold && p.ExpireDate >= today)
                          .ToList();

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Product, ProductDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<ProductDTO>>(data);
        }


        public static bool ReleaseProductFromSection(ProductReleaseDTO dto)
        {
            var section = DAL.DataAccessFactory.SectionData().Read(dto.SectionId);
            if (section == null) throw new Exception("Section not found.");

            var sectionProductList = DAL.DataAccessFactory.SectionProductData().Read()
                                        .Where(sps => sps.SectionID == dto.SectionId && sps.ProductID == dto.ProductId)
                                        .ToList();

            var sp = sectionProductList.FirstOrDefault();
            if (sp == null) throw new Exception("Product not found in section.");

            if (dto.Quantity > sp.Quantity)
            {
                throw new Exception("Cannot release more than stored quantity.");
            }

            // Deduct quantity
            sp.Quantity -= dto.Quantity;
            section.Quantity -= dto.Quantity;

            // If zero, remove relation
            if (sp.Quantity == 0)
            {
                DAL.DataAccessFactory.SectionProductData().Delete(sp.Id);
            }
            else
            {
                DAL.DataAccessFactory.SectionProductData().Update(sp);
            }

            DAL.DataAccessFactory.SectionData().Update(section);
            return true;
        }





    }
}
