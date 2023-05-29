using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    NetworkRunner networkRunner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnExit()
    {
        NetworkRunner networkRunnerInScene = FindObjectOfType<NetworkRunner>();

        // �̹� ��Ʈ��ũ ���ʰ� �ִ� ��� ����
        if (networkRunnerInScene != null)
            networkRunner = networkRunnerInScene;

        networkRunner.Shutdown();

        SceneManager.LoadScene("SunjooScene");
    }
}
