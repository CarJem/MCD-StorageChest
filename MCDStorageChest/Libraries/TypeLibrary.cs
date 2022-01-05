using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCDStorageChest.Libraries
{
    public static class TypeLibrary
    {
        public static HashSet<string> AllEnchantments = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public static HashSet<string> PowerfulEnchantments = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "FinalShout",
            "Chilling",
            "Protection",
            "GravityPulse",

            "CriticalHit",
            "Exploding",
            "RadianceMelee",
            "GravityMelee",
            "Shockwave",
            "Swirling",

            "Gravity",
            "TempoTheft",
            "ChainReaction",
            "RadianceRanged",
            "ShockWeb",
        };
        public static HashSet<string> ArmorEnchantments = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "Swiftfooted",
            "PotionFortification",
            "Snowing",
            "SurpriseGift",
            "Burning",
            "Cowardice",
            "Deflecting",
            "Electrified",
            "Thorns",
            "Explorer",
            "Frenzied",
            "Celerity",
            "Recycler",
            "FoodReserves",
            "FireTrail",
            "HealthSynergy",
            "SpeedSynergy",
            "SpiritSpeed",

            "FinalShout",
            "Chilling",
            "Protection",
            "GravityPulse",
        };
        public static HashSet<string> MeleeEnchantments = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "Weakening",
            "FireAspect",
            "Looting",
            "Chains",
            "Echo",
            "Stunning",
            "Rampaging",
            "Freezing",
            "Committed",
            "PoisonedMelee",
            "Prospector",
            "EnigmaResonatorMelee",
            "SoulSiphon",
            "Thundering",
            "Sharpness",
            "Leeching",

            "CriticalHit",
            "Exploding",
            "RadianceMelee",
            "GravityMelee",
            "Shockwave",
            "Swirling",
        };
        public static HashSet<string> RangedEnchantments = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
            "Accelerating",
            "Growing",
            "AnimaConduitRanged",
            "RapidFire",
            "Infinity",
            "Unchanting",
            "Piercing",
            "Power",
            "WildRage",
            "Punch",
            "Ricochet",
            "Supercharge",
            "FuseShot",
            "BonusShot",
            "FireAspect",
            "MultiShot",

            "Gravity",
            "TempoTheft",
            "ChainReaction",
            "RadianceRanged",
        };

        public static HashSet<string> Items_All = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public static HashSet<string> Items_Artifacts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public static HashSet<string> Items_Armor = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public static HashSet<string> Items_MeleeWeapons = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public static HashSet<string> Items_RangedWeapons = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public static HashSet<string> ArmorProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    }
}
