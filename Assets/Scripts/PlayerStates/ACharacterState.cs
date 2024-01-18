namespace OpSpark
{
  using UnityEngine;

  public abstract class ACharacterState : ICharacterState
  {
    // Class members
    protected ICharacter character;
    readonly string _name;
    protected int characterInt = LobbyManager.Instance.CharacterSelection;

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

    public virtual void Fly()
    {
      character.SetState(new Flying(character));
    }

    public virtual void Update()
    {
      // switch states depending on the input action
      if (character.IsFirePressed)
      {
        Fire();
      }
      else if (character.IsJumpPressed && character.CanJump)
      {
        Jump();
      }
      else if (character.IsJumpPressed && character.CanFly)
      {
        Fly();
      }
      else if (character.CanSlide && IsTouchingWall())
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

    protected bool IsTouchingWall() 
    {
      if (!IsTouchingPlatform())
      {
        // TODO: layerMask filter is not working
        if (character.Feet.IsTouchingLayers(LayerMask.GetMask(Strings.WALL)))
        {
          return true;
        }
        return false;
      }
      return false;
    }
  }
}
