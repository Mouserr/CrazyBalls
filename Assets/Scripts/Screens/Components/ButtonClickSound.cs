using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Screens.Components
{
    public class ButtonClickSound : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Goki_UI_Click");
        }
    }
}