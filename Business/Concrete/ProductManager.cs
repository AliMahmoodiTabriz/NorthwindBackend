using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Caching;
using Core.Aspect.Autofac.Logging;
using Core.Aspect.Autofac.Performance;
using Core.Aspect.Autofac.Transaction;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utility.Results;
using DataAccsess.Abstract;
using DataAccsess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProdcutDal _prodcutDal;

        public ProductManager(IProdcutDal prodcutDal)
        {
            _prodcutDal = prodcutDal;
        }

        [CacheRemoveAspect("IProductService.Get")]
        [ValidationAspect(typeof(ProductValidator), Priority = 0)]
        public IDataResult<Product> Add(Product product)
        {
            //var isExsist = _prodcutDal.Get(p => p.ProductID == product.ProductID);
            //if (isExsist != null)
            //    return new ErrorDataResult<Product>("This product is already available");
            return new SuccsessDataResult<Product>(_prodcutDal.Add(product));
        }

        [CacheRemoveAspect("IProductService.Get")]
        public IResult Delete(Product product)
        {
            _prodcutDal.Delete(product);
            return new SuccsessResult(Messages.ProductDeleted, Messages.ProductDeletedId);
        }

        [SecuredOperation("GetById")]
        [CacheAspect(1)]
        //[LogAspect(typeof(DatabaseLogger))]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccsessDataResult<Product>(_prodcutDal.Get(p => p.ProductID == productId));
        }

        [CacheAspect(1)]
        [PerformanceAspect(1)]
        public IDataResult<List<Product>> GetList()
        {
            return new SuccsessDataResult<List<Product>>(_prodcutDal.GetList().ToList());
        }

        [CacheAspect(1)]
        public IDataResult<List<Product>> GetListByCategoryId(int categoryId)
        {
            return new SuccsessDataResult<List<Product>>(_prodcutDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        //[TransactionScopeAspect]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _prodcutDal.Update(product);
            return new SuccsessResult(Messages.ProductUpdated, Messages.ProductUpdatedId);
        }
    }
}
