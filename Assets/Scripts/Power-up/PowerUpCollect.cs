// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Player;
// using UnityEngine;
// using Random = UnityEngine.Random;
//
// namespace Power_up
// {
//     public class PowerUpCollect : MonoBehaviour
//     {
//         public List<PowerUp> powerUps = new List<PowerUp>();
//         public GameObject pickedupEffect;
//
//         private void OnTriggerEnter2D(Collider2D other)
//         {
//             if (other.CompareTag("PlayerRed") || other.CompareTag("PlayerBlue"))
//             {
//                 StartCoroutine(Pickup(other));
//             }
//         }
//
//         // IEnumerator Pickup(Collider2D player)
//         // {
//         //     Debug.Log("Pickup");
//         //     Instantiate(pickedupEffect, transform.position, transform.rotation);
//         //
//         //     PowerUp stats = player.GetComponent<PowerUp>();
//         //     // powerUps[Random.Range(0, powerUps.Count)];
//         //
//         //     Destroy(gameObject);
//         // }
//         
//     }
// }
//
