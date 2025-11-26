using UnityEngine;
using UnityEngine.UI;

public class UiPanel : MonoBehaviour
{
    [Header("Button Selected")]
    [SerializeField] Selectable buttonSelectable;
    
    public void OpenPanel() {
        gameObject.SetActive(true);
        if(buttonSelectable!=null) buttonSelectable.Select();
    }
}
