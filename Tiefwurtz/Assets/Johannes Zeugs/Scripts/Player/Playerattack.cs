using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tiefwurtz
{
    public class Playerattack : MonoBehaviour
    {
        private GameObject enemy;
        private Enemy enemyScr;
        private void Start()
        {
            enemy = GameObject.Find("Enemy");
            enemyScr = enemy.GetComponent<Enemy>();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown("g"))
            {
                enemyScr.TakeDamage(20f);
            }
        }
    }
}
