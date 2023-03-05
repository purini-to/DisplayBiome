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
        public override string Texture => "Terraria/Images/UI/InfoIcon_8";

        public override void SetStaticDefaults()
        {
            InfoName.SetDefault("Biome");
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
            var zoneInfected = p.ZoneCorrupt || p.ZoneCrimson || p.ZoneHallow;
            if (p.ZoneCorrupt)
            {
                var isNormal = true;
                if (p.ZoneDesert)
                {
                    if (p.ZoneUndergroundDesert)
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.CorruptUndergroundDesert"));
                        isNormal = false;
                    }
                    else
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.CorruptDesert"));
                        isNormal = false;
                    }
                }
                if (p.ZoneSnow && p.ZoneRockLayerHeight)
                {
                    values.Add(Language.GetTextValue("Bestiary_Biomes.CorruptIce"));
                    isNormal = false;
                }
                if (isNormal)
                {
                    if (p.ZoneRockLayerHeight)
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.UndergroundCorruption"));
                    } else
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.TheCorruption"));
                    }
                }
            }
            if (p.ZoneCrimson)
            {
                var isNormal = true;
                if (p.ZoneDesert)
                {
                    if (p.ZoneUndergroundDesert)
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.CrimsonUndergroundDesert"));
                        isNormal = false;
                    }
                    else
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.CrimsonDesert"));
                        isNormal = false;
                    }
                }
                if (p.ZoneSnow && p.ZoneRockLayerHeight)
                {
                    values.Add(Language.GetTextValue("Bestiary_Biomes.CrimsonIce"));
                    isNormal = false;
                }
                if (isNormal)
                {
                    if (p.ZoneRockLayerHeight)
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.UndergroundCrimson"));
                    }
                    else
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.Crimson"));
                    }
                }
            }
            if (p.ZoneHallow)
            {
                var isNormal = true;
                if (p.ZoneDesert)
                {
                    if (p.ZoneUndergroundDesert)
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.HallowUndergroundDesert"));
                        isNormal = false;
                    }
                    else
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.HallowDesert"));
                        isNormal = false;
                    }
                }
                if (p.ZoneSnow && p.ZoneRockLayerHeight)
                {
                    values.Add(Language.GetTextValue("Bestiary_Biomes.HallowIce"));
                    isNormal = false;
                }
                if (isNormal)
                {
                    if (p.ZoneRockLayerHeight)
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.UndergroundHallow"));
                    }
                    else
                    {
                        values.Add(Language.GetTextValue("Bestiary_Biomes.TheHallow"));
                    }
                }
            }
            if (p.ZoneSnow && !zoneInfected)
            {
                if (p.ZoneRockLayerHeight)
                {
                    values.Add(Language.GetTextValue("Bestiary_Biomes.UndergroundSnow"));
                }
                else
                {
                    values.Add(Language.GetTextValue("Bestiary_Biomes.Snow"));
                }
            }
            if (p.ZoneDesert && !zoneInfected)
            {
                if (p.ZoneUndergroundDesert)
                {
                    values.Add(Language.GetTextValue("Bestiary_Biomes.UndergroundDesert"));
                }
                else
                {
                    values.Add(Language.GetTextValue("Bestiary_Biomes.Desert"));
                }
            }
            if (p.ZoneJungle && !zoneInfected)
            {
                if (p.ZoneRockLayerHeight)
                {
                    values.Add(Language.GetTextValue("Bestiary_Biomes.UndergroundJungle"));
                }
                else
                {
                    values.Add(Language.GetTextValue("Bestiary_Biomes.Jungle"));
                }
            }
            if (p.ZoneBeach)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.Ocean"));
            }
            if (p.ZoneDungeon)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.TheDungeon"));
            }
            if (p.ZoneGlowshroom)
            {
                values.Add(Language.GetTextValue("TownNPCMoodBiomes.Mushroom"));
            }
            if (p.ZoneTowerSolar)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.SolarPillar"));
            }
            if (p.ZoneTowerVortex)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.VortexPillar"));
            }
            if (p.ZoneTowerNebula)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.NebulaPillar"));
            }
            if (p.ZoneTowerStardust)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.StardustPillar"));
            }
            if (p.ZoneForest)
            {
                values.Add(Language.GetTextValue("TownNPCMoodBiomes.Forest"));
            }
            if (p.ZoneSkyHeight)
            {
                values.Add(Language.GetTextValue("GameUI.LayerSpace"));
            }
            if (p.ZoneUnderworldHeight)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.TheUnderworld"));
            }
            if (p.ZoneGranite)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.Granite"));
            }
            if (p.ZoneMarble)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.Marble"));
            }
            if (p.ZoneHive)
            {
                values.Add(Language.GetTextValue("MapObject.BeeHive"));
            }
            if (p.ZoneLihzhardTemple)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.TheTemple"));
            }
            if (p.ZoneGraveyard)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.Graveyard"));
            }
            if (p.ZoneRain)
            {
                values.Add(Language.GetTextValue("GameUI.Rain"));
            }
            if (p.ZoneSandstorm)
            {
                values.Add(Language.GetTextValue("GameUI.Sandstorm"));
            }
            if (p.ZoneMeteor)
            {
                values.Add(Language.GetTextValue("Bestiary_Biomes.Meteor"));
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
    }
}