using System;
using System.Reflection;
using UnityModManagerNet;
using HarmonyLib;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SolastaCustomMerchants
{
    public class Main
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public static void Log(string msg)
        {
            if (logger != null) logger.Log(msg);
        }

        public static void Error(Exception ex)
        {
            if (logger != null) logger.Error(ex.ToString());
        }

        public static void Error(string msg)
        {
            if (logger != null) logger.Error(msg);
        }

        public static UnityModManager.ModEntry.ModLogger logger;
        public static bool enabled;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                logger = modEntry.Logger;
                var harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Error(ex);
                throw;
            }
            return true;
        }

        [HarmonyPatch(typeof(MainMenuScreen), "RuntimeLoaded")]
        static class MainMenuScreen_RuntimeLoaded_Patch
        {
            static void Postfix()
            {
                JObject merchants = JObject.Parse(File.ReadAllText(UnityModManager.modsPath + @"/SolastaCustomMerchants/Merchants.json"));
                foreach(var merchant in merchants)
                {
                    MerchantDefinition merchantDefinition;
                    try
                    {
                        merchantDefinition = DatabaseRepository.GetDatabase<MerchantDefinition>().GetElement(merchant.Key);
                        Log("Found merchant: " + merchant.Key);
                    }
                    catch
                    {
                        Log("Merchant not found: " + merchant.Key);
                        continue;
                    }
                    foreach(var item in (JObject)merchant.Value)
                    {
                        ItemDefinition itemDefinition;
                        try
                        {
                            itemDefinition = DatabaseRepository.GetDatabase<ItemDefinition>().GetElement(item.Key);
                            Log("Found item: " + item.Key);
                            var stockUnitDescription = new StockUnitDescription();
                            AccessTools.Field(stockUnitDescription.GetType(), "itemDefinition").SetValue(stockUnitDescription, itemDefinition);
                            foreach (var attribute in (JObject)item.Value)
                            {
                                string aString = attribute.Value.ToString();
                                try
                                {
                                    if (int.TryParse(aString, out int aNumber)) {
                                        AccessTools.Field(stockUnitDescription.GetType(), attribute.Key).SetValue(stockUnitDescription, aNumber); 
                                    } else
                                    {
                                        AccessTools.Field(stockUnitDescription.GetType(), attribute.Key).SetValue(stockUnitDescription, aString);
                                    }
                                    Log("Found attribute: " + attribute.Key + ", Set to:" + aString);
                                }
                                catch
                                {
                                    Log("Attribute not found: " + aString);
                                }
                            }
                        }
                        catch
                        {
                            Log("Item not found: " + item.Key);
                            continue;
                        }
                    }
                }
            }
        }
    }
}