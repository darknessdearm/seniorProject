﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    // Start is called before the first frame update
    public Image HPE;
    public Image HPP;
    public Text TimeCount;
    public Text scoreText;
    public Canvas overlay;
    public Canvas vic;
    public Canvas pa;
    
    public Image Emeny;
    public Image eff;
    public Image[] dam = new Image[3];
    public bool isHalf = false;

    private float startP;
    private float startE;
    private double sc = -888;
    private bool triggered = true;
    private float health_p;
    float timer;
    int count = 0;
    int one = 1;

    float ss;
    float delay;

    private bool isDead = false;

  

    [Tooltip("get score")]
    public GameObject score;
    private HandData check;

    

    void Start()
    {
        check = score.GetComponent<HandData>();
        startE = check.HPE;
        startP = check.HPP;
        timer = 60;
        eff.enabled = false;
        delay = 0.6f;
    }

    void Update()
    {
        
        if (check.isWin == true)
        {

            SetText();
            delay -= Time.deltaTime;
            if (delay < 0.5f)
            {
                Emeny.enabled = false;
                eff.enabled = true;
            }
            if (delay <= 0.3f)
            {
                eff.enabled = false;
            }
            if(delay <= 0)
            {
                vic.gameObject.SetActive(true);
                scoreText.text = "Score : " + check.score;
            }
        }
        else
        {
            if (timer <= 0 || check.HPP <= 0)
            {
                SetText();
                overlay.gameObject.SetActive(true);
                
            }
            else if (pa.isActiveAndEnabled)
            {
                SetText();
            }
            else
            {
                timer -= Time.deltaTime;
                SetText();
                if ((timer < 54.5 && triggered) && one == 1 && count == 0)
                {
                    BossDamage();
                    triggered = false;
                    one = 0;
                    count++;
                }
                if (timer > 45.5 && timer < 47.5 && !triggered && one == 0)
                {
                    BossDamage();
                    one = 1;
                    count++;
                }
                if (timer > 39 && timer <= 40 && !triggered)
                {
                    BossDamage();
                    triggered = true;
                    count++;
                }
                if (timer < 35 && count == 3)
                {
                    BossDamage();
                    count++;
                }
                if (timer < 30 && count == 4)
                {
                    BossDamage();
                    count++;
                }
                if (timer < 20 && count == 5)
                {
                    BossDamage();
                    count++;
                }
                if (timer < 15 && count == 6)
                {
                    BossDamage();
                    count++;
                }
            }
            
            
        }
    }

    public void PlayerDamage()
    {
        if(sc != check.score)
        {
            float amount = (float)check.score / 100 * 20;

            check.HPE -= amount;

            HPE.fillAmount = check.HPE / startE;
            sc = check.score;
            if (check.HPE <= 0 && !isDead)
            {
                check.isWin = true;
            }
            else
            {
                check.isWin = false;
            }
        }
    }

    public void BossDamage()
    {
        if (check.HPE <= 20 || check.moveCount> 10)
        {
            float mm = (float)(10);

            //check.HPP -= mm;
            //PP.fillAmount = check.HPP / startP;
            check.HPE += mm;
            if(check.HPE > startE)
            {
                check.HPE = startE;
            }
            HPE.fillAmount = check.HPE / startP;
        }
        else if (check.HPE<50 && count >= 4)
        {
            float mm = (float)(30);

            check.HPP -= mm;
            HPP.fillAmount = check.HPP / startP;
            //check.HPE += mm;
            //HPE.fillAmount = check.HPE / start;
        }
        else
        {
            float mm = (float)(5);

            check.HPP -= mm;
            HPP.fillAmount = check.HPP / startP;
        }

        //hp.HPP = health_p;

        /*if (health_p <= 0)
        {
            hp.isWin = false;
        }*/

        /*if (check.HPE <= 50 && one == 1)
        {
            float mm = (float)(50);

            health_p -= mm;
            HPP.fillAmount = health_p / start;

        }*/

    }

    void SetText()
    {
        if (timer <= 9.5)
        {
            TimeCount.text = "00:0" + timer.ToString("f0");
        }
        else if (timer >= 59)
        {
            TimeCount.text = "01:00";
        }
        else
        {
            TimeCount.text = "00:" + timer.ToString("f0");
        }
    }


}
