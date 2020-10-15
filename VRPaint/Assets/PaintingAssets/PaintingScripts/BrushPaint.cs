namespace VRTK.Examples
{
    using System;
    using System.Collections.Specialized;
    using System.Runtime.Remoting.Messaging;
    using UnityEngine;

    public class BrushPaint : MonoBehaviour
    {
        public VRTK_InteractableObject linkedObject;
        public Transform paintSpawnPoint;
        public LineRenderer linePrefab;
        private LineRenderer line;
        public float lineSize;
        private int numPoints = 0;

        protected bool painting;

        protected virtual void OnEnable()
        {
            painting = false;
            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
                linkedObject.InteractableObjectUnused += InteractableObjectUnused;
            }
        }

        protected virtual void OnDisable()
        {
            painting = false;
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            }
        }

        public void Update()
        {
            if (painting) PaintDot();
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            painting = true;
            line = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            numPoints = 0;
            UnityEngine.Debug.Log("VVV USED VVV");
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {
            painting = false;
            UnityEngine.Debug.Log("xxx UNUSED! xxx");
        }

        protected virtual void PaintDot()
        {
            line.SetVertexCount(numPoints + 1);
            line.SetPosition(numPoints, paintSpawnPoint.position);
            numPoints++;
        }
    }
}