﻿using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace SuperNewRoles.Patch
{
    class ResetDeviceCooldown
    {
        [HarmonyPatch]
        public static void Postfix()
        {
            SuperNewRoles.Patch.AdminPatch.ClearAndReload();
            SuperNewRoles.Patch.CameraPatch.ClearAndReload();
            SuperNewRoles.Patch.VitalsPatch.ClearAndReload();
        }
    }
}
