﻿using System;
using System.Linq;
using System.Xml.Linq;

namespace Project.Spells
{
	public struct Attributes : IComparable<Attributes>
	{
		public readonly int Block;
		public readonly int Combo;
		public readonly int Stamina;

		public Attributes(int stamina, int block, int combo)
		{
			Stamina = stamina;
			Block = block;
			Combo = combo;
		}

		public static Attributes operator +(Attributes a1, Attributes a2)
		{
			return new Attributes(
				a1.Stamina + a2.Stamina,
				a1.Block + a2.Block,
				a1.Combo + a2.Combo
				);
		}

		public static Attributes Parse(XElement element)
		{
			if (element == null) throw new ArgumentNullException("element");

			return new Attributes
				(
				int.Parse(element.Attribute("stamina").Value),
				int.Parse(element.Attribute("block").Value),
				int.Parse(element.Attribute("combo").Value)
				);
		}

		public override string ToString()
		{
			return "Stamina: " + Stamina
			       + "\nBlock: " + Block
			       + "\nCombo: " + Combo;
		}

		private int Sum()
		{
			return Stamina + Block + Combo;
		}

		public int CompareTo(Attributes attributes)
		{
			return Sum().CompareTo(attributes.Sum());
		}
	}

	public struct AttributesMultiplier
	{
		public readonly int BlockPercent;
		public readonly int ComboPercent;
		public readonly int StaminaPercent;

		public AttributesMultiplier(int staminaPercent, int blockPercent, int comboPercent)
		{
			StaminaPercent = staminaPercent;
			BlockPercent = blockPercent;
			ComboPercent = comboPercent;
		}

		public static Attributes operator *(AttributesMultiplier a1, Attributes a2)
		{
			return new Attributes
				(
				(int)((double)(a1.StaminaPercent + 100) / 100 * a2.Stamina),
				(int)((double)(a1.BlockPercent + 100) / 100 * a2.Block),
				(int)((double)(a1.ComboPercent + 100) / 100 * a2.Combo)
				);
		}

		public static Attributes operator *(Attributes a1, AttributesMultiplier a2)
		{
			return a2 * a1;
		}

		public static AttributesMultiplier operator +(AttributesMultiplier a1, AttributesMultiplier a2)
		{
			return new AttributesMultiplier
				(
				a1.StaminaPercent + a2.StaminaPercent,
				a1.BlockPercent + a2.BlockPercent,
				a1.ComboPercent + a2.ComboPercent
				);
		}

		public static AttributesMultiplier Parse(XElement element)
		{
			if (element == null) throw new ArgumentNullException("element");

			return new AttributesMultiplier
				(
				(int)element.Attribute("stamina"),
				(int)element.Attribute("block"),
				(int)element.Attribute("combo")
				);
		}

		public override string ToString()
		{
			return String.Format("Stamina: +{0:d}%\nBlock: +{1:d}%\nCombo: +{2:d}%",
			                     (StaminaPercent), (BlockPercent), (ComboPercent));
		}
	}
}
