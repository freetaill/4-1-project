using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using Unity.VisualScripting;
using UnityEngine.Diagnostics;

public class GpsLocation : MonoBehaviour
{
    public Text GPSOut;
    public Text GPSLength;
    public Text GPSUpdateLength;
    public bool isUpdating;

    // 순서대로 이전값 현재값
    double[] latitude = { -500.0d, 0.0d };
    double[] longitude = { -500.0d, 0.0d };

    double length;
    double U_length;
    int countTime = 0;
    int steps;

    public IEnumerator Getlocation_Run(double speed)
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
            double distence = Getresult();

            if(distence > speed && distence < (speed * 3)) 
            { 
                U_length = Math.Round(distence, 6);
            }
            else { U_length = length / countTime; }

            countTime += 3 - Maxwait;

            GPSUpdateLength.text = U_length.ToString();

            length += U_length;
            U_length = 0;
            GPSLength.text = "" + length;
        }

        isUpdating = !isUpdating;
        Input.location.Stop();
    }// end of GPS

    double Getresult()
    {
        double dist = 0.0d;
        if (latitude[0] == -500.0d)
        {
            latitude[0] = Math.Round(Input.location.lastData.latitude, 6);
            longitude[0] = Math.Round(Input.location.lastData.longitude, 6);
        }
        else {
            latitude[1] = Math.Round(Input.location.lastData.latitude, 6);
            longitude[1] = Math.Round(Input.location.lastData.longitude, 6);
            GPSOut.text = latitude[0] + "\n" + latitude[1] + "\n\n" + longitude[0] + "\n" + longitude[1];

            dist = Distance(latitude[0], longitude[0], latitude[1], longitude[1]);

            latitude[0] = latitude[1]; 
            longitude[0] = longitude[1];
        }

        return dist;
    }

    double Distance(double p_lat, double p_lon, double now_lat, double now_lon)
    {
        double dist = 0.0d;
        double theta = p_lon- now_lon;

        dist = Math.Sin(deg2rad(p_lat)) * Math.Sin(deg2rad(now_lat)) +
            Math.Cos(deg2rad(p_lat)) * Math.Cos(deg2rad(now_lat)) * Math.Cos(deg2rad(theta));
        dist = Math.Acos(dist);
        dist = rad2deg(dist);
        dist = dist * 60 * 1.1515;
        dist = dist * 1.609344 * 1000;

        return dist;
    }

    double deg2rad(double deg)
    {
        return (double)(deg * Math.PI / (double)180.0d);
    }

    double rad2deg(double rad)
    {
        return (double)(rad * (double)180.0d / Math.PI);
    }
}