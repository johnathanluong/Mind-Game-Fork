using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEvent : MonoBehaviour
{
    [SerializeField] List<GameObject> zones;
    [SerializeField] GameObject shield;
    [SerializeField] List<Sprite> shieldSprites;
    [SerializeField] List<AudioClip> shieldNoises;
    private AudioSource audio;

    private Vector3 speedVector = new Vector3(20f, 0, 0);
    private string direction;
    private bool eventIsActive;
    private bool success;

    void Start()
    {
        audio = this.gameObject.AddComponent<AudioSource>();
    }

    public bool Success()
    {
        return success;
    }
    public IEnumerator StartEvent()
    {
        direction = "right";
        success = false;
        for (int i = 0; i < zones.Count; i++)
            zones[i].SetActive(true);
        shield.GetComponent<SpriteRenderer>().sprite = shieldSprites[0];
        shield.transform.position = new Vector3(0, -3.96f, 0);
        shield.SetActive(true);
        eventIsActive = true;

        while (eventIsActive)
            yield return null;
    }

    private IEnumerator endEvent()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < zones.Count; i++)
            zones[i].SetActive(false);
        shield.SetActive(false);
        eventIsActive = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (eventIsActive)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                direction = "idle";
                if (shield.transform.position.x >= -0.6f && shield.transform.position.x <= 0.6f)
                {
                    success = true;
                    shield.GetComponent<SpriteRenderer>().sprite = shieldSprites[0];
                    audio.PlayOneShot(shieldNoises[0]);
                }
                else
                {
                    success = false;
                    shield.GetComponent<SpriteRenderer>().sprite = shieldSprites[1];
                    audio.PlayOneShot(shieldNoises[1]);
                }
                
                StartCoroutine(endEvent());
            }
            if (direction == "right")
            {
                shield.transform.position += speedVector * Time.deltaTime;
                if (shield.transform.position.x >= 8f)
                    direction = "left";
            }
            else if (direction == "left")
            {
                shield.transform.position -= speedVector * Time.deltaTime;
                if (shield.transform.position.x <= -8f)
                    direction = "right";
            }
        }
    }
}
