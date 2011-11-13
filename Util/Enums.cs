// Berserker: Multi-platform open source game server.
// Copyright (C) 2011 Berserker Group
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berserker
{
	// TODO: Uncomment. To do so needs editing of all scripts etc.

/*	public enum Inventory : byte
	{
		Head = 1,
		Neck = 2,
		Backpack = 3,
		Body = 4,
		RightHand = 5,
		LeftHand = 6,
		Legs = 7,
		Feet = 8
	};
	
	public enum Skull : byte
	{
		Fist = 0,
		Club = 1,
		Sword = 2,
		Axe = 3,
		Distance = 4,
		Shielding = 5,
		Fishing = 6
	};
	
	public enum ItemType : uint
	{
		BlocksItem = 0x01,
		OnTop = 0x02,
		Container = 0x04,
		Stackable = 0x08,
		UsableWith = 0x10,
		PileUp = 0x20,
		WritableEdit = 0x40,
		WritableNoEdit = 0x80,
		FluidContainer = 0x100,
		Blocking = 0x0200, // wrong? blocks
		Movable = 0x400,
		BlocksProjectiles = 0x800,
		BlocksMonsters = 0x1000,
		Equipable = 0x2000,
		LightItems = 0x4000,
		CanSeeUnder = 0x8000,
		BlocksMagic = 0x10000,
		BlocksAutoWalk = 0x2000,
		Item = 0x40000,
		Creature = 0x80000
	};
	
	public enum AccessLevel : byte
	{
		Normal = 1,
		Npc = 2,
		Gamemaster = 3,
		Admin = 4
	};
	
	public sealed class ItemAttribute
	{
		public const string Weight = "weight";
		public const string Defense = "defense";
		public const string Attack = "attack";
		public const string Armor = "armor";
		public const string WeaponType = "weaponType";
		public const string SlotType = "slotType";
		public const string RuneSpellName = "runeSpellName";
		public const string Description = "description";
		public const string Charges = "charges";
		public const string ShowCharges = "showcharges"; // TODO: WRONG? showCharges?
		public const string AbsorbPercentManaDrain = "absorbPercentManaDrain"; // TODO: REMOVE?
		public const string StopDuration = "stopduration"; // TODO: WRONG? stopDuration?
		public const string ShowDuration = "showduration"; // TODO: WRONG? showDuration?
		public const string TransformEquipTo = "transformEquipTo";
		public const string DecayTo = "decayTo";
		public const string Duration = "duration";
		public const string ContainerSize = "containerSize";
		public const string ShootType = "shootType";
		public const string AmmoType = "ammoType";
	}
	
	public sealed class WeaponType
	{
		public const string Sword = "sword";
		public const string Axe = "axe";
		public const string Distance = "distance";
		public const string Club = "club";
		public const string Shield = "shield";
		public const string Ammunition = "ammunition";
	}
	
	public sealed class ShootType
	{
		public const string SmallStone = "smallstone";
		public const string Snowball = "snowball";
		public const string Spear = "spear";
		public const string ThrowingStar = "throwingstar";
		public const string ThrowingKnife = "throwingknife";
		public const string Energy = "energy";
		public const string Bolt = "bolt";
		public const string Arrow = "arrow";
		public const string PoisonArrow = "poisonarrow";
		public const string BurstArrow = "burstarrow";
		public const string PowerBolt = "powerbolt";
		public const string PowerArrow = "powerarrow"; // TODO: YES YES YES! Power bolt = power arrow (id 1118?)
	}
	
	public enum TextColor : byte
	{
		Blue = 5,
		LightBlue = 35,
		LightGreen = 30,
		Purple = 83,
		LightGrey = 129,
		DarkRed = 144,
		Red = 180,
		Orange = 198,
		Yellow = 210,
		WhiteExp = 215,
		None = 255
	};
	
	public enum Speech : byte
	{
		Normal = 0x01,
		Whisper = 0x02,
		Yell = 0x03,
		PrivateMessage = 0x04,
		ChannelYellow = 0x05,
		RvInit = 0x06,
		RvCounsellor = 0x07,
		RvReporter = 0x08,
		Broadcast = 0x09,
		Monster = 0x0E
	};
	
	public enum Message : byte
	{
		SmallInfo = 0x14,
		Green = 0x13,
		White = 0x11,
		ImportantWhite = 0x10,
		Red = 0x0A
	};
	
	public sealed class AmmoType
	{
		public const string Arrow = "arrow";
		public const string Bolt = "bolt";
	}
	
	public sealed class SlotType
	{
		public const string TwoHanded = "two-handed";
		public const string Backpack = "backpack";
		public const string Ring = "ring"; // TODO: REMOVE?
		public const string Necklace = "necklace";
		public const string Feet = "feet";
		public const string Legs = "legs";
		public const string Body = "body";
		public const string Head = "head";
	}
	
	public enum Race : byte 
	{
		Venom = 1,
		Blood = 2,
		Undead = 3,
		Fire = 4
	};
	
	public enum FightMode : byte
	{
		Offensive = 1,
		Balanced = 2,
		Defensive = 3
	};
	
	public enum FightStance : byte 
	{
		StandStill = 0,
		Chase = 1,
		KeepDistance = 2
	};
	
	// TODO: Dynamic
	public enum Vocation
	{
		None = 0,
		Knight = 1,
		Paladin = 2,
		Sorcerer = 3,
		Druid = 4,
		Total = 5
	};
	
	public enum Direction : byte 
	{
		North = 0,
		West = 3,
		South = 2,
		East = 1,
		None = 0xFF
	};
	
	public enum ChatLocal
	{
		Orange = 0x41,
		Say = 0x53,
		Whisper = 0x57,
		Yells = 0x59
	};
	
	public enum ChatGlobal
	{
		PrivateMessage = 0x50,
		Broadcast = 0x42,
	};
	
	public enum ChatAnonymous
	{
		White = 0x47,
		Green = 0x4D
	};
	
	public enum MagicEffect
	{
		DrawBlood = 0,
		LooseEnergy = 1,
		Puff = 2,
		BlockHit = 3,
		ExplosionArea = 4,
		ExplosionDamage = 5,
		FireArea = 6,
		YellowRings = 7,
		PoisonRings = 8,
		HitArea = 9,
		BlueBall = 10,
		EnergyDamage = 11,
		BlueSparkles = 12,
		RedSparkles = 13,
		GreenSparkles = 14,
		Burned = 15,
		SplashPoison = 16,
		MortArea = 17
	};
	
	public enum HealthStatus
	{
		Dead = 0,
		NearlyDead = 1,
		Critical = 2,
		HeavilyWounded = 3,
		LightlyWounded = 4,
		Healthy = 6,
		Total = 7
	};
	
	public enum ImmunityType
	{
		Physical,
		Poison,
		Fire,
		Electric
	};
	
	public enum DistanceType
	{
		EffectSpear = 0,
		EffectBolt = 1,
		EffectArrow = 2,
		EffectFireball = 3,
		EffectEnergy = 4,
		EffectPoisonArrow = 5,
		EffectBurstArrow = 6,
		EffectThrowingStar = 7,
		EffectThrowingKnife = 8,
		EffectSmallStone = 9,
		EffectBlackEnergy = 10, // SD
		EffectBigStone = 11,
		EffectPowerBolt = 13,
		EffectPowerArrow = 12, // ???
		EffectNone
	};
	
	public enum StackPosType
	{
		Ground = 0,
		TopItem = 1,
		Creature = 2,
		RegularItem = 3
	};
	
	public enum SpellType : byte
	{
		Rune,
		Monster, 
		PlayerSay // instant
	};
	
	public enum FindDistance
	{
		DistanceBeside,
		DistanceClose,
		DistanceFar,
		DistanceVeryFar
	};
	
	public enum FindLevel
	{
		LevelHigher,
		LevelLower,
		LevelSame
	};
	
	public enum FindDirection
	{
		DirectionNorth,
		DirectionSouth,
		DirectionEast,
		DirectionWest,
		DirectionNorthEast,
		DirectionNorthWest,
		DirectionSouthEast,
		DirectionSouthWest
	};
	
	public enum Fluid : byte
	{
		None = 0,
		Water = 1,
		Blood = 2,
		Beer = 3,
		Slime = 4,
		Lemonade = 5,
		Milk = 6,
		ManaFluid = 7,
		Wine = 7
	};*/
}
