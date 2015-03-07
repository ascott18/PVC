using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Project.Data;

namespace Project.Spells
{
	class AuraStun : Aura
	{
		protected AuraStun(XElement data) : base(data)
		{
			Applied += AuraStun_Applied;
		}

		void AuraStun_Applied(object sender, EventArgs e)
		{
			if (Target.CurrentCast != null)
				Target.CurrentCast.Cancel();
		}

		[XmlData.XmlParserAttribute("Stun")]
		public static Aura Create(XElement data)
		{
			return new AuraStun(data);
		}
	}
}
