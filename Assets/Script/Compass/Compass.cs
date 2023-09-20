using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    [SerializeField] private GameObject _markerPrefab;
    [SerializeField] private List<Marker> _Markers;
    [SerializeField] private RawImage _compassImage;
    [SerializeField] private Transform _player;
    [SerializeField] private Marker _newMarker;

    private float compassUnit;
    [SerializeField]private float offset;

    private void Start()
    {
        compassUnit = _compassImage.rectTransform.rect.width / 360f;
        AddMarker(_newMarker);
    }

    private void Update()
    {
        _compassImage.uvRect = new Rect(_player.localEulerAngles.y / 360f,0f,1f,1f);

        foreach(Marker marker in _Markers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
        }
    }

    public void AddMarker(Marker _markerElement)
    {
        GameObject newMarker = Instantiate(_markerPrefab, _compassImage.transform);
        _markerElement.image = newMarker.gameObject.GetComponent<Image>();
        _markerElement.image.sprite = _markerElement.icon;
        _markerElement.position.x = _markerElement.transform.position.x;
        _markerElement.position.y = _markerElement.transform.position.z;
        _Markers.Add(_markerElement);
    }

    private Vector2 GetPosOnCompass(Marker _markerElement)
    {
        Vector2 playerPos = new Vector2(_player.transform.position.x, _player.transform.position.z);
        Vector2 playerFwd = new Vector2(_player.transform.forward.x, _player.transform.forward.z);

        float angle = Vector2.SignedAngle(_markerElement.position - playerPos, playerFwd);

        return new Vector2(compassUnit*angle+offset,-45f);
    }
}