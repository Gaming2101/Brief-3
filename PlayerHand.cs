using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    // Variables
    public static PlayerHand me;
    [Header("Change These!")]
    public float grabSpeed = 0.04f;
    public float depositSpeed = 0.01f;

    [Header("Ignore")]
    public Animator ani;
    public Vector3 goldBagPos = new Vector3(-12, -609, -142.548996f);
    public Collider2D handCol;
    public Transform handPos;
    public AudioSource m_audioSource;

    // Ignore
    public bool canPickup;
    public Gold heldGold; // Gold in hand
    public Gold currentGold; // Gold that hand is moving to
    public Gold hoverGold; // Gold that can be picked up
    public GoldBag hoverBag; // Bag that can store Gold
    public bool isDepositing;
    public float grabbing;
    public bool foundGold;

    // Sets up
    private void Awake()
    {
        me = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Sets up everything
        ani = GetComponent<Animator>();
        InputController.grab.AddListener(Interact); // Change the input manager to yours
        StartCoroutine(Wait(0.5f, 1));
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Decides if can pick up gold
        if(heldGold)
            canPickup = false;
        GoldScore.me.showprompt = hoverGold || hoverBag;
        if (Time.time >= grabbing + 1)
            grabbing = 0;
        // Moves player hand to Gold
        if (currentGold & !canPickup & !foundGold & !isDepositing)
        {
            transform.position = Vector3.Lerp(transform.position, currentGold.offsetPos.transform.position, grabSpeed);
            if(Vector3.Distance(transform.position, currentGold.offsetPos.transform.position) <= 0.1f)
            {
                foundGold = true;
                StartCoroutine(Wait(0.2f, 2));
            }
        }
        // Moves player hand to Gold Bag
        if (isDepositing)
        {
            foundGold = false;
            transform.localPosition = Vector3.Lerp(transform.localPosition, goldBagPos, depositSpeed);
            if (Vector3.Distance(transform.localPosition, goldBagPos) <= 8f)
                ani.CrossFadeInFixedTime("Deposit", 0.1f);
            if (Vector3.Distance(transform.localPosition, goldBagPos) <= 7f)
            {
                currentGold = null;
                canPickup = false;
                StartCoroutine(Wait(0.5f, 1));
                isDepositing = false;
            }
        }
        // Moves gold to player hand
        if (heldGold)
            heldGold.transform.position = Vector3.Lerp(heldGold.transform.position, handPos.transform.position, 0.9f);
    }

    // Waits and then executes the corresponding commands
    public IEnumerator Wait(float time, int type)
    {
        yield return new WaitForSeconds(time);
        if(type == 1)
        {
            MoveToGold(GoldScore.me.goldBars[Random.Range(0, GoldScore.me.goldBars.Count)]);
            yield return new WaitForSeconds(0.2f);
            if (heldGold)
                Drop(false);
        }
        if (type == 2)
        {
            canPickup = true;
            ani.CrossFadeInFixedTime("Grab", 0.1f);
            yield return new WaitForSeconds(0.2f);
            MoveToBag(FindObjectOfType<GoldBag>());
        }
    }

    // Moves hand to gold position with offset
    public void MoveToGold(Gold gold)
    {
        currentGold = gold;
    }

    // Moves hand to gold bag
    public void MoveToBag(GoldBag bag)
    {
        isDepositing = true;
    }

    // Grabs or Drops the Gold
    public void Interact()
    {
        if (!heldGold)
            grabbing = Time.time;
        else
            Drop(true);
    }

    // Grabs the gold
    public void Grab(Gold gold)
    {
        GoldScore.me.goldBars.Remove(gold);
        m_audioSource.Play();
        heldGold = gold;
        hoverGold = null;
    }

    // Drops the gold
    public void Drop(bool press)
    {
        if (press & hoverBag)
            GoldScore.me.AddGold();
        heldGold.Drop(!press);
        heldGold = null;
    }
}
