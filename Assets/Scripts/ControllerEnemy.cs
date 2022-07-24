using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Animation))]
[RequireComponent(typeof(ControllerEnemy))]
public class ControllerEnemy : MonoBehaviour
{

    //��������� ����� �������. ��������� ���� ��.

    public float health;
    private float maxHealth;
    private Rigidbody rb;
    [SerializeField]
    private GameObject[] partsOfBody;
    private Animation _animation;
    public RectTransform healthBar;
    private ControllerEnemy controllerEnemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animation = GetComponent<Animation>();
        maxHealth = health;
        controllerEnemy = GetComponent<ControllerEnemy>();
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
        if (controllerEnemy.enabled == false)
            return;
        if (health != 0)
            health -= damage;

        //������ ������ ���� � ����������� �� ������ ��������.
        healthBar.localScale = new Vector3(health / maxHealth, healthBar.anchorMax.y, 1);
    }

    //������� �������� ������ � ��������� ��, ��� ���������� "�������" ��������.
    private void RagdollOn()
    {
        rb.constraints = RigidbodyConstraints.None;
        _animation.Stop();
        for (int i = 0; i < partsOfBody.Length; i++)
        {
            if (partsOfBody[i].TryGetComponent(out Rigidbody rigidbody))
                rigidbody.isKinematic = false;
        }
    }

}
