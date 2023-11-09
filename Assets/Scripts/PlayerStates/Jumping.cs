namespace OpSpark
{
  using UnityEngine;

  public class Jumping : ACharacterState
  {
    // int jumpCount = 0;
    // int jumpEndedCount = 0;
    float duration = 0;
    Vector2 defaultGravity;
    
    public Jumping(ICharacter character)
        : base(character) { }

    public override void Idle() { }

    public override void Enter()
    {
      base.Enter();
      character.Transform.localScale = new Vector2(character.DirectionX, 1f);
      defaultGravity = new Vector2(0f, -Physics2D.gravity.y);
      character.Animator.SetTrigger(Strings.JUMP);
    }

    public override void Exit()
    {
        base.Exit();
        character.Animator.ResetTrigger(Strings.JUMP);
    }

    public override void Climb() { /* do nothing */ }

    public override void Update()
    {
      base.Update();
      // TODO: update y velocity on what conditions? //
      if(IsTouchingPlatform() && character.IsJumpPressed)
      {
          PerformJump();
          // jumpCount++;
      }

      // TODO: update rate of return to platform //
      if(character.Rigidbody2D.velocity.y < 0)
      {
          // we're falling //
          character.Rigidbody2D.velocity -= defaultGravity * character.RateOfJumpingAcceleration * Time.deltaTime;
          // if (character.IsJumpPressed && jumpCount < 2 && jumpEndedCount > 0)
          // {
          //     // double jump
          //     // TODO: doesn't work
          //     PerformJump();
          //     jumpCount++;
          // }
      }


      // TODO: jump higher while key pressed //
      if(character.IsJumpPressed && character.Rigidbody2D.velocity.y > 0)
      {
          duration += Time.deltaTime;
          // if (duration > maxJumpDuration) character.IsJumpPressed = false;

          float pointInJumpDuration = duration / character.MaxJumpDuration;
          float appliedAntiGravity = character.AntiGravity;

          // if we're more than halfway through the allowable jump duration //
          if(pointInJumpDuration > 0.5f)
          {
              appliedAntiGravity = character.AntiGravity * (1 - pointInJumpDuration);
          }

          character.Rigidbody2D.velocity += defaultGravity * appliedAntiGravity * Time.deltaTime;
      }

      // player releases button //
      if(!character.IsJumpPressed)
      {
        duration = 0;
        // character.Animator.SetBool(Strings.IS_JUMPING, false);
        // but we're still falling, so snap back to earth a bit //
        if (character.Rigidbody2D.velocity.y > 0)
        {
          character.Rigidbody2D.velocity = new Vector2(character.Rigidbody2D.velocity.x, character.Rigidbody2D.velocity.y * character.SnapBackRate);
        }
        Run();
      }
    }

    private void PerformJump() 
    {
      duration = 0;
      float toY = character.JumpForce;
      character.Rigidbody2D.velocity = new Vector2(character.Rigidbody2D.velocity.x, toY);
    }
  }
}
