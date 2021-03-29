using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace EnemyRenamer
{
    class ReaperRenamer : MonoBehaviour
    {
        dfLabel dfLabel = null;


        void Awake()
        {

        }

        void Start()
        {
            string stringForInt = "the store is now closed, please leave the building ";

            GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(BraveResources.Load("DamagePopupLabel", ".prefab"), GameUIRoot.Instance.transform);

            Vector3 worldPosition = this.transform.position;
            dfLabel = gameObject.GetComponent<dfLabel>();
            dfLabel.gameObject.SetActive(true);
            dfLabel.Text = stringForInt;
            dfLabel.Color = Color.red;
            dfLabel.Opacity = 1f;
            dfLabel.transform.position = dfFollowObject.ConvertWorldSpaces(worldPosition, GameManager.Instance.MainCameraController.Camera, GameUIRoot.Instance.Manager.RenderCamera).WithZ(0f);
            dfLabel.transform.position = dfLabel.transform.position.QuantizeFloor(dfLabel.PixelsToUnits() / (Pixelator.Instance.ScaleTileScale / Pixelator.Instance.CurrentTileScale));


        }

        void Update()
        {
            Vector3 worldPosition = this.transform.position;
            dfLabel.transform.position = dfFollowObject.ConvertWorldSpaces(worldPosition, GameManager.Instance.MainCameraController.Camera, GameUIRoot.Instance.Manager.RenderCamera).WithZ(0f);
            dfLabel.transform.position = dfLabel.transform.position.QuantizeFloor(dfLabel.PixelsToUnits() / (Pixelator.Instance.ScaleTileScale / Pixelator.Instance.CurrentTileScale));
        }
        void OnDestroy()
        {
            UnityEngine.Object.Destroy(dfLabel.gameObject);
        }
    }
}
