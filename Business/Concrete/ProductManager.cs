using Business.Abstract;
using DataAccsess.Abstract;
using DataAccsess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProdcutDal _prodcutDal;

        public ProductManager(IProdcutDal prodcutDal)
        {
            _prodcutDal = prodcutDal;
        }

        public Product Add(Product product)
        {
            return _prodcutDal.Add(product);
        }

        public void Delete(Product product)
        {
            _prodcutDal.Delete(product);
        }

        public Product GetById(int productId)
        {
            return _prodcutDal.Get(p => p.ProductID == productId);
        }

        public List<Product> GetList()
        {
            return _prodcutDal.GetList().ToList();
        }

        public List<Product> GetListByCategoryId(int categoryId)
        {
            return _prodcutDal.GetList(p => p.CategoryId == categoryId).ToList();
        }

        public void Update(Product product)
        {
            _prodcutDal.Update(product);
        }
    }
}
