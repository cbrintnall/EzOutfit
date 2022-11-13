using Verse;
using HarmonyLib;
using System.Reflection;
using HugsLib;

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

    ModSettings.TaintedDefault = Settings.GetHandle("TaintedDefault", "TaintedDefaultTitle".Translate(), "TaintedDefaultDescription".Translate(), TaintedOptions.USE_PAWN, enumPrefix: "TaintedDefault_");
  }

  public override void SettingsChanged()
  {
    base.SettingsChanged();

    ModSettings.TaintedDefault = Settings.GetHandle("TaintedDefault", "TaintedDefaultTitle".Translate(), "TaintedDefaultDescription".Translate(), TaintedOptions.USE_PAWN, enumPrefix: "TaintedDefault_");
  }
}