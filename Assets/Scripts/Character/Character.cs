using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Configs;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Scripts
{
    public class Character: ICharacter
    {
        private readonly CharacterData _model;
        public string Id => _model.Id;
        public string Name => _model.Name;
        public string Description => _model.Description;
        public UnitType UnitType => _model.UnitType;
        public Sprite Icon => _model.Icon;
        public Sprite Image => _model.Image;
        public Sprite InGameSprite => _model.InGameSprite;
        public int Level { get; set; }

        public Dictionary<CharacterStatType, CharacterStat> Stats { get; }

        public CharacterActiveAbility ActiveAbility { get; }
        public CharacterAbility PassiveAbility { get; }
       
        public void RegisterStat(CharacterStat stat)
        {
            if (Stats.ContainsKey(stat.Type)) return;
            Stats.Add(stat.Type, stat);
        }

        public int GetStat(CharacterStatType statType)
        {
            if(Stats.TryGetValue(statType, out var stat))
            {
                return stat.MaxValue;
            }
            return 0;
        }
        
        public Character(CharacterData characterData)
        {
            _model = characterData;
            Stats = new Dictionary<CharacterStatType, CharacterStat>();
            ActiveAbility = new CharacterActiveAbility(_model.ActiveAbility, this);
            if (_model.PassiveAbility)
            {
                PassiveAbility = new CharacterAbility(_model.PassiveAbility, this);
            }
            
            SetLevel(1);
        }

        public void SetLevel(int level)
        {
            if (level<1||level > 10) return;
            Level = level;
            ResetStats();
        }

        public void LevelUp()
        {
            SetLevel(Level+1);
        }

        private void ResetStats()
        { 
              Stats.Clear();
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
