namespace OpSpark
{
  using UnityEngine;

  public class SlideJumping : ACharacterState
  {
    public SlideJumping(ICharacter character) : base(character) { }
    // Update is called once per frame
    public override void Update()
    {
      float toY = character.JumpForce;
      float toX = (character.Rigidbody2D.velocity.x + 0.5f) * character.DirectionX;
      character.Rigidbody2D.velocity = new Vector2(toX, toY);
    }
  }
}
