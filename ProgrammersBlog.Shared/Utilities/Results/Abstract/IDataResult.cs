using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
    /*
    Tekli veya çoklu kullanım yapabileceğimizden out olarak tanımlıyoruz.
    Ayrıca, farklı türlerle de uyumlu olması gerektiğinden Generic olarak tanımlıyoruz.
    new DataResult<Category>(ResultStatus.Success,category);
    new DataResult<IList<Category>>(ResultStatus.Success,categoryList);
    */
    public interface IDataResult<out T>: IResult
    {
        public T Data { get; }
    }
}
