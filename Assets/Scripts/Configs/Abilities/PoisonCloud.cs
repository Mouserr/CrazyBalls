using System.Collections.Generic;
using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Configs.Abilities
{
    [CreateAssetMenu(fileName = "PoisonCloud", menuName = "Configs/Ability/PoisonCloud")]
    public class PoisonCloud : AbilityConfig
    {
        [SerializeField]
        private FloatAbilityParameter _radius;
        [SerializeField]
        private SpriteRenderer _explosionEffectPrefab;

        public PoisonEffect EffectOnTouch;

        public override void Register()
        {
        }

        public override ISyncScenarioItem Apply(CastContext castContext, int abilityLevel)
        {
            var effect = PrefabHelper.Intantiate(_explosionEffectPrefab, Game.Instance.gameObject);
            effect.transform.position = castContext.Caster.Position;
            effect.transform.localScale = Vector3.zero;

            var radius = _radius.GetValue(abilityLevel);
            var targets = MapController.Instance.GetEnemiesInArea(castContext.TargetPoint,
                radius, castContext.Caster.PlayerId);

            for (int i = 0; i < targets.Count; i++)
            {
                Debug.Log($"Attaching poison to {targets[i]}", targets[i]);
                targets[i].AddEffect(new CharacterEffect(EffectOnTouch, abilityLevel));
            }

            return new SyncScenario(
                new AlphaTween(effect, 1),
                new ScaleTween(effect, 10 * radius * Vector3.one, 0.5f, EaseType.QuadIn),
                new AlphaTween(effect, 0, 0.3f, EaseType.QuadOut),
                new ActionScenarioItem(() => Object.Destroy(effect.gameObject))
            ).PlayAndReturnSelf();
        }

        protected override List<IAbilityParameter> GetParameters()
        {
            return new List<IAbilityParameter>
            {
                _radius
            };
        }
    }
}