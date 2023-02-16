using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tiefwurtz
{
    public class DashItemUI : MonoBehaviour
    {
        private Image currentDashItem;
        private bool canDash = true;
        public GameObject canDashText;
        private TextMeshProUGUI Dashtext;
        private float timer;
        public static bool DashTextPlayed;

        private void Start()
        {
            Dashtext = canDashText.GetComponent<TextMeshProUGUI>();
            currentDashItem = GetComponent<Image>();
        }
        private void Update()
        {
            if (!Move.dashUnlocked)
                return;

            if (Move.dashUnlocked && !DashTextPlayed)
            {
                if (timer > 4f)
                {
                    Dashtext.alpha = 0f;
                    DashTextPlayed = true;
                }
                else
                {
                    Dashtext.alpha = 255f;
                    timer +=  + 1 * Time.deltaTime;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && Move.dashUnlocked)
            {
                currentDashItem.fillAmount = 0f;
                canDash = false;
            }
            if (currentDashItem.fillAmount >= 1f)
            {
                canDash = true;
                return;
            }

            currentDashItem.fillAmount += 1f * Time.deltaTime;

        }
    }
}
