using Verse;
using HarmonyLib;
using System.Reflection;
using HugsLib;
using HugsLib.Settings;

[EarlyInit]
public class EzOutfit : ModBase
{
    const bool DROP_TAINTED_FEATURE = false;
    const string MOD_PACKAGE = "Odder.Ez.Outfit";
    const float MOD_VERSION = 0.5f;

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
        var patched = string.Join(", ", harmony.GetPatchedMethods());
        Logger.Message($"Patched the following methods: '{patched}'");
    }

    public override void DefsLoaded()
    {
        base.DefsLoaded();

        ReadSettings();

#if V1_4
        if (ModsConfig.IsActive(ModSettings.AssignmentCopyPackageId.Value))
        {
            Logger.Warning("Assignment copy is detected, enabling UI adjustments!");
            CreateFromPawn.EnableAssignmentCopyAdjustment = true;
        }
#endif
    }

    public override void SettingsChanged()
    {
        base.SettingsChanged();

        ReadSettings();
    }

    private void ReadSettings()
    {
        ModSettings.TaintedDefault = GetSetting("TaintedDefault", TaintedOptions.USE_PAWN);
        ModSettings.AssignmentCopyPackageId = GetSetting(
            "AssignmentCopyPackageId",
            "Haecriver.OutfitCopy"
        );

        if (DROP_TAINTED_FEATURE)
        {
            ModSettings.DropAllIncludesTattered = GetSetting("DropAllIncludesTattered", true);
            ModSettings.DropAllIncludesTainted = GetSetting("DropAllIncludesTainted", true);
        }
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
