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
}