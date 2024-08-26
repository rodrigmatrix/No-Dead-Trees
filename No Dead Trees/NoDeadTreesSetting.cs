using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;
using Game.UI.Widgets;
using System.Collections.Generic;

namespace NoDeadTrees
{
    [FileLocation("ModsSettings/" + nameof(NoDeadTrees) + "/" + nameof(NoDeadTrees))]
    [SettingsUIGroupOrder(MAIN_GROUP)]
    [SettingsUIShowGroupName(MAIN_GROUP)]
    public class NoDeadTreesSetting : ModSetting
    {
        public const string MAIN_GROUP = "Main";

        public const string TREE_REPLACEMENT_TYPE_DROPDOWN = "Tree Replacement Type Dropdown Dropdown";

        public NoDeadTreesSetting(IMod mod) : base(mod)
        {

        }

        [SettingsUISection(MAIN_GROUP, TREE_REPLACEMENT_TYPE_DROPDOWN)]
        public DeadTreeReplacementType DeadTreeReplacementTypeDropdown { get; set; } = DeadTreeReplacementType.Elderly;

        public DropdownItem<int>[] GetIntDropdownItems()
        {
            var items = new List<DropdownItem<int>>();

            for (var i = 0; i < 2; i += 1)
            {
                items.Add(new DropdownItem<int>()
                {
                    value = i,
                    displayName = i.ToString(),
                });
            }

            return items.ToArray();
        }

        public override void SetDefaults()
        {
            throw new System.NotImplementedException();
        }

        public enum DeadTreeReplacementType
        {
            Teen,
            Adult,
            Elderly,
        }
    }

    public class LocaleEN : IDictionarySource
    {
        private readonly NoDeadTreesSetting m_Setting;
        public LocaleEN(NoDeadTreesSetting setting)
        {
            m_Setting = setting;
        }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "No Dead Trees" },
                { m_Setting.GetOptionTabLocaleID(NoDeadTreesSetting.MAIN_GROUP), "Settings" },
                
                { m_Setting.GetOptionLabelLocaleID(nameof(NoDeadTreesSetting.DeadTreeReplacementTypeDropdown)), "Tree replacement type" },
                { m_Setting.GetOptionDescLocaleID(nameof(NoDeadTreesSetting.DeadTreeReplacementTypeDropdown)), $"When a tree becomes dead, the selected type will replace all dead trees with this state." },

                { m_Setting.GetEnumValueLocaleID(NoDeadTreesSetting.DeadTreeReplacementType.Teen), "Teen" },
                { m_Setting.GetEnumValueLocaleID(NoDeadTreesSetting.DeadTreeReplacementType.Adult), "Adult" },
                { m_Setting.GetEnumValueLocaleID(NoDeadTreesSetting.DeadTreeReplacementType.Elderly), "Elderly" },

            };
        }

        public void Unload()
        {

        }
    }

    public class LocalePT : IDictionarySource
    {
        private readonly NoDeadTreesSetting m_Setting;
        public LocalePT(NoDeadTreesSetting setting)
        {
            m_Setting = setting;
        }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Substituir Árvores mortas" },
                { m_Setting.GetOptionTabLocaleID(NoDeadTreesSetting.MAIN_GROUP), "Configurações" },

                { m_Setting.GetOptionLabelLocaleID(nameof(NoDeadTreesSetting.DeadTreeReplacementTypeDropdown)), "Tipo de árvore de reposição" },
                { m_Setting.GetOptionDescLocaleID(nameof(NoDeadTreesSetting.DeadTreeReplacementTypeDropdown)), $"Quando uma árvore morre, o tipo selecionado substituirá todas as árvores mortas com este estado." },

                { m_Setting.GetEnumValueLocaleID(NoDeadTreesSetting.DeadTreeReplacementType.Teen), "Jovem" },
                { m_Setting.GetEnumValueLocaleID(NoDeadTreesSetting.DeadTreeReplacementType.Adult), "Maduro" },
                { m_Setting.GetEnumValueLocaleID(NoDeadTreesSetting.DeadTreeReplacementType.Elderly), "Ancião" },

            };
        }

        public void Unload()
        {

        }
    }
}
