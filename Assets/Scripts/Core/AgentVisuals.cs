using UnityEngine;

public class AgentVisuals : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _isMoving;
    private bool _isEmpty;
    public void SetPocket(bool isEmpty)
    {
        _isEmpty = isEmpty;
        UpdateAnimationState();
    }

    public void SetMovement(bool isMoving)
    {
        _isMoving = isMoving;
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        _animator.SetBool("IsMove", _isMoving);
        _animator.SetBool("IsEmpty", _isEmpty);
        _animator.SetBool("IsCarryMove", _isEmpty && _isMoving);
    }
}