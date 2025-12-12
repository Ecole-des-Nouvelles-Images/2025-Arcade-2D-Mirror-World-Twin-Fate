using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonSelect : MonoBehaviour
{
    public GameObject firstselectedbutton;
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstselectedbutton);
    }

}
