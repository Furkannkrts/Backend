using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user,List<OperationClaim> operationClaims);//kullanıcı bilgisi ve rolleri verdik
        //burada user kullanma sebebiyle nesneleri core katmanına taşıdık.
        //Çünkü user bilgisini göndermem lazım token üretebilmek için

    }
}
