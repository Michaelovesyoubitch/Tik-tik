using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Animation))]
public class ControllerEnemy : MonoBehaviour
{

    //��������� ����� �������. ��������� ���� ��.

    public float health;
    private float maxHealth;
    private Rigidbody rb;
    [SerializeField]
    private GameObject[] partsOfBody;
    private Animation anim;
    public RectTransform healthBar;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        maxHealth = health;
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
        if (GetComponent<ControllerEnemy>().enabled == false)
            return;
        if (health != 0)
            health -= damage;

        //������ ������ ���� � ����������� �� ������ ��������.
        healthBar.localScale = new Vector3(health / maxHealth, 1, 1);
    }

    //������� �������� ������ � ��������� ��, ��� ���������� "�������" ��������.
    private void RagdollOn()
    {
        rb.constraints = RigidbodyConstraints.None;
        anim.Stop();
        for (int i = 0; i < partsOfBody.Length; i++)
        {
            if (partsOfBody[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                rigidbody.isKinematic = false;
        }
    }

}
