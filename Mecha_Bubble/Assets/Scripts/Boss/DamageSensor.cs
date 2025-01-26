using UnityEngine;

public class DamageSensor : MonoBehaviour
{

    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _hittingColor = Color.red;

    [SerializeField] private CapsuleCollider2D _collider;
    [SerializeField] private SpriteRenderer _sprite;

    public void EnableHit()
    {
        if (_collider.enabled)
            return;
            
        _sprite.color = _hittingColor;
        _collider.enabled = true;
    }

    public void DisableHit()
    {
        if (!_collider.enabled)
            return;
        
        _sprite.color = _defaultColor;
        _collider.enabled = false;
    }

    public void ChangeColliderOffset(Vector2 offset)
    {
        _collider.offset = offset;
    }

    public void ChangeColliderSize(Vector2 size)
    {
        _collider.size = size;
    }

    public void ChangeColliderDiretion(bool vertical)
    {
        _collider.direction = vertical ? CapsuleDirection2D.Vertical : CapsuleDirection2D.Horizontal;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Player Hitted");
    }

}
