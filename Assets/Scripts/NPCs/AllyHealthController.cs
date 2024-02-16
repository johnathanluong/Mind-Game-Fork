using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllyHealthController : MonoBehaviour
{
    [SerializeField] PartyStats allyStats;
    [SerializeField] GameObject damageCounter;
    private TMP_Text healthPoints;
    private TMP_Text damageDone;
    private Image healthBarImage;

    public PartyStats AllyStats
    {
        set { allyStats = value; }
    }
    public GameObject DamageCounter
    {
        set { damageCounter = value; }
    }

    // Start is called before the first frame update
    public void setup()
    {
        healthBarImage = gameObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();
        healthPoints = gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        damageDone = damageCounter.GetComponent<TMP_Text>();
        setHealth();
    }

    public IEnumerator takeDamage(int damage)
    {
        StartCoroutine(textAnimation(damage));
        yield return StartCoroutine(updateHealth());
        yield return new WaitForSeconds(1f);
        damageDone.text = "";
    }

    private IEnumerator textAnimation(int damage)
    {
        damageDone.text = damage.ToString();
        yield break;
    }

    public void setHealth()
    {
        healthBarImage.fillAmount = (float) allyStats.CurHP / allyStats.MaxHP;
        healthPoints.text = allyStats.CurHP + "/" + allyStats.MaxHP;
    }

    public IEnumerator updateHealth()
    {
        int numFrames = 30;
        float incrementFill = ((float) allyStats.CurHP / allyStats.MaxHP - healthBarImage.fillAmount) / numFrames;
        healthPoints.text = allyStats.CurHP + "/" + allyStats.MaxHP;

        for (int i = 0; i < numFrames; i++)
        {
            healthBarImage.fillAmount += incrementFill;
            
            yield return new WaitForSeconds(1f/(numFrames * 2));
        }
        
        // Accounts for error from incrementFill subtraction
        healthBarImage.fillAmount = (float) allyStats.CurHP / allyStats.MaxHP;
    }
}
