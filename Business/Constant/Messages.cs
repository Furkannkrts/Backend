
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.Constant
{
    public static class Messages//değişen bir şey olmayacağı için static kullnadık
    {
        public static string ProductAdded = "Ürün başarıyla eklendi";
        public static string ProductDeleted = "Ürün başarıyla silindi";
        public static string ProductUpdated = "Ürün başarıyla güncellendi";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError="Şifre Hatalı";
        public static string SuccessfulLogin="Sisteme giriş başarılı";
        public static string UserAlreadyExists="Kullanıcı zaten mevcut";

        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated="Access token başarıyla oluşturuldu";
    }
}
