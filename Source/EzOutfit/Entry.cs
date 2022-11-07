using Verse;
using HarmonyLib;
using System.Reflection;

[StaticConstructorOnStartup]
public static class EzOutfit
{
  const string MOD_PACKAGE = "OtterBee.Ez.Outfit";

  static EzOutfit()
  {
    Log.Message("Patching...");
    var harmony = new Harmony(MOD_PACKAGE);
    harmony.PatchAll(Assembly.GetExecutingAssembly());
    Log.Message("Done");
  }
}