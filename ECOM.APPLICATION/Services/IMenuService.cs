using ECOM.DOMAIN.DTO.Entities;
using ECOM.INFRASTRUCTURE.Repository;

namespace ECOM.INFRASTRUCTURE.Repository
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuItemsEntity>> GetDataListAsync();
    }

    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepo;

        public MenuService(IMenuRepository menuRepo)
        {
            _menuRepo = menuRepo;
        }

        public async Task<IEnumerable<MenuItemsEntity>> GetDataListAsync()
        {
            return await _menuRepo.GetAll();
        }
    }
}
