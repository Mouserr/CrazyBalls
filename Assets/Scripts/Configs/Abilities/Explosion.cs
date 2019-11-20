using System.Collections.Generic;
using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.SyncCodes;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations;
using Assets.Scripts.Core.SyncCodes.SyncScenario.Implementations.Tween;
using Assets.Scripts.Core.Tween;
using Assets.Scripts.Core.Tween.TweenObjects;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Configs.Abilities
{
    [CreateAssetMenu(fileName = "Explosion", menuName = "Configs/Ability/Explosion")]
    public class Explosion : ActiveAbilityConfig, ICastingAreaProvider
    {
        [SerializeField]
        private FloatAbilityParameter _radius;
        [SerializeField]
        private IntAbilityParameter _damage;
        [SerializeField]
        private SpriteRenderer _explosionEffectPrefab;


        public float GetRadius(int abilityLevel)
        {
            return _radius.GetValue(abilityLevel);
        }

        public override void Register()
        {
        }

        public override bool CouldApply(CastContext castContext, int abilityLevel, ICharacter caster)
        {
            return true;
        }

        public override ISyncScenarioItem Apply(CastContext castContext, int abilityLevel, ICharacter caster)
        {
            var effect = PrefabHelper.Intantiate(_explosionEffectPrefab, Game.Instance.gameObject);
            effect.transform.position = castContext.CasterPoint;
            effect.transform.localScale = Vector3.zero;

            var radius = _radius.GetValue(abilityLevel);
            var targets = MapController.Instance.GetEnemiesInArea(castContext.TargetPoint,
                radius, castContext.CasterPlayerId);

            for (int i = 0; i < targets.Count; i++)
            {
                DamageSystem.ApplyDamage(caster, _damage.GetValue(abilityLevel), targets[i]);
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
                _radius,
                Cooldown,
                _damage
            };
        }

    }
}