using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class FoodBonusController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBonus(FoodBonusData bonusData)
    {
        this.transform.Find("_value").GetComponent<TextMeshProUGUI>().text = bonusData.Food.ToString();
    }
}
