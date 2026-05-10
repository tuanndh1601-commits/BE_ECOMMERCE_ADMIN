using ECOM.DOMAIN.DTO.Entities;
using ECOM.INFRASTRUCTURE.Attributes;
using ECOM.INFRASTRUCTURE.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECOM.INFRASTRUCTURE.Repository
{
    public interface IMenuRepository
    {
        Task<IEnumerable<MenuItemsEntity>> GetAll();
    }

    public class MenuRepository : BaseRepository, IMenuRepository
    {
        public MenuRepository(IDbConnectionFactory context) : base(context) { }

        public async Task<IEnumerable<MenuItemsEntity>> GetAll()
        {
            string sql = "SELECT * FROM dbo.DM_MenuItems";
            return await QueryListAsync<MenuItemsEntity>(sql);
        }
    }
}
