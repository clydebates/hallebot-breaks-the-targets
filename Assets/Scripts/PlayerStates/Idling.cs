namespace OpSpark
{
    using UnityEngine;

    public class Idling : ACharacterState
    {
        public Idling(ICharacter character)
            : base(character) { }

    public override void Idle() {}

    public override void Enter()
        {
            base.Enter();
            character.Animator.SetBool(Strings.IS_IDLING, true);
            // character.Transform.localScale = new Vector2(1f, 1f);
        }

        public override void Exit()
        {
            base.Exit();
            character.Animator.SetBool(Strings.IS_IDLING, false);
        }
    }
}
