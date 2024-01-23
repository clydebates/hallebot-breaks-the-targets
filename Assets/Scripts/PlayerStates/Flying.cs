namespace OpSpark
{
  using UnityEngine;

  public class Flying : ACharacterState
  {
    Vector2 defaultGravity;
    float acceleration;
    float rateOfMovement = 8f;
    float rateOfAcceleration = 10f;

    public Flying(ICharacter character)
        : base(character) { }

    public override void Fly() { }

    public override void Enter()
    {
      base.Enter();
      defaultGravity = new Vector2(0f, -Physics2D.gravity.y);
      character.Animator.SetBool(Strings.IS_FLYING, true);
      character.PlayerAudio.PlayJumpSound();
    }

    public override void Exit()
    {
      base.Exit();
      character.Animator.SetBool(Strings.IS_FLYING, false);
      character.PlayerAudio.CancelJumpSound();
    }

    public override void Update()
    {
      base.Update();

      if (character.CanFly)
      {
        character.Rigidbody2D.velocity = new Vector2(character.Rigidbody2D.velocity.x, character.FlyForce);
        if (character.IsMovePressed)
        {
          UpdateAcceleration();
          float toX = rateOfMovement * acceleration * character.DirectionX;
          character.Transform.localScale = new Vector2(character.DirectionX, 1f);
          Rigidbody2D rb = character.Rigidbody2D;
          rb.velocity = new Vector2(toX, rb.velocity.y);
        }
      }
      else
      {
        // fall
        character.PlayerAudio.CancelJumpSound();
        character.Rigidbody2D.velocity -= defaultGravity * character.RateOfJumpingAcceleration * Time.deltaTime;
      }


      // if(isJumping && character.Rigidbody2D.velocity.y > 0)
      // {
      //   if (!character.IsFlyingEnabled()) isJumping = false;

      //   // character.Rigidbody2D.velocity += defaultGravity * character.AntiGravity * Time.deltaTime;
      // }
    }

    // private void PerformFly() 
    // {
    //   // duration = 0;
    //   // float toY = character.JumpForce;
    //   character.Rigidbody2D.AddForce(character.Transform.up * character.FlyForce);
    //   // character.Rigidbody2D.velocity = new Vector2(character.Rigidbody2D.velocity.x, toY);
    // }
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
