using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowDamageOnEnemy : MonoBehaviour
{
    private TMP_Text damageDone;
    private GameObject HPFiller;
    [SerializeField] Enemy enemyStats;
    private Image hpImage;
    private Image background;

    // Start is called before the first frame update
    void Start()
    {
        damageDone = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        HPFiller = gameObject.transform.GetChild(1).gameObject;
        hpImage = HPFiller.GetComponent<Image>();
        background = gameObject.GetComponent<Image>();
    }

    public Enemy EnemyStats
    {
        get { return enemyStats; }
        set { enemyStats = value; }
    }

    // May have to pass in reference to enemy taking damage
    public IEnumerator doDamage(int damage, int curHP)
    {
        StartCoroutine(textAnimation(damage));
        yield return StartCoroutine(updateHealth(curHP));
        damageDone.text = "";
        HPFiller.SetActive(false);
        background.enabled = false;
    }

    private IEnumerator textAnimation(int damage)
    {
        damageDone.text = damage.ToString();
        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator updateHealth(int curHP)
    {
        HPFiller.SetActive(true);
        background.enabled = true;
        int numFrames = 30;
        float incrementFill = ((float) curHP / enemyStats.MaxHP - hpImage.fillAmount) / numFrames;

        for (int i = 0; i < numFrames; i++)
        {
            hpImage.fillAmount += incrementFill;
            
            yield return new WaitForSeconds(1f/(numFrames * 2));
        }
        
        // Accounts for error from incrementFill subtraction
        hpImage.fillAmount = (float) curHP / enemyStats.MaxHP;
        yield return new WaitForSeconds(1f);
    }
}
