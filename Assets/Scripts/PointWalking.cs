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
        //����� ��� ������, ���� ��������� ����� "�������", �� ����������� � ������ � ������.
        //���� ����� ��������, �Ѩ ����� �������� � �������� "NEXTPOINT" ���� ��������� �����.

        if (nextActive && !finish && !start)
        {
            player.GetComponent<NavMeshAgent>().SetDestination(nextPoint.transform.position);
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
                player.GetComponent<NavMeshAgent>().SetDestination(nextPoint.transform.position);
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
