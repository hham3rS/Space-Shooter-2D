using UnityEngine;

namespace SpaceShooter.Misc
{
    public class Explosion : MonoBehaviour
    {
        void Start()
        {
            Destroy(this.gameObject, 3.0f);
        }
    }
}
