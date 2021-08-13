using Provenance.DataLayer.Base;

namespace Provenance.DataLayer.Entities
{
	public class Role : EntityBase
	{

		public string Name { get; set; }

		public virtual User User { get; set; }

	}
}
