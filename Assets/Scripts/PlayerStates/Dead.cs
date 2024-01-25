namespace OpSpark
{
  using UnityEngine;

  public class Dead : ACharacterState
  {
    public Dead(ICharacter character)
        : base(character) { }

    public override void Idle() { }

    public override void Enter()
    {
      base.Enter();
      character.Animator.SetBool(Strings.IS_IDLING, true);
    }

    public override void Exit() { }
    public override void Update() { }
  }
}
