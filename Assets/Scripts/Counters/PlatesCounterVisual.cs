using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaos
{
    // Namespace specific properties -------------------------------------------



    public class PlatesCounterVisual : MonoBehaviour
    {
        // Public Properties ---------------------------------------------------



        // Private Fields ------------------------------------------------------

        [SerializeField] private PlatesCounter platesCounter;
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private Transform plateVisual;

        private List<GameObject> plateVisualGameobjectList = new List<GameObject>();

        // Intitalization ------------------------------------------------------



        // Unity Methods -------------------------------------------------------

        private void OnEnable()
        {
            platesCounter.onPlateSpawned += PlatesCounter_onPlateSpawned;
            platesCounter.onPlateRemoved += PlatesCounter_onPlateRemoved;
        }

        private void OnDisable()
        {
            platesCounter.onPlateSpawned -= PlatesCounter_onPlateSpawned;
            platesCounter.onPlateRemoved -= PlatesCounter_onPlateRemoved;
        }

        // Public Methods ------------------------------------------------------



        // Private Methods -----------------------------------------------------



        // Event Handlers ------------------------------------------------------

        private void PlatesCounter_onPlateSpawned(object sender, System.EventArgs e)
        {
            Transform plateVisualTransform = Instantiate(plateVisual, counterTopPoint);

            float plateOffsetY = 0.1f;
            plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameobjectList.Count, 0);

            plateVisualGameobjectList.Add(plateVisualTransform.gameObject);
        }

        private void PlatesCounter_onPlateRemoved(object sender, System.EventArgs e)
        {
            GameObject plateGameObject = plateVisualGameobjectList[plateVisualGameobjectList.Count - 1];
            plateVisualGameobjectList.Remove(plateGameObject);
            Destroy(plateGameObject);
        }
    }
}
