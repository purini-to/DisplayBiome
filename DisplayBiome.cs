using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DisplayBiome
{
    public class DisplayBiome : Mod
    {
    }

    public class BiomeInfoDisplay : InfoDisplay
    {
        private PropertyInfo[] propertyInfos;

        public override string Texture => "Terraria/Images/UI/InfoIcon_8";

        public override void SetStaticDefaults()
        {
            InfoName.SetDefault("Biome");

            var ignores = new string[] { "ZonePurity", "ZoneOverworldHeight", "ZoneRockLayerHeight", "ZoneSkyHeight", "ZoneRain", "ZoneUndergroundDesert", "ZoneSandstorm", "ZoneDirtLayerHeight", "ZoneWaterCandle", "ZonePeaceCandle", "ZoneGemCave", "ZoneOldOneArmy" };
            propertyInfos = Main.LocalPlayer.GetType().GetProperties();
            propertyInfos = Array.FindAll(propertyInfos, (p) => p.Name.StartsWith("Zone") && p.PropertyType.Name == "Boolean" && !ignores.Contains(p.Name));
        }

        public override bool Active()
        {
            return true;
        }

        public override string DisplayValue()
        {
            Player p = Main.LocalPlayer;

            // Vanila Biomes
            var values = new List<string>();
            foreach (var prop in propertyInfos)
            {
                if ((bool)prop.GetValue(p))
                {
                    var biome = replaceZonePrefix(prop.Name);
                    var text = getText(p, biome);
                    values.Add(text);
                }
            }

            // Mod Biomes
            var loader = LoaderManager.Get<BiomeLoader>();
            FieldInfo target = loader.GetType().GetField("list", BindingFlags.Instance | BindingFlags.NonPublic);
            if (target != null || typeof(IList).IsAssignableFrom(target.FieldType))
            {
                foreach(ModBiome biome in (List<ModBiome>)target.GetValue(loader))
                {
                    if (!biome.IsBiomeActive(p))
                    {
                        continue;
                    }
                    var text = biome.DisplayName.GetTranslation(Language.ActiveCulture);
                    values.Add(text);
                }
            }

            return string.Join(",", values);
        }

        private string replaceZonePrefix(string zoneString)
        {
            return zoneString.Replace("Zone", "");
        }

        private string getText(Player p, string biome)
        {
            var text = "";
            switch (biome)
            {
                case "Corrupt":
                    biome = "TheCorruption";
                    break;
                case "Beach":
                    biome = "Ocean";
                    break;
                case "Glowshroom":
                    text = Language.GetTextValue("TownNPCMoodBiomes.Mushroom");
                    break;
                case "Hive":
                    text = Language.GetTextValue("MapObject.BeeHive");
                    break;
                case "LihzhardTemple":
                    biome = "TheTemple";
                    break;
            }
            if (text == "")
            {
                var key = $"Bestiary_Biomes.{biome}";
                text = Language.GetTextValue(key);
                if (text.Equals(key))
                {
                    key = $"TownNPCMoodBiomes.{biome}";
                    text = Language.GetTextValue(key);
                    if (text.Equals(key))
                    {
                        key = $"GameUI.Layer{biome.Replace("Normal", "").Replace("Height", "")}";
                        text = Language.GetTextValue(key);
                    }
                }
            }
            return text;
        }
    }
}