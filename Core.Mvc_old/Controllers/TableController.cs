using Core.Mvc.ViewConfigurations.Table;
using Microsoft.AspNetCore.Mvc;

namespace Core.Mvc.Controllers
{
	public class TableController : StandardController
	{
		public IActionResult Index()
		{
			Index index = new Index();

			return this.ViewConfiguration(index);
		}
	}
}