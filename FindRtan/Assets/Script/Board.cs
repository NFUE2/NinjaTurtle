using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Board : MonoBehaviour
{
    public static Board Instance;

    public Card cardPrefab;

    public List<Card> cardList;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float speed = 10.0f;
    private float startTime;
    private float distanceLength;

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
        var c = cardPrefab;
        var L = cardList;

        startPosition = new Vector3(0, -5);
        startTime = Time.time;
        distanceLength = Vector3.Distance(startPosition, endPosition);

        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => UnityEngine.Random.Range(0f, 7f)).ToArray();

        for (int i = 0; i < 16; i++)
        {
            Card go = Instantiate(c);
            L.Add(go);
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;

            L[i].startPosition = startPosition;
            L[i].endPosition = new Vector3(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }

        GameManager.Instance.cardCount = arr.Length;
    }
    void Update()
    {
        var c = cardPrefab;
        var L = cardList;

        for (int i = 0; i < L.Count; i++)
        {
            float distcovered = (Time.time - startTime) * speed;
            float franJourney = distcovered / distanceLength;
            L[i].transform.position = Vector3.Lerp(L[i].startPosition, L[i].endPosition, franJourney);
        }
    }
}