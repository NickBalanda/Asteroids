using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class ButtonBehaviour : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IDeselectHandler {

    //Audio
    public string hoverSound = "BUTTON_hover";
    public string clickSound = "BUTTON_click";

    Button btn;

    void Start () {
        if (GetComponent<Button>()) {
            btn = GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }  
    }

    // When highlighted with mouse.
    public void OnPointerEnter(PointerEventData eventData) {
       
        if (!EventSystem.current.alreadySelecting)
            EventSystem.current.SetSelectedGameObject(gameObject);
    }
    // When selected.
    public void OnSelect(BaseEventData eventData) {
        SoundManager.PlaySFX(gameObject, hoverSound);
    }

    public void OnDeselect(BaseEventData eventData) {
        GetComponent<Selectable>().OnPointerExit(null);
    }
    void TaskOnClick() {
        SoundManager.PlaySFX(gameObject,clickSound);
    }

}
