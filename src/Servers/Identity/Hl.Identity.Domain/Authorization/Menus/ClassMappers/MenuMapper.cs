using Hl.Core.ClassMapper;

namespace Hl.Identity.Domain.Authorization.Menus.ClassMappers
{
    public class MenuMapper : HlClassMapper<Menu>
    {
        public MenuMapper()
        {
            Table("auth_menu");
            AutoMap();
        }
    }
}
