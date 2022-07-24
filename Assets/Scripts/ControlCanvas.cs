using UnityEngine;

public class ControlCanvas : MonoBehaviour
{
    //�������������� �������.

    private RaycastHit hit;
    private LayerMask mask = 1 << 5;
    private Transform targetPlace;
    [SerializeField] private GameObject gun;

    //�� ��������, ��� �� ���� � ������� �� Attack. ��. ��� ����� ��� ������������� ����.

    private void Start()
    {
        targetPlace = transform.GetChild(0);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, Mathf.Infinity, mask))

                {
                    targetPlace.transform.position = hit.point;
                }
            }

        }
    }
}
