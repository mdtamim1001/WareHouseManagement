using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.interfaces;
using DAL.Interfaces;
using DAL.Models;
using DAL.repos;
using DAL.Repos;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepo<Product, int, bool> ProductData()
        {
            return new ProductRepo();
        }

        public static IRepo<Section, int, bool> SectionData()
        {
            return new SectionRepo();
        }

        public static IRepo<Shipment, int, bool> ShipmentData()
        {
            return new ShipmentRepo();
        }

        public static IRepo<SectionProducts, int, bool> SectionProductData()
        {
            return new SectionProductRepo();
        }
        public static IAuth<bool> AuthData()
        {
            return new ManagerRepo();
        }
        public static IRepo<Token, string, Token> TokenData()
        {
            return new TokenRepo();

        }
    }
}
