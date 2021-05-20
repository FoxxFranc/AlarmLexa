using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterRoom : MonoBehaviour
{
    [SerializeField] private UnityEvent _entered, _leaved;

    public event UnityAction Entered
    {
        add => _entered?.AddListener(value);
        remove => _entered?.RemoveListener(value);
    }

    public event UnityAction Leaved
    {
        add => _leaved?.AddListener(value);
        remove => _leaved?.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _entered?.Invoke();
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _leaved?.Invoke();
        }
    }
}
