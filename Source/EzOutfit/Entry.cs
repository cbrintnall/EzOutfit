using Verse;
using HarmonyLib;
using System.Reflection;
using HugsLib;
using HugsLib.Settings;

[EarlyInit]
public class EzOutfit : ModBase
{
    const string MOD_PACKAGE = "OtterBee.Ez.Outfit";
    const float MOD_VERSION = 0.4f;

    public override string ModIdentifier => "EzOutfit";
    protected override bool HarmonyAutoPatch => false;

    EzOutfit() { }

    public override void EarlyInitialize()
    {
        base.EarlyInitialize();

        Logger.Message("Patching...");
        var harmony = new Harmony(MOD_PACKAGE);
        harmony.PatchAll(Assembly.GetExecutingAssembly());
        Logger.Message($"Done, {ModIdentifier} loaded version={MOD_VERSION}");
    }

    public override void DefsLoaded()
    {
        base.DefsLoaded();

        ReadSettings();

        if (ModsConfig.IsActive(ModSettings.AssignmentCopyPackageId.Value))
        {
            Logger.Warning("Assignment copy is detected, enabling UI adjustments!");
            // CreateFromPawn.EnableAssignmentCopyAdjustment = true;
        }
    }

    public override void SettingsChanged()
    {
        base.SettingsChanged();

        ReadSettings();
    }

    private void ReadSettings()
    {
        ModSettings.TaintedDefault = GetSetting("TaintedDefault", TaintedOptions.USE_PAWN);
        ModSettings.DropAllIncludesTattered = GetSetting("DropAllIncludesTattered", true);
        ModSettings.DropAllIncludesTainted = GetSetting("DropAllIncludesTainted", true);
        ModSettings.AssignmentCopyPackageId = GetSetting(
            "AssignmentCopyPackageId",
            "Haecriver.OutfitCopy"
        );
    }

    private SettingHandle<T> GetSetting<T>(string name, T defaultValue) =>
        Settings.GetHandle(
            name,
            $"{name}Title".Translate(),
            $"{name}Description".Translate(),
            defaultValue,
            enumPrefix: $"{name}_"
        );
}
