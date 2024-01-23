namespace OpSpark
{
  using UnityEngine;

  public class Sliding : ACharacterState
  {
    readonly float firingSpeed = 0.5f;
    float canFire = 0;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);
    private float canJump = 0;
    private float maxJumpTime = 0.3f;
    private bool isWallJumping = false;

    public Sliding(ICharacter character) : base(character) { }

    public override void Slide() { }
    public override void Run() { }

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

    public override void Jump()
    {
      // wall jumping
      if (Time.time > canJump)
      {
        isWallJumping = true;
        character.Rigidbody2D.velocity = new Vector2(character.Transform.localScale.x * wallJumpingPower.x, wallJumpingPower.y);
        canJump = Time.time + maxJumpTime;
      }
      else
      {
        isWallJumping = false;
        Idle();
      }
    }

    public override void Enter()
    {
      base.Enter();
      character.Transform.localScale = new Vector2(-character.Transform.localScale.x, 1f);
      character.DirectionX = character.Transform.localScale.x;
      character.Animator.SetBool(Strings.IS_SLIDING, true);
    }

    public override void Update()
    {
      if (!isWallJumping) PerformSlide();
      base.Update();
    }

    public override void Exit()
    {
      character.Animator.SetBool(Strings.IS_SLIDING, false);
      base.Exit();
    }

    private void PerformSlide()
    {
      character.Rigidbody2D.velocity = new Vector2(0, Mathf.Clamp(character.Rigidbody2D.velocity.y, -character.SlideSpeed, float.MaxValue));
    }
  }
}
