using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeEvent : MonoBehaviour
{
    [Serializable] class KeyMap
    {
        public KeyCode key;
        public Sprite value;
    }
    [SerializeField] GameObject pressEffect;
    [SerializeField] GameObject keyTemplate;
    [SerializeField] List<GameObject> movingKeys;
    [SerializeField] List<KeyCode> possibleKeys;
    [SerializeField] List<KeyCode> correctInputs;
    [SerializeField] List<KeyMap> keyMappings;
    [SerializeField] bool eventActive;
    [SerializeField] int activeKeys;
    [SerializeField] int focusedKey;
    [SerializeField] int numKeys;
    [SerializeField] int numPerfect;
    [SerializeField] List<AudioClip> perfectSounds;
    [SerializeField] AudioClip wrongSound;
    [SerializeField] List<GameObject> sections;
    private float speed;
    private AudioSource audioManager;
    private Color perfect = new Color32(0, 255, 0, 150);
    private Color neutral = new Color32(0, 255, 0, 50);
    private Color bad = new Color32(255, 0, 0, 150);

    // Start is called before the first frame update
    void Start()
    {
        speed = 13f;
        eventActive = false;
        numKeys = 4;
        audioManager = this.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (focusedKey == numKeys)
            endEvent();

        if (movingKeys.Count > 0)
        {
            for (int i = 0; i < activeKeys; i++)
                movingKeys[i].transform.position += (Vector3.left * speed) * Time.deltaTime;

            if (movingKeys[activeKeys - 1].transform.position.x < 5.25f && activeKeys != numKeys)
                activeKeys++;
            
            if (movingKeys[focusedKey].transform.position.x < -10f)
            {
                focusedKey++;
            }
        }

        if (eventActive)
        {
            if (Input.anyKey && movingKeys[0].transform.position.x < 8.4)
            {
                if (focusedKey < numKeys && movingKeys[focusedKey].GetComponent<QuickTimeCollision>().IsPerfectTiming() == 1)
                {
                    movingKeys[focusedKey].SetActive(false);
                    if (Input.GetKeyDown(correctInputs[focusedKey]))
                    {
                        sections[0].GetComponent<SpriteRenderer>().color = perfect;
                        audioManager.PlayOneShot(perfectSounds[focusedKey % 4]);
                        numPerfect++;
                    }
                    else
                    {
                        // Play incorrect key sound
                        sections[0].GetComponent<SpriteRenderer>().color = bad;
                        audioManager.PlayOneShot(wrongSound);
                    }
                    focusedKey++;
                }
                else
                    if (sections[0].GetComponent<SpriteRenderer>().color == neutral)
                    {
                        sections[0].GetComponent<SpriteRenderer>().color = bad;
                        audioManager.PlayOneShot(wrongSound);
                    }
            }
            else
                sections[0].GetComponent<SpriteRenderer>().color = neutral;
        }
        else
            sections[0].GetComponent<SpriteRenderer>().color = neutral;
    }

    public int getNumPerfect()
    {
        return numPerfect;
    }

    public int getNumKeys()
    {
        return numKeys;
    }

    public IEnumerator normalQuicktime()
    {
        yield return new WaitForSeconds(0.1f);
        numPerfect = 0;
        activeKeys = 1;
        focusedKey = 0;
        numKeys = 4;
        foreach (GameObject obj in sections)
            obj.SetActive(true);

        // Generate arrows for input
        for (int i = 0; i < numKeys; i++)
        {
            System.Random rand = new System.Random();
            correctInputs.Add(possibleKeys[rand.Next(0, possibleKeys.Count)]);
        }

        for (int i = 0; i < correctInputs.Count; i++)
        {
            for (int j = 0; j < keyMappings.Count; j++)
            {
                if (correctInputs[i] == keyMappings[j].key)
                {
                    GameObject temp = Instantiate(keyTemplate, keyTemplate.transform.position, keyTemplate.transform.rotation);
                    temp.GetComponent<SpriteRenderer>().sprite = keyMappings[j].value;
                    movingKeys.Add(temp);
                    break;
                }
            }
        }

        eventActive = true;

        while (eventActive)
            yield return null;
        yield return eventActive;
    }

    public IEnumerator comboQuicktime(MoveBase move)
    {
        yield return new WaitForSeconds(0.1f);
        numPerfect = 0;
        activeKeys = 1;
        focusedKey = 0;

        int numKeySets = 0;

        if (move.Keys1.Count > 0)
            numKeySets++;
        if(move.Keys2.Count > 0)
            numKeySets++;
        if(move.Keys3.Count > 0)
            numKeySets++;

        System.Random rand = new System.Random();
        int randSet = rand.Next(0, numKeySets) + 1;

        switch (randSet)
        {
            case 1:
                numKeys = move.Keys1.Count;
                correctInputs = new List<KeyCode>(move.Keys1);
                break;
            case 2:
                numKeys = move.Keys2.Count;
                correctInputs = new List<KeyCode>(move.Keys2);
                break;
            case 3:
                numKeys = move.Keys3.Count;
                correctInputs = new List<KeyCode>(move.Keys3);
                break;
        }

        foreach (GameObject obj in sections)
            obj.SetActive(true);

        // Generate arrows for input
        for (int i = 0; i < numKeys; i++)
        {
            for (int j = 0; j < keyMappings.Count; j++)
            {
                if (correctInputs[i] == keyMappings[j].key)
                {
                    GameObject temp = Instantiate(keyTemplate, keyTemplate.transform.position, keyTemplate.transform.rotation);
                    temp.GetComponent<SpriteRenderer>().sprite = keyMappings[j].value;
                    movingKeys.Add(temp);
                    break;
                }
            }
        }

        eventActive = true;
        while (eventActive)
            yield return null;
        yield return eventActive;
    }

    public void endEvent()
    {
        for (int i = 0; i < movingKeys.Count; i++)
        {
            GameObject.Destroy(movingKeys[i]);
        }
        movingKeys.Clear();
        correctInputs.Clear();
        eventActive = false;
        foreach (GameObject obj in sections)
            obj.SetActive(false);
    }
}
