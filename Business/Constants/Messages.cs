using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    //sabit olduğu için her seferinde newlememek için static yaparız
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";

        public static string ProductUpdated = "Ürün güncellendi";

        public static string ProductNameInvalid = "Ürün ismi geçersiz";

        public static string MaintenanceTime = "Sistem bakımda";

        public static string ProductsListed = "ürünler listelendi";

        public static string ProductCountOfCategoryError ="Bir Kategoride en fazla 10 ürün olabilir" ;

        public static string ProductNameAlreadyExists= "Bu isimde zaten başka bir ürün var ";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        public static string AuthorizationDenied="Yetkiniz yok.";

    }
}
