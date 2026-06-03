using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PasswordTrigger : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public GameObject inputPanel;
    public InputField inputField;
    public string correctPassword = "1234";  // 正确的密码

    private bool isPlayerInRange;
    private bool isInputActive;
    private bool isInputComplete;

    private void Start()
    {
        dialogBox.SetActive(false);
        inputPanel.SetActive(false);
        isInputActive = false;
        isInputComplete = false;
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isInputActive)
            {
                // 关闭输入面板
                DeactivateInputField();
            }
            else
            {
                // 显示输入面板
                ActivateInputField();
            }
        }

        if (isInputActive && isInputComplete && Input.GetKeyDown(KeyCode.Return))
        {
            // 验证密码
            CheckPassword();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogBox.SetActive(false);
            DeactivateInputField();
        }
    }

    private void ActivateInputField()
    {
        inputPanel.SetActive(true);
        inputField.text = "";
        inputField.Select();
        inputField.ActivateInputField();
        isInputActive = true;
        isInputComplete = false;

        // 禁用事件系统的选中对象，以允许玩家输入密码
        EventSystem.current.SetSelectedGameObject(null);

        // 启用 StandaloneInputModule，使输入模块处理 UI 事件
        StandaloneInputModule inputModule = EventSystem.current.gameObject.GetComponent<StandaloneInputModule>();
        inputModule.ActivateModule();
    }

    private void DeactivateInputField()
    {
        inputPanel.SetActive(false);
        isInputActive = false;

        // 禁用 StandaloneInputModule，停止输入模块处理 UI 事件
        StandaloneInputModule inputModule = EventSystem.current.gameObject.GetComponent<StandaloneInputModule>();
        inputModule.DeactivateModule();
    }

    private void CheckPassword()
    {
        string input = inputField.text;
        if (input == correctPassword)
        {
            Debug.Log("密码正确");
            // 密码正确的处理逻辑
        }
        else
        {
            Debug.Log("密码错误");
            // 密码错误的处理逻辑
        }

        DeactivateInputField();
    }
}
