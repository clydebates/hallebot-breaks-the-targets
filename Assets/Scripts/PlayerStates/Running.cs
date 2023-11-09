namespace OpSpark
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Running : ACharacterState
  {
    float acceleration;
    float rateOfMovement = 8f;
    float rateOfAcceleration = 10f;

    public Running(ICharacter character)
        : base(character) { }

    public override void Run() {}

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
      // running
      float directionX = character.DirectionX;
      character.Transform.localScale = new Vector2(directionX, 1f);
      if (directionX < 0)
      {
        Debug.Log(directionX);
      }

      UpdateAcceleration();
      float toX = rateOfMovement * acceleration * directionX;
      Rigidbody2D rb = character.Rigidbody2D;
      rb.velocity = new Vector2(toX, rb.velocity.y);
    }

    private void UpdateAcceleration()
    {
        if (acceleration < 1)
        {
            acceleration += Time.deltaTime * rateOfAcceleration;
        }
        else if (acceleration != 0)
        {
            acceleration -= Time.deltaTime * rateOfAcceleration;
          if (acceleration < 0)
          {
            acceleration = 0;
          }
        }
    }
  }
}
