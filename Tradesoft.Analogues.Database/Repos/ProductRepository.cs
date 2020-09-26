using System.Data.Entity.Migrations;
using System.Linq;
using Tradesoft.Analogues.Database.Models;

namespace Tradesoft.Analogues.Database.Repos
{
    public class ProductRepository
    {
        public Product CreateOrUpdate(Product product)
        {
            using (var _context = new AnalogyContext())
            {
                var entity = Get(product.VendorCode, product.Manufacturer);
                if (entity != null)
                    return entity;

                if (_context.Products.Find(product.Id) != null)
                    return Update(product);

                _context.Products.Add(product);
                _context.SaveChanges();
                return product;
            }
        }

        public Product Update(Product entity)
        {
            using (var _context = new AnalogyContext())
            {
                _context.Products.AddOrUpdate(entity);
                _context.SaveChanges();
                return entity;
            }
        }

        public Product Get(string vendorCode, string manufacturer)
        {
            var toFind = new Product() { Manufacturer = manufacturer, VendorCode = vendorCode };
            using (var _context = new AnalogyContext())
                 return _context.Products
                    .FirstOrDefault(p => p.Manufacturer == toFind.Manufacturer && p.VendorCode == toFind.VendorCode);
        }

        public void Delete(int id)
        {
            using (var _context = new AnalogyContext())
            {
                var entity = _context.Products.Find(id);
                _context.Products.Remove(entity);
                _context.SaveChanges();
            }
        }

    }
}
