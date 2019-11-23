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

        private bool _isPlaying;
        private TeamController _firstController;
        private TeamController _secondController;

        private Dictionary<int, int> _previousUnitIndexes = new Dictionary<int, int>(2);

        private TeamController _currentController;

        public event Action<int> GameOver;

        public static Game Instance
        {
            get { return _instance ?? (_instance = Object.FindObjectOfType<Game>()); }
        }

        public UnitController CurrentUnit { get; private set; }

        public event Action<UnitController> TurnPrepared;
        public event Action<UnitController> TurnStarted;

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
            _isPlaying = true;
            MapController.Instance.AllUnitsStopped += OnAllStopped;
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

            TurnPrepared?.Invoke(CurrentUnit);
        }

        public void StartTurn()
        {
            _currentController?.StartTurn(CurrentUnit);
            TurnStarted?.Invoke(CurrentUnit);
        }

        public void ActivateAbility()
        {
            _currentController?.CurrentUnit.Character.ActiveAbility.Apply(new CastContext
            {
                Caster = CurrentUnit
            });
        }

        public void Clear()
        {
            _isPlaying = false;
            MapController.Instance.NoMoreUnitsAtMap -= OnAllUnitsDead;
            MapController.Instance.AllUnitsStopped -= NextTurn;
            MapController.Instance.Clear();
            _previousUnitIndexes.Clear();
            _currentController = null;
            _firstController.Clear();
            _secondController.Clear();
        }

        private void OnAllUnitsDead(int player)
        {
            _isPlaying = false;
            MapController.Instance.WaitForAllScenarios(() =>
            {
                Clear();
                GameOver?.Invoke(player);
            });
        }

        private void OnAllStopped()
        {
            if (!_isPlaying)
            {
                return;
            }

            MapController.Instance.WaitForAllScenarios(() => Game.Instance.NextTurn());
        }
    }
}
