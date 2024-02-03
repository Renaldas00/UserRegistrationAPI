using UserRegistration.DAL.Entities;
using UserRegistration.DAL.Repositories.Interfaces;

namespace UserRegistration.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }

        public Guid Create(Account model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var exists = _context.Account.Any(x => x.UserName == model.UserName);
            if (exists)
                throw new ArgumentException("Username already exists");

            _context.Account.Add(model);
            _context.SaveChanges();
            return model.Id;
        }
        public Account? Get(string userName)
        {
            if (userName == null)
                throw new ArgumentNullException(nameof(userName));

            return _context.Account.FirstOrDefault(x => x.UserName == userName);
        }
        public bool Exists(Guid id)
        {
            return _context.Account.Any(x => x.Id == id);
        }
        public void Delete(Guid id)
        {
            var account = _context.Account.Find(id);
            if (account != null)
            {
                _context.Account.Remove(account);
                _context.SaveChanges();
            }
        }
    }
}
