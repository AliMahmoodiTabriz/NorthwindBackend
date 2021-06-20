using Core.Utility.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<Product> GetById(int productId);
        IDataResult<List<Product>> GetList();
        IDataResult<List<Product>> GetListByCategoryId(int categoryId);
        IDataResult<Product> Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);
    }
}
