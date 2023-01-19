using GalaSoft.MvvmLight.Messaging;
using MVVM_Basics.Models;

namespace MVVM_Basics.Helpers;


public class AddSongToQueueMessage : MessageBase
{
    public Song Song { get; set; }

	public AddSongToQueueMessage(Song song)
	{
		Song = song;
	}
}
