using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Configs;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonComponent : MonoBehaviour
{
    public Character Character { get; set; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetIcon(Sprite icon)
    {
        this.transform.Find("_icon").GetComponent<Image>().sprite = icon;
    }

    public void SetSelection(bool selection)
    {
        if (selection)
        {
            this.transform.Find("_icon").GetComponent<Image>().color = Color.yellow;
            
        }
        else
        {
            this.transform.Find("_icon").GetComponent<Image>().color = Color.white;
        }
    }
    
}
