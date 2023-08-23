using System;
using System.Collections.Generic;
using AmongUs.GameOptions;
using SuperNewRoles.Buttons;
using SuperNewRoles.Mode;
using SuperNewRoles.Patches;
using UnityEngine;
using static SuperNewRoles.Modules.CustomOption;
using static SuperNewRoles.Modules.CustomOptionHolder;
using static SuperNewRoles.Roles.RoleClass;

namespace SuperNewRoles.Roles.Impostor;

class EvilSeer
{
    internal static class CustomOptionData
    {
        public static CustomRoleOption EvilSeerOption;
        public static CustomOption EvilSeerPlayerCount;
        public static CustomOption EvilSeerMode;
        public static CustomOption EvilSeerLimitSoulDuration;
        public static CustomOption EvilSeerSoulDuration;
        public static CustomOption EvilSeerIsUniqueSetting;
        public static CustomOption EvilSeerIsFlashBodyColor;
        public static CustomOption EvilSeerIsReportingBodyColorName;
        public static CustomOption EvilSeerFlashColorMode;
        public static CustomOption EvilSeerIsCrewSoulColor;
        public static CustomOption EvilSeerMadmateSetting;

        internal static void SetupCustomOptions()
        {
            EvilSeerOption = SetupCustomRoleOption(201900, true, RoleId.EvilSeer);
            EvilSeerPlayerCount = Create(201901, true, CustomOptionType.Impostor, "SettingPlayerCountName", ImpostorPlayers[0], ImpostorPlayers[1], ImpostorPlayers[2], ImpostorPlayers[3], EvilSeerOption);
            EvilSeerMode = Create(201902, false, CustomOptionType.Impostor, "SeerMode", new string[] { "SeerModeBoth", "SeerModeFlash", "SeerModeSouls" }, EvilSeerOption);
            EvilSeerLimitSoulDuration = Create(201903, false, CustomOptionType.Impostor, "SeerLimitSoulDuration", false, EvilSeerOption);
            EvilSeerSoulDuration = Create(201904, false, CustomOptionType.Impostor, "SeerSoulDuration", 15f, 0f, 120f, 5f, EvilSeerLimitSoulDuration, format: "unitCouples");
            EvilSeerIsUniqueSetting = Create(201906, false, CustomOptionType.Impostor, "EvilSeerIsUniqueSetting", true, EvilSeerOption);
            EvilSeerIsFlashBodyColor = Create(201907, false, CustomOptionType.Impostor, "EvilSeerIsFlashColor", true, EvilSeerIsUniqueSetting);
            EvilSeerIsReportingBodyColorName = Create(201908, false, CustomOptionType.Impostor, "EvilSeerIsReportingBodyColorName", true, EvilSeerIsFlashBodyColor);
            EvilSeerFlashColorMode = Create(201909, false, CustomOptionType.Impostor, "EvilSeerFlashColorMode", new string[] { "EvilSeerColorModeclear", "EvilSeerColorModeLightAndDark" }, EvilSeerIsFlashBodyColor);
            EvilSeerIsCrewSoulColor = Create(201910, false, CustomOptionType.Impostor, "EvilSeerIsCrewSoulColor", true, EvilSeerIsUniqueSetting);
            EvilSeerMadmateSetting = Create(201905, false, CustomOptionType.Impostor, "CreateMadmateSetting", false, EvilSeerOption);
        }
    }

    internal static class RoleData
    {
        public static List<PlayerControl> EvilSeerPlayer;
        public static Color32 color = ImpostorRed;
        public static List<(Vector3, int)> deadBodyPositions;

        public static float soulDuration;
        public static bool limitSoulDuration;
        public static int mode;
        public static bool IsUniqueSetting;
        public static int FlashColorMode;
        public static bool IsCreateMadmate;
        public static void ClearAndReload()
        {
            EvilSeerPlayer = new();
            deadBodyPositions = new();
            limitSoulDuration = CustomOptionData.EvilSeerLimitSoulDuration.GetBool();
            soulDuration = CustomOptionData.EvilSeerSoulDuration.GetFloat();
            mode = ModeHandler.IsMode(ModeId.SuperHostRoles) ? 1 : CustomOptionData.EvilSeerMode.GetSelection();
            IsUniqueSetting = !ModeHandler.IsMode(ModeId.SuperHostRoles) && CustomOptionData.EvilSeerIsUniqueSetting.GetBool();
            FlashColorMode = CustomOptionData.EvilSeerFlashColorMode.GetSelection();
            IsCreateMadmate = CustomOptionData.EvilSeerMadmateSetting.GetBool();
        }
    }
}