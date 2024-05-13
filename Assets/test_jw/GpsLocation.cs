using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using Unity.VisualScripting;

public class GpsLocation : MonoBehaviour
{
    public Text GPSOut;
    public Text GPSLength;
    public Text GPSUpdateLength;
    public bool isUpdating;

    // 순서대로 이전값 현재값
    float[] latitude = { -1, -1 };
    double[,] lati_detail = new double[2,3];
    double[] lati_total = new double[3];
    float[] longitude = { -1, -1 };
    double[,] longi_detail = new double[2, 3];
    double[] longi_total = new double[3];
    double lati_cos;
    double lati_C;
    double D = 2 * Math.PI * 6378.135 / 360;

    double length;
    double U_length;

    private void Update()
    {
        if (!isUpdating)
        {
            StartCoroutine(Getlocation());
            isUpdating = !isUpdating;
        }
    }

    IEnumerator Getlocation()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }

        // check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield return new WaitForSeconds(3);

        Input.location.Start();

        // wait until service initailze
        int Maxwait = 3;
        while (Input.location.status == LocationServiceStatus.Initializing && Maxwait > 0)
        {
            yield return new WaitForSeconds(1);
            Maxwait--;
        }

        // service didn't init in 20 sec
        if (Maxwait < 1)
        {
            GPSOut.text = "Time Out";
            print("timed out");
            yield break;
        }

        // connection failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSOut.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted
            GPSOut.text = "Location: \n" + Input.location.lastData.latitude + " \n" +
                Input.location.lastData.longitude + " \n" +
                Input.location.lastData.altitude + " \n" +
                Input.location.lastData.horizontalAccuracy + " \n" +
                Input.location.lastData.timestamp;

            Getresult();
            U_length =  Math.Sqrt(Math.Pow((Math.Floor(lati_total[0]) * lati_C) +
                (Math.Floor(lati_total[1]) * lati_C / 60) + (lati_total[2] + lati_C / 360), 2) +
                Math.Pow((Math.Floor(longi_total[0]) * D) + (Math.Floor(longi_total[1]) * D / 60) +
                (longi_total[2] * D / 360), 2));
            length += U_length;
            GPSLength.text = "" + lati_total[0] + " " + lati_total[1] + " " + lati_total[2] + " \n"
                + longi_total[0] + " " + longi_total[1] + " " + longi_total[2];
            GPSLength.text = "" + U_length;
            Update_location();
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }

        isUpdating = !isUpdating;
        Input.location.Stop();
    }// end of GPS

    void Getresult()
    {
        if (latitude[0] == -1)
        {
            latitude[0] = Input.location.lastData.latitude;
            lati_detail[0, 0] = latitude[0];
            lati_detail[0, 1] = (latitude[0] - Math.Floor(lati_detail[0,0])) * 60;
            lati_detail[0, 2] = (lati_detail[0,1] - Math.Floor(lati_detail[0, 1])) * 60;

            longitude[0] = Input.location.lastData.longitude;
            longi_detail[0, 0] = longitude[0];
            longi_detail[0, 1] = (longitude[0] - Math.Floor(longi_detail[0, 0])) * 60;
            longi_detail[0, 2] = (longi_detail[0, 1] - Math.Floor(longi_detail[0, 1])) * 60;
        }
        else {
            latitude[1] = Input.location.lastData.latitude;
            lati_detail[1, 0] = latitude[0];
            lati_detail[1, 1] = (latitude[0] - Math.Floor(lati_detail[0, 0])) * 60;
            lati_detail[1, 2] = (lati_detail[0, 1] - Math.Floor(lati_detail[0, 1])) * 60;
            lati_total[0] = Math.Floor(lati_detail[0, 0] - lati_detail[1, 0]);
            lati_total[1] = Math.Floor(lati_detail[0, 1] - lati_detail[1, 1]);
            lati_total[2] = (lati_detail[0, 2] - lati_detail[1, 2]);

            longitude[1] = Input.location.lastData.longitude;
            longi_detail[1, 0] = longitude[0];
            longi_detail[1, 1] = (longitude[0] - Math.Floor(longi_detail[0, 0])) * 60;
            longi_detail[1, 2] = (longi_detail[0, 1] - Math.Floor(longi_detail[0, 1])) * 60;
            longi_total[0] = Math.Floor(longi_detail[0, 0] - longi_detail[1, 0]);
            longi_total[1] = Math.Floor(longi_detail[0, 1] - longi_detail[1, 1]);
            longi_total[2] = (longi_detail[0, 2] - longi_detail[1, 2]);

            lati_cos = ((int)latitude[0] + (int)latitude[1]) / 2;
            lati_C = lati_cos * D;
        }
    }
    void Update_location()
    {
        for(int i = 0; i< 3; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                lati_detail[j,i] = lati_detail[j,i];
                longi_detail[j, i] = longi_detail[j,i];
            }
        }
    }
}