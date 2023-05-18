using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService 
    {
        private IProductDal _productdal; //field oluşturduk.veri erişim katmanını böyle enjekte ederiz.
                                         //kullanmak mantığıyla

        public ProductManager(IProductDal productdal)//constructor oluşturduk
        {
            _productdal = productdal;
        }


        public IDataResult<Product> GetById(int productId)
        {
            return new SuccesDataResult<Product>(_productdal.get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetList()
        {
            //var a = _productdal.GetList().ToList();
            //var b = new SuccesDataResult<List<Product>>(a);//alttaki satırın hata yönetimi böyle yapılabilir.
            //return b;
            return new SuccesDataResult<List<Product>>(_productdal.GetList().ToList());
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccesDataResult<List<Product>>(_productdal.GetList(p=>p.CategoryId == categoryId).ToList()); 
        }
        
        [ValidationAspect(typeof(ProductValidator), Priority = 2)]
        [ValidationAspect(typeof(ProductValidator), Priority = 1)]//önce bu çalışır

        public IResult Add(Product product)
        {
            
                //business code
                _productdal.add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productdal.delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }
        public IResult Update(Product product)
        {
            _productdal.update(product);    
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
