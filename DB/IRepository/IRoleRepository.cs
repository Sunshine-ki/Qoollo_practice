using System.Collections.Generic;
using System.Threading.Tasks;

namespace QoolloSSO.backend.DataBase.IRepository
{
	public interface IRoleRepository
	{
		Task CreateRoleAsync(Role role);
		Task UpdateRoleAsync(string Id, Role role);
		Task DeleteRoleAsync(string Id);
		Task<List<Role>> GetRolesAsync();
		Task<Role> GetRoleAsync(string Id);
	}
}