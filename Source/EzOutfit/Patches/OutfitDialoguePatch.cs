using HarmonyLib;
using RimWorld;


#if V1_4
[HarmonyPatch(typeof(Dialog_ManageOutfits), nameof(Dialog_ManageOutfits.DoWindowContents))]
static class Dialog_ManageOutfits_OutfitDialogue_Patch 
{
  static AccessTools.FieldRef<Dialog_ManageOutfits, Outfit> selectedOutfitRef = AccessTools.FieldRefAccess<Dialog_ManageOutfits, Outfit>("selOutfitInt");

  static void Postfix(Dialog_ManageOutfits __instance)
  {
    CreateFromPawn.DoButton(__instance);
  }
}
#endif