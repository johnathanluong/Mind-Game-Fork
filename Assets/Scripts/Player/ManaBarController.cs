using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBarController : MonoBehaviour
{

    [SerializeField] PlayerStats playerStats;
    private TMP_Text manaPoints;
    private Image manaBarImage;

    // Start is called before the first frame update
    void Start()
    {
        manaBarImage = gameObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();
        manaPoints = gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        setMana();
    }

    public void setMana()
    {
        manaBarImage.fillAmount = (float) playerStats.CurMP / playerStats.MaxMP;
        manaPoints.text = playerStats.CurMP + "/" + playerStats.MaxMP;
    }

    public IEnumerator updateMana()
    {
        int numFrames = 30;
        float incrementFill = ((float) playerStats.CurMP / playerStats.MaxMP - manaBarImage.fillAmount) / numFrames;
        manaPoints.text = playerStats.CurMP + "/" + playerStats.MaxMP;

        for (int i = 0; i < numFrames; i++)
        {
            manaBarImage.fillAmount += incrementFill;
            
            yield return new WaitForSeconds(1f/(numFrames * 2));
        }
        
        // Accounts for error from incrementFill subtraction
        manaBarImage.fillAmount = (float) playerStats.CurMP / playerStats.MaxMP;
    }
}
