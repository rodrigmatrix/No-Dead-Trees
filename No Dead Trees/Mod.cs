using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using NoDeadTrees;

namespace NoDeadTrees
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(NoDeadTrees)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        public static NoDeadTreesSetting m_Setting;

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            m_Setting = new NoDeadTreesSetting(this);
            m_Setting.RegisterInOptionsUI();
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));
            GameManager.instance.localizationManager.AddSource("pt-BR", new LocalePT(m_Setting));


            AssetDatabase.global.LoadSettings(nameof(NoDeadTrees), m_Setting, new NoDeadTreesSetting(this));
            updateSystem.UpdateAt<NoDeadTreesSystem>(SystemUpdatePhase.PrefabUpdate);
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }
    }
}
