namespace OpSpark
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class Jumping : ACharacterState
  {
    float jumpForce = 15;
    float maxJumpDuration = 0.8f;
    [Range(1, 10)] float antiGravity = 7;
    [Range(1, 10)] float rateOfAcceleration = 5;
    [Range(0.1f, 0.7f)] float snapBackRate = 0.3f;
    Vector2 defaultGravity = new Vector2(0f, -Physics2D.gravity.y);
    int jumpCount = 0;
    int jumpEndedCount = 0;
    float duration = 0;
    
    public Jumping(ICharacter character)
        : base(character) { }

    public override void Enter()
    {
        base.Enter();
        character.Transform.localScale = new Vector2(character.DirectionX, 1f);
        
    }

    public override void Exit()
    {
        base.Exit();
        character.Animator.SetBool(Strings.IS_RUNNING, true);
    }

    public override void Update()
    {
      base.Update();
      // TODO: update y velocity on what conditions? //
      if(IsTouchingPlatform() && character.IsInputPressed)
      {
          PerformJump();
          jumpCount++;
      }

      // TODO: update rate of return to platform //
      if(character.Rigidbody2D.velocity.y < 0)
      {
          // we're falling //
          character.Rigidbody2D.velocity -= defaultGravity * rateOfAcceleration * Time.deltaTime;
          if (character.IsInputPressed && jumpCount < 2 && jumpEndedCount > 0)
          {
              // double jump
              // TODO: doesn't work
              PerformJump();
              jumpCount++;
          }
      }


      // TODO: jump higher while key pressed //
      if(character.IsInputPressed && character.Rigidbody2D.velocity.y > 0)
      {
          duration += Time.deltaTime;
          // if (duration > maxJumpDuration) character.IsInputPressed = false;

          float pointInJumpDuration = duration / maxJumpDuration;
          float appliedAntiGravity = antiGravity;

          // if we're more than halfway through the allowable jump duration //
          if(pointInJumpDuration > 0.5f)
          {
              appliedAntiGravity = antiGravity * (1 - pointInJumpDuration);
          }

          character.Rigidbody2D.velocity += defaultGravity * appliedAntiGravity * Time.deltaTime;
      }

      // player releases button //
      if(!character.IsInputPressed)
      {
          duration = 0;
          character.Animator.SetBool(Strings.IS_JUMPING, false);
          // but we're still falling, so snap back to earth a bit //
          if (character.Rigidbody2D.velocity.y > 0)
          {
              character.Rigidbody2D.velocity = new Vector2(character.Rigidbody2D.velocity.x, character.Rigidbody2D.velocity.y * snapBackRate);
          }
          Idle();
      }
    }

    private void PerformJump() 
    {
        duration = 0;
        character.Animator.SetBool(Strings.IS_JUMPING, true);
        float toY = jumpForce;
        character.Rigidbody2D.velocity = new Vector2(character.Rigidbody2D.velocity.x, toY);
    }

    private bool IsTouchingPlatform()
    {
        if (character.Feet.IsTouchingLayers(LayerMask.GetMask(Strings.PLATFORM))) 
        {
            jumpEndedCount = 0;
            jumpCount = 0;
            return true;
        }
        return false;
    }
  }
}
