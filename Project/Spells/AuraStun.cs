﻿using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Project.Data;

namespace Project.Spells
{
	internal class AuraStun : Aura
	{
		protected AuraStun(XElement data) : base(data)
		{
			Applied += AuraStun_Applied;
		}

		private void AuraStun_Applied(object sender, EventArgs e)
		{
			if (Target.CurrentCast != null)
				Target.CurrentCast.Cancel();
		}

		[XmlData.XmlParserAttribute("Stun")]
		public static Aura Create(XElement data)
		{
			return new AuraStun(data);
		}

		public override void GetTooltip(StringBuilder sb)
		{
			sb.AppendLine(String.Format("Stun: {0:F1} sec", Duration));
		}
	}
}
