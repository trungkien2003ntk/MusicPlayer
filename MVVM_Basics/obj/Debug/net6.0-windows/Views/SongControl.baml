   M S B A M L   `   `   `  ����_  [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aeHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.WpfMaterialDesignThemes.Wpf  5      {Sclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf#MaterialDesignThemes.Wpf.Converters  5      F BMVVM_Basics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MVVM_Basics.Views.ConverterMVVM_Basics.Views.Converter 5      5clr-namespace:MVVM_Basics.ViewsMVVM_Basics.Views 5      #   MVVM_Basics.Views.SongControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      RwpfHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.Wpf  5      d
convertersSclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf  5      Acustom_converters)clr-namespace:MVVM_Basics.Views.Converter  5      +localclr-namespace:MVVM_Basics.Views  5         MaterialDesignBody#���   5      $��Regular"�5	      $��13(�5
      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MainFont#���  5      ���#181818$��90��5       � 	Resources 5      ?  9MVVM_Basics.Views.Converter.SongToSongCoverImageConverter ! SongToSongCoverImageConverter9  3MVVM_Basics.Views.Converter.SongToSongTitleCoverter  SongToSongTitleConverter;  5MVVM_Basics.Views.Converter.SongToSongArtistConverter  SongToSongArtistConverter:  4MVVM_Basics.Views.Converter.SongToSongPcPathCoverter  SongToSongPcPathCoverter<  6MVVM_Basics.Views.Converter.SongToSongDurationCoverter  SongToSongDurationCoverter;  5MVVM_Basics.Views.Converter.TimeSpanToDoubleConverter  TimeSpanToDoubleConverter;  5MVVM_Basics.Views.Converter.DoubleToTimeSpanConverter  DoubleToTimeSpanConverterB	  <MVVM_Basics.Views.Converter.SongToSongDurationDoubleCoverter $	  SongToSongDurationDoubleCoverter8
  2MVVM_Basics.Views.Converter.SongToIsLikedConverter 
 SongToIsLikedConverter��%  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     &		 �     &	
 �       5   
   6:     5   
   64     5   
   66     5   
   65     5    
   67     5!   
   66     5"   
   66   	  5#   
   6=   
  5$   
   63   5%      .��5&      �  � ColumnDefinitions 5'   
   �� 5(      $
��0.9* �6   $	��350��6,   �� 5)      $
��1.5* �6   �� 5*      $
��0.8* �6   .+�5-   
   �� $��0��6    �� Orientation$ 
Horizontal=�5.      ���#181818.+�5/      �� $��13q�6   $��Uniform=�50      ��51      �� *CurrentPlayingSong+ �� 	Converter# [ � 53       � RowDefinitions 54      �� 55      �� 56      .+�5:      �� $��0��6     currentPlayingSongTitle#���  5;      $
��Left=�5<       � ToolTip 5=      ��  �� Path$
 Text�	 �� RelativeSource	 �� 
 �� Mode$

 Self=���5>      �� *CurrentPlayingSong+# [ �� 5B      $��1��6     currentPlayingSongArtist#���  5C      $
��Left=�5D      ��5E      �� *CurrentPlayingSong+# [ 5G      d� 5H      $��15q�6   $ Save to Library��5I       ) %MaterialDesignFlatPrimaryToggleButton#��[ 5K      ���#1ed55f���#f9f9f9��5J      0   *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ Heart=�   Size 21��5N      �� *ToggleLikedSongCommand+��5O      �� 	 �� *Self+$ 	IsChecked� d� 	IsChecked 5P      �� *CurrentPlayingSong+# [
 5Q      � 5T   
   $��1��6    5U      �� 5V      $
��0.4* �6    �� 5W      $
��1.5* �6    �� 5X      $��1* �6    .+�5[      r� -   5         r�Name$ mediaPlayer��5[       r� Volume$ 1Q�60    r� LoadedBehavior$ Manual=�5^       r� UnloadedBehavior$ Manual=�63    r� Source 5\      �� *CurrentPlayingSong+# [ *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5_      +  %Microsoft.Xaml.Behaviors.EventTrigger  5`         	EventName$ 
MediaEnded��6%   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5a      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 60   �� *SkipNextCommand+5b      5d      � 5f      -   5        $��ControllerGrid��5f      $��1��6)   $��Center=�66    5g      �� 5h      �� 5i      $��35 �6'   �� 5j      .+�5n      �� -   5        $��0��5n        ButtonTemplate1#���  5o        trackControlBtnSkip#���  5p      ��5q        $ SkipPrevious=� 305r      d� 5u      $��1��6   
�������
���   � $  MaterialDesignActionToggleButton#���  5z      $+ %MaterialDesignActionLightToggleButton��5{      ��5x      ��  �� ElementName$ ControllerGrid��$&  ColumnDefinitions[1].Width.Value���5y      �� $ ControllerGrid��$&  ColumnDefinitions[1].Width.Value���5|        $
 Play=� 301   +MaterialDesignThemes.Wpf.ToggleButtonAssist   	OnContent 5}        $ Pause=� 30 5~      �� *	IsPlaying+�� 5�      $��2��6   #���  5�      #���  5�      ��5�      �� *SkipNextCommand+��5�        $ SkipNext=� 305�      � 5�      $��2��6    5�      �� 5�      $��60 �6'   �� 5�      �� 5�      $��60 �6'   .+�5�      �� -   5         ��Name$ TxblPlayedDuration��5�      $��0��5�        trackControlTxblDuration#���  5�      ��5�      �� $ 
sliderTime��$ Value�# [  �� StringFormat$ 
{0:mm\:ss}�� �� Mode$ OneWay=�5�      �� 5�      -   5        $ TxblTotalDuration��5�      $��2��5�      #���  5�      ��5�      �� *CurrentPlayingSong+# [ $ 
{0:mm\:ss}��5�      �� 5�      -   5         ��Name$ 
sliderTime��5�      $��1��6-    � SmallChange$ 0Q�5�       � LargeChange$ 0Q�5�        SliderStyleTrack#���  5�      $��Center=�5�       � Maximum 5�      �� *CurrentPlayingSong+# [	 5�      5�      5�      � 5�   
   $��2��6   $��0 0 15 0q�5�      $��Right=�5�      $��Center=�6+    5�      �� 5�      �� 5�      �� 5�      $	��150 �6#   .+�5�      �� -   5          ��Name$  btnQueue��5�      $��0��5�      $��30��5�      $��30��5�      $��5q�5�      #���  5�        additionalControlBtn#���  5�      ��5�        $ 
QueueMusic=� 25��5�      �� *%#DataContext.DisplayQueuePageCommand+	 �� ! �� AncestorType"  MVVM_Basics.Views.MainWindow"!  $ OneWay=�5�      �� 5�      -   5        $  	btnVolume��5�      $��30��5�      $��30��5�      $��5q�5�      $��1��5�      $y�Visible=�5�      #���  5�      #���  5�      ��5�        $ 
VolumeHigh=� 255�      �� 5�      -	   5        $ sliderVolume��5�      $��2��5�      " � Value$	" 100Q�5�      # � Minimum$# 0Q�5�      $	 100Q�6!   $ 10Q�6/   $��Center=�5�      #���  5�      5�      5�      5�      