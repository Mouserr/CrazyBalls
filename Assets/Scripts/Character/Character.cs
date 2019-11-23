using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Configs;
using Assets.Scripts.Units;

namespace Assets.Scripts
{
    public class Character: ICharacter
    {
        private readonly CharacterData _model;
        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public UnitType UnitType { get; protected set; }
        public string Icon { get; protected set; }
        public int Level { get; set; }

        public Dictionary<CharacterStatType, CharacterStat> Stats { get; }
        public List<CharacterAbility> Abilities { get; }
        
        public List<CharacterEffect> ActiveEffects { get; }

        public CharacterActiveAbility ActiveAbility { get; }
        public CharacterAbility PassiveAbility { get; }
       
        public void RegisterStat(CharacterStat stat)
        {
            if (Stats.ContainsKey(stat.Type)) return;
            Stats.Add(stat.Type, stat);
        }

        public void AddEffect(CharacterEffect effectConfig)
        {
            if (ActiveEffects.Find(x => x.Config.EffectType == effectConfig.Config.EffectType) == null)
            {
                ActiveEffects.Add(effectConfig);
            }
        }

        public void OnTurnStart(CastContext castContext)
        {
            foreach (var effect in ActiveEffects)
            {
                effect.Apply(castContext);
                effect.LeftTurns--;
            }

            RemoveEndedEffects();
        }

        public Character(CharacterData characterData)
        {
            _model = characterData;
            Stats = new Dictionary<CharacterStatType, CharacterStat>();
            Abilities = new List<CharacterAbility>();
            ActiveEffects = new List<CharacterEffect>();
            Id = _model.Id;
            Name = _model.Name;
            Description = _model.Description;
            Icon = _model.Icon;
            Level = 1;

            ActiveAbility = new CharacterActiveAbility(_model.ActiveAbility, this);
            if (_model.PassiveAbility)
            {
                PassiveAbility = new CharacterAbility(_model.PassiveAbility, this);
            }
            UnitType = _model.UnitType;
        }

        public void SetLevel(int level)
        {
            Level = level;
            ResetStats();
        }

        private void RemoveEndedEffects()
        {
            for (int i = 0; i < ActiveEffects.Count;)
            {
                if (ActiveEffects[i].LeftTurns <= 0)
                {
                    ActiveEffects.RemoveAt(i);
                    continue;
                }

                i++;
            }
        }

        private void ResetStats()
        {
              var maxHealth = _model.Health.GetValue(Level - 1);
              var health = new CharacterStat(CharacterStatType.Health, maxHealth);
              this.RegisterStat(health);
              
              var maxSpeed = _model.MaxSpeed.GetValue(Level - 1);
              var speed = new CharacterStat(CharacterStatType.MaxSpeed, maxSpeed);
              this.RegisterStat(speed);
              
              var maxEnergy = _model.Energy.GetValue(Level - 1);
              var energy = new CharacterStat(CharacterStatType.Energy, maxEnergy);
              this.RegisterStat(energy);
              
              var maxPassiveDamage = _model.PassiveDamage.GetValue(Level - 1);
              var passiveDamage = new CharacterStat(CharacterStatType.PassiveDamage, maxPassiveDamage);
              this.RegisterStat(passiveDamage);
        }
    }
}
