using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CyberCountry.UI
{
    public class TDUI_Button : MonoBehaviour
    {
        public float CooldeownTime = 0;
        public Image FillImage;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            FillImage.enabled = false; //TODO: Всю работу с FillImage можно вынести в отдельный метод
            FillImage.fillAmount = 1;

        }

        public void myClick()
        {
            StartCoroutine(CoolDown(CooldeownTime));
        }

        IEnumerator CoolDown(float sec) //TODO: тут не считается время точно. Есть погрешность. UPD вообще не так считается как надо.
        {
            if (sec==0)
            {
                yield break;
            }
            FillImage.enabled = true; //TODO: Вынести в отдельный метод
            _button.interactable = false;
            
            var dt = ((sec)/100f);
            
            while (FillImage.fillAmount>0)
            {
                FillImage.fillAmount -= 0.01f;
                yield return new WaitForSeconds(dt);
            }
            float t2 = Time.time;

            _button.interactable = true;
            FillImage.fillAmount = 1;
            FillImage.enabled = false;
        }
        
        
    }
}