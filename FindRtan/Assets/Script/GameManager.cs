using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currectStage;            // 스테이지를 나타내는 것을 받아줌
    public static int usingStage = 1;   // 사용가능한 스테이지

    public RankRecord record;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject retryTxt;
    public Animator matchFailTxt;
    public Animator matchTrueTxt1;
    public Animator matchTrueTxt2;
    public Animator matchTrueTxt3;
    public Animator matchTrueTxt4;
    public Animator matchTrueTxt5;
    public Animator matchTrueTxt6;
    public Animator matchTrueTxt7;
    public Animator matchTrueTxt8;

    public Text tryTxt;
    public Text totalScoreTxt;
    public GameObject nextTxt;
    public GameObject endingTxt;

    public GameObject scoreTitle;


    public GameObject cardWrapper;
    public Text countText;
    public float countDownTimer = 5f;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip alert;
    public AudioClip isWrong;

    private bool soundPlayed = false;

    public int cardCount = 0;

    int tryCount = 0;
    int addScore = 0;
    public int totalScore = 0;

    float time = 0.0f;

    public int level;   // 게임 스테이지를 나타내는 int

    public bool isNext; // 게임 클리어 bool

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
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        isNext = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Board.Instance.gameStart == true)
        {
            time += Time.deltaTime;
        }

        timeTxt.text = time.ToString("N2");

        
            if (time >= 40f && !soundPlayed)
            {
                timeTxt.color = Color.red;
                audioSource.PlayOneShot(alert);
                soundPlayed = true;
            }

        
        if(time>50)
        {
            EndGame();
        }

        if (firstCard != null)
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
            addScore += 10;
         if(cardCount == 0)
         {
                isNext = true;
                EndGame();
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

        tryCount++;
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
    public void EndGame()
    {
        Time.timeScale = 0.0f;
        
        totalScore = addScore - tryCount - (int)time;
        tryTxt.text = tryCount.ToString();
        totalScoreTxt.text = totalScore.ToString();
        scoreTitle.SetActive(true);
        currectStage = level;
        if (isNext == true)
        {
            

            currectStage++;
            if (usingStage < currectStage)
                usingStage = currectStage;
            if(level == 3)
            {
                endingTxt.SetActive(true);
            }
            else
            {
                nextTxt.SetActive(true);
            }
            
        }
        else
        {
            retryTxt.SetActive(true);
        }

        record.Record(level, totalScore);
        
    }
}
