using HugsLib;
using HugsLib.Settings;

public enum TaintedOptions
{
    YES,
    NO,
    USE_PAWN
}

public static class ModSettings
{
    public static SettingHandle<TaintedOptions> TaintedDefault;
    public static SettingHandle<bool> DropAllIncludesTattered;
    public static SettingHandle<bool> DropAllIncludesTainted;
    public static SettingHandle<string> AssignmentCopyPackageId;
}
