using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int productId);
        List<Product> GetList();
        List<Product> GetListByCategoryId(int categoryId);
        Product Add(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}
