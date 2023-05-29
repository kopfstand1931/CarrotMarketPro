using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayer : NetworkBehaviour
{
    public TextMeshProUGUI playerNickNameTM;
    public ChatManager chatManager; // ChatManager ��ũ��Ʈ ����

    private NetworkCharacterControllerPrototype _cc;
    private Vector3 _forward;

    public static NetworkPlayer Local { get; set; }

    [Networked(OnChanged = nameof(OnNickNameChanged))]
    public NetworkString<_16> nickName { get; set; }

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        _forward = transform.forward;

        // ChatManager ������Ʈ�� ã�Ƽ� �Ҵ�
        chatManager = FindObjectOfType<ChatManager>();
    }

    private void Update()
    {
        if (Object.HasInputAuthority && Input.GetKeyDown(KeyCode.Return))
        {
            chatManager.SendChatMessage(); // ChatManager�� ���� ä�� �޽��� ����
        }
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            RPC_SetNickName(GameDataManager.Instance.getLoggedInUserCode());
            Debug.Log("Spawned Local Player");
        }
        else
        {
            Debug.Log("Spawned Remote Player");
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_SendMessage(string message, RpcInfo info = default)
    {
        string formattedMessage = string.Format("{0}: {1}", nickName, message);
        chatManager.ReceiveChatMessage(formattedMessage); // ChatManager�� ���� ä�� �޽��� ����
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


/*
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayer : NetworkBehaviour
{
    public TextMeshProUGUI playerNickNameTM;
    public ChatManager chatManager; // ChatManager ��ũ��Ʈ ����

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
        if (Object.HasInputAuthority && Input.GetKeyDown(KeyCode.Return))
        {
            chatManager.SendChatMessage(); // ChatManager�� ���� ä�� �޽��� ����
        }
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            RPC_SetNickName(GameDataManager.Instance.getLoggedInUserCode());
            Debug.Log("Spawned Local Player");
        }
        else
        {
            Debug.Log("Spawned Remote Player");
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_SendMessage(string message, RpcInfo info = default)
    {
        string formattedMessage = string.Format("{0}: {1}", nickName, message);
        chatManager.ReceiveChatMessage(formattedMessage); // ChatManager�� ���� ä�� �޽��� ����
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
*/

/*
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

            // RPC_SetNickName(PlayerPrefs.GetString("PlayerNickname"));
            RPC_SetNickName(GameDataManager.Instance.getLoggedInUserName());

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
*/