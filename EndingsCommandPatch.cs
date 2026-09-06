using EternalCycleServer;
using HarmonyLib;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SPTarkov.DI.Annotations;
using SPTarkov.Reflection.Patching;
using SPTarkov.Server.Core.Constants;
using SPTarkov.Server.Core.Controllers;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Generators;
using SPTarkov.Server.Core.Generators.Loot;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Helpers.Items;
using SPTarkov.Server.Core.Helpers.Profile;
using SPTarkov.Server.Core.Helpers.Server;
using SPTarkov.Server.Core.Helpers.Traders;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Bot;
using SPTarkov.Server.Core.Models.Eft.Dialog;
using SPTarkov.Server.Core.Models.Eft.Profile;
using SPTarkov.Server.Core.Models.Spt.Tables;
using SPTarkov.Server.Core.Routers;
using SPTarkov.Server.Core.Utils;
using SPTarkov.Server.Core.Utils.Cloners;
using System;
using System.Net;
using System.Reflection;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Json;
using WTTContentBackport.Commands;
using static EternalCycleServer.ContextManager;

namespace CyreneStory
{
    [Injectable]
    public class EndingsCommandPatch : AbstractPatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(EndingsCommand).GetMethod("PerformAction", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        }
        [PatchPrefix]
        public static bool Prefix(EndingsCommand __instance, UserDialogInfo commandHandler, MongoId sessionId, SendMessageRequest request, ref ValueTask<string> __result)
        {
            __result =  new ValueTask<string>(request.DialogId);
            return false;
        }
    }
}