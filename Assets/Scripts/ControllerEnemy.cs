using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Animation))]
[RequireComponent(typeof(ControllerEnemy))]
public class ControllerEnemy : MonoBehaviour
{

    //ПОЛУЧЕНИЕ УРОНА ВРАГАМИ. ИЗМЕНЕНИЯ БАРА ХП.

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
        if (controllerEnemy.enabled == false)
            return;
        if (health != 0)
            health -= damage;

        //Меняем размер бара в зависимости от уровня здоровья.
        healthBar.localScale = new Vector3(health / maxHealth, healthBar.anchorMax.y, 1);
    }

    //Функция включает рэгдол и выключает всё, что заставляет "куколку" дёргаться.
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
