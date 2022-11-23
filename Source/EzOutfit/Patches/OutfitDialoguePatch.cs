using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using System.Linq;
using System.Collections.Generic;

[HarmonyPatch(typeof(Dialog_ManageOutfits), nameof(Dialog_ManageOutfits.DoWindowContents))]
static class Dialog_ManageOutfits_OutfitDialogue_Patch 
{
  static AccessTools.FieldRef<Dialog_ManageOutfits, Outfit> selectedOutfitRef = AccessTools.FieldRefAccess<Dialog_ManageOutfits, Outfit>("selOutfitInt");

  static void Postfix(Dialog_ManageOutfits __instance)
  {
    CreateFromPawn.DoButton(__instance);

    if (selectedOutfitRef(__instance) != null)
    {

    }
  }
}