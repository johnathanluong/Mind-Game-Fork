using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
public class BattleSystem : MonoBehaviour
{
    public enum State { Start, ActionSelect, Attack, Special, Item, AllySelect, EnemySelect, Guard, InputUnavailable, EndBattle }
    class BattleEnemy
    {
        public Enemy EnemyClass { get; set; }
        public int CurHP { get; set; }
        public GameObject Model { get; set; }
        public GameObject Selector { get; set; }

        public BattleEnemy(Enemy enemyClass, int curHP, GameObject model, GameObject selector)
        {
            EnemyClass = enemyClass;
            CurHP = curHP;
            Model = model;
            Selector = selector;
        }
    }

    [Serializable] class DialogueSet
    {
        public string bossName;
        public TextAsset dialogueScript;
    }
    [SerializeField] List<TMP_Text> actions;
    [SerializeField] List<TMP_Text> options;
    [SerializeField] List<string> battleIntros;
    [SerializeField] List<BattleEnemy> enemies = new List<BattleEnemy>();
    [SerializeField] TMP_Text dialogText;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] BattleState battleState;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject manaBar;
    [SerializeField] GameObject enemyHealthBar;
    [SerializeField] GameObject playerSprite;
    [SerializeField] GameObject allySprite;
    [SerializeField] GameObject enemySpriteTemplate;
    [SerializeField] List<GameObject> enemySprites;
    [SerializeField] GameObject enemySelectFieldTemplate;
    [SerializeField] List<GameObject> enemySelectFields;
    [SerializeField] GameObject quicktimeEvent;
    [SerializeField] GameObject dialogueManager;
    [SerializeField] GameObject inputManager;
    [SerializeField] List<TMP_Text> allySelectors;
    [SerializeField] List<GameObject> scrollArrows;
    [SerializeField] List<GameObject> partyStats;
    [SerializeField] List<DialogueSet> bossDialogues;
    [SerializeField] List<DialogueSet> battleEndDialogues;
    [SerializeField] List<GameObject> turnIndicators;
    [SerializeField] List<GameObject> disableForDialogue;
    [SerializeField] List<Sprite> attackTypes;
    [SerializeField] List<GameObject> attackStatDisplay;
    [SerializeField] GameObject guardQuicktime;
    private int curAction;
    private int curOption;
    private int curTarget;
    private int itemPage;
    private int curTurn;
    public bool isBoss;
    private int Enemyremoval=0;
    private bool hasAlly;
    private bool allyJustAdded;
    private bool dialoguePlayed;
    private State curState;
    private State sourceState;
    private HealthBarController healthBarController;
    private ManaBarController manaBarController;
    private EnemyHealthController enemyHealthController;
    private GameManager gameManager;
    private AudioSource Audio;
    private GameObject endScreen;
    private GameObject Smo;
    public AudioClip soundClip1=null;
    public AudioClip soundClip2=null;
    public AudioClip soundClip3=null;
    public AudioClip soundClip4=null;
    public AudioClip soundClip5=null;
    public AudioClip soundClip6=null;
    //official music
    public AudioClip Tutorial1=null;
    public AudioClip Tutorial2=null;
    public AudioClip Barney=null;
    public AudioClip L=null;
    public AudioClip MCBoss=null;
    private int TutorialPhase2=0;
    private int[] isGuarding = new int[2];
    //public Animation anim;
    int j=0;
    int p =0;
    int k=0;
    //attack sound effect
    public AudioClip AttackS =null;
    public AudioClip SpecialS =null;
    void Start() 
    {
        endScreen = GameObject.Find("BlackImage");
        endScreen.SetActive(false);
        int Complete1 = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("Y")).value;
        TutorialPhase2 = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("tem")).value;
        curAction = 0;
        curTurn = 0;
        curState = State.Start;
        StartCoroutine(fightStart());
        Audio = this.gameObject.AddComponent<AudioSource>();
        gameManager = GameManager.Instance;
        healthBarController = healthBar.GetComponent<HealthBarController>();
        manaBarController = manaBar.GetComponent<ManaBarController>();
        enemyHealthController = enemyHealthBar.GetComponent<EnemyHealthController>();
        if (Complete1 == 0)
            playerSprite.GetComponent<SpriteRenderer>().sprite = playerStats.BattleSprite;
        else if (Complete1 == 1)
            playerSprite.GetComponent<SpriteRenderer>().sprite = playerStats.BattleSprite2;
        else if(Complete1 == 2)
            playerSprite.GetComponent<SpriteRenderer>().sprite = playerStats.BattleSprite3;
        
        curTarget = 0;
    }
    
    void Update() 
    {
        BattleAudio();
        // Allow for selection of ACTIONS
        switch (curState)
        {
            case State.ActionSelect:
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    actions[curAction].color = Color.black;
                    curAction = ((curAction - 1) % actions.Count);
                    if (curAction == -1)
                        curAction = actions.Count - 1;
                    actions[curAction].color = Color.blue;
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    actions[curAction].color = Color.black;
                    curAction = (curAction + 1) % actions.Count;
                    actions[curAction].color = Color.blue;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    curState = (State) System.Enum.Parse(typeof(State), actions[curAction].text);
                    actions[curAction].color = Color.black;
                    dialogText.text = "";
                    curOption = 0;

                    if (curState == State.Attack)
                    {
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            enemies[i].Selector.SetActive(true);
                            enemies[i].Selector.transform.GetChild(0).GetChild(0).GetComponent<EnemyHealthController>().setHealth(enemies[i].CurHP);
                        }
                        enemies[0].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.blue;
                        enemySprites[0].GetComponent<SpriteRenderer>().color = new Color32(255, 197, 197, 255);
                        curTarget = 0;
                        sourceState = State.Attack;
                        curState = State.EnemySelect;
                    }
                    if (curState == State.Special)
                    {
                        setAttackText();

                        if (options[curOption].text != "")
                            options[curOption].color = Color.blue;
                        else
                            StartCoroutine(noSpecialsAvailable());
                    }
                    if (curState == State.Item)
                    {
                        itemPage = 1;

                        for (int i = 0; i < Math.Min(playerStats.Inventory.Count, 4); i++)
                            options[i].text = playerStats.Inventory[i].name;

                        if (playerStats.Inventory.Count > 4)
                            activateScrollArrows();

                        if (options[curOption].text != "")
                            options[curOption].color = Color.blue;
                        else
                            StartCoroutine(noItemsAvailable());
                    }
                    if (curState == State.Guard)
                    {
                        StartCoroutine(guard());
                    }
                }
                break;
            case State.Attack:
                break;
            case State.Special:
                optionInputs();
                break;
            case State.Item:
                optionInputs();
                break;
            case State.Guard:
                break;
            case State.EnemySelect:
                selectEnemyInputs();
                break;
            case State.AllySelect:
                selectAllyInputs();
                break;
            default:
                break;
        }
    }

    private void selectAllyInputs()
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && allySelectors.Count > 1)
        {
            allySelectors[curTarget].color = Color.black;
            curTarget = (curTarget + 1) % 2;
            allySelectors[curTarget].color = Color.blue;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            allySelectors[curTarget].color = Color.black;
            curState = State.InputUnavailable;
            StartCoroutine(selectTarget());
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            curState = sourceState;
            allySelectors[curTarget].color = Color.black;
            sourceState = State.InputUnavailable;
            dialogText.text = "";
            setUpPage();
            options[curOption % 4].color = Color.blue;
            curTarget = 0;
        }
    }
    private void selectEnemyInputs()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (enemies.Count > 1 && (curTurn == 0) ? !playerStats.MoveSet[curOption].Base.Aoe : !battleState.Ally.MoveSet[curOption].Base.Aoe)
            {
                enemies[curTarget].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.black;
                if (curTarget == 0)
                {
                    enemySprites[curTarget].GetComponent<SpriteRenderer>().color = Color.white;
                    curTarget = enemies.Count - 1;
                    enemies[curTarget].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.blue;
                    enemySprites[curTarget].GetComponent<SpriteRenderer>().color = new Color32(255, 197, 197, 255);
                }
                else
                {
                    enemySprites[curTarget].GetComponent<SpriteRenderer>().color = Color.white;
                    curTarget--;
                    enemies[curTarget].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.blue;
                    enemySprites[curTarget].GetComponent<SpriteRenderer>().color = new Color32(255, 197, 197, 255);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (enemies.Count > 1 && (curTurn == 0) ? !playerStats.MoveSet[curOption].Base.Aoe : !battleState.Ally.MoveSet[curOption].Base.Aoe)
            {
                enemies[curTarget].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.black;
                if (curTarget == enemies.Count - 1)
                {
                    enemySprites[curTarget].GetComponent<SpriteRenderer>().color = Color.white;
                    curTarget = 0;
                    enemies[curTarget].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.blue;
                    enemySprites[curTarget].GetComponent<SpriteRenderer>().color = new Color32(255, 197, 197, 255);
                }
                else
                {
                    enemySprites[curTarget].GetComponent<SpriteRenderer>().color = Color.white;
                    curTarget++;
                    enemies[curTarget].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.blue;
                    enemySprites[curTarget].GetComponent<SpriteRenderer>().color = new Color32(255, 197, 197, 255);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            curState = State.InputUnavailable;
            if ((curTurn == 0) ? playerStats.MoveSet[curOption].Base.Aoe : battleState.Ally.MoveSet[curOption].Base.Aoe)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.black;
                    enemySprites[i].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            else
            {
                enemies[curTarget].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.black;
                enemySprites[curTarget].GetComponent<SpriteRenderer>().color = Color.white;
            }
            for (int i = 0; i < enemies.Count; i++)
                enemies[i].Selector.SetActive(false);
            enemySprites[curTarget].GetComponent<SpriteRenderer>().color = Color.white;
            StartCoroutine(selectTarget());
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (sourceState == State.Attack)
            {
                enemies[curTarget].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.black;
                enemySprites[curTarget].GetComponent<SpriteRenderer>().color = Color.white;
                for (int i = 0; i < enemies.Count; i++)
                    enemies[i].Selector.SetActive(false);
                curTarget = 0;
                StartCoroutine(selectAction());
                sourceState = State.InputUnavailable;
            }
            else if (sourceState == State.Special)
            {
                for (int i = 0; i < enemies.Count; i++)
                    enemies[i].Selector.SetActive(false);

                if ((curTurn == 0) ? playerStats.MoveSet[curOption].Base.Aoe : battleState.Ally.MoveSet[curOption].Base.Aoe)
                {
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.black;
                        enemySprites[i].GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
                else
                {
                    enemies[curTarget].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.black;
                    enemySprites[curTarget].GetComponent<SpriteRenderer>().color = Color.white;
                }
        
                curTarget = 0;

                setAttackText();
                curState = State.Special;
                sourceState = State.InputUnavailable;
                options[curOption].color = Color.blue;
            }
            else if (sourceState == State.Item)
            {
                curState = State.Item;
                sourceState = State.InputUnavailable;
                enemySprites[curTarget].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    private IEnumerator normalAttack()
    {
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        dialogText.text = "";
        yield return StartCoroutine(quicktimeEvent.GetComponent<QuickTimeEvent>().normalQuicktime());
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(.3f);

        yield return StartCoroutine(attackDamageEnemy(curTarget));
        
        if (curState == State.EndBattle)
        {
            yield return StartCoroutine(wonBattle());
        }
        else
        {
            yield return StartCoroutine(continueBattle(false));
        }
    }

    private void setAttackText()
    {
        if (curTurn == 0)
        {
            for (int i = 0; i < options.Count; i++)
            {
                options[i].transform.position += new Vector3(0, 0.15f, 0);
                if (i < playerStats.MoveSet.Count)
                {
                    options[i].text = playerStats.MoveSet[i].Base.name;
                    if (playerStats.MoveSet[i].Base.Type == Types.Physical)
                        attackStatDisplay[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = attackTypes[0];
                    else if (playerStats.MoveSet[i].Base.Type == Types.Magical)
                        attackStatDisplay[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = attackTypes[1];
                    else
                        attackStatDisplay[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = attackTypes[2];
                    attackStatDisplay[i].transform.GetChild(1).GetComponent<TMP_Text>().text = playerStats.MoveSet[i].Base.ManaCost + " MP";
                }
                else
                {
                    options[i].text = "";
                    attackStatDisplay[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                    attackStatDisplay[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
                }
            }
        }
        else
        {
            for (int i = 0; i < options.Count; i++)
            {
                options[i].transform.position += new Vector3(0, 0.15f, 0);
                if (i < battleState.Ally.MoveSet.Count)
                {
                    options[i].text = battleState.Ally.MoveSet[i].Base.name;
                    if (battleState.Ally.MoveSet[i].Base.Type == Types.Physical)
                        attackStatDisplay[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = attackTypes[0];
                    else if (battleState.Ally.MoveSet[i].Base.Type == Types.Magical)
                        attackStatDisplay[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = attackTypes[1];
                    else
                        attackStatDisplay[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = attackTypes[2];
                    attackStatDisplay[i].transform.GetChild(1).GetComponent<TMP_Text>().text = battleState.Ally.MoveSet[i].Base.ManaCost + " MP";
                }
                else
                {
                    options[i].text = "";
                    attackStatDisplay[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                    attackStatDisplay[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
                }
            }
        }
    }
    private void setUpPage()
    {
        for (int i = (itemPage - 1) * 4; i < itemPage * 4; i++)
        {
            if (playerStats.Inventory.Count > i)
                options[i % 4].text = playerStats.Inventory[i].name;
            else
                options[i % 4].text = "";
        }
    }
    private void activateScrollArrows()
    {
        if (itemPage > 1)
            scrollArrows[0].SetActive(true);
        else
            scrollArrows[0].SetActive(false);

        if (itemPage * 4 < playerStats.Inventory.Count)
            scrollArrows[1].SetActive(true);
        else
            scrollArrows[1].SetActive(false); 
    }
    private void optionInputs()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (curState == State.Item && playerStats.Inventory.Count > 4)
            {
                if (curOption - 2 < (itemPage - 1) * 4 && itemPage != 1)
                {
                    itemPage--;
                    setUpPage();
                    activateScrollArrows();
                }
            }

            if (curState == State.Item && curOption > 1)
            {
                options[curOption % 4].color = Color.black;
                curOption -= 2;
                options[curOption % 4].color = Color.blue;
            }
            else if (curState == State.Special && options[(curOption + 2) % 4].text != "")
            {
                options[curOption % 4].color = Color.black;
                curOption = (curOption + 2) % 4;
                options[curOption % 4].color = Color.blue;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (curState == State.Item && playerStats.Inventory.Count > 4)
            {
                if (curOption + 2 >= itemPage * 4 && curOption + 2 < playerStats.Inventory.Count)
                {
                    itemPage++;
                    setUpPage();
                    activateScrollArrows();   
                }
            }
            
            if (curState == State.Item && curOption + 2 <= playerStats.Inventory.Count - 1)
            {
                options[curOption % 4].color = Color.black;
                curOption += 2;
                options[curOption % 4].color = Color.blue;
            }
            else if (curState == State.Special && options[(curOption + 2) % 4].text != "")
            {
                options[curOption % 4].color = Color.black;
                curOption = (curOption + 2) % 4;
                options[curOption % 4].color = Color.blue;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (curOption % 4 == 0 || curOption % 4 == 2)
            {
                if (options[(curOption + 1) % 4].text != "")
                {
                    options[curOption % 4].color = Color.black;
                    curOption++;
                    options[curOption % 4].color = Color.blue;
                }
            }
            else
            {
                if (options[(curOption - 1) % 4].text != "")
                {
                    options[curOption % 4].color = Color.black;
                    curOption--;
                    options[curOption % 4].color = Color.blue;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            options[curOption % 4].color = Color.black;
            foreach (TMP_Text option in options)
                option.text = "";
            if (curState == State.Special)
            {
                for (int i = 0; i < options.Count; i++)
                {
                    options[i].transform.position += new Vector3(0, -0.15f, 0);
                    attackStatDisplay[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                    attackStatDisplay[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
                }
            }
            else if (curState == State.Item)
            {
                scrollArrows[0].SetActive(false);
                scrollArrows[1].SetActive(false);
            }
            StartCoroutine(selectAction());
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            string selectedOption = options[curOption % 4].text;
            options[curOption % 4].color = Color.black;
            foreach (TMP_Text option in options)
                option.text = "";

            if (curState == State.Special)  
            {
                for (int i = 0; i < options.Count; i++)
                {
                    options[i].transform.position += new Vector3(0, -0.15f, 0);
                    attackStatDisplay[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                    attackStatDisplay[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
                }
                if (curTurn == 0)
                {
                    if (playerStats.CurMP >= playerStats.MoveSet[curOption].Base.ManaCost)
                    {
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            enemies[i].Selector.SetActive(true);
                            enemies[i].Selector.transform.GetChild(0).GetChild(0).GetComponent<EnemyHealthController>().setHealth(enemies[i].CurHP);
                        }
                        if (playerStats.MoveSet[curOption].Base.Aoe)
                        {
                            for (int i = 0; i < enemies.Count; i++)
                            {
                                enemies[i].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.blue;
                                enemySprites[i].GetComponent<SpriteRenderer>().color = new Color32(255, 197, 197, 255);
                            }
                        }
                        else
                        {
                            enemies[0].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.blue;
                            enemySprites[0].GetComponent<SpriteRenderer>().color = new Color32(255, 197, 197, 255);
                        }
                        curTarget = 0;
                        sourceState = State.Special;
                        curState = State.EnemySelect;
                    }
                    else
                        StartCoroutine(notEnoughMana());
                }
                else
                {
                    if (battleState.Ally.CurMP >= battleState.Ally.MoveSet[curOption].Base.ManaCost)
                    {
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            enemies[i].Selector.SetActive(true);
                            enemies[i].Selector.transform.GetChild(0).GetChild(0).GetComponent<EnemyHealthController>().setHealth(enemies[i].CurHP);
                        }
                        if (battleState.Ally.MoveSet[curOption].Base.Aoe)
                        {
                            for (int i = 0; i < enemies.Count; i++)
                            {
                                enemies[i].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.blue;
                                enemySprites[i].GetComponent<SpriteRenderer>().color = new Color32(255, 197, 197, 255);
                            }
                        }
                        else
                        {
                            enemies[0].Selector.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().color = Color.blue;
                            enemySprites[0].GetComponent<SpriteRenderer>().color = new Color32(255, 197, 197, 255);
                        }
                        curTarget = 0;
                        sourceState = State.Special;
                        curState = State.EnemySelect;
                    }
                    else
                        StartCoroutine(notEnoughMana());
                }
            }
            else if (curState == State.Item)
            {
                scrollArrows[0].SetActive(false);
                scrollArrows[1].SetActive(false);

                if (playerStats.Inventory[curOption].Revive)
                {
                    curState = State.InputUnavailable;
                    if (!hasAlly || (battleState.Ally != null && battleState.Ally.CurHP > 0))
                    {
                        StartCoroutine(cannotRevive());
                    }
                    else
                    {
                        // Revive teammate
                        StartCoroutine(reviveMember());
                        playerStats.Inventory.RemoveAt(curOption);
                    }
                }
                else
                {
                    StartCoroutine(allySelectSetup());
                }
            }
        }
    }
    

    private IEnumerator allySelectSetup()
    {
        yield return StartCoroutine(typeMessage("Select the item's recipient.", 0f, curState));
        curTarget = 0;
        sourceState = State.Item;
        curState = State.AllySelect;
        partyStats[0].transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().color = Color.blue;
    }
    private IEnumerator cannotRevive()
    {
        yield return StartCoroutine(typeMessage("There's nobody to revive...", 1f, curState));
        dialogText.text = "";
        setUpPage();
        activateScrollArrows();
        options[curOption % 4].color = Color.blue;
        curState = State.Item;
    }

    private IEnumerator notEnoughMana()
    {
        //curState = State.InputUnavailable;
        yield return StartCoroutine(typeMessage("You don't have enough mana for this move...", 1f, curState));
        dialogText.text = "";
        setAttackText();
        options[curOption % 4].color = Color.blue;
        curState = State.Special;
    }
    private IEnumerator selectTarget()
    {
        if (sourceState == State.Attack)
        {
            yield return StartCoroutine(normalAttack());
        }
        else if (sourceState == State.Special)
        {
            if (curTurn == 0)
                yield return StartCoroutine(useSpecial(playerStats.MoveSet[curOption].Base));
            else
                yield return StartCoroutine(useSpecial(battleState.Ally.MoveSet[curOption].Base));
        }
        else if (sourceState == State.Item)
        {
            yield return StartCoroutine(useItem(playerStats.Inventory[curOption]));

            if (curTarget == 0 || (curTarget == 1 && battleState.Ally.CurHP > 0))
                playerStats.Inventory.Remove(playerStats.Inventory[curOption]);
            else
                yield break;
        }

        sourceState = State.InputUnavailable;
    }
    private IEnumerator allyTurn()
    {
        yield return StartCoroutine(selectAction());
        while (curTurn == 1)
            yield return null;
    }
    private IEnumerator enemyTurn(int enemyIndex)
    {
        // Random attack
        if (enemies[enemyIndex].CurHP > 0)
        {
            yield return StartCoroutine(enemyAttack(enemyIndex));

            // Check if enemy is last to do an attack and if battle is still ongoing
            if (!(curState == State.EndBattle) && enemyIndex == enemies.Count - 1)
            {
                // Return to action select state after turn if both player and enemy are still alive, end fight if player or enemy HP hits 0
                actions[curAction].color = Color.black;
                curAction = 0;
                yield return StartCoroutine(selectAction());
            }
        }
        else
            yield break;
    }

    private IEnumerator guard()
    {
        curState = State.InputUnavailable;
        disableForDialogue[0].SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);

        GuardEvent guardController = guardQuicktime.GetComponent<GuardEvent>();
        yield return StartCoroutine(guardController.StartEvent());

        disableForDialogue[0].SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        if (guardController.Success() == true)
        {
            isGuarding[curTurn] = 1;
            yield return StartCoroutine(typeMessage($"{(curTurn == 0 ? playerStats.Name : battleState.Ally.Name)} is guarding!", 1f, curState));
        }
        else
        {
            isGuarding[curTurn] = 0;
            yield return StartCoroutine(typeMessage($"{(curTurn == 0 ? playerStats.Name : battleState.Ally.Name)} failed to guard...", 1f, curState));
        }
        turnIndicators[curTurn].SetActive(false);
        yield return StartCoroutine(continueBattle(false));
    }
    private IEnumerator noSpecialsAvailable()
    {
        yield return StartCoroutine(typeMessage("You don't have any special moves yet.", .5f, curState));
        yield return StartCoroutine(selectAction());
    }
    private IEnumerator noItemsAvailable()
    {
        yield return StartCoroutine(typeMessage("You don't have any items yet.", .5f, curState));

        // Enemy turn
        yield return StartCoroutine(selectAction());
    }
    private IEnumerator fightStart()
    {
        GameObject tempSprite = enemySpriteTemplate;
        GameObject tempSelect = enemySelectFieldTemplate;
        enemySpriteTemplate.SetActive(true);
        for (int i = 0; i < battleState.EnemyList.Count; i++)
        {
            tempSprite = Instantiate(enemySpriteTemplate, enemySpriteTemplate.transform.position, enemySpriteTemplate.transform.rotation);

            // Works for up to 3 enemies populating scene, no more to avoid clutter.
            if (battleState.EnemyList.Count == 2)
                tempSprite.transform.position += new Vector3(0, (float) 1 - (2 * i), 0);
            else if (battleState.EnemyList.Count == 3)
            {
                tempSprite.transform.position += new Vector3(0, (float) 1.75 * (1 - i), 0);
            }

            tempSprite.GetComponent<SpriteRenderer>().sprite = battleState.EnemyList[i].BattleSprite;
            tempSprite.transform.GetChild(0).GetChild(0).GetComponent<ShowDamageOnEnemy>().EnemyStats = battleState.EnemyList[i];

            tempSelect = Instantiate(enemySelectFieldTemplate, enemySelectFieldTemplate.transform.position + new Vector3(0, -0.6f * i, 0), enemySelectFieldTemplate.transform.rotation);
            tempSelect.transform.GetChild(0).GetChild(0).GetComponent<EnemyHealthController>().EnemyStats = battleState.EnemyList[i];
            tempSelect.transform.GetChild(0).GetChild(1).GetComponent<EnemyNameController>().EnemyStats = battleState.EnemyList[i].BaseStats;
            tempSelect.transform.GetChild(0).GetChild(2).GetComponent<EnemyLvlController>().Level = battleState.EnemyList[i].Level;

            enemies.Add(new BattleEnemy(battleState.EnemyList[i], battleState.EnemyList[i].MaxHP, tempSprite, tempSelect));
            enemySelectFields.Add(tempSelect);
            enemySprites.Add(tempSprite);
        }
        enemySpriteTemplate.SetActive(false);

        for (int i = 0; i < bossDialogues.Count; i++)
        {
            if (enemies[0].EnemyClass.Name == bossDialogues[i].bossName)
            {
                isBoss = true;
                dialogueManager.GetComponent<BossDialogue>().InkJSON = bossDialogues[i].dialogueScript;
                break;
            }
        }

        for (int i = 0; i < enemies.Count; i++)
            enemies[i].Selector.transform.GetChild(0).GetChild(0).GetComponent<EnemyHealthController>().setup();

        if (battleState.Ally != null && !isBoss && battleState.Ally.CurHP > 0)
        {
            yield return StartCoroutine(addAlly());
            hasAlly = true;
        }

        System.Random rand = new System.Random();
        if (enemies.Count == 1)
            yield return StartCoroutine(typeMessage($"{enemies[0].EnemyClass.Name} {battleIntros[rand.Next(battleIntros.Count)]}", 1f, curState));
        else
            yield return StartCoroutine(typeMessage($"A hoarde approaches!", 1f, curState));
        yield return StartCoroutine(selectAction());
    }

    private IEnumerator addAlly()
    {
        // Move player sprite up to make space for ally
        playerSprite.transform.position += new Vector3(0, 1f, 0);

        // Duplicate player sprite and replace sprite image with ally's
        allySprite = Instantiate(playerSprite, playerSprite.transform.position + new Vector3(0, -2.5f, 0), playerSprite.transform.rotation);
        allySprite.GetComponent<SpriteRenderer>().sprite = battleState.Ally.BattleSprite;

        // Move MC health/mana stats to the left
        partyStats[0].SetActive(false);
        partyStats[0].transform.position += new Vector3(-4.5f, 0, 0);

        // Replace empty stats in object with party member stats
        AllyNameController allyNameController = partyStats[1].transform.GetChild(1).GetChild(0).GetComponent<AllyNameController>();
        allyNameController.AllyStats = battleState.Ally;
        allyNameController.setup();
        AllyHealthController allyHealthController = partyStats[1].transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<AllyHealthController>();
        allyHealthController.AllyStats = battleState.Ally;
        allyHealthController.DamageCounter = allySprite.transform.GetChild(0).GetChild(0).gameObject;
        allyHealthController.setup();
        AllyManaController allyManaController = partyStats[1].transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<AllyManaController>();
        allyManaController.AllyStats = battleState.Ally;
        allyManaController.setup();
        allySelectors.Add(partyStats[1].transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>());
        for (int i = 0; i < partyStats.Count; i++)
            partyStats[i].SetActive(true);
        yield break;
    }
    private IEnumerator selectAction()
    {
        isGuarding[curTurn] = 0;
        turnIndicators[curTurn].SetActive(true);
        yield return StartCoroutine(typeMessage("What would you like to do?", 0.1f, curState));

        curState = State.ActionSelect;
        actions[curAction].color = Color.blue;
    }
    private IEnumerator useSpecial(MoveBase selectedMove)
    {
        curState = State.InputUnavailable;
        string optionMessage = $"You used {selectedMove.name}...";
        yield return StartCoroutine(typeMessage(optionMessage, 0f, curState));
        if (selectedMove.Aoe)
            yield return StartCoroutine(AOEDamageCalls(selectedMove));
        else
            yield return StartCoroutine(specialDamageEnemy(selectedMove, curTarget));
        
        if (curState == State.EndBattle)
        {
            yield return StartCoroutine(wonBattle());
        }
        else
        {
            yield return StartCoroutine(continueBattle(false));
        }   
    }

    private IEnumerator continueBattle(bool allyRevived)
    {
        turnIndicators[curTurn].SetActive(false);
        if (curTurn == 0 && hasAlly && battleState.Ally.CurHP != 0 && !allyJustAdded && !allyRevived)
        {
            curTurn = 1;
            yield return StartCoroutine(allyTurn());
        }
        else if (!allyJustAdded)
        {
            curTurn = 0;

            for (int i = 0; i < enemies.Count; i++)
                yield return StartCoroutine(enemyTurn(i));
        }
        else
        {
            allyJustAdded = false;
            // for (int i = 0; i < enemies.Count; i++)
            //     yield return StartCoroutine(enemyTurn(i));
            yield return StartCoroutine(selectAction());
        }
    }

    private void MCLevelUp()
    {
        playerStats.Xp -= playerStats.XpNeeded;
        playerStats.Level += 1;
        playerStats.Points += 2;
        playerStats.MaxHP += 5;
        playerStats.MaxMP += 2;
    }

    private IEnumerator AllyLevelUp()
    {
        battleState.Ally.Xp -= battleState.Ally.XpNeeded;
        battleState.Ally.Level += 1;
        battleState.Ally.MaxHP += 5;
        battleState.Ally.MaxMP += 2;
        battleState.Ally.CurHP += 5;
        battleState.Ally.CurMP += 2;
        battleState.Ally.Attack = (int)(10f + (battleState.Ally.AttackGrowth * (float)battleState.Ally.Level));
        battleState.Ally.Defense = (int)(10f + (battleState.Ally.DefenseGrowth * (float)battleState.Ally.Level));
        battleState.Ally.Speed = (int)(10f + (battleState.Ally.SpeedGrowth * (float)battleState.Ally.Level));
        for(int i = 0; i < battleState.Ally.LeveledMoves.Count; ++i)
        {
            if(battleState.Ally.Level == battleState.Ally.LeveledMoves[i].Level)
            {
                if(battleState.Ally.MoveSet.Count < 4)
                    battleState.Ally.MoveSet.Add(new LearnableMove(battleState.Ally.LeveledMoves[i].Move, null));
                else
                    battleState.Ally.LearnableMoves.Add(new LearnableMove(battleState.Ally.LeveledMoves[i].Move, null));

                yield return StartCoroutine(typeMessage($"{battleState.Ally.Name} has learned {battleState.Ally.LeveledMoves[i].Move.name}!", 0.5f, curState));
            }
        }
    }

    private IEnumerator lostBattle()
    {
        DontDestroyOnLoad(this);
        Fader fader = FindObjectOfType<Fader>();
        fader.DontDestoryCanvas();
        yield return StartCoroutine(typeMessage($"You lost the battle...", 0.25f, curState));
        StartCoroutine(fader.FadeIn(1f));
        yield return new WaitForSeconds(1f);
        Destroy(allySprite);
        for (int i = 0; i < enemySprites.Count; i++)
            Destroy(enemySprites[i]);
        for (int i = 0; i < enemySelectFields.Count; i++)
            Destroy(enemySelectFields[i]);
        endScreen.SetActive(true);
        gameManager.inBattle = false;
        battleState.LostBattle();
        SceneManager.UnloadSceneAsync("FightTemplate");
        yield return StartCoroutine(fader.FadeOut(1f));
        fader.DestoryCanvas();
        Destroy(this.gameObject);
    }
    private IEnumerator ShowEndFightDialogue()
    {
        BossDialogue bossDialogue = dialogueManager.GetComponent<BossDialogue>();
        dialogueManager.SetActive(true);
        inputManager.SetActive(true);
        turnIndicators[curTurn].SetActive(false);
        if (battleState.Ally != null)
        {
            for (int i = 0; i < disableForDialogue.Count; i++)
                disableForDialogue[i].SetActive(false);
        }
        else
        {
            for (int i = 0; i < disableForDialogue.Count - 1; i++)
                disableForDialogue[i].SetActive(false);
        }

        for (int i = 0; i < battleEndDialogues.Count; i++)
        {
            if (battleState.EnemyList[0].Name == battleEndDialogues[i].bossName)
            {
                bossDialogue.InkJSON = battleEndDialogues[i].dialogueScript;
                break;
            }
        }

        bossDialogue.GetComponent<BossDialogue>().beginDialogue();
        yield return StartCoroutine(WaitForDialogue());

        // Turn objects back on
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        if (battleState.Ally != null)
        {
            for (int i = 0; i < disableForDialogue.Count; i++)
                disableForDialogue[i].SetActive(true);
        }
        else
        {
            for (int i = 0; i < disableForDialogue.Count - 1; i++)
                disableForDialogue[i].SetActive(true);
        }
    }
    private IEnumerator WaitForDialogue()
    {
        BossDialogue bossDialogue = dialogueManager.GetComponent<BossDialogue>();
        while (!bossDialogue.isFinished())
        {
            yield return null;
        }
    }
    private IEnumerator wonBattle()
    {
        curState = State.InputUnavailable;

        turnIndicators[curTurn].SetActive(false);
        int xp = 0;
        int money = 0;
        bool leveledUp = false;
        for (int i = 0; i < battleState.EnemyList.Count; i++)
        {
            xp += battleState.EnemyList[i].XP;
            money += battleState.EnemyList[i].Money;
        }
        playerStats.Xp += xp;
        if (battleState.Ally != null)
            battleState.Ally.Xp += xp;
        playerStats.Money += money;
       
        yield return StartCoroutine(typeMessage($"You won {xp} XP and {money} cash!", .5f, curState));
        while (playerStats.Xp >= playerStats.XpNeeded)
        {
            MCLevelUp();
            leveledUp = true;
        }

        if (leveledUp)
        {
            yield return new WaitForSeconds(.5f);
            yield return StartCoroutine(typeMessage($"You are now level {playerStats.Level}!", .5f, curState));
            leveledUp = false;
        }

        if (battleState.Ally != null)
        {
            while (battleState.Ally.Xp >= battleState.Ally.XpNeeded)
            {
                yield return StartCoroutine(AllyLevelUp());
                leveledUp = true;
            }

            if (leveledUp)
            {
                yield return new WaitForSeconds(.5f);
                yield return StartCoroutine(typeMessage($"{battleState.Ally.Name} is now level {battleState.Ally.Level}!", .5f, curState));
            }
        }

        yield return new WaitForSeconds(.5f);
        battleState.EndBattle();
        Destroy(allySprite);
        for (int i = 0; i < enemySprites.Count; i++)
            Destroy(enemySprites[i]);
        for (int i = 0; i < enemySelectFields.Count; i++)
            Destroy(enemySelectFields[i]);
        gameManager.inBattle = false;
        SceneManager.UnloadSceneAsync("FightTemplate");
    }

    private IEnumerator enemyAttack(int enemyIndex)
    {   
        System.Random rand = new System.Random();
        int randAttack = rand.Next(enemies[enemyIndex].EnemyClass.Moves.Count);
        int addtDmg = rand.Next(enemies[enemyIndex].EnemyClass.Attack + 1);
        int target;
        bool isAOE = enemies[enemyIndex].EnemyClass.Moves[randAttack].Aoe;

        if (hasAlly && battleState.Ally.CurHP != 0)
            target = rand.Next(0, 2);
        else
        {
            target = 0;
            isAOE = false;
        }
        yield return StartCoroutine(typeMessage($"{enemies[enemyIndex].EnemyClass.Name} used {enemies[enemyIndex].EnemyClass.Moves[randAttack].name}...", 1f, curState));
        float temp1;
        int damageAmt;
        if (isGuarding[target] == 1)
        {
            temp1 = rand.Next(enemies[enemyIndex].EnemyClass.Moves[randAttack].Power / 2 + addtDmg, enemies[enemyIndex].EnemyClass.Moves[randAttack].Power + addtDmg);
            if(target == 0)
                damageAmt = (int)(temp1 * playerStats.DefCalc);
            else
                damageAmt = (int)(temp1 * battleState.Ally.DefCalc);
        }
        else
        {
            /*
            if (enemies[enemyIndex].EnemyClass.Moves[randAttack].Power + addtDmg >= playerStats.Defense)
                damageAmt = (enemies[enemyIndex].EnemyClass.Moves[randAttack].Power * 2) + addtDmg - playerStats.Defense;
            else
                damageAmt = (enemies[enemyIndex].EnemyClass.Moves[randAttack].Power * enemies[enemyIndex].EnemyClass.Moves[randAttack].Power) + addtDmg / playerStats.Defense;
            */

            temp1 = (enemies[enemyIndex].EnemyClass.Moves[randAttack].Power * enemies[enemyIndex].EnemyClass.Attack + addtDmg) / 8;

            if (target == 0)
                damageAmt = (int)(temp1 * playerStats.DefCalc);
            else
                damageAmt = (int)(temp1 * battleState.Ally.DefCalc);
        }

        if (target == 0 || isAOE)
        {
            Audio.PlayOneShot(AttackS);
            if (damageAmt > playerStats.CurHP)
                damageAmt = playerStats.CurHP;

            playerStats.CurHP -= damageAmt;
            yield return StartCoroutine(healthBarController.takeDamage(damageAmt));

            if (playerStats.CurHP <= 0)
            {
                yield return StartCoroutine(typeMessage($"{playerStats.Name} fainted!", 1f, curState));
                playerSprite.SetActive(false);

                curState = State.EndBattle;
                yield return StartCoroutine(lostBattle());
            }  
        }
        if (target == 1 || isAOE)
        {
            Audio.PlayOneShot(AttackS);
            if (damageAmt > battleState.Ally.CurHP)
                damageAmt = battleState.Ally.CurHP;

            battleState.Ally.CurHP -= damageAmt;
            yield return StartCoroutine(partyStats[1].transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<AllyHealthController>().takeDamage(damageAmt));

            if (battleState.Ally.CurHP <= 0)
            {
                yield return StartCoroutine(typeMessage($"{battleState.Ally.Name} fainted!", 1f, curState));
                allySprite.SetActive(false);
            }
        }
        
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator attackDamageEnemy(int enemyIndex)
    {
        // May have to pass in a reference to the player who's attacking and enemy being attacked for formula 
        System.Random rand = new System.Random();
        Audio.PlayOneShot(AttackS);
        int damageDealt;
        if (curTurn == 0)
        {
            damageDealt = rand.Next((int) (playerStats.TotalAttack * 0.6), (int) (playerStats.TotalAttack * 1));
            if (damageDealt >= enemies[enemyIndex].EnemyClass.Defense)
                damageDealt = damageDealt * 2 - enemies[enemyIndex].EnemyClass.Defense;
            else
                damageDealt = (int) (damageDealt * damageDealt / enemies[enemyIndex].EnemyClass.Defense);
        }
        else
        {
            damageDealt = rand.Next((int) (battleState.Ally.TotalAttack * 0.6), (int) (battleState.Ally.TotalAttack * 1));
            if (damageDealt >= enemies[enemyIndex].EnemyClass.Defense)
                damageDealt = damageDealt * 2 - enemies[enemyIndex].EnemyClass.Defense;
            else
                damageDealt = (int) (damageDealt * damageDealt / enemies[enemyIndex].EnemyClass.Defense);
        }
        
        // Base damage on performance in quicktime event
        int numKeys = quicktimeEvent.GetComponent<QuickTimeEvent>().getNumKeys();
        float quicktimeSuccess = quicktimeEvent.GetComponent<QuickTimeEvent>().getNumPerfect();
        damageDealt = (int) (((float) damageDealt / numKeys) * quicktimeSuccess);
        if (damageDealt > enemies[enemyIndex].CurHP)
            damageDealt = enemies[enemyIndex].CurHP;

        enemies[enemyIndex].CurHP -= damageDealt;
        yield return StartCoroutine(enemies[enemyIndex].Model.transform.GetChild(0).GetChild(0).GetComponent<ShowDamageOnEnemy>().doDamage(damageDealt, enemies[enemyIndex].CurHP));
        if (isBoss && !dialoguePlayed && enemies[enemyIndex].CurHP <= (enemies[enemyIndex].EnemyClass.MaxHP / 2))
        {
            // Do dialogue
            yield return new WaitForSeconds(.25f);
            curState = State.InputUnavailable;
            yield return StartCoroutine(showBossDialogue());
            yield return new WaitForSeconds(.25f);
            if (battleState.Ally != null)
            {
                battleState.Ally.CurHP = battleState.Ally.MaxHP;
                battleState.Ally.CurMP = battleState.Ally.MaxMP;
                yield return StartCoroutine(addAlly());
                hasAlly = true;
                allyJustAdded = true;
                yield return StartCoroutine(typeMessage($"{battleState.Ally.Name} has joined the fight!", 1f, curState));
            }
        }
        else if (enemies[enemyIndex].CurHP <= 0)
        {
            yield return new WaitForSeconds(.5f);
            if (isBoss)
            {
                yield return StartCoroutine(ShowEndFightDialogue());
                Explosion(enemyIndex);
            }
            else
                Smoke(enemyIndex);
            yield return new WaitForSeconds(enemies[enemyIndex].Model.transform.GetChild(1).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            yield return StartCoroutine(typeMessage($"{enemies[enemyIndex].EnemyClass.Name} fainted!", 0f, curState));
            removeEnemy(enemyIndex);
            yield return new WaitForSeconds(1f);
            
            if (j==0)
            {
                if(enemyIndex==0)
                {
                    p=0;
                }
                if(enemyIndex==1)
                {
                    p=1;
                }
                if(enemyIndex==2)
                {
                    p=2;
                }
            }
            else if (j==1)
            {
                if(enemyIndex==1 && p==0)
                {
                    p=2;
                    k=0;
                }
                if(enemyIndex==0 && p==0)
                {
                    p=1;
                    k=0;
                }
                if(enemyIndex==0 && p==1)
                {
                    p=0;
                    k=1;
                }
                if(enemyIndex==1 && p==1)
                {
                    p=2;
                    k=1;
                }
                if(enemyIndex==1 && p==2)
                {
                    p=1;
                    k=2;
                }
                if(enemyIndex==0 && p==2)
                {
                    p=0;
                    k=2;
                }
            }
            else if (j==2)
            {
                if(enemyIndex==0 && p==2 && k==0)
                p=1;
                if(enemyIndex==0 && p==1 && k==0)
                p=2;
                if(enemyIndex==0 && p==0 && k==1)
                p=2;
                if(enemyIndex==0 && p==2 && k==1)
                p=0;
                if(enemyIndex==0 && p==1 && k==2)
                p=0;
                if(enemyIndex==0 && p==0 && k==2)
                p=1;
            }

            j++;
            if (enemies.Count == 0)
                curState = State.EndBattle;
        }
        else
            yield return new WaitForSeconds(1f);
    }
    private void removeEnemy(int enemyIndex)
    {
        Destroy(enemies[enemyIndex].Model);
        Destroy(enemies[enemyIndex].Selector);
        enemies.RemoveAt(enemyIndex);
        enemySprites.RemoveAt(enemyIndex);
        enemySelectFields.RemoveAt(enemyIndex);
    }
    private IEnumerator showBossDialogue()
    {
        BossDialogue bossDialogue = dialogueManager.GetComponent<BossDialogue>();
        dialogueManager.SetActive(true);
        inputManager.SetActive(true);
        turnIndicators[curTurn].SetActive(false);
        if (battleState.Ally != null)
        {
            for (int i = 0; i < disableForDialogue.Count; i++)
                disableForDialogue[i].SetActive(false);
        }
        else
        {
            for (int i = 0; i < disableForDialogue.Count - 1; i++)
                disableForDialogue[i].SetActive(false);
        }
        bossDialogue.beginDialogue();
        dialoguePlayed = true;

        yield return StartCoroutine(WaitForDialogue());

        // Turn objects back on
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        if (battleState.Ally != null)
        {
            for (int i = 0; i < disableForDialogue.Count; i++)
                disableForDialogue[i].SetActive(true);
        }
        else
        {
            for (int i = 0; i < disableForDialogue.Count - 1; i++)
                disableForDialogue[i].SetActive(true);
        }
        yield return new WaitForSeconds(.5f);
        dialogText.text = "";
    }
    private IEnumerator AOEDamageCalls(MoveBase selectedMove)
    {
        if (curTurn == 0)
        {
            playerStats.CurMP -= selectedMove.ManaCost;
            yield return StartCoroutine(manaBarController.updateMana());
        }
        else
        {
            battleState.Ally.CurMP -= selectedMove.ManaCost;
            yield return StartCoroutine(partyStats[1].transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<AllyManaController>().updateMana());
        }
        yield return new WaitForSeconds(.5f);

        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        dialogText.text = "";
        yield return StartCoroutine(quicktimeEvent.GetComponent<QuickTimeEvent>().comboQuicktime(selectedMove));
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(.3f);

        for (int i = 0; i < enemies.Count; i++)
            yield return StartCoroutine(specialDamageAOE(selectedMove, i));

        // Remove enemy from list here so it doesn't mess up for loop above.
        for (int i = enemies.Count - 1; i >= 0; i--)
            if (enemies[i].CurHP <= 0)
                removeEnemy(i);

        if (enemies.Count == 0)
            curState = State.EndBattle;
    }

    private IEnumerator specialDamageAOE(MoveBase selectedMove, int enemyIndex)
    {
        int attackStat = (curTurn == 0) ? playerStats.TotalAttack : battleState.Ally.TotalAttack;         
        int damageDealt;
        float temp1;
        float temp2;
        float temp3;
        float temp4;
        float weaknessMult;
        float random = UnityEngine.Random.Range(0.9f, 1.1f);

        if(selectedMove.Type == enemies[enemyIndex].EnemyClass.Weaknesss)
        {
            weaknessMult = 1.3f;
        }
        else if(selectedMove.Type == enemies[enemyIndex].EnemyClass.Resist)
        {
            weaknessMult = 0.7f;
        }
        else
        {
            weaknessMult = 1f;
        }

        // Bonus damage based on performance in quicktime event (full combo = 2x damage)
        //damageDealt = (int) (((float) selectedMove.Power / 4) * quicktimeEvent.GetComponent<QuickTimeEvent>().getNumPerfect()) + selectedMove.Power;
        temp1 = quicktimeEvent.GetComponent<QuickTimeEvent>().getNumPerfect() * selectedMove.Power * attackStat;
        temp2 = temp1 / 10;
        temp3 = temp2 * weaknessMult;
        temp4 = temp3 * enemies[enemyIndex].EnemyClass.DefCalc * random;

        // Special attacks do base damage in addition to calculated damage
        damageDealt = (int)temp4 + (int)(selectedMove.Power * weaknessMult);

        if (damageDealt > enemies[enemyIndex].CurHP)
            damageDealt = enemies[enemyIndex].CurHP;

        enemies[enemyIndex].CurHP -= damageDealt;
        Audio.PlayOneShot(SpecialS);
        yield return StartCoroutine(enemies[enemyIndex].Model.transform.GetChild(0).GetChild(0).GetComponent<ShowDamageOnEnemy>().doDamage(damageDealt, enemies[enemyIndex].CurHP));
        
        if (isBoss && !dialoguePlayed && enemies[enemyIndex].CurHP <= (enemies[enemyIndex].EnemyClass.MaxHP / 2))
        {
            // Do dialogue
            yield return new WaitForSeconds(.25f);
            curState = State.InputUnavailable;
            yield return StartCoroutine(showBossDialogue());
            yield return new WaitForSeconds(.25f);
            if (battleState.Ally != null)
            {
                battleState.Ally.CurHP = battleState.Ally.MaxHP;
                battleState.Ally.CurMP = battleState.Ally.MaxMP;
                yield return StartCoroutine(addAlly());
                hasAlly = true;
                allyJustAdded = true;
                yield return StartCoroutine(typeMessage($"{battleState.Ally.Name} has joined the fight!", 1f, curState));
            }
        }
        else if (enemies[enemyIndex].CurHP <= 0)
        {
            yield return new WaitForSeconds(.5f);
            if (isBoss)
            {
                yield return StartCoroutine(ShowEndFightDialogue());
                Explosion(enemyIndex);
            }
            else
                Smoke(enemyIndex);
            yield return new WaitForSeconds(enemies[enemyIndex].Model.transform.GetChild(1).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            yield return StartCoroutine(typeMessage($"{enemies[enemyIndex].EnemyClass.Name} fainted!", 1f, curState));
        }
        else
            yield return new WaitForSeconds(.5f);
    }
    private IEnumerator specialDamageEnemy(MoveBase selectedMove, int enemyIndex)
    {
        int attackStat; 
        if (curTurn == 0)
        {
            playerStats.CurMP -= selectedMove.ManaCost;
            attackStat = playerStats.TotalAttack;
            yield return StartCoroutine(manaBarController.updateMana());
        }
        else
        {
            battleState.Ally.CurMP -= selectedMove.ManaCost;
            attackStat = battleState.Ally.TotalAttack;
            yield return StartCoroutine(partyStats[1].transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<AllyManaController>().updateMana());
        }
        yield return new WaitForSeconds(.5f);

        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        dialogText.text = "";
        yield return StartCoroutine(quicktimeEvent.GetComponent<QuickTimeEvent>().comboQuicktime(selectedMove));
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(.3f);
        
        int damageDealt;
        float temp1;
        float temp2;
        float temp3;
        float temp4;
        float weaknessMult;
        float random = UnityEngine.Random.Range(0.9f, 1.1f);

        if(selectedMove.Type == enemies[enemyIndex].EnemyClass.Weaknesss)
        {
            weaknessMult = 1.3f;
        }
        else if(selectedMove.Type == enemies[enemyIndex].EnemyClass.Resist)
        {
            weaknessMult = 0.7f;
        }
        else
        {
            weaknessMult = 1f;
        }

        // Bonus damage based on performance in quicktime event (full combo = 2x damage)
        //damageDealt = (int) (((float) selectedMove.Power / 4) * quicktimeEvent.GetComponent<QuickTimeEvent>().getNumPerfect()) + selectedMove.Power;
        temp1 = quicktimeEvent.GetComponent<QuickTimeEvent>().getNumPerfect() * (selectedMove.Power * 1.15f)  * (attackStat * 0.75f);
        temp2 = temp1 / 10;
        temp3 = temp2 * weaknessMult;
        temp4 = temp3 * enemies[enemyIndex].EnemyClass.DefCalc * random;

        // Special attacks do base damage in addition to calculated damage
        damageDealt = (int)temp4 + (int)(selectedMove.Power * weaknessMult);

        if (damageDealt > enemies[enemyIndex].CurHP)
        {
            if (!dialoguePlayed && isBoss)
            {
                System.Random rand = new System.Random();
                damageDealt = rand.Next(enemies[enemyIndex].CurHP / 2, enemies[enemyIndex].CurHP);
            }
            else
                damageDealt = enemies[enemyIndex].CurHP;
        }

        enemies[enemyIndex].CurHP -= damageDealt;
        Audio.PlayOneShot(SpecialS);
        yield return StartCoroutine(enemies[enemyIndex].Model.transform.GetChild(0).GetChild(0).GetComponent<ShowDamageOnEnemy>().doDamage(damageDealt, enemies[enemyIndex].CurHP));

        if (weaknessMult < 1f)
            yield return StartCoroutine(typeMessage("It's not very effective...", 0.5f, curState));
        else if (weaknessMult > 1f)
            yield return StartCoroutine(typeMessage("It's super effective!", 0.5f, curState));

        if (isBoss && !dialoguePlayed && enemies[enemyIndex].CurHP <= (enemies[enemyIndex].EnemyClass.MaxHP / 2))
        {
            // Do dialogue
            yield return new WaitForSeconds(.25f);
            curState = State.InputUnavailable;
            yield return StartCoroutine(showBossDialogue());
            yield return new WaitForSeconds(.25f);
            if (battleState.Ally != null)
            {
                battleState.Ally.CurHP = battleState.Ally.MaxHP;
                battleState.Ally.CurMP = battleState.Ally.MaxMP;
                yield return StartCoroutine(addAlly());
                hasAlly = true;
                allyJustAdded = true;
                yield return StartCoroutine(typeMessage($"{battleState.Ally.Name} has joined the fight!", 1f, curState));
            }
        }
        else if (enemies[enemyIndex].CurHP <= 0)
        {
            yield return new WaitForSeconds(.5f);
            if (isBoss)
            {
                yield return StartCoroutine(ShowEndFightDialogue());
                Explosion(enemyIndex);
            }
            else
                Smoke(enemyIndex);
            yield return new WaitForSeconds(enemies[enemyIndex].Model.transform.GetChild(1).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            yield return StartCoroutine(typeMessage($"{enemies[enemyIndex].EnemyClass.Name} fainted!", 0f, curState));
            removeEnemy(enemyIndex);
            yield return new WaitForSeconds(.5f);
            if (j==0)
            {
                if(enemyIndex==0)
                {
                    p=0;
                }
                if(enemyIndex==1)
                {
                    p=1;
                }
                if(enemyIndex==2)
                {
                    p=2;
                }
            }
           else if (j==1)
            {
                if(enemyIndex==1 && p==0)
                {
                    p=2;
                    k=0;
                }
                if(enemyIndex==0 && p==0)
                {
                    p=1;
                    k=0;
                }
                if(enemyIndex==0 && p==1)
                { 
                    p=0;
                    k=1;
                }
                if(enemyIndex==1 && p==1)
                {
                    p=2;
                    k=1;
                }
                if(enemyIndex==1 && p==2)
                {
                    p=1;
                    k=2;
                }
                if(enemyIndex==0 && p==2)
                { 
                    p=0;
                    k=2;
                }

            }
            else if (j==2)
            {
                if(enemyIndex==0 && p==2 && k==0)
                    p=1;
                if(enemyIndex==0 && p==1 && k==0)
                    p=2;
                if(enemyIndex==0 && p==0 && k==1)
                    p=2;
                if(enemyIndex==0 && p==2 && k==1)
                    p=0;
                if(enemyIndex==0 && p==1 && k==2)
                    p=0;
                if(enemyIndex==0 && p==0 && k==2)
                    p=1;
            }
            j++;

            if (enemies.Count == 0)
                curState = State.EndBattle;
        }
        else
            yield return new WaitForSeconds(1f);
    }
    private IEnumerator useItem(ItemBase selectedItem)
    {
        curState = State.InputUnavailable;
        if (curTarget != 0 && battleState.Ally.CurHP <= 0)
        {
            yield return StartCoroutine(typeMessage($"{battleState.Ally.Name} is downed and can only be revived.", .5f, curState));
            yield return StartCoroutine(typeMessage("Select the item's recipient.", 0f, curState));
            curState = State.AllySelect;
            allySelectors[curTarget].color = Color.blue;
        }
        else
        {
            string user = (curTarget == 0) ? "You" : "Ally";
            yield return StartCoroutine(typeMessage($"{user} used {selectedItem.name}...", 1f, curState));

            int amtRecovered = 0;
            
            if (selectedItem.Stat == ItemBase.AffectedStat.HP)
            {
                if (curTarget == 0)
                {
                    
                    if (playerStats.MaxHP - playerStats.CurHP < selectedItem.Effect)
                    {
                        amtRecovered = playerStats.MaxHP - playerStats.CurHP;
                        playerStats.CurHP = playerStats.MaxHP;
                    }
                    else
                    {
                        amtRecovered = selectedItem.Effect;
                        playerStats.CurHP += selectedItem.Effect;
                    }
                    yield return StartCoroutine(healthBarController.updateHealth());
                }
                else
                {
                    if (battleState.Ally.MaxHP - battleState.Ally.CurHP < selectedItem.Effect)
                    {
                        amtRecovered = battleState.Ally.MaxHP - battleState.Ally.CurHP;
                        battleState.Ally.CurHP = battleState.Ally.MaxHP;
                    }
                    else
                    {
                        amtRecovered = selectedItem.Effect;
                        battleState.Ally.CurHP += selectedItem.Effect;
                    }
                    yield return StartCoroutine(partyStats[1].transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<AllyHealthController>().updateHealth());
                }
            }
            else if (selectedItem.Stat == ItemBase.AffectedStat.MP)
            {
                if (curTarget == 0)
                {
                    if (playerStats.MaxMP - playerStats.CurMP < selectedItem.Effect)
                    {
                        amtRecovered = playerStats.MaxMP - playerStats.CurMP;
                        playerStats.CurMP = playerStats.MaxMP;
                    }
                    else
                    {
                        amtRecovered = selectedItem.Effect;
                        playerStats.CurMP += selectedItem.Effect;
                    }
                    yield return StartCoroutine(manaBarController.updateMana());
                }
                else
                {
                    if (battleState.Ally.MaxMP - battleState.Ally.CurMP < selectedItem.Effect)
                    {
                        amtRecovered = battleState.Ally.MaxMP - battleState.Ally.CurMP;
                        battleState.Ally.CurMP = battleState.Ally.MaxMP;
                    }
                    else
                    {
                        amtRecovered = selectedItem.Effect;
                        battleState.Ally.CurMP += selectedItem.Effect;
                    }
                    yield return StartCoroutine(partyStats[1].transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<AllyManaController>().updateMana());
                }
            }

            yield return StartCoroutine(typeMessage($"{user} recovered {amtRecovered} {(selectedItem.Stat == ItemBase.AffectedStat.HP ? "HP" : "MP")}!", 1f, curState));

            turnIndicators[curTurn].SetActive(false);
            yield return StartCoroutine(continueBattle(false));
        }
    }

    private IEnumerator typeMessage(string message, float delay, State source)
    {
        curState = State.InputUnavailable;
        dialogText.text = "";
        
        foreach (var letter in message)
        {
            yield return new WaitForSeconds(1f/45);
            dialogText.text += letter;
        }

        yield return new WaitForSeconds(delay);
        curState = source;
    }

    private IEnumerator reviveMember()
    {
        battleState.Ally.CurHP = (int) (battleState.Ally.MaxHP / 2);
        allySprite.SetActive(true);
        yield return StartCoroutine(partyStats[1].transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<AllyHealthController>().updateHealth());
        yield return StartCoroutine(typeMessage($"{battleState.Ally.Name} has been revived!", 1f, curState));
        dialogText.text = "";
        yield return StartCoroutine(continueBattle(true));
    }

    private void BattleAudio()
    {  
        // World = ((Ink.Runtime.IntValue) DialogueManager.GetInstance().GetVariableState("MC")).value;
        //Audio = this.gameObject.AddComponent<AudioSource>();
        if(Audio.isPlaying)
            return;
        else
        {
            //Audio = this.gameObject.AddComponent<AudioSource>();
            if(!isBoss && gameManager.World==0)
            {
                Audio.clip=soundClip1;
                Audio.loop=true;
                Audio.PlayOneShot(soundClip1);
                Audio.volume = .75f;
                //   Audio.PlayMusic(1f,0, "World1Battles");
            }
            if(isBoss && gameManager.World==0)
            {
                if((battleState.EnemyList[0].Name =="Tutorial Guy") || (battleState.EnemyList[0].Name=="Tutorial Guy 2.0") )
                {
                    if(TutorialPhase2==0)
                    {
                        Audio.clip=Tutorial1;
                        Audio.loop=true;
                        Audio.PlayOneShot(Tutorial1);
                        Audio.volume = .75f;
                    }
                    else if(TutorialPhase2==1)
                    {
                        Audio.clip=Tutorial2;
                        Audio.loop=true;
                        Audio.PlayOneShot(Tutorial2);
                        Audio.volume = .75f;
                    }
                     else
                        return;
                }
                else if(battleState.EnemyList[0].Name=="Barney")
                {
                    Audio.clip=Barney;
                    Audio.loop=true;
                    Audio.PlayOneShot(Barney);
                    Audio.volume = .75f;
                }
                else
                {
                    Audio.clip=soundClip2;
                    Audio.loop=true;
                    Audio.PlayOneShot(soundClip2);
                    Audio.volume = .75f;
                }

                // Audio.PlayMusic(1f,1, "World1Battles");
            }
                if(!isBoss && gameManager.World!=0)
                {
                    Audio.clip=soundClip3;
                    Audio.loop=true;
                    Audio.PlayOneShot(soundClip3);
                    Audio.volume = .75f;
                // Audio.PlayMusic(1f,4, "World2");
                }
            if(isBoss && gameManager.World!=0)
            { 
                if(battleState.EnemyList[0].Name=="L")
                {
                Audio.clip=L;
                Audio.loop=true;
                Audio.PlayOneShot(L);
                Audio.volume = .75f;
                }
                else if(battleState.EnemyList[0].Name=="MCBoss")
                {
                    Audio.clip=MCBoss;
                    Audio.loop=true;
                    Audio.PlayOneShot(MCBoss);
                    Audio.volume = .75f;
                }
                else 
                {
                    Audio.clip=soundClip4;
                    Audio.loop=true;
                    Audio.PlayOneShot(soundClip4);
                    Audio.volume = .75f;
                }
                    //Audio.PlayMusic(1f,5, "World2");
            }
        }
    }
    private void Explosion(int enemyIndex)
    {
        enemies[enemyIndex].Model.transform.GetChild(2).gameObject.SetActive(true);
        enemies[enemyIndex].Model.GetComponent<SpriteRenderer>().enabled = false;
        enemies[enemyIndex].Model.transform.GetChild(2).GetComponent<Animator>().Play("Explos2");
        //anim.Play("Explos2");
        Audio.clip=soundClip5;
        Audio.loop=false;
        Audio.PlayOneShot(soundClip5);
    }
    private void Smoke(int enemyIndex)
    {
        enemies[enemyIndex].Model.transform.GetChild(1).gameObject.SetActive(true);
        enemies[enemyIndex].Model.GetComponent<SpriteRenderer>().enabled = false;
        enemies[enemyIndex].Model.transform.GetChild(1).GetComponent<Animator>().Play("Smoke");
        Audio.clip=soundClip6;
        Audio.loop=false;
        Audio.PlayOneShot(soundClip6);
    }
}