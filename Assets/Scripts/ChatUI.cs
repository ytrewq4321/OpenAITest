using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI answer;
    [SerializeField] private RectTransform rect;
    [SerializeField] private GameObject inputField;
    [SerializeField] private Button enterButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private OpenAI ai;

    public static UnityEvent<string> TextUpdated = new();
    public static UnityEvent ChatClosed = new();
    public static UnityEvent ChatOpened = new();

    void Start()
    {
        enterButton.onClick.AddListener(() => ai.CreateData(inputField.GetComponent<TMP_InputField>().text));
        TextUpdated.AddListener(text=>OnUpdateField(text));

        ChatOpened.AddListener(OnChatOpen);

        closeButton.onClick.AddListener(OnChatClose);
    }

    public void OnUpdateField(string text)
    {
        answer.text += text;        
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y+600f);        
    }

    private void OnChatOpen()
    {
        gameObject.SetActive(true);
    }

    private void OnChatClose()
    {
        gameObject.SetActive(false);
        ChatClosed.Invoke();
    }
}
