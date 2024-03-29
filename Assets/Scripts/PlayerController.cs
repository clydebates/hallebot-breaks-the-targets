using UnityEngine;
using UnityEngine.InputSystem;
using OpSpark;
using System.Collections;
using System.Threading.Tasks;


public class PlayerController : MonoBehaviour, ICharacter
{
  [Header("Required Game Objects")]
  [SerializeField] GameObject fireSpawnPoint;
  [SerializeField] GameObject prefabFireball;

  [Header("Character Abilities")]
  [SerializeField] bool canJump = true;
  [SerializeField] bool canSlide = true;
  [SerializeField] bool canFly = false;

  [Header("Movement Params")]
  [SerializeField][Range(1, 20)] float rateOfAcceleration;
  [SerializeField][Range(1, 20)] float rateOfMovement;

  [Header("Jumping Params")]
  [SerializeField] float jumpForce = 15;
  [SerializeField] float maxJumpDuration;
  [SerializeField][Range(1, 10)] float rateOfJumpingAcceleration = 5f;
  [SerializeField][Range(1, 10)] float antiGravity = 7;
  [SerializeField][Range(0.1f, 0.7f)] float snapBackRate = 0.3f;

  [Header("Flying Params")]
  [SerializeField] float flyForce;
  [SerializeField] private int maxFlyingTime = 2000;
  [SerializeField] private int flyRecoveryTime = 1500;
  private bool isFlyingTimerRunning = false;

  [Header("Wall Slide Params")]
  [SerializeField][Range(0.1f, 3f)] float slideSpeed;

  float directionX = 1f;
  Vector2 inputMovement;
  ICharacterState state;
  bool isMovePressed = false;
  bool isJumpPressed = false;
  bool isFirePressed = false;
  InputAction.CallbackContext inputAction;
  PlayerAudio playerAudio;

  // [Header("Jumping Params")]

  // COMPONENTS
  Rigidbody2D rb;
  Animator animator;
  BoxCollider2D feet;
  CapsuleCollider2D body;

  // Public Interface Members
  public Transform Transform => transform;
  public Animator Animator => animator;
  public Rigidbody2D Rigidbody2D => rb;
  public BoxCollider2D Feet => feet;
  public CapsuleCollider2D Body => body;
  public Vector2 InputMovement => inputMovement;
  public InputAction.CallbackContext InputAction => inputAction;
  public GameObject FireSpawnPoint => fireSpawnPoint;
  public GameObject PrefabFireball => prefabFireball;
  //Jumping params
  public float JumpForce => jumpForce;
  public float MaxJumpDuration => maxJumpDuration;
  public float RateOfJumpingAcceleration => rateOfJumpingAcceleration;
  public float AntiGravity => antiGravity;
  public float SnapBackRate => snapBackRate;
  public bool CanJump => canJump;
  public bool CanSlide => canSlide;
  //Flying params
  public bool CanFly => canFly;
  public float FlyForce => flyForce;
  // Input Params
  public bool IsFirePressed => isFirePressed;
  public bool IsMovePressed => isMovePressed;
  public bool IsJumpPressed => isJumpPressed;
  // Wall Sliding/Jump 
  public float SlideSpeed => slideSpeed;

  public float DirectionX { get => directionX; set => directionX = value; }
  public PlayerAudio PlayerAudio { get => playerAudio; }
  public float RateOfMovement { get => rateOfMovement; }
  public float RateOfAcceleration { get => rateOfAcceleration; }

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    feet = GetComponent<BoxCollider2D>();
    body = GetComponent<CapsuleCollider2D>();
    playerAudio = GetComponent<PlayerAudio>();
  }

  void Start()
  {
    state = new Idling(this);
  }

  void Update()
  {
    state.Update();
  }

  public void OnMove(InputAction.CallbackContext context)
  {
    /*
    * The Move action produces a Vector2 (x, y), registering x and y axis input.
    */
    inputAction = context;
    inputMovement = context.ReadValue<Vector2>();

    if (inputMovement.x != 0)
    {
      directionX = inputMovement.x > 0 ? 1 : -1;
      if (context.started)
      {
        isMovePressed = true;
      }
      else if (context.performed)
      {
        isMovePressed = true;
      }
    }

    if (context.canceled)
    {
      // movement should end //
      isMovePressed = false;
    }
  }

  public void OnJump(InputAction.CallbackContext context)
  {
    inputAction = context;

    if (context.performed)
    {
      isJumpPressed = true;
      if (canFly && !isFlyingTimerRunning) StartFlyingTimer();
    }
    else if (context.canceled)
    {
      isJumpPressed = false;
    }
  }

  public void OnFire(InputAction.CallbackContext context)
  {
    inputAction = context;
    if (context.started)
    {
      isFirePressed = true;
    }
    else if (context.performed)
    {
      isFirePressed = true;
    }
    else if (context.canceled)
    {
      isFirePressed = false;
    }
  }

  //fired from the Animator
  // OR from Sliding state
  public void OnFireAway()
  {
    transform.localScale = new Vector2(directionX, 1f);
    // instantiate prefab
    GameObject projectile = Instantiate(
        prefabFireball,
        fireSpawnPoint.transform.position,
        Quaternion.identity);

    playerAudio.PlayLaserSound();

    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
    // TODO: make projectile velocity a SerializeField
    rb.velocity = fireSpawnPoint.transform.right * 20f * transform.localScale.x;
    // destroy object after certain amount of time
    // Destroy(projectile, lifespan);
    Destroy(projectile, 0.5f);
  }

  private async void StartFlyingTimer()
  {
    isFlyingTimerRunning = true;
    await Task.Delay(maxFlyingTime);
    canFly = false;
    FxManager.Instance.JetpackDead(flyRecoveryTime / 1000);
    await Task.Delay(flyRecoveryTime);
    canFly = true;
    isFlyingTimerRunning = false;
  }

  public void SetState(ICharacterState state)
  {
    this.state.Exit();
    this.state = state;
    this.state.Enter();
  }
}
