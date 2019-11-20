using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class UnitsPool : Singleton<UnitsPool>
    {
        private readonly Dictionary<UnitType, GameObjectPool<UnitController>> pools = new Dictionary<UnitType, GameObjectPool<UnitController>>();

        public void Register(UnitType unitType, UnitController prefab, int startCount)
        {
            if (!pools.ContainsKey(unitType) && prefab != null)
            {
                pools[unitType] = new GameObjectPool<UnitController>(gameObject.AddChild(unitType.ToString()), prefab, startCount);
            }
        }

        public UnitController GetUnitPrefab(UnitType unitType)
        {
            GameObjectPool<UnitController> unitPool;
            if (!pools.TryGetValue(unitType, out unitPool))
            {
                Debug.LogError(string.Format("Can't find pool for {0}", unitType));
                return null;
            }

            return unitPool.GetObject();
        }

        public UnitController AddUnitToMap(Character character, int playerId)
        {
            UnitController unitController = GetUnitPrefab(character.UnitType);
            unitController.SetCharacter(character, playerId);
            MapController.Instance.AddUnit(unitController);

            return unitController;
        }


        public void ReleaseUnit(UnitController unitController)
        {
            GameObjectPool<UnitController> unitPool;
            if (!pools.TryGetValue(unitController.Character.UnitType, out unitPool))
            {
                Debug.LogError(string.Format("Can't find pool for {0}", unitController.Character.UnitType));
                return;
            }
            unitController.Clear();
            unitPool.ReleaseObject(unitController);
        }

        public void Clear()
        {
            foreach (KeyValuePair<UnitType, GameObjectPool<UnitController>> gameObjectPool in pools)
            {
                gameObjectPool.Value.ClearPull();
            }
            pools.Clear();
        }
    }
}