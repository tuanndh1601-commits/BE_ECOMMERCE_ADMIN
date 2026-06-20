using ECOM.DOMAIN.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ECOM.APPLICATION.Interfaces.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Auto Apply Global Query Filter cho tất cả các bảng có Soft Delete
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(ConvertFilterExpression(entityType.ClrType));
                }
            }
        }

        private static System.Linq.Expressions.LambdaExpression ConvertFilterExpression(Type type)
        {
            var parameter = System.Linq.Expressions.Expression.Parameter(type, "e");
            var property = System.Linq.Expressions.Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
            var falseConstant = System.Linq.Expressions.Expression.Constant(false);
            var body = System.Linq.Expressions.Expression.Equal(property, falseConstant);
            return System.Linq.Expressions.Expression.Lambda(body, parameter);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentIdUser = _currentUserService.Id;
            var currentTime = DateTime.Now;

            foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        dynamic entityAdd = entry.Entity;
                        entityAdd.CreatedBy = currentIdUser;
                        entityAdd.CreatedAt = currentTime;
                        break;

                    case EntityState.Modified:
                        entry.Property("CreatedBy").IsModified = false;
                        entry.Property("CreatedAt").IsModified = false;

                        dynamic entityMod = entry.Entity;
                        entityMod.UpdatedBy = currentIdUser;
                        entityMod.UpdatedAt = currentTime;
                        break;

                    case EntityState.Deleted:
                        // Đổi cơ chế sang Soft Delete đánh dấu trường logic
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.DeletedBy = currentIdUser;
                        entry.Entity.DeletedAt = currentTime;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
