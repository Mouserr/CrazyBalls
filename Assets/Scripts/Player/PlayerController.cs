﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public PlayerData model;
    public Wallet Wallet { get; set; }
    public List<Character> Characters { get; set; }
    public List<Character> BattleGroup { get; set; }

    public EpisodeData CurrentEpisode;

    public bool WonLastBattle = false;
    
    public void AssingToBattle(Character character)
    {
        BattleGroup.Add(character);
        if (BattleGroup.Count > 3) BattleGroup.Remove(BattleGroup.First());
    }
    
    public void RemoveFromBattle(Character character)
    {
        BattleGroup.Remove(character);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        Characters = model.Characters.Select(c => new Character(c)).ToList();
        BattleGroup = new List<Character>();
        for (int i = 0; i < Math.Min(3, Characters.Count) ; i++)
        {
            AssingToBattle(Characters[i]);
        }
        Wallet = new Wallet();
        Wallet.AddTransaction(CurrencyType.Food, model.Food);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
