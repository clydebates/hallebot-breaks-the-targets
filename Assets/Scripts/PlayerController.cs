using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using OpSpark;
using System.Linq;

public class PlayerController : MonoBehaviour, ICharacter
{
  [Header("Required Game Objects")]
  [SerializeField] GameObject fireSpawnPoint;
  [SerializeField] GameObject prefabFireball; 
  [SerializeField] [Range(1, 10)] float rateOfAcceleration;
  float directionX = 1f;
  Vector2 inputMovement;
  ICharacterState state;
  bool isInputPressed = false;
  InputAction.CallbackContext inputAction;


  // [Header("Jumping Params")]

  // allowable state transitions
  readonly Dictionary<string, string[]> _allowedTransitions =
  new()
  {
      { 
        Strings.STATE_IDLING, 
        new string[] { Strings.STATE_RUNNING, Strings.STATE_JUMPING, Strings.STATE_CLIMBING, Strings.STATE_FIRING } 
      },
      {
        Strings.STATE_RUNNING,
        new string[] { Strings.STATE_IDLING, Strings.STATE_JUMPING, Strings.STATE_CLIMBING, Strings.STATE_FIRING }
      },
      {
        Strings.STATE_JUMPING,
        new string[] { Strings.STATE_IDLING, Strings.STATE_CLIMBING, Strings.STATE_FIRING }
      },
      { 
        Strings.STATE_FIRING, 
        new string[] { Strings.STATE_IDLING, Strings.STATE_RUNNING } 
      },
      { 
        Strings.STATE_CLIMBING, 
        new string[] { Strings.STATE_JUMPING, Strings.STATE_FIRING } 
      },
  };


  // COMPONENTS
  Rigidbody2D rb;
  Animator animator;
  BoxCollider2D feet;

  // Public Interface Members
  public Transform Transform => transform;
  public Animator Animator => animator;
  public Rigidbody2D Rigidbody2D => rb;
  public BoxCollider2D Feet => feet;
  public Vector2 InputMovement => inputMovement;
  public float DirectionX => directionX;
  public bool IsInputPressed => isInputPressed;
  public InputAction.CallbackContext InputAction => inputAction;
  public GameObject FireSpawnPoint => fireSpawnPoint;
  public GameObject PrefabFireball => prefabFireball;

  void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
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
        directionX = inputMovement.x > 0 ? 1 : -1;

        if (inputMovement.x != 0)
        {
            isInputPressed = context.started || context.performed;
        }

        if (context.canceled)
        {
            // movement should end //
            isInputPressed = false;
        }
        Run();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
      inputAction = context;
      isInputPressed = context.started || context.performed;
      isInputPressed = !context.canceled;
      Jump();
    }

    public void OnFire(InputAction.CallbackContext context) { 
      inputAction = context;
      isInputPressed = context.started || context.performed;
      isInputPressed = !context.canceled;
      Fire();
    }

    //fired from the Animator   
    public void OnFireAway()
    {
      // instantiate prefab
      GameObject projectile = Instantiate(
          prefabFireball, 
          fireSpawnPoint.transform.position, 
          Quaternion.identity);

      Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
      // TODO: make projectile velocity a SerializeField
      rb.velocity = fireSpawnPoint.transform.right * 20f * transform.localScale.x;
      // destroy object after certain amount of time
      // Destroy(projectile, lifespan);
      Destroy(projectile, 2f);
    }

    public void SetState(ICharacterState state)
    {
      if (_allowedTransitions[this.state.Name].Contains(state.Name))
      {
        this.state.Exit();
        this.state = state;
        this.state.Enter();
      }
    }

    // delegate to current state
    void Idle()
    {
        this.state.Idle();
    }

    void Run()
    {
        this.state.Run();
    }

    void Jump()
    {
        this.state.Jump();
    }

    void Fire()
    {
        this.state.Fire();
    }

    void Climb()
    {
        this.state.Climb();
    }
}
