namespace OpSpark
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public interface ICharacter
    {
        void SetState(ICharacterState state);
        Transform Transform { get; }
        Animator Animator { get; }
        Rigidbody2D Rigidbody2D { get; }
        BoxCollider2D Feet { get; }
        Vector2 InputMovement { get; }
        InputAction.CallbackContext InputAction { get; }
        GameObject FireSpawnPoint { get; } 
        GameObject PrefabFireball { get; }
        float DirectionX { get; }
        bool IsInputPressed { get; }
    }
}
