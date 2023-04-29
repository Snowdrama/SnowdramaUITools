using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snowdrama.UI
{
    /// <summary>
    /// This class fits all the children into horizontal columns based on some size.
    /// </summary>
    [ExecuteInEditMode]
    public class UIHorizontalGroup : MonoBehaviour
    {
        [Header("Gap")]
        public float gapX = 0.1f;
        public float gapY = 0.1f;
        [Header("Active Items")]
        public bool useActive;
        [Header("Force Update")]
        public bool forceUpdate = false;
        public List<RectTransform> children = new List<RectTransform>();

        [Range(1, 100)]
        public int rowCount = 1;
        public float columnHeight = 40;

        [Header("Debug")]
        public int columnCount = 0;
        public float percentWidth;
        public float percentHeight;

        void Update()
        {
            if (transform.childCount != children.Count || forceUpdate)
            {
                CollectChildren();
                if (children.Count > 0)
                {
                    int index = 0;
                    for (int i = 0; i < children.Count; i++)
                    {
                        for (int x = 0; x < columnCount; x++)
                        {
                            for (int y = 0; y < rowCount; y++)
                            {
                                if (index < children.Count)
                                {
                                    var child = children[index];
                                    var minX = x * percentWidth;
                                    var maxX = x * percentWidth + percentWidth;

                                    var maxY = 1.0f - (y * percentHeight);
                                    var minY = 1.0f - ((y + 1) * percentHeight);

                                    child.anchorMin = new Vector2(minX, minY);
                                    child.anchorMax = new Vector2(maxX, maxY);

                                    child.offsetMin = new Vector2(0, 0);
                                    child.offsetMax = new Vector2(0, 0);
                                }
                                ++index;
                            }
                        }
                    }
                }
            }
        }

        private void CalculateValues()
        {
            columnCount = Mathf.CeilToInt((float)children.Count / (float)rowCount);
            percentWidth = 1.0f / (float)columnCount;
            percentHeight = 1.0f / (float)rowCount;
        }

        private void CollectChildren()
        {
            children.Clear();
            foreach (Transform child in transform)
            {
                if (useActive)
                {
                    if (child.gameObject.activeSelf)
                    {
                        children.Add(child.GetComponent<RectTransform>());
                    }
                }
                else
                {
                    children.Add(child.GetComponent<RectTransform>());
                }
            }
        }

    }
}