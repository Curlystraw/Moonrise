using System;
using System.Collections.Generic;

namespace ItemSpace
{
	public class Weapon : EquipItem
	{
		private WeaponType type;
		private WeaponWeight weight;
		private WeaponPrefix prefix;
		private WeaponInfix infix;
		private WeaponSuffix suffix;

		private int range;

		private int attackBonus, hpBonus;
		private double attackMult, speedMult;

		private static List<WeaponPrefix> prefixApostrophes = new List<WeaponPrefix>( new[] {
			WeaponPrefix.Soldier, WeaponPrefix.Knight, WeaponPrefix.Captain, 
			WeaponPrefix.Ogre, WeaponPrefix.Titan, WeaponPrefix.Dragon, 
			WeaponPrefix.Medic, WeaponPrefix.Doctor, WeaponPrefix.Surgeon
		});

		private static List<WeaponSuffix> suffixNoThes = new List<WeaponSuffix>( new[] {
			WeaponSuffix.Sight, WeaponSuffix.Strength, WeaponSuffix.Might, WeaponSuffix.Power, WeaponSuffix.Destruction
		});

		public Weapon() : this(WeaponType.Crossbow, WeaponWeight.Medium, WeaponPrefix.Great, WeaponInfix.Silver, WeaponSuffix.Assassin)
		{
			
		}

		public Weapon(WeaponType type, WeaponWeight weight, WeaponPrefix prefix, WeaponInfix infix, WeaponSuffix suffix)
		{
			name = "Weapon";
			this.itemClass = ItemClass.Weapon;
			this.type = type;
			this.weight = weight;
			this.prefix = prefix;
			this.infix = infix;
			this.suffix = suffix;

			attackBonus = 0;
			attackMult = 1;
			speedMult = 1;
			hpBonus = 0;

			switch (prefix) {
			case WeaponPrefix.Great:
				attackMult = 1.15;
				break;
			case WeaponPrefix.Mighty:
				attackMult = 1.25;
				break;
			case WeaponPrefix.Masterful:
				attackMult = 1.4;
				break;
			case WeaponPrefix.Soldier:
				attackMult = 1.2;
				break;
			case WeaponPrefix.Knight:
				attackMult = 1.3;
				break;
			case WeaponPrefix.Captain:
				attackMult = 1.45;
				break;
			case WeaponPrefix.Ogre:
				attackMult = 1.3;
				break;
			case WeaponPrefix.Titan:
				attackMult = 1.4;
				break;
			case WeaponPrefix.Dragon:
				attackMult = 1.6;
				break;
			case WeaponPrefix.Medic:
				hpBonus = 20;
				break;
			case WeaponPrefix.Doctor:
				hpBonus = 30;
				break;
			case WeaponPrefix.Surgeon:
				hpBonus = 40;
				break;
			}

			switch (infix) {
			case WeaponInfix.Bronze:
				attackBonus = 10;
				break;
			case WeaponInfix.Steel:
				attackBonus = 15;
				break;
			case WeaponInfix.Silver:
				attackBonus = 20;
				break;
			case WeaponInfix.Platinum:
				attackBonus = 25;
				break;
			case WeaponInfix.Titanium:
				attackBonus = 30;
				break;
			case WeaponInfix.Diamond:
				attackBonus = 35;
				break;
			case WeaponInfix.Obsidian:
				attackBonus = 40;
				break;
			}

			switch (suffix) {
			case WeaponSuffix.Wind:
				speedMult = 1.15;
				break;
			case WeaponSuffix.Gale:
				speedMult = 1.2;
				break;
			case WeaponSuffix.Storm:
				speedMult = 1.25;
				break;
			}

			string weightStr, prefixStr, infixStr, typeStr, suffixStr;

			weightStr = weight.ToString ();

			prefixStr = prefix.ToString ();
			if (prefixApostrophes.Contains (prefix))
				prefixStr += "'s";

			infixStr = infix.ToString ();

			typeStr = type.ToString ();

			suffixStr = suffix.ToString ();
			if (suffix == WeaponSuffix.Sight)
				suffixStr = "True " + suffixStr;
			if (!suffixNoThes.Contains (suffix))
				suffixStr = "of the " + suffixStr;

			name = String.Join (" ", new[] {
				weightStr, prefixStr, infixStr, typeStr, suffixStr
			});
		}

		public int AttackBonus {
			get {
				return attackBonus;
			}
		}

		public double AttackMult {
			get {
				return attackMult;
			}
		}

		public int HpBonus {
			get {
				return hpBonus;
			}
		}

		public double SpeedMult {
			get {
				return speedMult;
			}
		}
	}

	public enum WeaponType
	{
		Crossbow
	}

	public enum WeaponWeight
	{
		Light,
		Medium,
		Heavy
	}

	public enum WeaponPrefix
	{
		None,
		Great,
		Mighty,
		Masterful,
		Soldier,
		Knight,
		Captain,
		Ogre,
		Titan,
		Dragon,
		Medic,
		Doctor,
		Surgeon,
		Fast,
		Quick,
		Lightning,
		Duke,
		Lord,
		King,
		Legendary,
		Ultimate
	}

	public enum WeaponInfix
	{
		None,
		Bronze,
		Steel,
		Silver,
		Platinum,
		Titanium,
		Diamond,
		Obsidian
	}

	public enum WeaponSuffix
	{
		None,
		Wind,
		Gale,
		Storm,
		Rogue,
		Assassin,
		Shadow,
		Eagle,
		Hawk,
		Sight,
		Strength,
		Might,
		Power,
		Destruction
	}
}

