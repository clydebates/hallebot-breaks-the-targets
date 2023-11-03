namespace OpSpark
{
  using UnityEngine;
  using UnityEngine.InputSystem;

  public class Firing : ACharacterState
  {
    public Firing(ICharacter character) : base(character) { }

    public override void Enter()
    {
      base.Enter();
      character.Animator.SetTrigger(Strings.FIRE);
    }

    public override void Exit()
    {
      base.Exit();
    }

    public override void OnAnimationEnd(string name)
    {
      base.OnAnimationEnd(name);
      if (Strings.FIRE.Equals(name))
      {
        if (character.InputMovement.x != 0)
        {
          Run();
        } else
        {
          Idle();
        }
      }
    }

    public override void Fire() {
      base.Fire();

      // TODO: go to the correct state based on user input
      Idle();
    }
  }
}
