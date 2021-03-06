using PakReader.Pak;
using PakReader.Parsers.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FModel
{
    public static class Res
    {
        public const string ParsingNotSupported = "Parsing of {0} types isn't supported yet.";
    }

    public class Globals
    {
        /// <summary>
        /// string is the pak file name
        /// PakFileReader is the reader where you can grab the FPakEntries, MountPoint and more
        /// </summary>
        public static readonly Dictionary<string, PakFileReader> CachedPakFiles = new Dictionary<string, PakFileReader>();
        public static bool bSearch = false; // trigger the event to select a file thank to the search window
        public static string sSearch = string.Empty; // this will be the file name triggered
        public static FGame Game = new FGame(EGame.Unknown, EPakVersion.LATEST);
        public const EFModel Build =
#if RELEASE
            EFModel.Release;
#elif DEBUG
            EFModel.Debug;
#else
            EFModel.Unknown;
#endif
    }

    public class FGame
    {
        public EGame ActualGame;
        public EPakVersion Version;

        public FGame(EGame game, EPakVersion version)
        {
            ActualGame = game;
            Version = version;
        }

        public string GetName()
        {
            return ActualGame switch
            {
                EGame.Fortnite => "Fortnite",
                EGame.Valorant => "Valorant",
                EGame.DeadByDaylight => "Dead By Daylight",
                EGame.Borderlands3 => "Borderlands 3",
                EGame.MinecraftDungeons => "Minecraft Dungeons",
                EGame.BattleBreakers => "Battle Breakers",
                EGame.Spellbreak => "Spellbreak",
                EGame.StateOfDecay2 => "State of Decay 2",
                EGame.TheCycleEA => "The Cycle (Early Access)",
                EGame.Unknown => "Unknown",
                _ => "Unknown",
            };
        }
    }

    static class FColors
    {
        public const string Red = "#E06C75";
        public const string Orange = "#D19A66";
        public const string Yellow = "#E5C07B";
        public const string Purple = "#C678DD";
        public const string Blue = "#61AFEF";
        public const string Discord = "#8B9BD4";
        public const string Green = "#98C379";
        public const string LightGray = "#BBBBBB";
        public const string DarkGray = "#9B9B9B";
        public const string White = "#EFEFEF";
    }

}
