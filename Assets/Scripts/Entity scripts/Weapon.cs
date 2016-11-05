using System;

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

		public Weapon() : this(WeaponType.Crossbow, WeaponWeight.Medium, WeaponPrefix.Great, WeaponInfix.Silver, WeaponSuffix.Assassin)
		{
			
		}

		public Weapon(WeaponType type, WeaponWeight weight, WeaponPrefix prefix, WeaponInfix infix, WeaponSuffix suffix)
		{
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
		Destruction
	}
}

