namespace OpSpark
{
  using UnityEngine;
  using UnityEngine.InputSystem;

  public interface ICharacter
  {
    void SetState(ICharacterState state);
    void OnFireAway();
    Transform Transform { get; }
    Animator Animator { get; }
    Rigidbody2D Rigidbody2D { get; }
    BoxCollider2D Feet { get; }
    CapsuleCollider2D Body { get; }
    Vector2 InputMovement { get; }
    InputAction.CallbackContext InputAction { get; }
    GameObject FireSpawnPoint { get; }
    GameObject PrefabFireball { get; }
    PlayerAudio PlayerAudio { get; }
    float DirectionX { get; set; }
    float RateOfAcceleration { get; }
    float RateOfMovement { get; }
    bool IsMovePressed { get; }
    bool IsJumpPressed { get; }
    bool IsFirePressed { get; }
    float JumpForce { get; }
    float MaxJumpDuration { get; }
    float RateOfJumpingAcceleration { get; }
    float AntiGravity { get; }
    float SnapBackRate { get; }
    float SlideSpeed { get; }
    bool CanJump { get; }
    bool CanFly { get; }
    bool CanSlide { get; }
    float FlyForce { get; }
  }
}
