using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Animation))]
[RequireComponent(typeof(ControllerEnemy))]
public class ControllerEnemy : MonoBehaviour
{

    //��������� ����� �������. ��������� ���� ��.

    public RectTransform healthBar;
    public float health;
    
    [SerializeField]
    private GameObject[] _partsOfBody;
    private Animation _animation;
    private float _maxHealth;
    private Rigidbody _rb;
    private ControllerEnemy _controllerEnemy;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animation = GetComponent<Animation>();
        _maxHealth = health;
        _controllerEnemy = GetComponent<ControllerEnemy>();
    }

    //�� ���� ���������� �������� ���� ����.
    private void Update()
    {

        if (health <= 0)
        {
            health = 0;
            RagdollOn();
        }

    }

    public void TakeDamage(int damage)
    {
        //��� ����������� ����������� �������� ���� �� ���������. �����, ����� ����� �� �������, ���� ����� �� ����� �� ������ ���������.
        if (_controllerEnemy.enabled == false)
            return;
        if (health != 0)
            health -= damage;

        //������ ������ ���� � ����������� �� ������ ��������.
        healthBar.localScale = new Vector3(health / _maxHealth, healthBar.anchorMax.y, 1);
    }

    //������� �������� ������ � ��������� ��, ��� ���������� "�������" ��������.
    private void RagdollOn()
    {
        _rb.constraints = RigidbodyConstraints.None;
        _animation.Stop();
        for (int i = 0; i < _partsOfBody.Length; i++)
        {
            if (_partsOfBody[i].TryGetComponent(out Rigidbody rigidbody))
                rigidbody.isKinematic = false;
        }
    }

}
