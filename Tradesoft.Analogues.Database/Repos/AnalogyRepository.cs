using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tradesoft.Analogues.Database.Models;

namespace Tradesoft.Analogues.Database.Repos
{
    public class AnalogyRepository
    {
        public void Delete(int id1, int id2)
        {
            using (var _context = new AnalogyContext())
            {
                var entity = Find(id1, id2);
                _context.Entry(entity).State = EntityState.Deleted;
                _context.SaveChanges();
            }
        }

        public IEnumerable<Analogy> GetAll()
        {
            using (var _context = new AnalogyContext())
                return _context.Analogues
                    .Include(a => a.Product1)
                    .Include(a => a.Product2)
                    .ToList();
        }

        public IEnumerable<Analogy> GetAnalogues(int id)
        {
            using (var _context = new AnalogyContext())
            {
                return _context.Analogues
                    .Where(a => a.Product1Id == id || a.Product2Id == id)
                    .Include(a => a.Product1)
                    .Include(a => a.Product2)
                    .ToList();
            }
        }

        public Analogy Find(int id1, int id2)
        {
            using (var _context = new AnalogyContext())
                return _context.Analogues.Find(id1, id2) ?? _context.Analogues.Find(id2, id1);
        }

        public Analogy CreateOrUpdate(Analogy analogy)
        {
            //analogy.Product1.VendorCode = Regex.Replace(analogy.Product1.VendorCode, "[,\\./-]", string.Empty);
            //analogy.Product2.VendorCode = Regex.Replace(analogy.Product2.VendorCode, "[,\\./-]", string.Empty);
            //analogy.Product1.Manufacturer = analogy.Product1.Manufacturer.ToLower();
            //analogy.Product2.Manufacturer = analogy.Product2.Manufacturer.ToLower();
            using (var _context = new AnalogyContext())
            {
                var productRepository = new ProductRepository();
                analogy.Product1 = productRepository.CreateOrUpdate(analogy.Product1);
                analogy.Product2 = productRepository.CreateOrUpdate(analogy.Product2);
                analogy.Product1Id = analogy.Product1.Id;
                analogy.Product2Id = analogy.Product2.Id;

                var existAnalogy = Find(analogy.Product1.Id, analogy.Product2.Id);
                if (existAnalogy != null)
                {
                    _context.Entry(analogy).State = EntityState.Modified;
                    _context.SaveChanges();
                    return analogy;
                }

                analogy = Create(analogy);
                return analogy;
            }
        }

        public Analogy Create(Analogy entity)
        {
            using (var _context = new AnalogyContext())
            {
                _context.Products.Attach(entity.Product1);
                _context.Products.Attach(entity.Product2);
                entity = _context.Analogues.Add(entity);
                _context.SaveChanges();
                return entity;
            }
        }
    }
}
