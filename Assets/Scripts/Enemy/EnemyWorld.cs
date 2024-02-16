using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using DG.Tweening;

public class EnemyWorld : MonoBehaviour
{
    public List<EnemyBase> enemies;
    public List<int> level;
    public MemoryBase memory;
    public bool wander = true;
    public int range = 3;
    public int wanderDistance = 3;
    public float chaseSpeed = 5f;
    public float wanderSpeed = 2f;
    public BattleState currentBattle;
    public string battleArena;

    private GameObject player;
    private AIDestinationSetter setter;
    private AILerp pathfinder;
    private EnemyAnim animator;
    private Seeker seeker;
    private GameObject initialPos;
    private GameObject wanderPos;
    private MenuController menuController;
    private GameManager gameManager;
    private Fader fader;
    private GameObject faderParent;
    private InputManager inputManager;
    private DialogueManager dialogueManager;
    float distance;
    bool foundWander = false;
    bool restart = false;

    public bool debug = false;
    

    private void Start()
    {
        if (memory)
        {
            if (memory.IsViewed)
            {
                Destroy(this.gameObject);
            }
        }

        player = GameObject.FindGameObjectWithTag("Player");
        setter = gameObject.GetComponent<AIDestinationSetter>();
        pathfinder = gameObject.GetComponent<AILerp>();
        animator = gameObject.GetComponent<EnemyAnim>();
        seeker = gameObject.GetComponent<Seeker>();
        menuController = MenuController.Instance;
        gameManager = GameManager.Instance;
        dialogueManager = DialogueManager.GetInstance();
        CreateInitialPos();
    }

    private void Update()
    {
        if (memory)
        {
            if (memory.IsViewed)
            {
                Destroy(initialPos);
                Destroy(wanderPos);
                Destroy(this.gameObject);
            }
        }
        if (!gameManager.inBattle) 
        { 
            if (restart)
            {
                pathfinder.isStopped = false;
                foundWander = false;
                Wandering();

                restart = false;
            }

            if (menuController.MenuOpened == false || dialogueManager.dialogueIsPlaying || gameManager.playerStopped)
            {
                if(dialogueManager.dialogueIsPlaying || gameManager.playerStopped)//added so enemies wont fight you while you press dialogue stuff
                pathfinder.isStopped = true;
                else{
                pathfinder.isStopped = false;
            
                ChasePlayer();
                animator.isMoving = pathfinder.velocity.magnitude > 0;
                animator.moveX = Mathf.Clamp(pathfinder.velocity.x, -1, 1);
                }
            }
            else
            {
                pathfinder.isStopped = true;
                animator.isMoving = false;
            }
        }
    }
    public void EnterCombat()
    {
        StopAllCoroutines();
        pathfinder.isStopped = false;

        gameManager.StartCombat();
        
        for(int i = 0; i < enemies.Count; ++i)      //adds the enemies detailed on the obejct to the enemylist and load the battle
        {
            currentBattle.EnemyList.Add(new Enemy(enemies[i], level[i]));
            //Debug.Log(currentBattle.EnemyList[i].Attack + " " + currentBattle.EnemyList[i].DefCalc + " " + currentBattle.EnemyList[i].Speed);
        }
        
        DontDestroyOnLoad(this);
        List<GameObject> allObjects = SceneManager.GetActiveScene().GetRootGameObjects().ToList<GameObject>();

        SceneManager.LoadSceneAsync(battleArena, LoadSceneMode.Additive);

        int check = allObjects.Count;
        for(int i = 0; i < check; ++i)
        {
            if (allObjects[i].tag == "Enemy")
            {
                allObjects.RemoveAt(i);
                --i;
                --check;
            }
        }
        
        for (int i = 0; i < allObjects.Count; ++i)
        {
            allObjects[i].SetActive(false);
        }

        Destroy(initialPos);            //destroy the initial pos object
        Destroy(wanderPos);
        Destroy(gameObject);            //destroys this enemy from the world
    }
    private void ChasePlayer()      //determines the distance from enemy to player, if they are in range target the player, else go back to initial position
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance < range)
        {
            pathfinder.speed = chaseSpeed;
            setter.target = player.transform;
        }
        else
        {
            if (wander)
            {
                setter.target = wanderPos.transform;
                pathfinder.speed = wanderSpeed;
                if (pathfinder.velocity.magnitude == 0)
                {
                    if (pathfinder.reachedDestination)
                    {
                        StartCoroutine(Wandering());
                    }
                }
            }
            else
            {
                setter.target = initialPos.transform;
            }
        }
    }

    private IEnumerator Wandering()
    {
        Vector3 randPos;
        int randX;
        int randY;
        Path path;

        do
        {
            randPos = initialPos.transform.position;
            randX = Random.Range(-wanderDistance, wanderDistance);
            randY = Random.Range(-wanderDistance, wanderDistance);

            randPos.x += randX;
            randPos.y += randY;

            path = seeker.StartPath(transform.position, randPos);
            yield return StartCoroutine(path.WaitForPath());
            if (debug)
            {
                Debug.Log(path.GetTotalLength());
            }
            if (path.GetTotalLength() <= (float)(wanderDistance * 2) && IsWalkable(randPos))
            {
                wanderPos.transform.position = randPos;
                setter.target = wanderPos.transform;
                foundWander = true;
            }
            else
            {
                foundWander = false;
            }
        } while (!foundWander);
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.3f, GameLayers.instance.CollisionLayer | GameLayers.instance.InteractableLayer) != null)
        {
            return false;
        }
        return true;
    }
    public void StopMovement()
    {
        StopAllCoroutines();
        pathfinder.isStopped = false;
        restart = true;
    }

    private void CreateInitialPos()     //creates an empty object with the initial position of the enemy
    {
        initialPos = new GameObject(name + " initialPos");
        initialPos.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        wanderPos = new GameObject(name + " wanderPos");
        wanderPos.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
