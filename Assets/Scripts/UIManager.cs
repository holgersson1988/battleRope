using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class UIManager : MonoBehaviour {

    public Font font;
    public Text criticalMessagePrefab;
    private bool isShowingMessage;
    private List<Text> messageQueue;
    private List<float> messageQueueTimers;
    private float currentMessageTimer = 0.0f;
    private float criticalMessageDuration = 3.0f;//4
    private const int sizeStandard = 26;
    private float fadeTime = 0.2f;
    private bool isFading = false;

    IEnumerator fadeTransition;
    LevelManager levelManager;

    void Start() {
        messageQueue = new List<Text>();
        messageQueueTimers = new List<float>();
        isShowingMessage = false;

        levelManager = FindObjectOfType<LevelManager>();
        

        CreateAndQueueCriticalMessage("Splat Ropes!", levelManager.player1Color, sizeStandard);
        CreateAndQueueCriticalMessage("GO!", levelManager.player1Color, sizeStandard);
        //CreateAndQueueCriticalMessage("Find your blue home planet", levelManager.player1Color, sizeStandard);
    }

    // Update is called once per frame
    void Update() {

        transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.67f);
        if (!isShowingMessage && messageQueue.Count > 0)
        {
            isShowingMessage = true;

            fadeTransition = Fade(fadeTime, 1f, Vector3.one, true);
            StartCoroutine(fadeTransition);
        }

        if (isShowingMessage && !isFading)
        {
            currentMessageTimer -= Utility.DeltaTime();
            if (currentMessageTimer < 0.0f)
            {
                fadeTransition = Fade(fadeTime, 0f, Vector3.zero, false);
                StartCoroutine(fadeTransition);
            }
        }
    }

    public Text CreateCriticalMessage(string str, Color col, int size)
    {
        Text newMessage = Instantiate(criticalMessagePrefab, transform.position, Quaternion.identity) as Text;
        newMessage.transform.SetParent(transform, true);
        newMessage.text = str;
        newMessage.color = col;
        newMessage.fontSize = size;
        newMessage.enabled = false;
        newMessage.transform.localScale = Vector3.zero;
        return newMessage;
    }

    public void QueueCriticalMessage(Text criticalMessage)
    {
        messageQueue.Add(criticalMessage);
    }

    public void CreateAndQueueCriticalMessage(string str, Color col, int size = sizeStandard)
    {
        Text newMessage = CreateCriticalMessage(str, col, size);
        messageQueue.Add(newMessage);
    }

    private IEnumerator Fade(float time, float targetAlpha, Vector3 targetScale, bool fadingIn)
    {
        isFading = true;
        float percent = 0.0f;
        float speed = 1f / time;
        if (messageQueue[0] == null)
        {
            yield return 0;
        }
        isShowingMessage = true;
        Text currentMessage = messageQueue[0];
        currentMessage.enabled = true;
        float startAlpha = currentMessage.color.a;
        Vector2 startScale = currentMessage.transform.localScale;
        Color col = currentMessage.color;
        while (percent < 1f)
        {
            percent += Utility.DeltaTime() * speed;
            currentMessage.color = new Color(col.r, col.g, col.b, Mathf.Lerp(startAlpha, targetAlpha, percent));
            currentMessage.transform.localScale = Vector3.Lerp(startScale, targetScale, percent);
            yield return null;
        }

        isFading = false;
        if (fadingIn)
        {
            currentMessageTimer = criticalMessageDuration;
        }
        else // FadeOut
        {
            messageQueue.Remove(currentMessage);
            Destroy(currentMessage.gameObject);
            isShowingMessage = false;
        }
    }


    private void OnShipOutOfFuel()
    {
        CreateAndQueueCriticalMessage("OUT OF FUEL!!!", Color.red, 32);
    }
}
