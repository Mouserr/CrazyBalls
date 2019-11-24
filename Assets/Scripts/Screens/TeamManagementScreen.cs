using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Assets.Scripts;
using Assets.Scripts.Screens;
using Assets.Scripts.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class TeamManagementScreen : AbstractScreen
{
    public PlayerController Player;
    
    public GameObject CharacterButtonPrefab;

    public GameObject CharacterSelectionIconPrefab;
    public Character SelectedCharacter { get; set; }

    private List<GameObject> _buttons = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Focus()
    {
        CreateButtons();
        if (Player.Characters.Any())
        {
            SelectedCharacter = Player.Characters.First();
            RefreshValues();
        }
    }

    private void CreateButtons()
    {
        var charactersPanel = transform.Find("_charactersPanel");
        foreach (var button in _buttons)
        {
            GameObject.Destroy(button);
        }
     
        foreach (var character in Player.Characters)
        {
            var btn = Instantiate(CharacterButtonPrefab, charactersPanel);
            
            btn.GetComponent<CharacterButtonComponent>().SetIcon(character.Icon);
            btn.GetComponent<CharacterButtonComponent>().Character = character;
            btn.GetComponent<UnityEngine.UI.Button>()
                .onClick
                .AddListener(() => { SelectCharacter(character);});
            
            _buttons.Add(btn);
        }
    }

    
    
    private void SelectCharacter(Character character)
    {
        SelectedCharacter = character;
        RefreshValues();
    }

    public void RefreshValues()
    {
        GameObject.Find("_characterAvatar").GetComponent<Image>().sprite = SelectedCharacter.Image;
        GameObject.Find("_characterName").GetComponent<TextMeshProUGUI>().text = SelectedCharacter.Name;
        GameObject.Find("_characterLevel").GetComponent<TextMeshProUGUI>().text = $"LVL {SelectedCharacter.Level}";
        GameObject.Find("_description").GetComponent<TextMeshProUGUI>().text = SelectedCharacter.Description;
        
        GameObject.Find("_hpStat").GetComponent<StatUIComponent>().SetValue(SelectedCharacter.GetStat(CharacterStatType.Health).ToString());
        
       // GameObject.Find("_energyStat").GetComponent<StatUIComponent>().SetValue(SelectedCharacter.GetStat(CharacterStatType.Energy).ToString());
        
        GameObject.Find("_damageStat").GetComponent<StatUIComponent>().SetValue(SelectedCharacter.GetStat(CharacterStatType.PassiveDamage).ToString());
        
        GameObject.Find("_speedStat").GetComponent<StatUIComponent>().SetValue(SelectedCharacter.GetStat(CharacterStatType.MaxSpeed).ToString());

        GameObject.Find("_lvlUpCost").GetComponent<StatUIComponent>().SetValue(GetLvlUpCost().ToString());
        GameObject.Find("_food").GetComponent<StatUIComponent>().SetValue(Player.Wallet.GetAmount(CurrencyType.Food).ToString());
        
        GameObject.Find("_lvlUpBtn").GetComponent<UnityEngine.UI.Button>().enabled = CanLvlUp();
        
        
    }

    private int GetLvlUpCost()
    {
        if (SelectedCharacter == null) return 0;
        var baseCost = 100;
        return SelectedCharacter.Level * baseCost;
    }

    private bool CanLvlUp()
    {
        if(SelectedCharacter.Level+1>10) return false;
        var need = GetLvlUpCost();
        
        
        return Player.Wallet.GetAmount(CurrencyType.Food)>=need;
    }

    public void LevelUp()
    {
        if(!CanLvlUp()) return;
        var need = GetLvlUpCost();
        if (Player.Wallet.AddTransaction(CurrencyType.Food, -need))
        {
            SelectedCharacter.LevelUp();
            RefreshValues();
        }
    }

    public void Back()
    {
       ScreensManager.Instance.OpenScreen(ScreenType.StoryTell);
    }

    public void PickForBattle()
    {
        if (SelectedCharacter != null)
        {
            Player.AssingToBattle(SelectedCharacter);

            foreach (var buttonObj in _buttons)
            {
                var selector = buttonObj.GetComponent<CharacterButtonComponent>();
                selector.SetSelection(Player.BattleGroup.Contains(selector.Character));
            }
        }
    }

}
