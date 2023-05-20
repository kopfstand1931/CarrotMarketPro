using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayer : NetworkBehaviour
{
    public TextMeshProUGUI playerNickNameTM;
    [Networked] private TickTimer delay { get; set; }

    private Text _messages;

    private NetworkCharacterControllerPrototype _cc;

    private Vector3 _forward;
    
    public static NetworkPlayer Local { get; set; }

    [Networked(OnChanged = nameof(OnNickNameChanged))]
    public NetworkString<_16> nickName { get; set; }


    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        _forward = transform.forward;
    }


    private void Update()
    {
        if (Object.HasInputAuthority && Input.GetKeyDown(KeyCode.R))
        {
            RPC_SendMessage("Hey Mate!");  // RŰ�� ������ �޽��� ����.
        }
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority) 
        {
            Local = this;

            RPC_SetNickName(PlayerPrefs.GetString("PlayerNickname"));

            Debug.Log("Spawned Local Player");
        }
        else Debug.Log("Spawned Remote Player");
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]  // (�÷��̾ ���� �Է� ������ �ִ� Ŭ���̾�Ʈ�� �� �޼ҵ带 ȣ���ϵ��� ���, ��� Ŭ���̾�Ʈ���� ����.)
    public void RPC_SendMessage(string message, RpcInfo info = default)
    {
        if (_messages == null)
            _messages = FindObjectOfType<Text>();
        if (info.Source == Runner.Simulation.LocalPlayer)
            message = $"You said: {message}\n";
        else
            message = $"Some other player said: {message}\n";
        _messages.text = message;
    }


    static void OnNickNameChanged(Changed<NetworkPlayer> changed)
    {
        changed.Behaviour.OnNickNameChanged();
    }
    private void OnNickNameChanged()
    {
        playerNickNameTM.text = nickName.ToString();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        this.nickName = nickName;
    }
}