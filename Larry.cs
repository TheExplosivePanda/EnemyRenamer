using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace EnemyRenamer
{
    class Larry : MonoBehaviour
    {
        dfLabel dfLabel = null;
        public static string[] namesDB = null;
        tk2dSprite sprite = null;

        void Start()
        {
            string stringForInt = "HI IM LARRY";
            if (namesDB != null)
            {
                stringForInt = namesDB[UnityEngine.Random.Range(0, namesDB.Length)];
            }
            sprite = this.gameObject.GetComponent<tk2dSprite>();
            GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(BraveResources.Load("DamagePopupLabel", ".prefab"), GameUIRoot.Instance.transform);

            Vector3 worldPosition = this.transform.position;
            if (UnityEngine.Random.value > 0.99f)
            {
                dfLabel.Glitchy = true;
            }
            dfLabel = gameObject.GetComponent<dfLabel>();
            dfLabel.gameObject.SetActive(true);
            dfLabel.Text = stringForInt;
            dfLabel.Color = Color.HSVToRGB(UnityEngine.Random.value, 1.0f, 1.0f);
            dfLabel.Opacity = 1f;
            dfLabel.transform.position = dfFollowObject.ConvertWorldSpaces(worldPosition, GameManager.Instance.MainCameraController.Camera, GameUIRoot.Instance.Manager.RenderCamera).WithZ(0f);
            dfLabel.transform.position = dfLabel.transform.position.QuantizeFloor(dfLabel.PixelsToUnits() / (Pixelator.Instance.ScaleTileScale / Pixelator.Instance.CurrentTileScale));
        }

        void Update()
        {
            Vector3 worldPosition = this.transform.position;
            if (sprite != null)
            {
                worldPosition = sprite.WorldBottomCenter;
            }

            dfLabel.transform.position = dfFollowObject.ConvertWorldSpaces(worldPosition, GameManager.Instance.MainCameraController.Camera, GameUIRoot.Instance.Manager.RenderCamera).WithZ(0f);
            dfLabel.transform.position = dfLabel.transform.position.QuantizeFloor(dfLabel.PixelsToUnits() / (Pixelator.Instance.ScaleTileScale / Pixelator.Instance.CurrentTileScale));
        }
        void OnDestroy()
        {
            UnityEngine.Object.Destroy(dfLabel.gameObject);
        }
    }
}
