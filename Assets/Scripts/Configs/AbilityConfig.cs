using System;
using System.Collections.Generic;
using Assets.Scripts.Core.SyncCodes.SyncScenario;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Configs
{
    public abstract class AbilityConfig : ScriptableObject
    {
        public TargetType TargetType;
        public Sprite Icon;

        public abstract ISyncScenarioItem Apply(CastContext castContext, int abilityLevel, ICharacter caster);
        protected abstract List<IAbilityParameter> GetParameters();

        private int _maxLevel;

        public int MaxLevel
        {
            get { return _maxLevel; }
        }

        protected virtual void OnEnable()
        {
            List<IAbilityParameter> parameters = GetParameters();
            _maxLevel = 0;
            for (int i = 0; i < parameters.Count; i++)
            {
                _maxLevel = Math.Max(_maxLevel, parameters[i].MaxLevel);
            }
        }

        public abstract void Register();
    }

    public abstract class ActiveAbilityConfig : AbilityConfig
    {
        public FloatAbilityParameter Cooldown;

        public abstract bool CouldApply(CastContext castContext, int abilityLevel, ICharacter caster);
    }
    
    public interface ICastingAreaProvider
    {
        float GetRadius(int abilityLevel);
    }

    public interface ICastingRangeProvider
    {
        float GetRange(int abilityLevel);
    }
}