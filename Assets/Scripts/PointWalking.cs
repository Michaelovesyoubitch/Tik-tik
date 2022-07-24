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
    private bool isFirst = true;
    [NonSerialized] public bool pastPoint, isActive;
    private Animator _anim;
    private GameObject _canvas;
    private Transform _camera;
    private NavMeshAgent _playerNavMesh;
    private Camera _cameraComponent, _playerCamera;
    private PointWalking _nextPointWalking;
    

    private void Start()
    {
        _anim = player.GetComponent<Animator>();
        _canvas = transform.GetChild(1).gameObject;
        _playerNavMesh = player.GetComponent<NavMeshAgent>();
        _playerCamera = player.GetComponentInChildren<Camera>();
        _cameraComponent = GetComponentInChildren<Camera>();
        _camera = transform.GetChild(0);
        _nextPointWalking = nextPoint.GetComponent<PointWalking>();
    }

    private void Update()
    {
        //Игрок идёт дальше, если следующая точка "активна", не применяется к старту и финишу.
        //ЕСЛИ ТОЧКА КОНЕЧНАЯ, ВСЁ РАВНО ВСТАВЬТЕ В ЗНАЧЕНИЕ "NEXTPOINT" САМУ ФИНАЛЬНУЮ ТОЧКУ.

        if (!finish && !start)
        {
            if (_nextPointWalking.isActive)
            _playerNavMesh.SetDestination(nextPoint.transform.position);
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
                _playerNavMesh.SetDestination(nextPoint.transform.position);
            }
        }
    }

    //В игре для лучшего обзора работает переключение камер. Если на следующей точке есть враги, поставьте флажок "Need Canvas".

    //Если камера перевала не нужна, если на следующей точке нет врагов, выключите Канвас точки и отключите компонент Camera.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _anim.SetBool("isMoving", false);
            isActive = false;
            pastPoint = true;
            if (needCanvas)
            {
                _camera.tag = "MainCamera";
                _cameraComponent.enabled = true;
                _canvas.SetActive(true);
                _canvas.transform.GetChild(0).gameObject.SetActive(true);
                _playerCamera.enabled = false;
            }

            if (almostFinish)
            {
                _nextPointWalking.isActive = true;
            }

            if (finish)
                SceneManager.LoadScene(0);

        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            _anim.SetBool("isMoving", true);
            if (needCanvas)
            {
                _playerCamera.enabled = true;
                _camera.tag = "Untagged";
                _cameraComponent.enabled = false;
                _canvas.SetActive(false);
                _canvas.transform.GetChild(0).gameObject.SetActive(false);

            }
        }
    }

}
