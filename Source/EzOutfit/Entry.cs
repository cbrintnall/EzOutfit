using Verse;
using HarmonyLib;
using System.Reflection;
using HugsLib;
using HugsLib.Settings;

[EarlyInit]
public class EzOutfit : ModBase
{
  const string MOD_PACKAGE = "OtterBee.Ez.Outfit";
  public override string ModIdentifier => "EzOutfit";
  protected override bool HarmonyAutoPatch => false;

  EzOutfit()
  { }

  public override void EarlyInitialize()
  {
    base.EarlyInitialize();

    Logger.Message("Patching...");
    var harmony = new Harmony(MOD_PACKAGE);
    harmony.PatchAll(Assembly.GetExecutingAssembly());
    Logger.Message("Done");
  }

  public override void DefsLoaded()
  {
    base.DefsLoaded();

    ModSettings.TaintedDefault = GetSetting("TaintedDefault", TaintedOptions.USE_PAWN);
    ModSettings.DropAllIncludesTattered = GetSetting("DropAllIncludesTattered", true);
    ModSettings.DropAllIncludesTainted = GetSetting("DropAllIncludesTainted", true);
  }

  public override void SettingsChanged()
  {
    base.SettingsChanged();

    ModSettings.TaintedDefault = GetSetting("TaintedDefault", TaintedOptions.USE_PAWN);
    ModSettings.DropAllIncludesTattered = GetSetting("DropAllIncludesTattered", true);
    ModSettings.DropAllIncludesTainted = GetSetting("DropAllIncludesTainted", true);
  }

  private SettingHandle<T> GetSetting<T>(string name, T defaultValue) => Settings.GetHandle(name, $"{name}Title".Translate(), $"{name}Description".Translate(), defaultValue, enumPrefix: $"{name}_");
}