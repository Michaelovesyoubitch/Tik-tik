using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    //УСЛОВИЯ АКТИВАЦИИ ТОЧКИ, НА КОТОРОЙ ЕСТЬ ВРАГИ.

    //Скрипт подсчитывает общее здоровье на точке. Когда сумма здоровья у врагов на точке равна нулю, точка активируется.

    //Точка зависит от противников, прикреплённых к ней. Нужно перенести их в массив.

    public GameObject[] enemy;
    public float countOfHealth;


    private void Start()
    {
        TakeDamage();
    }

    //Подсчёт здоровья "точки".

    public void TakeDamage()
    {

        if (enemy != null)
        {
            float summ = 0;
            foreach (var obj in enemy)
            {
                summ += obj.GetComponent<ControllerEnemy>().health;
            }
            countOfHealth = summ;
            summ = 0;
        }
    }

    private void Update()
    {

        //Подсчёт проводится постоянно.
        TakeDamage();
        //Если точка - финиш, автоматически подбегаем к ней, когда стоим на предфинише.
        if (GetComponent<PointWalking>().finish)
            return;
        //Если нет, нужно довести здоровье следующей точки до нуля. Она станет активна, когда здоровье будет равно нулю.
        if (countOfHealth <= 0 && !GetComponent<PointWalking>().finish && !GetComponent<PointWalking>().pastPoint)
        {
            GetComponent<PointWalking>().isActive = true;
        }
        else
            GetComponent<PointWalking>().isActive = false;
    }
}
