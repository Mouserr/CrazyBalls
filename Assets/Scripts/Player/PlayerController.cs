using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Configs;
using Assets.Scripts.Core;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public PlayerData model;
    public Wallet Wallet { get; set; }
    public List<Character> Characters { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        Characters = model.Characters.Select(c => new Character(c)).ToList();
        Wallet = new Wallet();
        Wallet.AddTransaction(CurrencyType.Food, model.Food);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
