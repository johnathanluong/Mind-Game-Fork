using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AllyManaController : MonoBehaviour
{
    [SerializeField] PartyStats allyStats;
    private TMP_Text manaPoints;
    private Image manaBarImage;

    public PartyStats AllyStats
    {
        set { allyStats = value; }
    }

    // Start is called before the first frame update
    public void setup()
    {
        manaBarImage = gameObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();
        manaPoints = gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        setMana();
    }

    public void setMana()
    {
        manaBarImage.fillAmount = (float) allyStats.CurMP / allyStats.MaxMP;
        manaPoints.text = allyStats.CurMP + "/" + allyStats.MaxMP;
    }

    public IEnumerator updateMana()
    {
        int numFrames = 30;
        float incrementFill = ((float) allyStats.CurMP / allyStats.MaxMP - manaBarImage.fillAmount) / numFrames;
        manaPoints.text = allyStats.CurMP + "/" + allyStats.MaxMP;

        for (int i = 0; i < numFrames; i++)
        {
            manaBarImage.fillAmount += incrementFill;
            
            yield return new WaitForSeconds(1f/(numFrames * 2));
        }
        
        // Accounts for error from incrementFill subtraction
        manaBarImage.fillAmount = (float) allyStats.CurMP / allyStats.MaxMP;
    }
}
