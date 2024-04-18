using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Threading;

public class Board : MonoBehaviour
{
    public static Board Instance;

    public Card cardPrefab;

    public List<Card> cardList;

    private Vector2 startPosition;
    private Vector2 endPosition;

    public bool gameStart = false;

    public int level;
    int i = 0;

    float moveDuration = 0.2f;
    float elapsedTime = 0f;

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
        StartCoroutine("MakeCard");
    }

    public IEnumerator MakeCard()
    {
        startPosition = new Vector2(0, -5);

        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => UnityEngine.Random.Range(0f, 7f)).ToArray();
        //GameManager gameManager = FindObjectOfType<GameManager>();

        for (i = 0; i < 16; i++)
        {
            Card go = Instantiate(cardPrefab);
            var T = 0.5f;

            cardList.Add(go);
            if(level == 1) 
            {
                float x = (i % 4) * 1.4f - 2.1f;
                float y = (i / 4) * 1.4f - 3.0f;
                cardList[i].endPosition = new Vector2(x, y);
            }
            else if (level == 2)
            {
                if (i == 0)
                {
                    float x = 0;
                    float y = -2;
                    cardList[i].endPosition = new Vector2(x, y);
                }
                if (0 < i && i < 4)
                {
                    float x = (i - 2);
                    float y = -1;
                    cardList[i].endPosition = new Vector2(x, y);
                }
                if (3 < i && i < 14)
                {
                    float x = ((i - 4) % 5) - 2;
                    float y = (i / 9);
                    cardList[i].endPosition = new Vector2(x, y);
                }
                if (13 < i && i < 16)
                {
                    if ((i % 2) == 0)
                    {
                        float x = -1;
                        float y = 2;
                        cardList[i].endPosition = new Vector2(x, y);
                    }
                    else
                    {
                        float x = 1;
                        float y = 2;
                        cardList[i].endPosition = new Vector2(x, y);
                    }
                }
            }
            else if (level == 3)
            {
                if (i < 2)
                {
                    float x = (i % 2) * 0.9f - 2.1f;
                    float y = -3;
                    cardList[i].endPosition = new Vector2(x, y);
                }
                if (i>1&&i < 4)
                {
                    float x = (i % 2) * 0.9f + 1.2f;
                    float y = -3;
                    cardList[i].endPosition = new Vector2(x, y);
                }
                if (i>3&&i<6)
                {
                    float x = (i % 2) * 0.9f - 2.1f;
                    float y = -2.1f;
                    cardList[i].endPosition = new Vector2(x, y);
                }
                if (i>5&&i<8)
                {
                    float x = (i % 2) * 0.9f + 1.2f;
                    float y = -2.1f;
                    cardList[i].endPosition = new Vector2(x, y);
                }
                if (i > 7 && i < 10)
                {
                    float x = (i % 2) * 0.9f - 2.1f;
                    float y = 1.2f;
                    cardList[i].endPosition = new Vector2(x, y);
                }
                if (i > 9 && i < 12)
                {
                    float x = (i % 2) * 0.9f + 1.2f;
                    float y = 1.2f;
                    cardList[i].endPosition = new Vector2(x, y);
                }
                if (i > 11 && i < 14)
                {
                    float x = (i % 2) * 0.9f - 2.1f;
                    float y = 2.1f;
                    cardList[i].endPosition = new Vector2(x, y);
                }
                if (i>13&&i < 16)
                {
                    float x = (i % 2) * 0.9f + 1.2f;
                    float y = 2.1f;
                    cardList[i].endPosition = new Vector2(x, y);
                }
            }

            cardList[i].startPosition = startPosition;
            go.GetComponent<Card>().Setting(arr[i]);
            InvokeRepeating("CardMove", 0, 0.01f);
            elapsedTime = 0f;
            yield return new WaitForSeconds(T);
        }
        gameStart = true;
        GameManager.Instance.cardCount = arr.Length;
    }
    public void CardMove()
    {
        elapsedTime += Time.deltaTime;

        cardList[i].transform.position = Vector2.Lerp(cardList[i].startPosition, cardList[i].endPosition, elapsedTime / moveDuration);
        if (elapsedTime > moveDuration)
        {
            CancelInvoke("CardMove");
        }
    }
}