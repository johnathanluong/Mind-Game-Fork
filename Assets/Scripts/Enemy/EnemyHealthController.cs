using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthController : MonoBehaviour
{

    [SerializeField] Enemy enemyStats;
    [SerializeField] TMP_Text healthPoints;
    [SerializeField] Image healthBarImage;

    // Start is called before the first frame update
    void Start()
    {
        setHealth(enemyStats.MaxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setup()
    {
        healthBarImage = gameObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();
        healthPoints = gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public Enemy EnemyStats
    {
        get { return enemyStats; }
        set { enemyStats = value; }
    }

    public void setHealth(int curHP)
    {
        healthBarImage.fillAmount = (float) curHP / enemyStats.MaxHP;
        healthPoints.text = curHP + "/" + enemyStats.MaxHP;
    }

    public IEnumerator updateHealth(int curHP)
    {
        int numFrames = 15;
        float incrementFill = ((float) enemyStats.CurHP / enemyStats.MaxHP - healthBarImage.fillAmount) / numFrames;
        healthPoints.text = curHP + "/" + enemyStats.MaxHP;

        for (int i = 0; i < numFrames; i++)
        {
            healthBarImage.fillAmount += incrementFill;
            
            yield return new WaitForSeconds(5);
        }
        
        // Accounts for error from incrementFill subtraction
        healthBarImage.fillAmount = (float) enemyStats.CurHP / enemyStats.MaxHP;
    }
}
