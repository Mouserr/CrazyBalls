using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Configs;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using Assets.Scripts.TeamControllers;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class BattleScreen : AbstractScreen
    {
        public PlayerController Player;
        // TMP
        [SerializeField]
        private List<CharacterData> _playerUnits;
        // TMP
        [SerializeField]
        private List<CharacterData> _mobs;

        [SerializeField]
        private List<UICharacterAbility> _abilities;

        [SerializeField]
        private TMP_Text _turnLabel;

        private void Awake()
        {
            Game.Instance.GameOver += OnGameOver;
            Game.Instance.TurnPrepared += OnTurnPrepared;
            _turnLabel.color = ColorHelper.SetAlpha(0, _turnLabel.color);
        }

        private void OnTurnPrepared(UnitController unitController)
        {
            if (unitController.PlayerId == 0)
            {
                _turnLabel.text = "Player Turn";
            }
            else
            {
                _turnLabel.text = "Enemy Turn";
            }

            new SyncScenario(
                    new ScaleTween(_turnLabel, Vector3.one * 0.9f),
                    new AlphaTween(_turnLabel, 1, 0.3f, EaseType.QuadOut),
                    new ScaleTween(_turnLabel, Vector3.one, 0.2f, EaseType.BackInOut),
                    new AlphaTween(_turnLabel, 0, 0.3f, EaseType.QuadIn),
                    new ActionScenarioItem(() => Game.Instance.StartTurn())
                ).Play();
        }

        private void OnGameOver(int loser)
        {
            if (loser == 0)
            {
                Lose();
            }
            else
            {
                Win();
            }
        }

        public override void Focus()
        {
            Game.Instance.PrepareGame(new PlayerTeamController(0), new AITeamController(1));
            var playerTeam = _playerUnits.Select(data =>
            {
                var character = new Character(data);
                character.SetLevel(1);
                return character;
            }).ToList();
            var enemies = _mobs.Select(data => 
            {
                var character = new Character(data);
                character.SetLevel(1);
                return character;
            }).ToList();

            var unitControllers = Game.Instance.SetupUpTeam(playerTeam, 0);
            for (int i = 0; i < unitControllers.Count; i++)
            {
                _abilities[i].Init(unitControllers[i]);
            }

            Game.Instance.SetupUpTeam(enemies, 1);
            Game.Instance.StartGame();
            base.Focus();
        }

        public override ISyncScenarioItem GetShowTransition(float duration = 0.3f)
        {
            return new CompositeItem(
                    base.GetShowTransition(duration),
                    new MoveTween(Game.Instance, Vector3.zero, duration, EaseType.Linear, TweenSpace.Local)
                );
        }

        protected override void PrepareToShow()
        {
            base.PrepareToShow();
            Game.Instance.transform.localPosition = new Vector3(6, 0 ,0);
        }

        public override ISyncScenarioItem GetHideTransition(float duration = 0.3f)
        {
            return new CompositeItem(
                    base.GetHideTransition(duration),
                    new MoveTween(Game.Instance, new Vector3(-6, 0 ,0), duration, EaseType.Linear, TweenSpace.Local)
                );
        }

        public void Surrender()
        {
            Game.Instance.Clear();
            ScreensManager.Instance.OpenScreen(ScreenType.Surrender);
        }

        public void Win()
        {
            ScreensManager.Instance.OpenScreen(ScreenType.Win);
        }

        public void Lose()
        {
            ScreensManager.Instance.OpenScreen(ScreenType.Lose);
        }

        public void ActivateSkill()
        {
            Game.Instance.ActivateAbility();
        }
    }
}
