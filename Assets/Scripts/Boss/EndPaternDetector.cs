using System;
using UnityEngine;

public class EndPaternDetector : MonoBehaviour
{
    [SerializeField] private BossLife _bossLifeBlue;
    [SerializeField] private BossLife _bossLifeRed;
    [SerializeField] private GameObject _bossLifeBar;
    [SerializeField] private GameObject WarnUI;
    private bool triggered = false;
    private float timer;
    private float delay = 5;
    

    private void Update()
    {
        if (triggered) { 
            timer += Time.deltaTime;
            if (timer >= delay)  {
                WarnUI.gameObject.SetActive(false);
                triggered = false;
                //Debug.Log("EndScrollTrigger");
                _bossLifeBlue.StartBoss();
                _bossLifeRed.StartBoss();
                //_bossLifeBar.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("EndPaternDetector"))
        {
            Debug.Log("Player Dangerous");
            WarnUI.gameObject.SetActive(true);
            triggered = true;

        }
    }
}
