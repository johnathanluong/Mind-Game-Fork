using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int World=0;

    [SerializeField] PlayerStats playerStats;
    [SerializeField] List<PartyStats> partyStats;
    [SerializeField] MemoryList memoryList;

    private List<Vector2> playerMoveInput;

    public bool playerStopped = false;
    public bool inBattle = false;

    private GameObject faderObj;
    private Fader fader;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        playerMoveInput = new List<Vector2>();
    }

    public PlayerStats PlayerStats
    {
        get { return playerStats; }
    }
    public List<PartyStats> PartyStats
    {
        get { return partyStats; }
    }
    public MemoryList MemoryList
    {
        get { return memoryList; }
    }


    public void StartCombat()
    {
        List<EnemyWorld> enemies = FindObjectsOfType<EnemyWorld>().ToList();

        for(int i = 0; i < enemies.Count; i++)
        {
            enemies[i].StopMovement();
        }
    }

    public void UpdateIsMoving(bool isMoving)
    {
        playerStats.IsMoving = isMoving;
    }
    public IEnumerator FadeIn(float time)
    {
        faderObj = GameObject.FindGameObjectWithTag("Fader");
        fader = faderObj.GetComponent<Fader>();
        yield return fader.FadeIn(time);
    }
    public IEnumerator FadeOut(float time)
    {
        faderObj = GameObject.FindGameObjectWithTag("Fader");
        fader = faderObj.GetComponent<Fader>();
        yield return fader.FadeOut(time);
    }

    public void pushInput(Vector2 input)
    {
        playerMoveInput.Add(input);
    }
    public bool readyInput()
    {
        return playerMoveInput.Count > 1;
    }
    public Vector2 popInput()
    {
        Vector2 input = playerMoveInput[0];
        playerMoveInput.RemoveAt(0);
        return input;
    }

    public void clearPlayerInput()
    {
        playerMoveInput.Clear();
    }
}
