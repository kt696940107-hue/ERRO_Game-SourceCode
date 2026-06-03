using System.Collections;
using UnityEngine;
using UnityEngine.UI; // 导入 Unity UI 的命名空间

public class PaymentScript : MonoBehaviour
{
    public int itemPrice = 10; // 物品價格
    public KeyCode paymentKey = KeyCode.E; // 觸發支付的按鍵
    public GameObject paymentDialog; // 顯示支付對話框的物件
    public AudioClip paymentSound; // 支付時的音效
    public Text paymentMessageText; // 用于显示支付消息的 Text 组件
    public string paymentSuccessMessage = "支付成功！";
    public string paymentFailureMessage = "支付失敗，硬幣不足！";

    private bool canPay = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (paymentDialog != null)
        {
            paymentDialog.SetActive(false);
        }
    }

    void Update()
    {
        if (canPay && Input.GetKeyDown(paymentKey))
        {
            ShowPaymentDialog();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPay = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPay = false;
            if (paymentDialog != null)
            {
                paymentDialog.SetActive(false);
            }
        }
    }

    void ShowPaymentDialog()
    {
        if (paymentDialog != null)
        {
            paymentDialog.SetActive(true);

            // 播放支付音效
            PlayPaymentSound();

            // 在 UI 文本中顯示支付消息
            DisplayPaymentMessageNew(paymentSuccessMessage); // 假設默認顯示支付成功消息

            // TODO: 在這裡顯示支付對話框，例如 UI 中的確認和取消按鈕

            // 假設按下確認按鈕後，執行支付
            // 在實際遊戲中，可以使用更複雜的 UI 系統或外部支付系統
            StartCoroutine(PayForItem());
        }
    }

    IEnumerator PayForItem()
    {
        // 在這裡可以實現支付的邏輯，例如檢查玩家擁有的硬幣數量
        // 如果條件滿足，執行支付並刪除物品

        // 检查硬币是否足够
        if (CoinUI.CurrentCoinQuantity >= itemPrice)
        {
            yield return new WaitForSeconds(1f); // 模擬支付的等待時間

            // 假設支付成功
            PaySuccess();
        }
        else
        {
            // 假設支付失敗
            PayFailure();
        }
    }

    void PaySuccess()
    {
        // 在這裡實現支付成功後的邏輯
        DisplayPaymentMessageNew(paymentSuccessMessage);

        // 扣減硬幣數量
        // 在實際遊戲中，這裡應該根據物品價格扣減玩家的硬幣數量
        CoinUI.CurrentCoinQuantity -= itemPrice;

        // 刪除物品
        Destroy(gameObject);

        // 關閉支付對話框
        if (paymentDialog != null)
        {
            paymentDialog.SetActive(false);
        }
    }

    void PayFailure()
    {
        // 在這裡實現支付失敗後的邏輯
        DisplayPaymentMessageNew(paymentFailureMessage);

        // 關閉支付對話框
        if (paymentDialog != null)
        {
            paymentDialog.SetActive(false);
        }
    }

    void PlayPaymentSound()
    {
        // 播放支付音效
        if (audioSource != null && paymentSound != null)
        {
            audioSource.PlayOneShot(paymentSound);
        }
    }

    void DisplayPaymentMessageNew(string message)
    {
        // 在 UI 文本中顯示支付消息
        if (paymentMessageText != null)
        {
            paymentMessageText.text = message;
        }
    }
}
