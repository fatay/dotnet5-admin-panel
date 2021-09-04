using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Data.Concrete.EntityFramework.Repositories;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProgrammersBlogContext _context;
        private EfArticleRepository _articleRepository;
        private EfCategoryRepository _categoryRepository;
        private EfCommentRepository _commentRepository;

        public UnitOfWork(ProgrammersBlogContext context)
        {
            _context = context;
        }

        // ?? => Is it null ?? (Null Coalescing Operator)
        public IArticleRepository Articles => _articleRepository ?? new EfArticleRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_context);

        public ICommentRepository Comments => _commentRepository ?? new EfCommentRepository(_context);


        public async Task<int> SaveAsync()
        {
            // Save changes and commit.
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
