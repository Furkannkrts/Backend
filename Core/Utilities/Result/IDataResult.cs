using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public interface IDataResult<T>: IResult//ıdataresult içinde ıresult içindeki succes ve message var demek
    {
        T Data { get; }
    }
}
