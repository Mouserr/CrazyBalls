using System.Collections.Generic;
using Assets.Scripts.Abilities;
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
    [CreateAssetMenu(fileName = "LineDamage", menuName = "Configs/Ability/LineDamage")]
    public class LineDamage : ActiveAbilityConfig
    {
        [SerializeField]
        private IntAbilityParameter _damage;
        [SerializeField]
        private LinesCollider _linesColliderPrefab;
        
        public override ISyncScenarioItem Apply(CastContext castContext, int abilityLevel)
        {
            var linesCollider = PrefabHelper.Intantiate(_linesColliderPrefab, Game.Instance.gameObject);
            linesCollider.transform.position = castContext.Caster.Position;
            linesCollider.transform.localScale = Vector3.one;
            linesCollider.OnTrigger += (unit) =>
            {
                if (unit.PlayerId != castContext.Caster.PlayerId)
                {
                    DamageSystem.ApplyDamage(castContext.Caster, _damage.GetValue(abilityLevel), unit);
                }
            };
            var renderers = linesCollider.GetComponentsInChildren<Renderer>();
            return new SyncScenario(
                new List<ISyncScenarioItem>{
                    
                    new ActionScenarioItem(() =>
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Explosion");
                    }),
                    new AlphaTween(renderers, 0),
                    new AlphaTween(renderers, 1, 0.1f, EaseType.QuadIn),
                    new AlphaTween(renderers, 0, 0.3f, EaseType.QuadOut)
                }, 
                (s, interrupted) => Object.Destroy(linesCollider.gameObject)
            ).PlayRegisterAndReturnSelf();
        }

        protected override List<IAbilityParameter> GetParameters()
        {
            return new List<IAbilityParameter>
            {
                Cooldown,
                _damage
            };
        }

        public override void Register()
        {
        }

        public override bool CouldApply(CastContext castContext, int abilityLevel)
        {
            return true;
        }
    }
}