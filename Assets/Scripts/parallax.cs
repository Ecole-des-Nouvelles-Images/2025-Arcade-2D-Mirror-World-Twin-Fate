using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Parallax Settings")]
    [SerializeField] private GameObject _parallaxObject;
    [SerializeField] private float speedParallax;
    
    private Vector2 parallax;

    void Update()
    {
        _parallaxObject.transform.position = parallax;
        float YMove = Time.deltaTime * speedParallax;

        parallax -= new Vector2(0, YMove);
    }
}