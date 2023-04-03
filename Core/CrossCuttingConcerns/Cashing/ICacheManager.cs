using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Cashing
{
    public interface ICacheManager
    {
        T Get<T>(string key);  //verilen key'ye bellekte karşılık gelen datayı getir
        object Get(string key);
        void Add(string key, object value, int duration);  //duration: Cache de ne kadar duracağını tutar(dk)

        bool IsAdd(string key);  //Cache de var mı kontrolü yapılır 

        void Remove(string key);    //Cache'den uçurma
        void RemoveByPattern(string pattern);    //isminde Category,Get geçen metotları uçur gibi
    }
}
