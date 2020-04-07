using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatUIComponent : MonoBehaviour
{
    public string Name;
    public string Value;
   // public Sprite Icon;

    private Image _imageObj;
    private TextMeshProUGUI _nameObj;
    private TextMeshProUGUI _valueObj;
    
    // Start is called before the first frame update
    void Start()
    {
//        var icon = this.transform.Find("_icon")?.gameObject;
//        _imageObj = icon.GetComponent<Image>();
//        _imageObj.sprite = Icon;
        
        var nameObj = this.transform.Find("_statName");
        _nameObj = nameObj.GetComponent<TextMeshProUGUI>();
        _nameObj.text = Name;
        
        var valueObj = this.transform.Find("_statValue");
        _valueObj = valueObj.GetComponent<TextMeshProUGUI>();
        _valueObj.text = Value;
    }

    public void SetName(string name)
    {
        Name = name;
        _nameObj.text = Name;
    }
    
    public void SetValue(string value)
    {
        Value = value;
        _valueObj.text = Value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
