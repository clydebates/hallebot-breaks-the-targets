namespace OpSpark
{
  using UnityEngine;

  public class Flying : ACharacterState
  {
    Vector2 defaultGravity;
    
    public Flying(ICharacter character)
        : base(character) { }

    public override void Fly() { }

    public override void Enter()
    {
      base.Enter();
      defaultGravity = new Vector2(0f, -Physics2D.gravity.y);
      character.Animator.SetBool(Strings.IS_FLYING, true);
    }

    public override void Exit()
    {
        base.Exit();
        character.Animator.SetBool(Strings.IS_FLYING, false);
    }

    public override void Update()
    {
      base.Update();
      // flyStamina = staminaRegen * Time.delaTime;
      // 
      if(character.CanFly)
      {
        // PerformFly();
        character.Rigidbody2D.velocity = new Vector2(character.Rigidbody2D.velocity.x, character.FlyForce);
      } else 
      {
        // fall
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
  }
}
