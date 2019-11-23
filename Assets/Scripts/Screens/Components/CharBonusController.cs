using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharBonusController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetBonus(CharacterBonusData bonusData)
    {
        this.transform.Find("_value").GetComponent<TextMeshProUGUI>().text = bonusData.AllyCharacter.Name;
        this.transform.Find("_image").GetComponent<Image>().sprite = bonusData.AllyCharacter.Icon;
    }
}
