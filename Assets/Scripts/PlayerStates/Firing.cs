namespace OpSpark
{
  public class Firing : ACharacterState
  {
    public Firing(ICharacter character) : base(character) { }

    public override void Fire() { }

    public override void Enter()
    {
      base.Enter();
      if (characterInt == 0)
      {
        character.Animator.SetTrigger(Strings.FIRE);
      }
      else if (characterInt == 1)
      {
        // spaceman purposefully has no firing animation
        character.OnFireAway();
      }
    }

    public override void Exit()
    {
      base.Exit();
    }
  }
}
