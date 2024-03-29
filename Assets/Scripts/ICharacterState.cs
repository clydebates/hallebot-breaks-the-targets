namespace OpSpark
{
  using UnityEngine;

  public interface ICharacterState
  {
    void Exit();
    void Enter();

    // state delegation methods
    void Idle();
    void Run();
    void Jump();
    void Fire();
    void Slide();
    void SlideJump();

    void Update();
    void OnAnimationEnd(string name);

    public string Name { get; }
  }
}
