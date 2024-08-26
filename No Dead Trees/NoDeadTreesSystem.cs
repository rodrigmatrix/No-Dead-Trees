using Game.Objects;
using Game;
using System;
using Unity.Collections;
using Unity.Entities;
using Colossal.Logging;
using Game.Common;
using Game.Tools;
using static NoDeadTrees.NoDeadTreesSetting;

namespace NoDeadTrees
{
    public partial class NoDeadTreesSystem : GameSystemBase
    {
        public static ILog log = LogManager.GetLogger($"{nameof(NoDeadTrees)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        EntityQuery _treesQuery;

        float time = 30f;
        float timeDelay = 30f;

        protected override void OnCreate()
        {
            base.OnCreate();
            _treesQuery = GetEntityQuery(new EntityQueryDesc()
            {
                All = [
                    ComponentType.ReadWrite<Tree>()
                ],
                None = [
                    ComponentType.ReadOnly<Deleted>(),
                    ComponentType.ReadOnly<Temp>()
                ]
            });
            RequireForUpdate(_treesQuery);
        }

        protected override void OnUpdate()
        {
            try
            {
                time = time + 1f * SystemAPI.Time.DeltaTime;
                if (time >= timeDelay)
                {
                    time = 0f;
                    var treeEntities = _treesQuery.ToEntityArray(Allocator.Temp);
                    TreeState treeState = Mod.m_Setting.DeadTreeReplacementTypeDropdown switch
                    {
                        DeadTreeReplacementType.Teen => TreeState.Teen,
                        DeadTreeReplacementType.Adult => TreeState.Adult,
                        DeadTreeReplacementType.Elderly => TreeState.Elderly,
                        _ => TreeState.Elderly
                    };
                    foreach (var entity in treeEntities)
                    {
                        Tree tree = EntityManager.GetComponentData<Tree>(entity);
                        if (tree.m_State == TreeState.Dead)
                        {
                            Tree newTree = new() { m_Growth = 0, m_State = treeState, };
                            EntityManager.SetComponentData(entity, newTree);
                        }
                    }
                    treeEntities.Dispose();
                }
            }
            catch (Exception ex)
            {
                log.Warn(ex.ToString());
            }
            
        }
    }
}