namespace OpSpark
{
  public class Firing : ACharacterState
  {
    public Firing(ICharacter character) : base(character) { }

    public override void Fire() { }

    public override void Enter()
    {
      base.Enter();
      character.Animator.SetTrigger(Strings.FIRE);
    }

    public override void Exit()
    {
      base.Exit();
    }
  }
}
