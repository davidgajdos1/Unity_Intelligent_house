using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Text;
using System.Collections.Generic;
using UnityEngine.UI;


public class displayWebcam : MonoBehaviour
{


    // Replace <Subscription Key> with your valid subscription key.               
    const string subscriptionKey = "1ef2a67d893a4e93a8f186e3ef0c7b59";
    // replace <myresourcename> with the string found in your endpoint URL
    //const string uriBase =  "https://faceappcloudy.cognitive.microsoft.com/face/v1.0/detect"; 
    const string uriBase = "https://faceappcloudy.cognitiveservices.azure.com/face/v1.0/detect";

    private static string user;


    // Get the path and filename to process from the user.
    async void MakeAnalysisRequest(string imageFilePath)
    {
        HttpClient client = new HttpClient();

        // Request headers.
        client.DefaultRequestHeaders.Add(
            "Ocp-Apim-Subscription-Key", subscriptionKey);

        // Request parameters. A third optional parameter is "details".
        string requestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
            "&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses," +
            "emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

        // Assemble the URI for the REST API Call.
        string uri = uriBase + "?" + requestParameters;

        HttpResponseMessage response;

        // Request body. Posts a locally stored JPEG image.
        byte[] byteData = GetImageAsByteArray(imageFilePath);

        using (ByteArrayContent content = new ByteArrayContent(byteData))
        {
            // This example uses content type "application/octet-stream".
            // The other content types you can use are "application/json"
            // and "multipart/form-data".
            content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            // Execute the REST API call.
            response = await client.PostAsync(uri, content);

            // Get the JSON response.
            string contentString = await response.Content.ReadAsStringAsync();


            // Display the JSON response.
            print("\nResponse:\n");
            print(contentString);
            string[] contentParts = contentString.Split(',');
            string[] returned_Id = contentParts[0].Split(':');
            string iD = returned_Id[1];
            iD = iD.Substring(1);
            iD = iD.Remove(iD.Length - 1);
            MakeRequest_getFace(iD);


        }
    }
    // Returns the contents of the specified file as a byte array.
    static byte[] GetImageAsByteArray(string imageFilePath)
    {
        using (FileStream fileStream =
            new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
        {
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }

    static async void MakeRequest_getFace(string faceId)
    {
        var faceId_edited = String.Format(@"""{0}""", faceId);
        var client = new HttpClient();
        var queryString = HttpUtility.ParseQueryString(string.Empty);
        const string subscriptionKey = "1ef2a67d893a4e93a8f186e3ef0c7b59";
        //""faceId"": "+ faceId_edited + @",  

        string json = @"{
               ""faceListId"": ""unity_players"",
               ""faceId"": " + faceId_edited + @",  
               ""maxNumOfCandidatesReturned"": 1,
               ""mode"" : ""matchPerson""
           }";

        Console.WriteLine(json);

        /*string json = @"{
              ""name"": ""unity_players""
          }";
          */

        // Request headers
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);


        //   var uri = "https://faceappcloudy.cognitiveservices.azure.com/face/v1.0/verify?" + queryString;
        var uri = "https://faceappcloudy.cognitiveservices.azure.com/face/v1.0/findsimilars?" + queryString;

        HttpResponseMessage response;

        //response = await client.GetAsync(uri);
        //Console.WriteLine(response.ToString());


        // Request body
        byte[] byteData = Encoding.UTF8.GetBytes(json);

        using (var content = new ByteArrayContent(byteData))
        {
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //response = await client.PutAsync(uri, content);
            response = await client.PostAsync(uri, content); ;
            //Console.WriteLine(content.ToString());
            //Console.WriteLine(response.ToString());
            string contentString = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(contentString);

            string[] return_values = contentString.Split(',');
            string[] returned_Id = return_values[0].Split(':');
            string[] returned_Confidence = return_values[1].Split(':');
            string iD = returned_Id[1];
            iD = iD.Substring(1);
            iD = iD.Remove(iD.Length - 1);
            string confidence = returned_Confidence[1];
            confidence = confidence.Remove(confidence.Length - 2);
            print(confidence);

            float confidence_float = float.Parse(confidence, System.Globalization.CultureInfo.InvariantCulture);
            //Console.WriteLine(iD);            
            //print(confidence_double.ToString());
            if (confidence_float > 0.5)
            {
                if (iD == "09510a44-1914-4b40-a8a1-c30cf6a88b6b") user = "Richard";
                if (iD == "0482e751-4fff-4d19-895a-f1aa69a37206") user = "David";
                if (iD == "645af3f4-cc6e-4a4e-b18f-73ca2cea891b") user = "Tomas";
            }
            else
            {
                print("Invalid user");
            }

        }

    }

    public string deviceName;
    WebCamTexture wct;
    public RawImage RawImage;
    public Color32Array colorArray;
    Color32[] data;

    [StructLayout(LayoutKind.Explicit)]
    public struct Color32Array
    {
        [FieldOffset(0)]
        public byte[] byteArray;

        [FieldOffset(0)]
        public Color32[] colors;
    }

    private string _SavePath = "C:/WebcamSnaps/";
    int _CaptureCounter = 0;
    String TakeSnapshot()
    {
        Texture2D snap = new Texture2D(wct.width, wct.height);
        snap.SetPixels(wct.GetPixels());
        snap.Apply();
        String path = _SavePath + _CaptureCounter.ToString() + ".png";
        System.IO.File.WriteAllBytes(_SavePath + _CaptureCounter.ToString() + ".png", snap.EncodeToPNG());
        ++_CaptureCounter;
        return path;
    }

    // Use this for initialization
    void Start()
    {
        user = "None";
        WebCamDevice[] devices = WebCamTexture.devices;
        deviceName = devices[0].name;
        wct = new WebCamTexture(deviceName, 640, 480, 60);
        colorArray = new Color32Array();
        colorArray.colors = new Color32[wct.width * wct.height];
        data = new Color32[wct.width * wct.height];

        //GetComponent<Renderer>().material.mainTexture = wct;
        RawImage.texture = wct;
        RawImage.material.mainTexture = wct;
        wct.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            String path = TakeSnapshot();
            print(path);

            if (File.Exists(path))
            {
                try
                {
                    MakeAnalysisRequest(path);
                    print("\nWait a moment for the results to appear.\n");
                }
                catch (Exception e)
                {
                    print("\n" + e.Message + "\nPress Enter to exit...\n");
                }
            }
            else
            {
                print("\nInvalid file path.\nPress Enter to exit...\n");
            }

        }
    }
    public byte[] getImageBytes()
    {
        return colorArray.byteArray;
    }

    public string GetAuthorizedUser()
    {
        return user;
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(1, 1, 175, 20),
        "Authorized User : " + GetAuthorizedUser() 
        );
    }

}



