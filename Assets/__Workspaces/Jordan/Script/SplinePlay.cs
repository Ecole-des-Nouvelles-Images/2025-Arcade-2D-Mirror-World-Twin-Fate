using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;


namespace __Workspaces.Jordan.Script
{
    public class SplinePlay : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private List<SplineAnimate> _enemiesToAnimate;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("PlayerBlue") || col.CompareTag("PlayerRed"))
            {
                foreach (SplineAnimate enemy in _enemiesToAnimate)
                {
                    enemy.Play();
                }
            }
        }
    }
}
