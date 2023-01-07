using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cerberus_Platform_Singleton
{
    public class CerberusPlatform : MonoBehaviour
    {

        private static CerberusPlatform Instance = null;
        public static CerberusPlatform instance
        {
            get
            {
                if (null == Instance)
                {
                    return null;
                }
                return Instance;
            }
        }

        [SerializeField]
        private PlatformMode platformMode;


        /// <summary>
        /// Class Declaration
        /// </summary>
        #region Managed Class Declaration

        #endregion

        /// <summary>
        /// Global Variable Declaration
        /// </summary>
        #region Global Variable Declaration

        #endregion

        private void Awake()
        {
            if (null == Instance)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this.gameObject);
            }

            Init();
        }

        private bool Init()
        {
            return true;
        }

        
        /// <summary>
        /// Settings Platform
        /// </summary>
        private void PlatformSeleted()
        {
            switch(platformMode)
            {
                case PlatformMode.Platform2D:
                    {

                    }
                    break;
                case PlatformMode.Platform3D:
                    {
                        
                    }
                    break;
                case PlatformMode.PlatformAR:
                    {

                    }
                    break;
                case PlatformMode.PlatformVR:
                    {

                    }
                    break;
                default:
                    {
                        //Do-Nothing
                    }
                    break;
            }
        }
    
    }
}