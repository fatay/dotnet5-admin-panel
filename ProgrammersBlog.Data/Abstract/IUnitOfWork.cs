using System;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
    // Garbage Collector için IDisposable implement ediyoruz.
    public interface IUnitOfWork : IAsyncDisposable
    {
        IArticleRepository Articles { get; }    // unitOfWork.Articles diyerek erişilebileceğiz.
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }    // _unitOfWork.Categories.AddAsync();

        // Commit and Save Changes (Kaydet)
        // _unitOfWork.Categories.DeleteAsync();
        // _unitOfWork.Users.AddAsync();
        // _unitOfWork.SaveAsync(); ---> Değişiklikleri kaydet.
        Task<int> SaveAsync();
    }
}
