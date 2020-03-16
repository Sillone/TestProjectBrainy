using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IMessageHandler
{
    MessageBus _bus;
    [SerializeField]
    Text _score;
    [SerializeField]
    Canvas _pauseMenu;
    bool _isPaused = false;
    void Start()
    {
        _bus = MessageBus.GetBus();
        _bus.Subscribe(this);
        _pauseMenu.enabled = false;
    }
    public void ButtonClick(string type)
    {
        switch (type)
        {
            case "RestartGame": { _bus.SendMessage(new MessageModel(null, MessageType.RestartGame)); break; }
            case "MainMenu": { _bus.SendMessage(new MessageModel(null, MessageType.MainMenu)); break; }
            default:
                break;
        }

    }
    public void OnPauseButtonClick(Button myBotton)
    {
        _isPaused = !_isPaused;
        if (_isPaused)
            myBotton.GetComponentInChildren<Text>().text = "PLAY";
        else
            myBotton.GetComponentInChildren<Text>().text = "PAUSE";
        _pauseMenu.enabled = _isPaused;
        _bus.SendMessage(new MessageModel(_isPaused, MessageType.Pause));
    }


    public void Handle(MessageModel model)
    {
        if (model.Type == MessageType.ChangeScore)
        {
            _score.text = model.Parametr as string;
        }
    }
}