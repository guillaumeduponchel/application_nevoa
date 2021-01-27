using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SceneInstanciation
{
    // Video related objects
    public GameObject m_videoPlayersObject;
    public VideoPlayer m_player1;
    public VideoPlayer m_player2;
    public RenderTexture m_renderTexture1;
    public RenderTexture m_renderTexture2;

    private string[] tabNameVid;

    // Camera related objects
    public GameObject m_camera;

    enum camStates {
        NotInitialized,
        Normal,
        VR
    };

    camStates current_cam_state = camStates.NotInitialized;


    // Start is called before the first frame update
    public SceneInstanciation(int nbSceno, bool isCameraVR)
    {
        instanciateVideo();
        instanciate3DObjects();
        instanciateCamera(isCameraVR);
        instanciateUI();
        instanciateAudio();
    }

    void instanciateVideo()
    {
        int width1 = 256;
        int height1= 256;
        int width2 = 256;
        int height2= 256;

        tabNameVid = new string[] {"Assets/Videos/SimpleTriangle/heavym_1.mp4", "Assets/Videos/SimpleTriangle/heavym_1.mp4"};

        m_renderTexture1 = new RenderTexture(width1, height1, 16, RenderTextureFormat.ARGB32);
        m_renderTexture1.Create();

        m_renderTexture2 = new RenderTexture(width2, height2, 16, RenderTextureFormat.ARGB32);
        m_renderTexture2.Create();

        m_videoPlayersObject = new GameObject("VideoPlayers");
        m_player1 = m_videoPlayersObject.AddComponent<VideoPlayer>();
        m_player2 = m_videoPlayersObject.AddComponent<VideoPlayer>();

        m_player1.targetTexture = m_renderTexture1;
        m_player2.targetTexture = m_renderTexture2;

        m_player1.isLooping = true;
        m_player2.isLooping = true;

        m_player1.url = tabNameVid[0];
        m_player2.url = tabNameVid[1];


    }

    void instanciateCamera(bool isCamVR)
    {
        m_camera = GameObject.FindGameObjectWithTag("MainCamera");
        if(isCamVR)
            setVRCamera();
        else
            setNormalCamera();
    }

    void setVRCamera()
    {
        if(current_cam_state == camStates.Normal)
        {
           CameraController.Destroy(m_camera.GetComponent<CameraController>());
        }
        //m_camera.AddComponent<VRCameraController>();
        current_cam_state = camStates.VR;
    }

    void setNormalCamera()
    {
        if(current_cam_state == camStates.VR)
        {
            //VRCameraController.Destroy(m_camera.GetComponent<VRCameraController>());
        }
        m_camera.AddComponent<CameraController>();
    }

    void instanciate3DObjects()
    {

    }

    void instanciateUI()
    {

    }

    void instanciateAudio()
    {

    }
}
