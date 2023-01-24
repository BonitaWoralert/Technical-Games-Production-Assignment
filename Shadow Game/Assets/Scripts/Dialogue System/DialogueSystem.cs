using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    private TextMeshProUGUI _textComponent;

    [SerializeField] Dialogue _dialogue;

    private GameObject _player;

    private int _index;

    private void Awake()
    {
        _textComponent = GetComponentInChildren<TextMeshProUGUI>();
        _player = GameObject.Find("Player");
    }

    private void Start()
    {
        _textComponent.color = _dialogue._color[_index];
        _textComponent.text = _dialogue._text[_index];
    }

    private void OnEnable()
    {
        _index = 0;
        _textComponent.color = _dialogue._color[_index];
        _textComponent.text = _dialogue._text[_index];
        _player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        _player.GetComponent<Movement>().enabled = false;
        _player.GetComponent<ShadowForm>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (_index < _dialogue._text.Length-1)
            {
                _index++;
                _textComponent.color = _dialogue._color[_index];
                _textComponent.text = _dialogue._text[_index];
            }
            else
            {
                _player.GetComponent<Movement>().enabled = true;
                _player.GetComponent<ShadowForm>().enabled = true;
                gameObject.SetActive(false);
            }
        }
    }
}
