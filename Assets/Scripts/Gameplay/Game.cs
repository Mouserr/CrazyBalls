using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.TeamControllers;
using Assets.Scripts.UI;
using Assets.Scripts.Units;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        private static Game _instance;
        
        private TeamController _firstController;
        private TeamController _secondController;

        private Dictionary<int, int> _previousUnitIndexes = new Dictionary<int, int>(2);

        private TeamController _currentController;

        public static Game Instance
        {
            get { return _instance ?? (_instance = Object.FindObjectOfType<Game>()); }
        }

        public UnitController CurrentUnit { get; private set; }

        public void PrepareGame(TeamController firstController, TeamController secondController)
        {
            _firstController = firstController;
            _secondController = secondController;
            _previousUnitIndexes[_firstController.PlayerId] = -1;
            _previousUnitIndexes[_secondController.PlayerId] = -1;
            _currentController = _secondController;
        }

        public void SetupUpTeam(List<Character> characters, int playerId)
        {
            foreach (var character in characters)
            {
                UnitsPool.Instance.AddUnitToMap(character, playerId);
            }
        }

        public void StartGame()
        {
            MapController.Instance.AllUnitsStopped += NextTurn;
            MapController.Instance.NoMoreUnitsAtMap += OnAllUnitsDead;
            NextTurn();
        }

        public void NextTurn()
        {
            if (_currentController == null)
            {
                return;
            }

            if (_currentController.PlayerId == _firstController.PlayerId)
            {
                _currentController = _secondController;
            }
            else
            {
                _currentController = _firstController;
            }

            var units = MapController.Instance.GetUnits(_currentController.PlayerId);
            var currentIndex = _previousUnitIndexes[_currentController.PlayerId] + 1;
            if (units.Count <= currentIndex)
            {
                currentIndex = 0;
            }

            CurrentUnit = units[currentIndex];
            _previousUnitIndexes[_currentController.PlayerId] = currentIndex;

            _currentController.StartTurn(CurrentUnit);
        }

        public void OnAllUnitsDead(int player)
        {
            Clear();
        }

        public void Clear()
        {
            MapController.Instance.NoMoreUnitsAtMap -= OnAllUnitsDead;
            MapController.Instance.AllUnitsStopped -= NextTurn;
            MapController.Instance.Clear();
            _previousUnitIndexes.Clear();
            _currentController = null;
            _firstController.Clear();
            _secondController.Clear();
        }
    }
}
