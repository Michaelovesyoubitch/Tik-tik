using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PointWalking : MonoBehaviour
{
    //МЕХАНИКА ТОЧЕК (ОСОБЕННО ТОЧЕК БЕЗ ВРАГОВ (СТАРТА, ФИНИША, ПРЕДФИНИША). СМЕНА КАМЕР НА ТОЧКАХ.


    //Скрипт разблокировки точек-перевалов. Нужно поместить на каждую точку и дать ссылку на следующую.
    //Изначально следующие точки заблокированы (по локальной переменной isActive), путь к ним возможен только после разблокировки.
    //Точка-перевал разблокируется, когда её триггер не будет соприкасаться с объектами с тегом "Enemy".


    public GameObject nextPoint, player, pastPointLink;

    //Поставьте галочку на старт, если точка-перевал первая (неожиданно, но об этом несколько ниже) и галочку на финиш, если точка - последняя на пути (чтобы не закладывался новый путь).
    //На предпоследнюю тоже не забудьте.
    public bool start, finish, almostFinish, needCanvas;
    private bool isFirst = true, nextActive;
    [NonSerialized] public bool pastPoint, isActive;
    private Animator anim;
    private GameObject canvas;

    private void Start()
    {
        anim = player.GetComponent<Animator>();
        canvas = transform.GetChild(1).gameObject;

    }

    private void Update()
    {
        nextActive = nextPoint.GetComponent<PointWalking>().isActive;
        //Игрок идёт дальше, если следующая точка "активна", не применяется к старту и финишу.
        //ЕСЛИ ТОЧКА КОНЕЧНАЯ, ВСЁ РАВНО ВСТАВЬТЕ В ЗНАЧЕНИЕ "NEXTPOINT" САМУ ФИНАЛЬНУЮ ТОЧКУ.

        if (nextActive && !finish && !start)
        {
            player.GetComponent<NavMeshAgent>().SetDestination(nextPoint.transform.position);
        }

        //Если точка стартовая, движение по ней происходит не автоматически, а после тапа.

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (start && touch.phase == TouchPhase.Began && isFirst)
            {
                isActive = false;
                isFirst = false;
                start = false;
                player.GetComponent<NavMeshAgent>().SetDestination(nextPoint.transform.position);
            }
        }
    }

    //В игре для лучшего обзора работает переключение камер. Если на следующей точке есть враги, поставьте флажок "Need Canvas".

    //Если камера перевала не нужна, если на следующей точке нет врагов, выключите Канвас точки и отключите компонент Camera.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isMoving", false);
            isActive = false;
            pastPoint = true;
            if (needCanvas)
            {
                transform.GetChild(0).tag = "MainCamera";
                GetComponentInChildren<Camera>().enabled = true;
                canvas.SetActive(true);
                canvas.transform.GetChild(0).gameObject.SetActive(true);
                player.GetComponentInChildren<Camera>().enabled = false;
            }

            if (almostFinish)
            {
                nextPoint.transform.GetComponent<PointWalking>().isActive = true;
            }

            if (finish)
                SceneManager.LoadScene(0);

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            anim.SetBool("isMoving", true);
            if (needCanvas)
            {
                player.GetComponentInChildren<Camera>().enabled = true;
                transform.GetChild(0).tag = "Untagged";
                GetComponentInChildren<Camera>().enabled = false;
                canvas.SetActive(false);
                canvas.transform.GetChild(0).gameObject.SetActive(false);

            }
        }
    }

}
