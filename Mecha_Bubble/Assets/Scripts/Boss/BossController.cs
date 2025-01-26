using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public enum BossStates
    {
        Idle, RightHandAttack, LeftHandAttack, Vomit, Dying
    }

    [Header("References")]
    [SerializeField] private Animator _animator;

    [SerializeField] private Transform _spitTransform;
    [SerializeField] private DamageSensor _leftHand;
    [SerializeField] private DamageSensor _rightHand;

    [SerializeField] private GameObject _vomit;
    [SerializeField] private GameObject _dropVomit;
    [SerializeField] private Transform _leftPlatform;
    [SerializeField] private Transform _midPlatform;
    [SerializeField] private Transform _rightPlatform;

    [Header("Variables")]
    [SerializeField] private float _speed;

    private float m_timer;
    private int m_platformIndex = 0;

    private BossStates m_currentState = BossStates.Idle;

    private bool m_moveToRight = true;
    private int m_hp = 20;

    private bool m_alive = true;

    private void Awake()
    {
        m_hp = 20;
        m_timer = 2f;

        LeftHandDamageDisable();
        RightHandDamageDisable();
    }

    private void Update()
    {
        ExecuteState();

        if (!m_alive)
            return;

        if (m_currentState == BossStates.Dying)
            return;

        CheckDeath();
        MoveToSides();
    }

    private void MoveToSides()
    {
        var position = transform.position;
        position.x += _speed * Time.deltaTime * (m_moveToRight ? 1f : -1f);
        transform.position = position;

        if (position.x >= 3f)
            m_moveToRight = false;
        else if (position.x <= -3f)
            m_moveToRight = true;
    }

    private void CheckDeath()
    {
        if (m_hp > 0)
            return;

        UpdateState(BossStates.Dying);
    }

    private void UpdateState(BossStates state)
    {
        if (m_currentState == state)
            return;

        m_currentState = state;

        _animator.Play(m_currentState.ToString());

        switch(m_currentState)
        {
            case BossStates.LeftHandAttack: _leftHand.GetComponent<SpriteRenderer>().sortingOrder = 100; break;
            case BossStates.RightHandAttack: _rightHand.GetComponent<SpriteRenderer>().sortingOrder = 100; break;
            default:
                _leftHand.GetComponent<SpriteRenderer>().sortingOrder = 0;
                _rightHand.GetComponent<SpriteRenderer>().sortingOrder = 0;
                break;
        }
    }

    private void ExecuteState()
    {
        switch (m_currentState)
        {
            case BossStates.Idle: IdleState(); break;
            case BossStates.RightHandAttack: RightHandAttack(); break;
            case BossStates.LeftHandAttack: LeftHandAttack(); break;
            case BossStates.Vomit: Vomit(); break;
            case BossStates.Dying: Dying(); break;
        }
    }

    private void IdleState()
    {
        m_timer -= Time.deltaTime;

        if (m_timer <= 0f)
        {
            UpdateState(BossStates.RightHandAttack);
            SetTimer(100f);
        }
    }

    private void RightHandAttack()
    {
        m_timer -= Time.deltaTime;

        if (m_timer <= 0f)
        {
            UpdateState(BossStates.LeftHandAttack);
            SetTimer(100f);
        }
    }

    private void LeftHandAttack()
    {
        m_timer -= Time.deltaTime;

        if (m_timer <= 0f)
        {
            UpdateState(BossStates.Vomit);
            SetTimer(100f);
        }
    }

    private void Vomit()
    {
        m_timer -= Time.deltaTime;

        if (m_timer <= 0f)
        {
            UpdateState(BossStates.Idle);
            PlatformElevate();
            SetTimer(100f);
        }
    }

    private void Dying()
    {
        m_timer -= Time.deltaTime;

        if (m_timer <= 0f)
        {
            m_alive = false;
        }
    }

    public void ThrowVomit()
    {
        var vomit = Instantiate(_vomit, _spitTransform.position, Quaternion.identity);
        vomit.transform.eulerAngles = new Vector3(0f, 0f, 180f);

        StartCoroutine(VomitAsc(vomit.transform));
    }

    private IEnumerator VomitAsc(Transform vomit)
    {
        var damageSensor = vomit.GetComponent<DamageSensor>();
        var vomitController = vomit.GetComponent<VomitController>();

        damageSensor.DisableHit();
        damageSensor.ChangeColliderDiretion(true);

        vomitController.AddForce(new Vector2(0f, 20f));

        var startPosition = vomit.position;
        var endPosition = startPosition + Vector3.up * 6f;

        while (vomit.transform.position.y <= endPosition.y)
            yield return null;

        vomitController.DisableGravity();
        vomit.position = endPosition;

        yield return new WaitForSeconds(2f);

        float height = vomit.position.y;

        Destroy(vomit.gameObject);

        yield return VomitDesc(height);
    }

    private IEnumerator VomitDesc(float height)
    {

        var dropVomits = new List<GameObject>
        {
            Instantiate(_dropVomit, new Vector3(0f, height, 0f), Quaternion.identity),
            Instantiate(_dropVomit, new Vector3(0f, height, 0f), Quaternion.identity)
        };

        var endOffset = new Vector2(0f, -0.1f);
        var endScale = new Vector2(1.55f, 0.3f);

        var startX = -5f;

        foreach (var vomit in dropVomits)
        {
            var xRandom = Random.Range(startX, startX + 5f);
            
            var startPosition = vomit.transform.position;
            startPosition.x = xRandom;
            vomit.transform.position = startPosition;

            var damageSensor = vomit.GetComponent<DamageSensor>();
            var vomitController = vomit.GetComponent<VomitController>();

            damageSensor.EnableHit();
            vomitController.EnableGravity();

            startX += 5f;
        }

        bool nextFrame = true;

        var auxDropVomits = new List<GameObject>(dropVomits);

        while (nextFrame)
        {
            nextFrame = false;
            var vomitsToRemove = new List<GameObject>();

            foreach (var vomit in auxDropVomits)
            {
                if (vomit.transform.position.y - 1f >= -2.5f)
                {
                    nextFrame = true;

                    break;
                }
                else
                {
                    var damageSensor = vomit.GetComponent<DamageSensor>();
                    var vomitController = vomit.GetComponent<VomitController>();

                    damageSensor.ChangeColliderDiretion(false);
                    vomitController.Impact();

                    damageSensor.ChangeColliderOffset(endOffset);
                    damageSensor.ChangeColliderSize(endScale);

                    vomitsToRemove.Add(vomit);
                }
            }

            foreach (var vomit in vomitsToRemove)
                auxDropVomits.Remove(vomit);

            if (nextFrame)
                yield return null;
        }

        yield return new WaitForSeconds(3f);

        Destroy(dropVomits[0].gameObject);
        dropVomits.RemoveAt(0);
        Destroy(dropVomits[0].gameObject);
        dropVomits.Clear();

        yield return null;
    }

    private void PlatformElevate()
    {
        Transform platform = m_platformIndex == 0 ? _rightPlatform : (m_platformIndex == 1 ? _midPlatform : _leftPlatform);

        StartCoroutine(PlatformUp(platform));

        m_platformIndex++;

        if (m_platformIndex > 2)
            m_platformIndex = 0;
    }

    private IEnumerator PlatformUp(Transform platform, float duration = 1f)
    {
        var startPosition = platform.position;
        var endPosition = startPosition;
        endPosition.y += 5.5f;

        float timer = 0f;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            platform.position = Vector3.Lerp(startPosition, endPosition, timer / duration);
            yield return null;
        }

        platform.position = endPosition;
        yield return null;

        yield return new WaitForSeconds(10f);
        yield return PlatformDown(platform);
    }

    private IEnumerator PlatformDown(Transform platform, float duration = 1f)
    {
        var startPosition = platform.position;
        var endPosition = startPosition;
        endPosition.y -= 5.5f;

        float timer = 0f;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            platform.position = Vector3.Lerp(startPosition, endPosition, timer / duration);
            yield return null;
        }

        platform.position = endPosition;
        yield return null;

        yield return new WaitForSeconds(1f);
        SetTimer(0f);
    }

    public void TakeDamage(int damage)
    {
        m_hp -= damage;
    }

    public void SetTimer(float timer)
    {
        m_timer = timer;
    }

    public void LeftHandDamageEnable()
    {
        _leftHand.EnableHit();
    }

    public void LeftHandDamageDisable()
    {
        _leftHand.DisableHit();
    }

    public void RightHandDamageEnable()
    {
        _rightHand.EnableHit();
    }

    public void RightHandDamageDisable()
    {
        _rightHand.DisableHit();
    }

    public void Death()
    {
        Debug.Log("Boss Destroyed");
        
        Destroy(gameObject);
    }

}
