using UnityEngine;

public class TeleportObject : MonoBehaviour
{
    public KeyCode triggerKey = KeyCode.E;

    public Transform senderObject;
    public Transform targetObject;

    private bool canTeleport = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = false;
        }
    }

    private void Update()
    {
        if (canTeleport && Input.GetKeyDown(triggerKey))
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        if (senderObject != null && targetObject != null)
        {
            Vector3 targetPosition = targetObject.position;
            senderObject.position = targetPosition;
        }
    }
}
