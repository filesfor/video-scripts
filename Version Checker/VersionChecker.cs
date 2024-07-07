using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class VersionChecker : MonoBehaviour
{
    public string githubRawLink;
    public TextMeshPro displayText;
    public string expectedVersion;
    public string gameappversion;
    public TextMeshPro gameappverionsText;
    public GameObject[] objectsToDisable;
    public GameObject[] objectsToEnable;

    void Start()
    {
        LoadTextFromGitHub(githubRawLink);
    }

    void LoadTextFromGitHub(string link)
    {
        StartCoroutine(FetchFromGitHub(link));
    }

    IEnumerator FetchFromGitHub(string link)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(link))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                string fetchedText = www.downloadHandler.text;

                if (fetchedText.Trim().Equals(expectedVersion))
                {
                    displayText.text = fetchedText;
                }
                else
                {
                    DisableObjects();
                    EnableObjects();
                    gameappverionsText.text = gameappversion;
                    displayText.text = fetchedText;
                }
            }
        }
    }

    void DisableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }

    void EnableObjects()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }
}
