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
    private Animator anim;
    private GameObject canvas;
    private Transform _camera;
    private NavMeshAgent playerNavMesh;
    private Camera cameraComponent, playerCamera;
    private PointWalking nextPointWalking;
    

    private void Start()
    {
        anim = player.GetComponent<Animator>();
        canvas = transform.GetChild(1).gameObject;
        playerNavMesh = player.GetComponent<NavMeshAgent>();
        playerCamera = player.GetComponentInChildren<Camera>();
        cameraComponent = GetComponentInChildren<Camera>();
        _camera = transform.GetChild(0);
        nextPointWalking = nextPoint.GetComponent<PointWalking>();



    }

    private void Update()
    {
        //����� ��� ������, ���� ��������� ����� "�������", �� ����������� � ������ � ������.
        //���� ����� ��������, �Ѩ ����� �������� � �������� "NEXTPOINT" ���� ��������� �����.

        if (!finish && !start)
        {
            if (nextPoint.GetComponent<PointWalking>().isActive)
            playerNavMesh.SetDestination(nextPoint.transform.position);
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
                playerNavMesh.SetDestination(nextPoint.transform.position);
            }
        }
    }

    //� ���� ��� ������� ������ �������� ������������ �����. ���� �� ��������� ����� ���� �����, ��������� ������ "Need Canvas".

    //���� ������ �������� �� �����, ���� �� ��������� ����� ��� ������, ��������� ������ ����� � ��������� ��������� Camera.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isMoving", false);
            isActive = false;
            pastPoint = true;
            if (needCanvas)
            {
                _camera.tag = "MainCamera";
                cameraComponent.enabled = true;
                canvas.SetActive(true);
                canvas.transform.GetChild(0).gameObject.SetActive(true);
                playerCamera.enabled = false;
            }

            if (almostFinish)
            {
                nextPointWalking.isActive = true;
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
                playerCamera.enabled = true;
                _camera.tag = "Untagged";
                cameraComponent.enabled = false;
                canvas.SetActive(false);
                canvas.transform.GetChild(0).gameObject.SetActive(false);

            }
        }
    }

}
