using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBag : MonoBehaviour
{
    public Collider2D col;
    public SpriteRenderer m_renderer;
    public AudioSource m_audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Sets up
        col = GetComponent<Collider2D>();
        m_renderer = GetComponent<SpriteRenderer>();
        m_audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame

    void Update()
    {
        // Changes colour if touching hand
        if (col.IsTouching(PlayerHand.me.handCol) & PlayerHand.me.heldGold)
        {
            PlayerHand.me.hoverBag = this;
            m_renderer.color = Color.yellow;
        }
        else
        {
            if (PlayerHand.me.hoverBag == this)
                PlayerHand.me.hoverBag = null;
            m_renderer.color = Color.white;
        }
    }

    public void Deposit()
    {
        m_audioSource.Play();
    }
}
