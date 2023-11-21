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
    void Climb();
    void Slide();

    void Update();
    void OnAnimationEnd(string name);

    public string Name { get; }
  }
}
