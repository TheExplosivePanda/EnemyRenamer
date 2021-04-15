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
        dfLabel nameLabel = null;
        public static List<string> namesDB = null;
        tk2dSprite sprite = null;
        public static float nameSize = 3;
        public static float opacityAmount = 0.9f;

        void Start()
        {
            
            string text = "HI IM LARRY";
            if (namesDB != null)
            {
                text = namesDB[UnityEngine.Random.Range(0, namesDB.Count)];
            }
            sprite = this.gameObject.GetComponent<tk2dSprite>();
            GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(BraveResources.Load("DamagePopupLabel", ".prefab"), GameUIRoot.Instance.transform);

            Vector3 worldPosition = this.transform.position;

            nameLabel = gameObject.GetComponent<dfLabel>();

            NamerModuleClassic.onNameSizeChanged += this.OnNameSizeChanged;
            NamerModuleClassic.onOpacityAmountChanged += this.OnOpacityAmountChanged;

            if (UnityEngine.Random.value > 0.995f)
            {
                nameLabel.Glitchy = true;
            }
            nameLabel.gameObject.SetActive(true);
            nameLabel.Text = text;
            nameLabel.Color = Color.HSVToRGB(UnityEngine.Random.value, 1.0f, 1.0f);
            nameLabel.Opacity = Larry.opacityAmount;
            nameLabel.transform.position = dfFollowObject.ConvertWorldSpaces(worldPosition, GameManager.Instance.MainCameraController.Camera, GameUIRoot.Instance.Manager.RenderCamera).WithZ(0f);
            nameLabel.transform.position = nameLabel.transform.position.QuantizeFloor(nameLabel.PixelsToUnits() / (Pixelator.Instance.ScaleTileScale / Pixelator.Instance.CurrentTileScale));
            nameLabel.TextScale = Larry.nameSize;
            xOffSet = CalculateCenterXoffset(nameLabel);
        }
        void OnOpacityAmountChanged()
        {
            if (nameLabel != null)
            {
                nameLabel.Opacity = Larry.opacityAmount;
            }
        }
        void OnNameSizeChanged()
        {
            if (this.nameLabel != null )
            {
                this.nameLabel.TextScale = nameSize;
                xOffSet = CalculateCenterXoffset(nameLabel);               
            }           
        }
        float CalculateCenterXoffset(dfLabel label)
        {
            return label.GetCenter().x - label.transform.position.x;
        }
        float xOffSet = 0;

        void Update()
        {
            Vector3 worldPosition = this.transform.position;
            if (sprite != null)
            {
                worldPosition = sprite.WorldBottomCenter;
            }
            if (nameLabel == null)
            {
                ETGModConsole.Log("yeah its back magic aight?");
            }
            Vector2 tempPos = dfFollowObject.ConvertWorldSpaces(worldPosition, GameManager.Instance.MainCameraController.Camera, GameUIRoot.Instance.Manager.RenderCamera).WithZ(0f);
            nameLabel.transform.position = tempPos.WithX(tempPos.x - xOffSet);
            nameLabel.transform.position = nameLabel.transform.position.QuantizeFloor(nameLabel.PixelsToUnits() / (Pixelator.Instance.ScaleTileScale / Pixelator.Instance.CurrentTileScale));
        }
        void OnDestroy()
        {
            UnityEngine.Object.Destroy(nameLabel.gameObject);
        }
    }
}
