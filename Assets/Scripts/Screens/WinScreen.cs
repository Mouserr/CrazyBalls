using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    public class WinScreen : AbstractScreen
    {
        public PlayerController Player;
        public GameObject BonusPanel;
        public GameObject FoodBonusPrefab;
        public GameObject CharBonusPrefab;
        public override void Focus()
        {
            var bonuses = Player.CurrentEpisode.BonusesWin;

            var foodBonus = bonuses.OfType<FoodBonusData>().FirstOrDefault();
            var characterBonus = bonuses.OfType<CharacterBonusData>().FirstOrDefault();

            if (foodBonus != null)
            {
                var foodBonusObj = Instantiate(FoodBonusPrefab, BonusPanel.transform) as GameObject;
                foodBonusObj.GetComponent<FoodBonusController>().SetBonus(foodBonus);
                Player.Wallet.AddTransaction(CurrencyType.Food, foodBonus.Food);
            }

            if (characterBonus != null)
            {
                var charBonusObj = Instantiate(CharBonusPrefab, BonusPanel.transform) as GameObject;
                charBonusObj.GetComponent<CharBonusController>().SetBonus(characterBonus);
                Player.Characters.Add(new Character(characterBonus.AllyCharacter));
            }
            
            

        }
        
        public void Ok()
        {
            ScreensManager.Instance.OpenScreen(ScreenType.StoryTell);
        }

        
    }
}