using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using System.Linq;
using System.Collections.Generic;

[HarmonyPatch(typeof(Dialog_ManageOutfits))]
[HarmonyPatch(nameof(Dialog_ManageOutfits.DoWindowContents))]
static class Dialog_ManageOutfits_OutfitDialogue_Patch 
{
  static void Postfix(Dialog_ManageOutfits __instance)
  {
    CreateFromPawn.DoButton(__instance);
    ExtrasButton.DoButton(__instance);
  }
}