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

    public Text countTxt;

    public GameObject endTitle;


    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip alert;
    public AudioClip isWrong;
    private bool soundPlayed = false;

    public int cardCount = 0;

    public int count = 0;

    float time = 0.0f;


    //첫번 째 카드 뒤집을 때 카운트 다운 기능(권도현)
    public Text countDownText;
    public GameObject countDownPanel;

    //5초 세팅
    private float countDownTime = 5f;

    private void Awake()
    {
        if (Instance == null)
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

        //첫번 째 카드를 선택했다면
        if (firstCard != null)
        {
            //카운트 다운 시작
            StartCountdown();
        }


    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);

            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if (cardCount == 0)
            {
                Time.timeScale = 0f;
                endTitle.SetActive(true);
                endTxt.gameObject.SetActive(true);
            }
        }
        else
        {
            audioSource.PlayOneShot(isWrong);
            matchFailTxt.SetTrigger("Fail");
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        //카드 짝 맞는 여부 상관없이 두번 째 카드까지 고르는 작업이 진행됬을 때는 카운트 다운 멈춤
        HideCountDown();
        secondCard = null;

        count++;
        countTxt.text = count.ToString();
    }
    public void StartCountdown()
    {
        // 0초 되기 전 까지는 1초씩 감소
        if (countDownTime > 0f)
        {
            countDownPanel.SetActive(true);
            countDownTime -= Time.deltaTime;
            countDownText.text = countDownTime.ToString("F0");
        }
        else
        {
            //0초 되면 화면 사라짐

            HideCountDown();

        }
    }
    public void HideCountDown()
    {
        countDownPanel.SetActive(false);
        firstCard.CloseCard();
        firstCard = null;
        countDownTime = 5f;
    }

}
