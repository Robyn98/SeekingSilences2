using UnityEngine;

namespace TimsFolder
{
    public abstract class InteractableBehaviour : MonoBehaviour
    {
        [SerializeField] protected AudioClip interactSound;

        public abstract void Interact(PlayerMove playerMove);
    }
}