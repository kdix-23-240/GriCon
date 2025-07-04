using UnityEngine;
using UniRx;

public class HandleInSideWarning : MonoBehaviour
{
    private readonly ReactiveProperty<bool> isHit = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsHit
    {
        get { return isHit; }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "collisionWarning")
        {
            return;
        }
        //Debug.Log("[InSide] Enter");
        // 衝突したオブジェクトの名前をログに表示
        if (other.gameObject.tag == "Stick")
        {
            isHit.Value = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "collisionWarning")
        {
            return;
        }
        //Debug.Log("[InSide1] Exit");
        // 衝突したオブジェクトの名前をログに表示
        if (other.gameObject.tag == "Stick")
        {
            isHit.Value = false;
        }
    }
}