using GalaSoft.MvvmLight.Messaging;

namespace MVVM_Basics.Helpers;

public class SearchMessage : MessageBase
{
    public string SearchString { get; set; }
    public SearchMessage(string searchString)
    {
        SearchString = searchString;
    }
}
