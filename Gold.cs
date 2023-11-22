using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    // Variables
    public Transform offsetPos;
    public Collider2D col;
    public SpriteRenderer m_renderer;
    public AudioSource m_audioSource;

    // Ignore
    public float dropTime;

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
        if (col.IsTouching(PlayerHand.me.handCol) & PlayerHand.me.canPickup & PlayerHand.me.currentGold == this)
        {
            PlayerHand.me.hoverGold = this;
            if (PlayerHand.me.grabbing > 0)
                PlayerHand.me.Grab(this);
            m_renderer.color = Color.yellow;
        }
        else
        {
            if(PlayerHand.me.hoverGold == this)
                PlayerHand.me.hoverGold = null;
            m_renderer.color = Color.white;
        }
        // Makes gold smaller if dropped
        if(dropTime > 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.1f, 0.1f, 0.1f), 0.04f);
            if (Time.time >= dropTime + 6)
                Destroy(gameObject);
        }
    }

    // Drops the gold
    public void Drop(bool fail)
    {
        if (fail)
            m_audioSource.Play();
        dropTime = Time.time;
    }
}
