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
    }

    public void Matched()
    {
       if(firstCard.idx == secondCard.idx)
       {
          audioSource.PlayOneShot(clip);

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
        audioSource.PlayOneShot(isWrong);
         matchFailTxt.SetTrigger("Fail");
         firstCard.CloseCard();
         secondCard.CloseCard();
       }

        firstCard = null;
        secondCard = null;

        count++;
        countTxt.text = count.ToString();
    } 

    //test
}
