using EternalCycleServer;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Controllers;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers.Items;
using SPTarkov.Server.Core.Helpers.Profile;
using SPTarkov.Server.Core.Helpers.Server;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Spt.Tables;
using SPTarkov.Server.Core.Routers;
using SPTarkov.Server.Core.Services.Modding.Custom;
using SPTarkov.Server.Core.Services.Ragfair;
using SPTarkov.Server.Core.Utils;
using SPTarkov.Server.Core.Utils.Cloners;
using System.Reflection;
using WTTContentBackport.Commands;
using static EternalCycleServer.ContextManager;

namespace CyreneStory;
public record ModMetadata : IModMetadata
{
    public string ModGuid { get; init; } = "eft.hiddenhiragi.story";

    public string Name { get; init; } = "始源篇章";

    public string Author { get; init; } = "HiddenHiragi";

    public List<string>? Contributors { get; init; }

    public SemanticVersioning.Version Version { get; init; } = new("1.4.4");

    public SemanticVersioning.Range SptVersion { get; init; } = new("~4.1.0");

    public bool HasPrepatcher { get; init; } = false;

    public List<string>? Incompatibilities { get; init; }

    public Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; } = new()
{
    { "projectspark.hiddenhiragi.eternalcycleserver", new SemanticVersioning.Range(">=1.6.6") },
    { "com.wtt.contentbackport", new SemanticVersioning.Range(">=2.0.0") },
    { "com.manimal.labsboiler", new SemanticVersioning.Range(">=2.0.0") }
};

    public string? Url { get; init; } = "https://github.com/sp-tarkov/server-mod-examples";

    public string License { get; init; } = "MIT";
}

[Injectable(TypePriority = OnLoadOrder.Preload + 1)]

public class Core(
        CustomItemService customItemService,
        ModHelper modHelper,
        ItemHelper itemHelper,
        JsonUtil jsonUtil,
        ICloner cloner,
        ConfigServer configServer,
        ImageRouter imageRouter,
        PresetHelper presetHelper,
        RagfairOfferService ragfairOfferService,
        RagfairController ragfairController,
        TemplateTable templateTable,
        LocaleTable localeTable,
        GlobalTable globalTable,
        TradersTable tradersTable,
        HideoutTable hideoutTable,
        LocationTable locationTable,
        BotTable botTable,
        HandbookHelper handbookHelper
        ) // We inject a logger for use inside our class, it must have the class inside the diamond <> brackets
    : IOnLoad // Implement the IOnLoad interface so that this mod can do something on server load
{
    public static string modPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data/");
    public Task OnLoadAsync(CancellationToken cancellationToken)
    {
        var databaseService = new DatabaseService(templateTable, localeTable, globalTable, tradersTable, hideoutTable, locationTable, botTable);
        var context = new LoadModContext
        {
            DB = databaseService,
            JsonUtil = jsonUtil,
            ConfigServer = configServer,
            ModHelper = modHelper,
            Logger = Utils.commonLogger,
            PresetHelper = presetHelper,
            ImageRouter = imageRouter,
            ItemHelper = itemHelper,
            Cloner = cloner
        };

        Utils.commonLogger.Error("这不是一个报错，只是一条日志。");
        Utils.commonLogger.Error("如果你把这条日志当做报错发到群内反馈，你可能会被我攻击、禁言或移出群聊。");
        Utils.commonLogger.Error("截图本条日志后，我将默认你已经接受以上后果。");

        new EndingsCommandPatch().Enable();

        var creator = "<color=#1E90FF>始源篇章</color>";
        TraderUtils.RegisterTrader(modPath, "trader/anastasia.jsonc", "res/avatar/", creator, creator);

        QuestUtils.RegisterQuest(modPath, "quest/chapter1.json", "res/questimage/");
        QuestZoneUtils.RegisterQuestZones(modPath, "quest/chapter1zone.json");
        QuestUtils.RegisterQuestLogicTree(modPath, "quest/chapter1logic.json");
        LocaleUtils.RegisterQuestLocale(modPath, "locale/chapter1/", creator, creator);

        QuestUtils.RegisterQuest(modPath, "quest/chapter2.json", "res/questimage/");
        QuestZoneUtils.RegisterQuestZones(modPath, "quest/chapter2zone.json");
        QuestUtils.RegisterQuestLogicTree(modPath, "quest/chapter2logic.json");
        LocaleUtils.RegisterQuestLocale(modPath, "locale/chapter2/", creator, creator);

        QuestUtils.RegisterQuest(modPath, "quest/chapter3.json", "res/questimage/");
        QuestZoneUtils.RegisterQuestZones(modPath, "quest/chapter3zone.json");
        QuestUtils.RegisterQuestLogicTree(modPath, "quest/chapter3logic.json");
        LocaleUtils.RegisterQuestLocale(modPath, "locale/chapter3/", creator, creator);

        QuestUtils.RegisterQuest(modPath, "quest/chapter4.json", "res/questimage/");
        QuestZoneUtils.RegisterQuestZones(modPath, "quest/chapter4zone.json");
        QuestUtils.RegisterQuestLogicTree(modPath, "quest/chapter4logic.json");
        LocaleUtils.RegisterQuestLocale(modPath, "locale/chapter4/", creator, creator);

        QuestUtils.RegisterQuest(modPath, "quest/episode1.json", "res/questimage/");
        QuestZoneUtils.RegisterQuestZones(modPath, "quest/episode1zone.json");
        QuestUtils.RegisterQuestLogicTree(modPath, "quest/episode1logic.json");
        LocaleUtils.RegisterQuestLocale(modPath, "locale/episode1/", creator, creator);

        QuestUtils.RegisterQuest(modPath, "quest/episode2.json", "res/questimage/");
        QuestZoneUtils.RegisterQuestZones(modPath, "quest/episode2zone.json");
        QuestUtils.RegisterQuestLogicTree(modPath, "quest/episode2logic.json");
        LocaleUtils.RegisterQuestLocale(modPath, "locale/episode2/", creator, creator);

        QuestUtils.RegisterQuest(modPath, "quest/episode3.json", "res/questimage/");
        QuestZoneUtils.RegisterQuestZones(modPath, "quest/episode3zone.json");
        QuestUtils.RegisterQuestLogicTree(modPath, "quest/episode3logic.json");
        LocaleUtils.RegisterQuestLocale(modPath, "locale/episode3/", creator, creator);

        QuestUtils.RegisterQuest(modPath, "quest/ticket.json", "res/questimage/");
        QuestZoneUtils.RegisterQuestZones(modPath, "quest/ending_zone.json");
        QuestUtils.RegisterQuestLogicTree(modPath, "quest/ticketlogic.json");
        LocaleUtils.RegisterQuestLocale(modPath, "locale/ticket/", creator, creator);

        QuestUtils.RegisterQuest(modPath, "quest/ending.json", "res/questimage/");
        QuestUtils.RegisterQuestLogicTree(modPath, "quest/ending_logic.json");
        LocaleUtils.RegisterQuestLocale(modPath, "locale/ending/", creator, creator);

        ItemUtils.RegisterItem(modPath, "items.json", creator, creator);
        AchievementUtils.RegisterAchievement(modPath, "achievement.json", "res/achievement/");
        AssortUtils.RegisterAssort(modPath, "assort.json");
        RecipeUtils.RegisterRecipe(modPath, "recipe.json");
        LocaleUtils.RegisterLocaleText(modPath, "locale/text/");

        CustomizationUtils.RegisterCustomization(modPath, "customization.json", "res/deco/");
        CustomizationUtils.RegisterHideoutCustomization(modPath, "hideoutcustom.json");

        //RagfairLoadPatch

        ELocationType aaaa = ELocationType.Custom;

        //商人锁和任务锁待做

        EventManager.DataLoadEvent.FixItemCompatibleEvent += (LoadModContext context) =>
        {
            context.DB.GetTrader(Traders.THERAPIST).Base.UnlockedByDefault = false;
            context.DB.GetTrader(Traders.SKIER).Base.UnlockedByDefault = false;
            context.DB.GetTrader(Traders.MECHANIC).Base.UnlockedByDefault = false;
            context.DB.GetTrader(Traders.PRAPOR).Base.UnlockedByDefault = false;
            context.DB.GetTrader(Traders.PEACEKEEPER).Base.UnlockedByDefault = false;
        };

        EventManager.DataLoadEvent.LoadQuestDataEvent += (LoadModContext context) =>
        {
            var quest = QuestUtils.GetQuest(QuestTpl.NETWORK_PROVIDER_PART_1, context);
            if (quest != null)
            {
                quest.Conditions.AvailableForStart?.Clear();
                QuestUtils.InitCompleteQuestDataConditions(quest.Conditions.AvailableForStart, new CompleteQuestData
                {
                    Id = "网络供应商1前置".ConvertHashID(),
                    QuestId = "Batya4".ConvertHashID(),
                    QuestStatus = 4, //Started
                }, context);
            }
        };

        EventManager.DataLoadEvent.LoadAchievementEvent += (LoadModContext context) =>
        {
            AchievementUtils.GetAchievement("694c60b50cb1e6ad639a5723", context.DB).Rewards = new List<Reward>();
        };

        EventManager.DataLoadEvent.FixItemCompatibleEvent += (LoadModContext context) => ItemUtils.GetItem(ItemTpl.KEY_OBSERVATION_ROOM, context)?.Properties?.MaximumNumberOfUsage = 0;

        return Task.CompletedTask;
    }
}
