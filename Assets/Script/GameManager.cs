using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IMessageHandler
{

    float _playerScore = 0;
    float _botScore = 0;

    MessageBus _bus;

    [SerializeReference]
    Transform _playerSpawn, _botSpawn;

    [SerializeReference]
    GameObject _PlayerObj, _BotObj;
    GameObject _myPalayer, _myBot;

    bool _isPause = false;
    void Start()
    {
        _bus = MessageBus.GetBus();
        _bus.Subscribe(this);
        _myPalayer = GameObject.Instantiate(_BotObj, _botSpawn);
        _myBot = GameObject.Instantiate(_PlayerObj, _playerSpawn);
    }
    void Update()
    {

    }
    void OnDeadEnemy(GameObject gameObject)
    {
        if (gameObject.tag == "Player")
        {
            _botScore++;
        }
        if (gameObject.tag == "Bot")
        {
            _playerScore++;
        }
        _bus.SendMessage(new MessageModel($"{_playerScore}:{_botScore}", MessageType.ChangeScore));
        NewRaund();

    }

    void Restart()
    {
        SceneManager.LoadScene("PlayScene");
    }
    void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void NewRaund()
    {

        Destroy(_myPalayer);
        Destroy(_myBot);
        var ammo = GameObject.FindGameObjectsWithTag("Ammo");
        foreach (var item in ammo)
        {
            Destroy(item);
        }

        _myPalayer = GameObject.Instantiate(_BotObj, _botSpawn);
        _myBot = GameObject.Instantiate(_PlayerObj, _playerSpawn);

    }
    void Pause()
    {
        _isPause = !_isPause;
        if (_isPause)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;

    }
    public void Handle(MessageModel messege)
    {
        switch (messege.Type)
        {
            case MessageType.OnDead: { OnDeadEnemy(messege.Parametr as GameObject); break; }
            case MessageType.RestartGame: { Restart(); break; }
            case MessageType.Pause: { Pause(); break; }
            case MessageType.MainMenu: { BackToMenu(); break; }
            default: break;
        }
        return;
    }
}
