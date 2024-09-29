/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private float _notificationTime;

    private void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        var notifChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif",
            Name = "Reminder Notification",
            Description = "Channel for Reminders Notifications",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);

        var notification = new AndroidNotification();
        notification.Title = "Do you like my game?";
        notification.Text = "It's made with love! <3";
        notification.SmallIcon = "icon_reminder";
        notification.LargeIcon = "icon_reminder";
        notification.FireTime = DateTime.Now.AddSeconds(_notificationTime);

        AndroidNotificationCenter.SendNotification(notification, "reminder_notif");
    }
}*/
