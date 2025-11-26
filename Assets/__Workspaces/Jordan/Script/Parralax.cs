using UnityEngine;

namespace __Workspaces.Jordan.Script
{
    public  class  Parallax : MonoBehaviour
    {
        [SerializeField] private GameObject parallaxObject;
        private Vector2 _parallax;
        [SerializeField] private float speedParallax;

        void  Update ()
        {
            parallaxObject.transform.position = _parallax;
            float yMove = Time.deltaTime * speedParallax;
            
            _parallax -= new Vector2( 0, yMove);
        } 
    }
}