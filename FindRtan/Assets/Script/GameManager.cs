using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject endTxt;
    public Animator matchFailTxt;
    public Animator matchTrueTxt1;
    public Animator matchTrueTxt2;
    public Animator matchTrueTxt3;
    public Animator matchTrueTxt4;
    public Animator matchTrueTxt5;
    public Animator matchTrueTxt6;
    public Animator matchTrueTxt7;
    public Animator matchTrueTxt8;

    public Text countTxt;

    public GameObject endTitle;


    public GameObject cardWrapper;
    public Text countText;
    public float countDownTimer = 5f;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip alert;
    public AudioClip isWrong;
    private bool soundPlayed = false;

    public int cardCount = 0;

    public int count = 0;

    float time = 0.0f;

    private void Awake() 
    {
        if(Instance ==null)
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (time >= 20f && !soundPlayed)
        {
            timeTxt.color = Color.red;
            audioSource.PlayOneShot(alert);
            soundPlayed = true;
        }


        if (time > 30f)
        {
         endTitle.SetActive(true);
          endTxt.SetActive(true);
          Time.timeScale = 0.0f;
        }

        if(firstCard != null)
        {
            ShowCountDown();
        }
    }

    public void Matched()
    {
       if(firstCard.idx == secondCard.idx)
       {
          audioSource.PlayOneShot(clip);

            if(firstCard.idx == 0)
            {
                matchTrueTxt1.SetTrigger("True");
            }
            if (firstCard.idx == 1)
            {
                matchTrueTxt2.SetTrigger("True");
            }
            if (firstCard.idx == 2)
            {
                matchTrueTxt3.SetTrigger("True");
            }
            if (firstCard.idx == 3)
            {
                matchTrueTxt4.SetTrigger("True");
            }
            if (firstCard.idx == 4)
            {
                matchTrueTxt5.SetTrigger("True");
            }
            if (firstCard.idx == 5)
            {
                matchTrueTxt6.SetTrigger("True");
            }
            if (firstCard.idx == 6)
            {
                matchTrueTxt7.SetTrigger("True");
            }
            if (firstCard.idx == 7)
            {
                matchTrueTxt8.SetTrigger("True");
            }

            firstCard.DestroyCard();
         secondCard.DestroyCard();
         cardCount -= 2;
         if(cardCount == 0)
         {
            Time.timeScale = 0f;
            endTitle.SetActive(true);
            endTxt.gameObject.SetActive(true);
         }
       }
       else
       {
         time += 1.5f;
        audioSource.PlayOneShot(isWrong);
         matchFailTxt.SetTrigger("Fail");
         firstCard.CloseCard();
         secondCard.CloseCard();
       }
        HideCountDown();
        firstCard = null;
        secondCard = null;

        count++;
        countTxt.text = count.ToString();
    } 

    public void ShowCountDown()
    {
        if(countDownTimer > 0f)
        {
            cardWrapper.SetActive(true);
            countDownTimer -= Time.deltaTime;
            countText.text = countDownTimer.ToString("F0");
        }
        else
        {
            HideCountDown();
        }
    }

    public void HideCountDown()
    {
        cardWrapper.SetActive(false);
        firstCard.CloseCard();
        firstCard = null;
        countDownTimer = 5f;
    }
}
