using System;

namespace Provenance.DataLayer.Base
{
	public class EntityBase
	{

		public EntityBase ()
		{
			Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }

	}
}
