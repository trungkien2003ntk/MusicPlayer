using GalaSoft.MvvmLight.Messaging;
using MVVM_Basics.Views.MessageAndNotification;

namespace MVVM_Basics.Helpers;

public class ShowNotificationMessage : MessageBase
{
    public NotificationType NotificationType { get; set; }
    public TargetType TargetType { get; set; }

    public ShowNotificationMessage(NotificationType notificationType, TargetType targetType)
    {
        NotificationType = notificationType;
        TargetType = targetType;
    }
}
