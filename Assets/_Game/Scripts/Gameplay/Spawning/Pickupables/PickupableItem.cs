using System.Collections;
using TMPro;
using UnityEngine;

public class PickupableItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private ParticleSystem _pickupEffect;
    [SerializeField] private GameObject _visual;

    private bool _pickedUp;

    private IPickupable _pickupableStrategy;
    public IPickupable PickupableStrategy => _pickupableStrategy;

    private void Start()
    {
        Destroy(gameObject, 20f);
    }

    private void OnDisable()
    {
        _pickupableStrategy.OnMessageShown -= OnMessageShown;
    }

    public void SetPickupable(IPickupable pickupable)
    {
        _pickupableStrategy = pickupable;
        _pickupableStrategy.OnMessageShown += OnMessageShown;
    }

    private void OnMessageShown(string message)
    {
        StartCoroutine(ShowText(message));
    }

    private IEnumerator ShowText(string message)
    {
        if (_pickedUp)
        {
            yield break;
        }

        _text.gameObject.SetActive(true);
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
        _text.text = message;
        yield return new WaitForSeconds(1f);
        while (_text.color.a > 0)
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - 0.005f);
            yield return null;
        }
        _text.gameObject.SetActive(false);
    }

    public void Collect(Player player)
    {
        if (_pickedUp)
        {
            return;
        }

        if (_pickupableStrategy != null)
        {
            bool canPickUp = _pickupableStrategy.CanPickUp(player);
            _pickupableStrategy.ApplyEffect(player);

            if (canPickUp && _pickupEffect != null)
            {
                _pickedUp = true;
                Instantiate(_pickupEffect, transform.position, Quaternion.identity);
            }

            _visual.SetActive(false);
            Destroy(gameObject, 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.CurrentPickupable = this;
            Collect(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.CurrentPickupable = null;
            Collect(player);
        }
    }
}
