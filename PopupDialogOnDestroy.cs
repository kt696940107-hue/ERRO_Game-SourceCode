using UnityEngine;
using UnityEngine.UI;

public class PopupDialogOnDestroy : MonoBehaviour
{
    public string dialogText;
    public Canvas popupCanvas;
    public Text dialogTextComponent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogTextComponent.text = dialogText;
            popupCanvas.enabled = true;
        }
    }
}
