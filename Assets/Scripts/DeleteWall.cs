using UnityEngine;

public class DeleteWall : MonoBehaviour
{
    private void Update()
    {
        if (GetComponentInParent<PointWalking>().isActive)
        {
            gameObject.SetActive(false);
        }
    }
}
