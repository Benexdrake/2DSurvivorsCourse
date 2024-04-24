using Godot;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

public partial class MetaProgression : Node
{
    [Export] private MetaUpgrade[] _metaUpgradeArray;
    public List<MetaUpgrade> MetaUpgrades { get; set; } = new();

    public override void _Ready()
    {
        GameEvents.ExperienceVialCollection += HandeExperienceVialDCollection;
        Init();
        Load();
        GameStats.MetaUpgradeCurrency = 1000;
    }

    private void Init()
    {
        if(FileAccess.FileExists(GameConstants.SAVE_FILE_PATH))
            return;
        MetaUpgrades = new();
        foreach (var metaUpgrade in _metaUpgradeArray)
        {
            var m = new MetaUpgrade()
            {
                Id =metaUpgrade.Id,
                MaxQuantity = metaUpgrade.MaxQuantity,
                Quantity = metaUpgrade.Quantity,
                Title= metaUpgrade.Title,
                Description = metaUpgrade.Description,
                ExperienceCost = metaUpgrade.ExperienceCost
            };
            MetaUpgrades.Add(m);
        }
        Save();
    }

    private void Load()
    {
        MetaUpgrades = new();
        if(!FileAccess.FileExists(GameConstants.SAVE_FILE_PATH))
        {
            Save();
            return;
        }

        using var file = FileAccess.Open(GameConstants.SAVE_FILE_PATH, FileAccess.ModeFlags.Read);
        var json = file.GetPascalString();
        
        dynamic save = JsonConvert.DeserializeObject(json);

        GameStats.MetaUpgradeCurrency = save.MetaUpgradeCurrency;

        foreach (var metaUpgrade in save.MetaUpgrades)
        {
            var m = new MetaUpgrade()
            {
                Id =metaUpgrade.Id,
                MaxQuantity = metaUpgrade.MaxQuantity,
                Quantity = metaUpgrade.Quantity,
                Title= metaUpgrade.Title,
                Description = metaUpgrade.Description,
                ExperienceCost = metaUpgrade.ExperienceCost
            };
            MetaUpgrades.Add(m);
        }
    }

    public void Save()
    {
        using var file = FileAccess.Open(GameConstants.SAVE_FILE_PATH, FileAccess.ModeFlags.Write);

        var data = new{GameStats.MetaUpgradeCurrency, MetaUpgrades};

        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        file.StorePascalString(json);      
    }

    private void HandeExperienceVialDCollection(int number)
    {
        GameStats.MetaUpgradeCurrency += number;
        Save();
    }

    public void AddMetaUpgrade(MetaUpgrade upgrade)
    {
        var upgradeInSave = MetaUpgrades.FirstOrDefault(x => x.Id.Equals(upgrade.Id));
        if (upgradeInSave != null)
            upgradeInSave.Quantity++;
        Save();
    }

    public int GetUpgradeCount(string id)
    {
        var meta = MetaUpgrades.FirstOrDefault(x => x.Id.Equals(id));
        if (meta != null)
            return meta.Quantity;
        return 0;
    }

    public override void _ExitTree()
    {
        GameEvents.ExperienceVialCollection -= HandeExperienceVialDCollection;
    }
}
