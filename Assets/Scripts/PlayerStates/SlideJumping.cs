namespace OpSpark
{
  using UnityEngine;

  public class SlideJumping : ACharacterState
  {
    public SlideJumping(ICharacter character) : base(character) { }
    // Update is called once per frame
    public override void Update()
    {
      character.Rigidbody2D.AddForce(new Vector2(8f * character.DirectionX, -8f));
    }
  }
}
