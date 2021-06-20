using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccsess.Abstract;
using DataAccsess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProdcutDal>();
        }
    }
}
