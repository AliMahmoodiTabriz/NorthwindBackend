﻿using Business.Abstract;
using Business.Constants;
using Core.Utility.Results;
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

        public IDataResult<Product> Add(Product product)
        {
            //var isExsist = _prodcutDal.Get(p => p.ProductID == product.ProductID);
            //if (isExsist != null)
            //    return new ErrorDataResult<Product>("This product is already available");
            return new SuccsessDataResult<Product>(_prodcutDal.Add(product));
        }

        public IResult Delete(Product product)
        {
            _prodcutDal.Delete(product);
            return new SuccsessResult(Messages.ProductDeleted);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccsessDataResult<Product>(_prodcutDal.Get(p => p.ProductID == productId));
        }

        public IDataResult<List<Product>> GetList()
        {
            return new SuccsessDataResult<List<Product>>(_prodcutDal.GetList().ToList());
        }

        public IDataResult<List<Product>> GetListByCategoryId(int categoryId)
        {
            return new SuccsessDataResult<List<Product>>(_prodcutDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        public IResult Update(Product product)
        {
            _prodcutDal.Update(product);
            return new SuccsessResult(Messages.ProductUpdated);
        }
    }
}
