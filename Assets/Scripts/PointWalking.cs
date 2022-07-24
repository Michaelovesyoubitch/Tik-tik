using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PointWalking : MonoBehaviour
{
    //�������� ����� (�������� ����� ��� ������ (������, ������, ����������). ����� ����� �� ������.


    //������ ������������� �����-���������. ����� ��������� �� ������ ����� � ���� ������ �� ���������.
    //���������� ��������� ����� ������������� (�� ��������� ���������� isActive), ���� � ��� �������� ������ ����� �������������.
    //�����-������� ��������������, ����� � ������� �� ����� ������������� � ��������� � ����� "Enemy".


    public GameObject nextPoint, player, pastPointLink;

    //��������� ������� �� �����, ���� �����-������� ������ (����������, �� �� ���� ��������� ����) � ������� �� �����, ���� ����� - ��������� �� ���� (����� �� ������������ ����� ����).
    //�� ������������� ���� �� ��������.
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
        //����� ��� ������, ���� ��������� ����� "�������", �� ����������� � ������ � ������.
        //���� ����� ��������, �Ѩ ����� �������� � �������� "NEXTPOINT" ���� ��������� �����.

        if (!finish && !start)
        {
            if (_nextPointWalking.isActive)
            _playerNavMesh.SetDestination(nextPoint.transform.position);
        }

        //���� ����� ���������, �������� �� ��� ���������� �� �������������, � ����� ����.

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

    //� ���� ��� ������� ������ �������� ������������ �����. ���� �� ��������� ����� ���� �����, ��������� ������ "Need Canvas".

    //���� ������ �������� �� �����, ���� �� ��������� ����� ��� ������, ��������� ������ ����� � ��������� ��������� Camera.

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
