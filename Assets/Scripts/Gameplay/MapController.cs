using System;
using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts
{
    public class MapController : Singleton<MapController>
    {
        private readonly Dictionary<int, List<UnitController>> unitsByPlayer = new Dictionary<int, List<UnitController>>();

        public event Action<int> NoMoreUnitsAtMap;

        public void AddUnit(UnitController unit)
        {
            List<UnitController> playerUnits;
            if (!unitsByPlayer.TryGetValue(unit.PlayerId, out playerUnits))
            {
                playerUnits = new List<UnitController>();
                unitsByPlayer[unit.PlayerId] = playerUnits;
            }
            playerUnits.Add(unit);
        }

        public void RemoveUnit(UnitController unit)
        {
            List<UnitController> playerUnits;
            if (!unitsByPlayer.TryGetValue(unit.PlayerId, out playerUnits))
            {
                return;
            }

            playerUnits.Remove(unit);
            if (playerUnits.Count == 0)
            {
                NoMoreUnitsAtMap?.Invoke(unit.PlayerId);
            }
        }

        public bool HasAvailableEnemy(UnitController askingUnit)
        {
            foreach (KeyValuePair<int, List<UnitController>> playerUnits in unitsByPlayer)
            {
                if (playerUnits.Key == askingUnit.PlayerId) continue;
                if (playerUnits.Value.Count > 0) return true;
            }

            return false;
        }

        public UnitController GetNearestEnemy(UnitController askingUnit)
        {
            float minSqrDistance = float.MaxValue;
            UnitController nearestUnit = null;
            var position = askingUnit.Position;
            foreach (KeyValuePair<int, List<UnitController>> playerUnits in unitsByPlayer)
            {
                if (playerUnits.Key == askingUnit.PlayerId) continue;
                foreach (UnitController unit in playerUnits.Value)
                {
                    float sqrDistance = (unit.Position - position).sqrMagnitude;
                    if (sqrDistance < minSqrDistance)
                    {
                        minSqrDistance = sqrDistance;
                        nearestUnit = unit;
                    }
                }
            }

            return nearestUnit;
        }

        public List<UnitController> GetEnemiesInArea(Vector2 position, float radius, int playerId)
        {
            float sqrRadius = radius * radius;
            List<UnitController> unitsInArea = new List<UnitController>();
            foreach (KeyValuePair<int, List<UnitController>> playerUnits in unitsByPlayer)
            {
                if (playerUnits.Key == playerId) continue;
                foreach (UnitController unit in playerUnits.Value)
                {
                    float sqrDistance = (unit.Position - position).sqrMagnitude;
                    if (sqrDistance < sqrRadius)
                    {
                        unitsInArea.Add(unit);
                    }
                }
            }

            return unitsInArea;
        }

        public void Clear()
        {
            foreach (KeyValuePair<int, List<UnitController>> playerUnits in unitsByPlayer)
            {
                foreach (UnitController unit in playerUnits.Value)
                {
                    UnitsPool.Instance.ReleaseUnit(unit);
                }
                playerUnits.Value.Clear();
            }
            unitsByPlayer.Clear();
        }

    }
}