using MCDStorageChest.Json.Classes;
using PostSharp.Patterns.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archetype = MCDStorageChest.Controls.ItemTemplates.RuneIcon.Archetype;

namespace MCDStorageChest.Models
{
    [Serializable, NotifyPropertyChanged]
    public class SearchModel
    {
        public bool SearchByName { get; set; } = false;
        public string SearchByName_Text { get; set; } = string.Empty;
        public bool SearchByName_UseIDs { get; set; } = false;


        public bool SearchByPowerLevel { get; set; } = false;
        public int SearchByPowerLevel_Value { get; set; } = 0;
        public bool[] SearchByPowerLevel_Mode { get; set; } = new bool[] { true, false, false };

        public bool LimitTo_Armor { get; set; } = true;
        public bool LimitTo_Ranged { get; set; } = true;
        public bool LimitTo_Melee { get; set; } = true;
        public bool LimitTo_Artifact { get; set; } = true;

        public bool LimitTo_Unique { get; set; } = true;
        public bool LimitTo_Rare { get; set; } = true;
        public bool LimitTo_Common { get; set; } = true;

        public bool? FilterBy_Gilded { get; set; } = null;
        public bool? FilterBy_Enchanted { get; set; } = null;

        public bool? FilterBy_RuneA { get; set; } = null;
        public bool? FilterBy_RuneC { get; set; } = null;
        public bool? FilterBy_RuneI { get; set; } = null;
        public bool? FilterBy_RuneO { get; set; } = null;
        public bool? FilterBy_RuneP { get; set; } = null;
        public bool? FilterBy_RuneR { get; set; } = null;
        public bool? FilterBy_RuneS { get; set; } = null;
        public bool? FilterBy_RuneT { get; set; } = null;
        public bool? FilterBy_RuneU { get; set; } = null;



        public bool Filter(Item item)
        {
            if (item.IsEquiped) return false;

            if (SearchByName)
            {
                if (SearchByName_UseIDs)
                {
                    if (!item.Type.Contains(SearchByName_Text)) return false;
                }
                else
                {
                    string name = StringLibrary.itemName(item.Type);
                    if (!name.Contains(SearchByName_Text)) return false;
                }

            }

            if (SearchByPowerLevel)
            {
                int val = SearchByPowerLevel_Value;
                int mode = Array.IndexOf(SearchByPowerLevel_Mode, true);
                switch (mode)
                {
                    case 0:
                        bool isInRange0 = item.Level <= val;
                        if (!isInRange0) return false;
                        break;
                    case 1:
                        bool isInRange1 = item.Level >= val;
                        if (!isInRange1) return false;
                        break;
                    case 2:
                        bool isInRange2 = item.Level == val;
                        if (!isInRange2) return false;
                        break;
                }
            }

            bool? Armor = TwoStateFilter(LimitTo_Armor, item.IsArmor);
            if (Armor.HasValue) return Armor.Value;

            bool? Artifact = TwoStateFilter(LimitTo_Artifact, item.IsArtifact);
            if (Artifact.HasValue) return Artifact.Value;

            bool? Ranged = TwoStateFilter(LimitTo_Ranged, item.IsRangedWeapon);
            if (Ranged.HasValue) return Ranged.Value;

            bool? Melee = TwoStateFilter(LimitTo_Melee, item.IsMeleeWeapon);
            if (Melee.HasValue) return Melee.Value;


            bool? Common = TwoStateFilter(LimitTo_Common, item.Rarity == Json.Enums.RarityEnum.Common);
            if (Common.HasValue) return Common.Value;

            bool? Rare = TwoStateFilter(LimitTo_Rare, item.Rarity == Json.Enums.RarityEnum.Rare);
            if (Rare.HasValue) return Rare.Value;

            bool? Unique = TwoStateFilter(LimitTo_Unique, item.Rarity == Json.Enums.RarityEnum.Unique);
            if (Unique.HasValue) return Unique.Value;


            bool? Enchanted = ThreeStateFilter(FilterBy_Enchanted, item.IsEnchanted);
            if (Enchanted.HasValue) return Enchanted.Value;

            bool? Gilded = ThreeStateFilter(FilterBy_Gilded, item.IsGilded);
            if (Gilded.HasValue) return Gilded.Value;

            bool? RuneA = RuneThreeState(FilterBy_RuneA, item.HasRune(Archetype.A));
            if (RuneA.HasValue) return RuneA.Value;

            bool? RuneC = RuneThreeState(FilterBy_RuneC, item.HasRune(Archetype.C));
            if (RuneC.HasValue) return RuneC.Value;

            bool? RuneI = RuneThreeState(FilterBy_RuneI, item.HasRune(Archetype.I));
            if (RuneI.HasValue) return RuneI.Value;

            bool? RuneO = RuneThreeState(FilterBy_RuneO, item.HasRune(Archetype.O));
            if (RuneO.HasValue) return RuneO.Value;

            bool? RuneP = RuneThreeState(FilterBy_RuneP, item.HasRune(Archetype.P));
            if (RuneP.HasValue) return RuneP.Value;

            bool? RuneR = RuneThreeState(FilterBy_RuneR, item.HasRune(Archetype.R));
            if (RuneR.HasValue) return RuneR.Value;

            bool? RuneS = RuneThreeState(FilterBy_RuneS, item.HasRune(Archetype.S));
            if (RuneS.HasValue) return RuneS.Value;

            bool? RuneT = RuneThreeState(FilterBy_RuneT, item.HasRune(Archetype.T));
            if (RuneT.HasValue) return RuneT.Value;

            bool? RuneU = RuneThreeState(FilterBy_RuneU, item.HasRune(Archetype.U));
            if (RuneU.HasValue) return RuneU.Value;


            return true;

            bool? RuneThreeState(bool? option, bool itemState)
            {
                switch (option)
                {
                    //Exclude
                    case false:
                        if (itemState) return null;
                        else return false;
                    //Include
                    case true:
                        if (itemState) return false;
                        else return null;
                    default:
                        return null;
                }
            }

            bool? TwoStateFilter(bool option, bool itemState)
            {
                switch (option)
                {
                    //Include
                    case true:
                        return null;
                    //Exclude
                    case false:
                        if (itemState) return false;
                        else return null;
                }
            }


            bool? ThreeStateFilter(bool? option, bool itemState)
            {
                switch (option)
                {
                    //Include
                    case true:
                        if (itemState) return null;
                        else return false;
                    //Exclude
                    case false:
                        if (itemState) return false;
                        else return null;
                    default:
                        return null;
                }
            }
        }
    }
}
