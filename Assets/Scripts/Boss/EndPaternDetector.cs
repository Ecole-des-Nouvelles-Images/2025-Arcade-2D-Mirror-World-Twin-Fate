using System;
using UnityEngine;

public class EndPaternDetector : MonoBehaviour
{
    [SerializeField] private BossLife _bossLifeBlue;
    [SerializeField] private BossLife _bossLifeRed;
    [SerializeField] private GameObject _bossLifeBar;
    private bool triggered = false;

    private void Start()
    {
        //_bossLifeBar.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("EndPaternDetector"))
        {
            triggered = true;
            //Debug.Log("EndScrollTrigger");
            _bossLifeBlue.StartBoss();
            _bossLifeRed.StartBoss();
            //_bossLifeBar.SetActive(true);
        }
    }
}
