using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;

    public GameObject front;
    public GameObject back;
    public GameObject clickBack;
    public Animator anim;

    public SpriteRenderer frontImage;

    public int idx = 0;

    public bool isEnd { get => Vector3.Distance(transform.position, endPosition) < 0.001f; }

    public AudioClip clip;
    AudioSource audioSource;

    void Start()
    {
     audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    //void Update(){}

    public void Setting(int number)
    {
     idx = number;
     frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }

    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);

        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);
        clickBack.SetActive(false);

        if (GameManager.Instance.firstCard == null )
        {
         GameManager.Instance.firstCard = this;
        }
        else
        {
            GameManager.Instance.secondCard = this;
            GameManager.Instance.Matched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    void DestroyCardInvoke()
    {
        Board.Instance.cardList.Remove(this);
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        clickBack.SetActive(true);
    }
}
