namespace OpSpark
{
  using UnityEngine;

  public abstract class ACharacterState : ICharacterState
  {
    // Class members
    protected ICharacter character;
    readonly string _name;

    // getters
    public string Name => _name;

    // Constructor
    public ACharacterState(ICharacter character)
    {
        this.character = character;
        this._name = this.GetType().Name;
    }

    // Methods
    public virtual void Enter()
    {
        Debug.Log($"Entering state {_name}");
    }

    public virtual void Exit()
    {
        Debug.Log($"Exiting state {_name}");
    }

    public virtual void Climb()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Fire()
    {
        character.SetState(new Firing(character));
    }

    public virtual void Idle()
    {
      character.SetState(new Idling(character));
    }

    public virtual void Jump()
    {
      character.SetState(new Jumping(character));
    }

    public virtual void Run()
    {
      character.SetState(new Running(character));
    }

    public virtual void Slide()
    {
      character.SetState(new Sliding(character));
    }

    public virtual void SlideJump()
    {
      character.SetState(new SlideJumping(character));
    }

    public virtual void Update()
    {
      // switch states depending on the input action
      if (character.IsFirePressed)
      {
        Fire();
      }
      else if (character.IsJumpPressed)
      {
        Jump();
      }
      else if (IsTouchingWall() != 0)
      {
        Slide();
      }
      else if (character.IsMovePressed)
      {
        Run();
      }
      else
      {
        Idle();
      }
    }

    // TODO: not needed?
    public virtual void OnAnimationEnd(string name)
    {
      // Debug.Log($"Animation {name} ended");
    }

    protected bool IsTouchingPlatform() 
    {
      return character.Feet.IsTouchingLayers(LayerMask.GetMask(Strings.PLATFORM));
    }

    /*
    * Returns:
    * if not touching a wall: 0
    * if touching wall to left side of character: -1
    * if touching wall to right side of character: 1
    */
    protected int IsTouchingWall() 
    {
      if (!IsTouchingPlatform())
      {
        float lengthOfCast = 0.6f;
        Vector2 characterPos = character.Transform.position;
        int wallLayerMask = LayerMask.GetMask(Strings.WALL);

        RaycastHit2D leftRayHit = Physics2D.Raycast(characterPos, Vector2.left, lengthOfCast, wallLayerMask);
        if(leftRayHit.collider != null)
        {
          return -1;
        }

        RaycastHit2D rightRayHit = Physics2D.Raycast(characterPos, Vector2.right, lengthOfCast, wallLayerMask);
        if (rightRayHit.collider != null)
        {
          return 1;
        }
      }
      return 0;
    }
  }
}
