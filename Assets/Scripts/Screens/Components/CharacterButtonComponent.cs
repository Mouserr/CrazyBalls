using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonComponent : MonoBehaviour
{
    public Sprite Icon;
    
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInChildren<Image>().sprite = Icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
