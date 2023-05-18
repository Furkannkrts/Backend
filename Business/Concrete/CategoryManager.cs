using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;//dependecy ınjection kullandık.!!!!!BURDA DİYORUZ Kİ ICategoryDal ihtiyacımız var
                                          //onuuu bu şekilde yapıyoruz
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetList()
        {  
            return new SuccesDataResult<List<Category>>(_categoryDal.GetList().ToList());
        }
    }
}
