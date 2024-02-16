using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControllerV3 : MonoBehaviour
{
    public GameObject sprite;
    private float moveSpeed;
    public float defaultSpeed;
    public float sprintSpeed;

    [SerializeField] bool isMoving;
    private Vector2 input;
    private Vector3 targetPos;

    CharacterAnimator animator;
    DialogueManager dialogueManager;
    MenuController menuController;
    GameManager gameManager;
    GameObject follower;
    Fader fader;
    private AudioSource Audio;
    public AudioClip[] soundClip;
    
    public PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        animator= sprite.GetComponent<CharacterAnimator>();
        transform.position = stats.Position.initialValue;
        dialogueManager = DialogueManager.GetInstance();
        menuController = MenuController.Instance;
        gameManager = GameManager.Instance;
        Audio =this.gameObject.AddComponent<AudioSource>();
        targetPos = transform.position;
        follower = GameObject.FindGameObjectWithTag("Follower");
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager.dialogueIsPlaying || menuController.MenuOpened ||  gameManager.playerStopped)  //can't move if dialogue is playing or menu is open
        {
            animator.Start(); //3.19.23 made this line so if you open the menu the character will stop and not be in walk animation
            //Debug.Log(dialogueManager.dialogueIsPlaying + " " + menuController.MenuOpened + " " + gameManager.playerStopped);
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed = sprintSpeed;
        else
            moveSpeed = defaultSpeed;

        if (!isMoving)
        {            
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            //Debug.Log("Isn't moving");
            if (input.x != 0)
                input.y = 0;

            if (Input.GetKeyDown(KeyCode.Tab))       //opens the menu when you press escape
            {
                menuController.OpenMenu();
            }

            if (input != Vector2.zero)
            {
                animator.moveX = input.x;
                animator.moveY = input.y;

                targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;


                if (IsWalkable(new Vector3(targetPos.x, targetPos.y - 0.5f)))
                {
                    gameManager.pushInput(input);
                    RandomStep();
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.isMoving = isMoving;
        gameManager.UpdateIsMoving(isMoving);
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.3f, GameLayers.instance.CollisionLayer | GameLayers.instance.InteractableLayer) != null)
        {
            return false;
        }
        return true;
    }


    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            if (isMoving)
            {
                transform.position = targetPos;
                StopAllCoroutines();
                isMoving = false;
            }

            StartCoroutine(EngageBattle(other));
        }    
    }

    private IEnumerator EngageBattle(Collider2D other)
    {
        gameManager.inBattle = true;

        //Player position on enemy collsion
        stats.Position.initialValue.x = Mathf.RoundToInt(this.transform.position.x) + 0.5f;
        stats.Position.initialValue.y = Mathf.RoundToInt(this.transform.position.y);
        gameManager.clearPlayerInput();
        stats.CurScene = SceneManager.GetActiveScene().name;

        fader = FindObjectOfType<Fader>();
        yield return fader.FadeIn(0.5f);

        //Enemy data
        EnemyWorld enemy = other.gameObject.GetComponent<EnemyWorld>();
        gameManager.playerStopped = false;
        if(follower != null)
            follower.transform.position = this.transform.position; 
        StartCoroutine(fader.FadeOut(0.5f));
        
        enemy.EnterCombat();
    }


    public float MoveSpeed
    {
        get { return moveSpeed; }
    }

    public bool IsMoving
    {
        get { return isMoving; }
    }

    void RandomStep()
    {
        Audio.clip = soundClip[Random.Range(0, soundClip.Length)];
        Audio.Play();
    }
}
