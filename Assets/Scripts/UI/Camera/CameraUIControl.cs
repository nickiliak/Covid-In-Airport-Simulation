using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraUIControl : MonoBehaviour
{
    public Camera CurrentCamera;
    public Button MainCameraButton;
    public Button CheckIn;
    public Button BaggageClaim;
    public Button CarRental;
    public Button Shop;
    public Button Toilet;
    public Button Toilet1;
    public Button Screening;
    public Button Exit;
    public Button Food;
    public Button Departing;
    public Button Arriving;

    // Start is called before the first frame update
    void Start()
    {
        MainCameraButton.onClick.AddListener(() => SwitchCameraView("MainCamera"));
        CheckIn.onClick.AddListener(() => SwitchCameraView("CheckInCam"));
        BaggageClaim.onClick.AddListener(() => SwitchCameraView("BaggageClaimCam"));
        CarRental.onClick.AddListener(() => SwitchCameraView("CarRentalCam"));
        Shop.onClick.AddListener(() => SwitchCameraView("ShopCam"));
        Toilet.onClick.AddListener(() => SwitchCameraView("ToiletCam"));
        Toilet1.onClick.AddListener(() => SwitchCameraView("Toilet (1)Cam"));
        Screening.onClick.AddListener(() => SwitchCameraView("ScreeningCam"));
        Exit.onClick.AddListener(() => SwitchCameraView("ExitCam"));
        Food.onClick.AddListener(() => SwitchCameraView("FoodCam"));
        Departing.onClick.AddListener(() => SwitchCameraView("DepartingCam"));
        Arriving.onClick.AddListener(() => SwitchCameraView("ArrivingCam"));
    }

    void SwitchCameraView(string CameraName)
    {
        
        GameObject Camera = GameObject.Find(CameraName);
        if (Camera != null)
        {
            CurrentCamera.enabled = false;
            CurrentCamera = Camera.GetComponent<Camera>();
            CurrentCamera.enabled = true;
        }
        gameObject.SetActive(false);
    }
}
