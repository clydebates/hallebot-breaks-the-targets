namespace OpSpark
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Running : ACharacterState
  {
    float acceleration;

    public Running(ICharacter character)
        : base(character) { }

    public override void Run() { }

    public override void Enter()
    {
      base.Enter();
      character.Animator.SetBool(Strings.IS_RUNNING, true);
    }

    public override void Exit()
    {
      base.Exit();
      character.Animator.SetBool(Strings.IS_RUNNING, false);
    }

    public override void Update()
    {
      base.Update();
      character.Transform.localScale = new Vector2(character.DirectionX, 1f);

      UpdateAcceleration();
      float toX = character.RateOfMovement * acceleration * character.DirectionX;
      Rigidbody2D rb = character.Rigidbody2D;
      rb.velocity = new Vector2(toX, rb.velocity.y);
    }

    private void UpdateAcceleration()
    {
      if (acceleration < 1)
      {
        acceleration += Time.deltaTime * character.RateOfAcceleration;
      }
      else if (acceleration != 0)
      {
        acceleration -= Time.deltaTime * character.RateOfAcceleration;
        if (acceleration < 0)
        {
          acceleration = 0;
        }
      }
    }
  }
}
