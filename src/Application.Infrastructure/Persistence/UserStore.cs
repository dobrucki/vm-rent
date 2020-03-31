using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Models;
using Application.Core.Ports;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistence
{
    public class UserStore : 
        IQueryableUserStore<User>, IUserEmailStore<User>, IUserPasswordStore<User>
    {
        private readonly PostgresContext _context;

        public UserStore(PostgresContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
        }

        public Task<string> GetUserIdAsync(
            User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(
            User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Username);
        }

        public Task SetUserNameAsync(
            User user, string userName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.Username = userName;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetNormalizedUserNameAsync(
            User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Username);
        }

        public Task SetNormalizedUserNameAsync(
            User user, string normalizedName, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<object>(null);
        }

        public async Task<IdentityResult> CreateAsync(
            User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            _context.Add(user);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError{ Description = $"Could not create user"});
        }

        public async Task<IdentityResult> UpdateAsync(
            User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            _context.Update(user);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError {Description = $"Could not update user"});
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            var userFromDb = await _context.Users.FindAsync(user.Id);
            _context.Remove(userFromDb);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError {Description = $"Could not delete user"});
        }

        public async Task<User> FindByIdAsync(
            string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Id.Equals(Guid.Parse(userId)), cancellationToken);
        }

        public async Task<User> FindByNameAsync(
            string normalizedUserName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Users
                .SingleOrDefaultAsync(u => 
                    u.Username.Equals(normalizedUserName.ToLower()), cancellationToken);
        }

        public Task SetPasswordHashAsync(
            User user, string passwordHash, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.PasswordHash = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPasswordHashAsync(
            User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(
            User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task SetEmailAsync(
            User user, string email, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.Username = email;
            return Task.FromResult<object>(null);
        }

        public Task<string> GetEmailAsync(
            User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Username);
        }

        public Task<bool> GetEmailConfirmedAsync(
            User user, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(true);
        }

        public Task SetEmailConfirmedAsync(
            User user, bool confirmed, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<object>(null);
        }

        public async Task<User> FindByEmailAsync(
            string normalizedEmail, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Users
                .SingleOrDefaultAsync(u => 
                    u.Username.Equals(normalizedEmail.ToLower()), cancellationToken);
        }

        public Task<string> GetNormalizedEmailAsync(
            User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Username);
        }

        public Task SetNormalizedEmailAsync(
            User user, string normalizedEmail, CancellationToken cancellationToken = default)
        {
            return Task.FromResult<object>(null);
        }

        public IQueryable<User> Users => _context.Users.AsQueryable();
    }
}