using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;

namespace ProgrammersBlog.Entities.Dtos
{
    public class ArticleDto : DtoGetBase
    {
        public Article Article { get; set; }

        // Neden DtoGetBase virtual kullandık? Override etmek için.
        // public override ResultStatus ResultStatus { get; set; } = ResultStatus.Success; (default değer)
    }
}
