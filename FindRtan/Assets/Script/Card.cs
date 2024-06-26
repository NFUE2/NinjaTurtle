using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;

    public GameObject front;
    public GameObject back;
    //public GameObject clickBack;
    public Animator anim;

    public SpriteRenderer frontImage;

    public int idx = 0;

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
     frontImage.sprite = Resources.Load<Sprite>($"turtle{idx}");
    }

    public void OpenCard()
    {
        if (Board.Instance.gameStart == true)
        {
            audioSource.PlayOneShot(clip);

            anim.SetBool("isOpen", true);
            //front.SetActive(true);
            //back.SetActive(false);
            //clickBack.SetActive(false);

            if (GameManager.Instance.firstCard == null)
            {
                GameManager.Instance.firstCard = this;
            }
            else
            {
                GameManager.Instance.secondCard = this;
                GameManager.Instance.Matched();
            }
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
        back.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        //clickBack.SetActive(true);
    }
}
