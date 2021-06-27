using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccsess.Abstract;
using DataAccsess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace DataAccsess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<OperationClaim> GetClaim(User user)
        {
            using (var context=new NorthwindContext())
            {
                var result = from oreationClaim in context.OperationClaim
                             join userOperationClaim in context.UserOperationClaim
                             on oreationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim {Id= oreationClaim.Id,Name= oreationClaim.Name };
                return result.ToList();
            }
        }
    }
}
