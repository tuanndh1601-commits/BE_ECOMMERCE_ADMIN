using ECOM.DOMAIN.DTO.Entities;

namespace ECOM.INFRASTRUCTURE.Repository
{
    public interface IMenuRepository
    {
        Task<IEnumerable<MenuItemsEntity>> GetAll();
    }
}
