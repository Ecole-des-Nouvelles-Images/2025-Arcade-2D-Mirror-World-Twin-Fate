using UnityEngine;
using UnityEngine.UI;

namespace __Workspaces.Jordan.Script.Player
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject PlayerBlue;
        [SerializeField] private GameObject PlayerRed;
        public Slider slider;
        public void SetMaxHealth(int health)
        {
            slider.maxValue = health;
            slider.value = health;
        }

        public void SetHealth(int health)
        {
            slider.value = health;
        }
    }
}