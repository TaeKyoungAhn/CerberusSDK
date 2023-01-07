using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Cerberus_Platform_API
{
    [HelpURL("https://tkablog.tistory.com/entry/Unity-VWorld-API%EB%A5%BC-%EC%82%AC%EC%9A%A9%ED%95%98%EC%97%AC-%EC%A7%80%EB%8F%84-%EB%9D%84%EC%9A%B0%EA%B8%B01")]
    public class StaticMapLoader : MonoBehaviour
    {
        public RawImage mapRawImage;

        [Header("¸Ê Á¤º¸ ÀÔ·Â")]
        public string strBaseURL = "http://api.vworld.kr/req/image?service=image&request=getmap&key=";
        public string latitude = "";
        public string longitude = "";
        public int zoomLevel = 14;
        public int mapWidth;
        public int mapHeight;
        public string strAPIKey = "";
        public GeoPath geoPath; // Path Scriptable Object

        private void Start()
        {
            StartCoroutine(VWorldMapLoad());
        }

        IEnumerator VWorldMapLoad()
        {
           // yield return null;

            StringBuilder str = new StringBuilder();
            str.Append(strBaseURL.ToString());
            str.Append(strAPIKey.ToString());
            str.Append("&format=png");
            str.Append("&basemap=GRAPHIC");
            str.Append("&center=");
            str.Append(longitude.ToString());
            str.Append(",");
            str.Append(latitude.ToString());
            str.Append("&crs=epsg:4326");
            str.Append("&zoom=");
            str.Append(zoomLevel.ToString());
            str.Append("&size=");
            str.Append(mapWidth.ToString());
            str.Append(",");
            str.Append(mapHeight.ToString());

            str.Append("&route=style:dashdot|color:rgb(0,120,207)|width:3|point:");  //Setting line Color & width, Design
            for(int i =0; i < geoPath.geoLocationPath.Count; i++)  //loop Path Count
            {
                str.Append(geoPath.geoLocationPath[i].longtitude.ToString("F7"));
                str.Append(" ");
                str.Append(geoPath.geoLocationPath[i].latitude.ToString("F7"));

                if (i == geoPath.geoLocationPath.Count - 1) //Skip end Point 
                {
                    break;
                }
                str.Append(",");
            }


            Debug.Log(str.ToString());

            //Requset API Texture
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(str.ToString());

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                mapRawImage.texture = DownloadHandlerTexture.GetContent(request);
            }
        }

    }
}
