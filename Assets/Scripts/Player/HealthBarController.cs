using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarController : MonoBehaviour
{

    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject damage;
    private TMP_Text healthPoints;
    private TMP_Text damageDone;
    private Image healthBarImage;

    // Start is called before the first frame update
    void Start()
    {
        healthBarImage = gameObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();
        healthPoints = gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        damageDone = damage.GetComponent<TMP_Text>();
        setHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        healthBarImage.fillAmount = (float) playerStats.CurHP / playerStats.MaxHP;
        healthPoints.text = playerStats.CurHP + "/" + playerStats.MaxHP;
    }

    public IEnumerator updateHealth()
    {
        int numFrames = 30;
        float incrementFill = ((float) playerStats.CurHP / playerStats.MaxHP - healthBarImage.fillAmount) / numFrames;
        healthPoints.text = playerStats.CurHP + "/" + playerStats.MaxHP;

        for (int i = 0; i < numFrames; i++)
        {
            healthBarImage.fillAmount += incrementFill;
            
            yield return new WaitForSeconds(1f/(numFrames * 2));
        }
        
        // Accounts for error from incrementFill subtraction
        healthBarImage.fillAmount = (float) playerStats.CurHP / playerStats.MaxHP;
    }
}
