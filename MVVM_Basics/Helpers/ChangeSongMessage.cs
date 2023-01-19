using GalaSoft.MvvmLight.Messaging;
using MVVM_Basics.Models;

namespace MVVM_Basics.Helpers;


public class ChangeSongMessage : MessageBase
{
    public Song Song { get; set; }

	public ChangeSongMessage(Song song)
	{
		Song = song;
	}
}
