using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

[HarmonyPatch(typeof(Alert), "OnClick")]
public static class Alert_TatteredApparelTargetFix
{
  private static bool highlightDropButton = false;

  /// <summary>
  /// Overrides the tattered apparel alert to direct the player to the appropriate 
  /// action they can take. For a single pawn, we will direct them to that pawn's apparel
  /// page and highlight the drop gear icon.
  /// 
  /// For multiple pawns, we will take them to the drop all button on the assign page and highlight 
  /// that instead.
  /// 
  /// This patch's functionality will be reversible via a HugsLib option in settings since we are
  /// completely removing base functionality with the prefix.
  /// </summary>
  static bool Prefix(Alert __instance)
  {
    if (__instance is Alert_TatteredApparel ata)
    {
      Find.MainTabsRoot.SetCurrentTab(EzOutfitDefs.Assign);

      return false;
    }

    return true;
  }
}