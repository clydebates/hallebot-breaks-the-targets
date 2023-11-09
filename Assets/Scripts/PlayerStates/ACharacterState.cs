namespace OpSpark
{
  using System.Collections.Generic;
  using System.Linq;
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
        // Debug.Log($"Entering state {_name}");
    }

    public virtual void Exit()
    {
        // Debug.Log($"Exiting state {_name}");
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

    public virtual void Update()
    {
      // TODO: all state switching should happen here
      // switch states depending on the input action
      if (character.IsMovePressed && character.IsJumpPressed)
      {
        Jump();
      }
      else if (character.IsMovePressed && character.IsFirePressed)
      {
        Fire();
      }
      else if (character.IsMovePressed)
      {
        Run();
      }
      else if (character.IsJumpPressed)
      {
        Jump();
      }
      else if (character.IsFirePressed)
      {
        Fire();
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

  }
}
