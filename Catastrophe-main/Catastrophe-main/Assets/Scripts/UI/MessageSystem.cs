using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem instance;

    private void Awake()
    {
        instance = this;
    }
    [SerializeField] GameObject damageMessage;

    List<TMPro.TextMeshPro> messagePool;

    [SerializeField] int objectCount;
    int count;
    private void Start()
    {
        messagePool = new List<TMPro.TextMeshPro>();
        for (int i = 0; i < objectCount; i++)
        {
            Populate();
        }
    }
    private void Populate()
    {
        GameObject go = Instantiate(damageMessage, transform);
        go.SetActive(false);
        messagePool.Add(go.GetComponent<TMPro.TextMeshPro>());
    }


    public void PostMessage(string text, Vector3 worldPosition)
    {
        if (messagePool == null || messagePool.Count == 0)
        {
            // Debug.LogWarning("Message pool is not initialized or empty.");
            return;
        }

        messagePool[count].transform.position = worldPosition;
        messagePool[count].text = text;
        messagePool[count].gameObject.SetActive(true);
        count = (count + 1) % objectCount;
    }
}