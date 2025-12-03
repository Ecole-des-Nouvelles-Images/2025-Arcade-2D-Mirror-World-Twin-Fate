using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject _parallaxObject;
    private Vector2 parallax;
    [SerializeField] private float speedParallax;

    void Update()
    {
        _parallaxObject.transform.position = parallax;
        float YMove = Time.deltaTime * speedParallax;

        parallax -= new Vector2(0, YMove);
    }
}