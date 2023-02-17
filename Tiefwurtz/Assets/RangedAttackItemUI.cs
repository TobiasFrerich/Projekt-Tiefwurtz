using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tiefwurtz
{
    public class RangedAttackItemUI : MonoBehaviour
    {
        private Image currentRAITEM;
        private bool canRangedAttack = true;
        public GameObject canRAText;
        private TextMeshProUGUI RAtext;
        private float timer;
        public static bool RATextPlayed;

        private void Start()
        {
            RAtext = canRAText.GetComponent<TextMeshProUGUI>();
            currentRAITEM = GetComponent<Image>();
        }
        private void Update()
        {
            if (!Playerattack.rangedAttackUnlocked)
                return;

            if (Playerattack.rangedAttackUnlocked && !RATextPlayed)
            {
                if (timer > 4f)
                {
                    RAtext.alpha = 0f;
                    RATextPlayed = true;
                }
                else
                {
                    RAtext.alpha = 255f;
                    timer += +1 * Time.deltaTime;
                }
            }

            if (Playerattack.rangedAttackUnlocked && Input.GetMouseButtonDown(1) && canRangedAttack)
            {
                currentRAITEM.fillAmount = 0f;
                canRangedAttack = false;
            }
            if (currentRAITEM.fillAmount >= 1f)
            {
                canRangedAttack = true;
                return;
            }

            currentRAITEM.fillAmount += 1f * Time.deltaTime;

        }
    }
}
