using UnityEngine;

public class DamageSensor : MonoBehaviour
{

    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _hittingColor = Color.red;

    [SerializeField] private CapsuleCollider2D[] _colliders;
    [SerializeField] private SpriteRenderer _sprite;

    public void EnableHit()
    {
        if (_colliders[0].enabled)
            return;
            
        _sprite.color = _hittingColor;

        foreach (var collider in _colliders)
            collider.enabled = true;
    }

    public void DisableHit()
    {
        if (!_colliders[0].enabled)
            return;
        
        _sprite.color = _defaultColor;
        foreach (var collider in _colliders)
            collider.enabled = false;
    }

    public void ChangeColliderOffset(Vector2 offset)
    {
        foreach (var collider in _colliders)
            collider.offset = offset;
    }

    public void ChangeColliderSize(Vector2 size)
    {
        foreach (var collider in _colliders)
            collider.size = size;
    }

    public void ChangeColliderDiretion(bool vertical)
    {
        foreach (var collider in _colliders)
            collider.direction = vertical ? CapsuleDirection2D.Vertical : CapsuleDirection2D.Horizontal;
    }

}
