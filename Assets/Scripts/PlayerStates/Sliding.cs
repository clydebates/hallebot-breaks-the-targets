namespace OpSpark
{
  using UnityEngine;

  public class Sliding : ACharacterState
    {
      readonly float firingSpeed = 0.5f;
      float canFire = 0;

      public Sliding(ICharacter character) : base(character) { }

      public override void Slide() {}
    public override void Jump()
    {
      base.SlideJump();
    }

    public override void Fire()
      {
      // skip firing state so we don't change animations
      // but still want to actually fire a projectile
        if (Time.time > canFire)
        {
          // allow holding the button to fire once every half second
          character.OnFireAway();
          canFire = Time.time + firingSpeed;
        }
      }

      public override void Enter()
      {
        base.Enter();
        // IsTouchingWall should never be 0 once we are in Sliding state
        if (IsTouchingWall() > 0)
        {
          // touching wall on right side, face left
          character.Transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
          // touching wall on left side, face right
          character.Transform.localScale = new Vector2(1f, 1f);
        }
        character.Animator.SetBool(Strings.IS_SLIDING, true);
        PerformSlide();
      }

      public override void Exit()
      {
          base.Exit();
          character.Animator.SetBool(Strings.IS_SLIDING, false);
          character.Rigidbody2D.gravityScale = -Physics2D.gravity.y;
      }

      private void PerformSlide()
      {
        character.Rigidbody2D.gravityScale = 0;
        character.Rigidbody2D.velocity = new Vector2(0, character.SlideSpeed);
      }
    }
}
