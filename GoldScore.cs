using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldScore : MonoBehaviour
{
    // Variables
    public static GoldScore me;
    public float time = 5;
    public int gold;
    public int requiredGold = 5;
    public TextMeshProUGUI timerT;
    public TextMeshProUGUI goldT;
    public TextMeshProUGUI spaceT;

    // Ignore
    float lastTime;
    public bool showprompt;
    public List<Gold> goldBars = new List<Gold>();
    public bool critical;
    public bool over;
    public AudioSource m_criticalAudioSource;
    public AudioSource m_clockAudioSource;
    public AudioSource m_audioSource;

    // Sets up
    private void Awake()
    {
        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Sets up
        foreach (Gold g in FindObjectsOfType<Gold>())
            goldBars.Add(g);
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Timer
        if(Time.time >= lastTime + 1 & time > 0)
        {
            time -= 1;
            lastTime = Time.time;
        }
        if(time <= 5 & !critical)
        {
            m_criticalAudioSource.Play();
            Destroy(m_criticalAudioSource, 5);
            critical = true;
        }
        timerT.text = time.ToString();
        spaceT.enabled = showprompt;
        // Show gold
        goldT.text = gold + "/" + requiredGold;
        if (time == 0 & !over)
            GameOver();
    }

    // Adds gold
    public void AddGold()
    {
        gold += 1;
    }

    // When game is over
    public void GameOver()
    {
        m_audioSource.Play();
        m_clockAudioSource.Stop();
        over = true;
        if (gold >= requiredGold)
        {
            // Win here
        }
        else
        {
            // Loose here
        }
    }
}
