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

    int i = 0;

    float moveDuration = 0.5f;
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
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => UnityEngine.Random.Range(0f, 7f)).ToArray();

        StartCoroutine("MakeCard");
    }

    public IEnumerator MakeCard()
    {
        startPosition = new Vector2(0, -5);

        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => UnityEngine.Random.Range(0f, 7f)).ToArray();

        for (i = 0; i < 16; i++)
        {
            Card go = Instantiate(cardPrefab);
            var T = moveDuration;

            cardList.Add(go);

            if(i==0)
            {
                float x = 0;
                float y = -2;
                cardList[i].endPosition = new Vector2(x, y);
            }
            if(0<i && i < 4)
            {
                float x = (i-2);
                float y = -1;
                cardList[i].endPosition = new Vector2(x, y);
            }
            if(3<i && i < 14)
            {
                float x = ((i-4)%5)-2;
                float y = (i/9);
                cardList[i].endPosition = new Vector2(x, y);
            }
            if(13 < i && i < 16)
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

            cardList[i].startPosition = startPosition;
            go.GetComponent<Card>().Setting(arr[i]);
            InvokeRepeating("CardMove", 0, Time.deltaTime);
            elapsedTime = 0f;
            yield return new WaitForSeconds(T);
        }

        GameManager.Instance.cardCount = arr.Length;
    }
    public void CardMove()
    {
        elapsedTime += Time.deltaTime;

        cardList[i].transform.position = Vector2.Lerp(cardList[i].startPosition, cardList[i].endPosition, elapsedTime / moveDuration);
        if (elapsedTime >= moveDuration)
        {
            CancelInvoke("CardMove");
        }
    }
}