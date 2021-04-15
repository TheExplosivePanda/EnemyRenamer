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
            string text = "the store is now closed, please leave the building";

            GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(BraveResources.Load("DamagePopupLabel", ".prefab"), GameUIRoot.Instance.transform);

            Vector3 worldPosition = this.transform.position;

            dfLabel = gameObject.GetComponent<dfLabel>();
            dfLabel.gameObject.SetActive(true);
            dfLabel.Text = text;
            dfLabel.Color = Color.red;
            dfLabel.Opacity = Larry.opacityAmount;
            dfLabel.TextScale = Larry.nameSize;
            dfLabel.transform.position = dfFollowObject.ConvertWorldSpaces(worldPosition, GameManager.Instance.MainCameraController.Camera, GameUIRoot.Instance.Manager.RenderCamera).WithZ(0f);
            dfLabel.transform.position = dfLabel.transform.position.QuantizeFloor(dfLabel.PixelsToUnits() / (Pixelator.Instance.ScaleTileScale / Pixelator.Instance.CurrentTileScale));
            NamerModuleClassic.onNameSizeChanged += this.OnNameSizeChanged;
            NamerModuleClassic.onOpacityAmountChanged += this.OnOpacityAmountChanged;

        }
        void OnNameSizeChanged()
        {
            if (dfLabel != null)
            {
                dfLabel.TextScale = Larry.nameSize;
            }           
        }
        void OnOpacityAmountChanged()
        {
            if (dfLabel != null)
            {
                dfLabel.Opacity = Larry.opacityAmount;
            }
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
