using UnityEngine;

namespace UnityUtility
{

#if UNITY_2017_1_OR_NEWER

    public class Rotate : MonoBehaviour
    {
        public Vector3 Axis = Vector3.up;
        public bool Local;
        public float Speed;

        void Update()
        {
            if (Local)
                transform.Rotate(Axis, Time.deltaTime * Speed);
            else
                transform.RotateAround(transform.position, Axis, Time.deltaTime * Speed);
        }
    }

#endif

}