using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComponent;

    [SerializeField] private Dialogue _dialogue;

    private GameObject _player;

    private int _index;
    private bool _open;

    private void Awake()
    {
        _player = GameObject.Find("Player");
    }

    private void Start()
    {
        if (_dialogue._color.Length > 0)
        {
            _textComponent.color = _dialogue._color[_index];
        }
        else
        {
            _textComponent.color = Color.black;
        }
        _textComponent.text = _dialogue._text[_index];
    }

    private void OnEnable()
    {
        _index = 0;
        _open = true;
        if (_dialogue._color.Length > 0)
        {
            _textComponent.color = _dialogue._color[_index];
        }
        else
        {
            _textComponent.color = Color.black;
        }
        _textComponent.text = _dialogue._text[_index];
        _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        _player.GetComponent<Movement>().enabled = false;
        _player.GetComponent<ShadowForm>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (!_open)
            {
                if (_index < _dialogue._text.Length - 1)
                {
                    _index++;
                    if (_dialogue._color.Length > _index)
                    {
                        _textComponent.color = _dialogue._color[_index];
                    }
                    else
                    {
                        _textComponent.color= Color.black;
                    }
                    _textComponent.text = _dialogue._text[_index];
                }
            }
            else if (_open)
            {
                _player.GetComponent<Movement>().enabled = true;
                _player.GetComponent<ShadowForm>().enabled = true;
                gameObject.SetActive(false);
                _open = false;
            }
        }
    }
}
