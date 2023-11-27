using UnityEngine;

public class DieOnHitForColliders : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("hit");
        if (other.gameObject.TryGetComponent(out BulletA bala))
        {
            MainParentAnimator.enabled = false;
            Destroy(bala.gameObject);
        }
    }

    public Animator MainParentAnimator { get; set; }
}