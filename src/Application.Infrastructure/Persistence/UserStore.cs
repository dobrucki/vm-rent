using System;
using System.Collections.Generic;
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
        IQueryableUserStore<User>, IUserEmailStore<User>, IUserPasswordStore<User>, IUserRoleStore<User>
    {
        private readonly PostgresContext _context;

        public UserStore(PostgresContext context)
        {
            _context = context;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
                    u.Username.ToUpper().Equals(normalizedUserName), cancellationToken);
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
                    u.Username.Equals(normalizedEmail.ToUpper()), cancellationToken);
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
        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var role = _context.Roles.SingleOrDefault(e => e.Name.Equals(roleName));
            if (role == null)
            {
                throw new InvalidOperationException("Role not found");
            }

            var userRole = new UserRole
            {
                Role = role,
                User = user
            };
            _context.Add(userRole);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var entity = _context.UserRoles
                .Where(e => e.User.Id.Equals(user.Id))
                .SingleOrDefault(e => e.Role.Name.Equals(roleName));
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            var roles = _context.UserRoles
                .Where(e => e.User.Id.Equals(user.Id))
                .Select(e => e.Role.Name)
                .ToList();
            return roles;
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var result = _context.UserRoles
                .Where(e => e.User.Id.Equals(user.Id))
                .Any(e => e.Role.Name.Equals(roleName));
            return Task.FromResult(result);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var result = _context.UserRoles
                .Where(e => e.Role.Name.Equals(roleName))
                .Select(e => e.User)
                .ToList();
            return result;
        }
    }
}