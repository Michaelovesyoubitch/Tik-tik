using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Animation))]
public class ControllerEnemy : MonoBehaviour
{

    //ПОЛУЧЕНИЕ УРОНА ВРАГАМИ. ИЗМЕНЕНИЯ БАРА ХП.

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

    //Не даст опуститься здоровью ниже нуля.
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
        //При выключенном контроллере наносить урон не получится. Нужно, чтобы враги не умирали, если игрок не стоит на нужной платформе.
        if (GetComponent<ControllerEnemy>().enabled == false)
            return;
        if (health != 0)
            health -= damage;

        //Меняем размер бара в зависимости от уровня здоровья.
        healthBar.localScale = new Vector3(health / maxHealth, 1, 1);
    }

    //Функция включает рэгдол и выключает всё, что заставляет "куколку" дёргаться.
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
