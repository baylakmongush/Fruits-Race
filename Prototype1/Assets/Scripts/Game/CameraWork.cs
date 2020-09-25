// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraWork.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in PUN Basics Tutorial to deal with the Camera work to follow the player
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------


using UnityEngine;


namespace Photon.Pun.Demo.PunBasics
{
    /// <summary>
    /// Camera work. Follow a target
    /// </summary>
    public class CameraWork : MonoBehaviour
    {

        [SerializeField]
        private bool followOnStart = false;


        Transform cameraTransform;

        bool isFollowing;

        void Start()
        {
            if (followOnStart)
            {
                OnStartFollowing();
            }
        }

        void LateUpdate()
        {
            if (cameraTransform == null && isFollowing)
            {
                OnStartFollowing();
            }

            if (isFollowing) {
                Follow ();
            }
        }
        public void OnStartFollowing()
        {
            cameraTransform = Camera.main.transform;
            isFollowing = true;
        }
        void Follow()
        {
            if (this.transform.position.x > 0 && this.transform.position.x < 79)
                cameraTransform.position = new Vector3(this.transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
        }
    }
}