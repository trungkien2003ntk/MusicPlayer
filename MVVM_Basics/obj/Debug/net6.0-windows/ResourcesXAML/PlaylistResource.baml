   M S B A M L   `   `   `  ����F  BMVVM_Basics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:MVVM_Basics.Views.ConverterMVVM_Basics.Views.Converter  5      5clr-namespace:MVVM_Basics.ViewsMVVM_Basics.Views  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3aeHclr-namespace:Microsoft.Xaml.Behaviors;assembly=Microsoft.Xaml.BehaviorsMicrosoft.Xaml.Behaviors 5      0    *MVVM_Basics.ResourcesXAML.PlaylistResourceW SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35_ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aR NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e0899   3MVVM_Basics.Views.Converter.SongToSongTitleCoverter   SongToSongTitleCoverter?   9MVVM_Basics.Views.Converter.SongToSongCoverImageConverter ! SongToSongCoverImageConverter<   6MVVM_Basics.Views.Converter.SongToSongDurationCoverter  SongToSongDurationCoverter;   5MVVM_Basics.Views.Converter.SongToSongArtistConverter  SongToSongArtistConverter4   .MVVM_Basics.Views.Converter.ItemIndexConverter  ItemIndexConverter?   9MVVM_Basics.Views.Converter.SongToLikedSongTitleConverter ! SongToLikedSongTitleConverter6   0MVVM_Basics.Views.Converter.PassThroughConverter  PassThroughConverter;   5MVVM_Basics.Views.Converter.ObjectToDateTimeConverter  ObjectToDateTimeConverter  BorderMediumColor 	 MenuPopupBrush  �� EndPoint �� 
StartPoint 
 ControlLightColor � Color � Offset  ControlMediumColor  miAddToQueue m� Command �� RelativeSource �� AncestorType �� Mode m� CommandParameter	 �� Path  miRemoveFromQueue  miToggleLikedSong
 �� 	Converter  miAddToPlaylist �� 
TargetType �� Property �� Value `� 	Converter  miRemoveFromPlaylist  ContextMenuTemplate �� 
TargetType �� CornerRadius J� SnapsToDevicePixels  MenuItemPanelTemplate  ContextMenuNormalBackground �� Color  ContextMenuPlaylist � 	Resources �� Triggers P� Property P� Value  ContextMenuHoverBackground " ContextMenuSongForPlaylistPage �� 	Placement �� ResourceKey  ContextMenuSongForQueuePage  ContextMenuSong  SongElementBackgroundStyle �� Binding �� Value   songItemForVerticalListStyle �� TextAlignment �� StringFormat  itemForWrapStyle � RowDefinitions  DefaultPlaylistCoverImage �� ImageSource J� Effect3	  -System.Windows.Media.Effects.DropShadowEffect  	  
BlurRadius! 	  ShadowDepth" 	  Opacity  txblCardStyle  songListStyle  CircularBook    songListForInsideScrollStyle ! songListForWrapStyle " txblPlaylistTitleStyle # CircularBlack $ txblPlaylistInfoStyle % MainFont & PlaylistGrid# A� Columns*
  $Microsoft.Xaml.Behaviors.Interaction$ 
  Triggers+  %Microsoft.Xaml.Behaviors.EventTrigger%   	EventName*  $Microsoft.Xaml.Behaviors.TriggerBase&   Actions2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction'   Command"   MVVM_Basics.Views.MainWindow( �� Mode)   CommandParameter ' gradientCardBackground  ( gradientCardBackground.Hover   5      H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      7	converter)clr-namespace:MVVM_Basics.Views.Converter  5      FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      3i+http://schemas.microsoft.com/xaml/behaviors  5      )viewsclr-namespace:MVVM_Basics.Views  5      Oi1Hclr-namespace:Microsoft.Xaml.Behaviors;assembly=Microsoft.Xaml.Behaviors  5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      % -  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &	 �     &		 �     &	 r   &	 n   &	 t   7  &	 �   7  &	 �   &	 �    &	 �    7  (�� �    5b      *MenuItem.SeparatorStyleKey+)&	 
    7  7  7  7  &	     7  7  7  7  0�� 5�   
   $ miAddToQueue��6   10�� 5�   
   $ miToggleLikedSong��6   10�� 5�   
   $ miRemoveFromPlaylist��6   10�� 5�   
   $ miAddToPlaylist��6   1&	 �    7  7  7  7  0�� 5  
   $ miAddToQueue��6   10�� 5  
   $ miRemoveFromQueue��6   10�� 5  
   $ miToggleLikedSong��6   10�� 5  
   $ miAddToPlaylist��6   1&	 ,    7  7  7  7  0�� 5H  
   $ miAddToQueue��6   10�� 5M  
   $ miToggleLikedSong��6   10�� 5P  
   $ miAddToPlaylist��6   1&	 �    &	 F    7  7  7   7  7  &	 �    &	      7  7  7  &	  >!    7  7  7  &	! o"    7  7  &	" f#    &	$ e$    &	& %    7    5      6(     5      6.     5      6+     5      6*     5      6#     5      6.     5      6%     5      6*   ��5      #525252��6/   �� 5      $  0.5,1&�5      $ 0.5,0&�5      .��5   
   � # � 
 6   $ 0Q�6C   � 5   
   # �  6   $	 0.5Q�6D   � 5   
   # � 
 6   $ 1Q�6C   5      m� 5!      $��Add to Queue��6$    5#      �� *31PlacementTarget.DataContext.AddSongToQueueCommand+ ��  ContextMenu$ FindAncestor=� 5$      ��  ��  ContextMenu$ FindAncestor=�$#	 PlacementTarget.SelectedItems�m� 5'      $��Remove from Queue��6)    5)      �� *86PlacementTarget.DataContext.RemoveSongFromQueueCommand+ ��  ContextMenu$ FindAncestor=� 5*      ��  ��  ContextMenu$ FindAncestor=�$#	 PlacementTarget.SelectedItems�m� 5-      ��5/      ��  ��  ContextMenu$ FindAncestor=�$#	 PlacementTarget.SelectedItems�8
    50      �� *42PlacementTarget.DataContext.ToggleLikedSongCommand+ ��  ContextMenu$ FindAncestor=� 51      ��  ��  ContextMenu$ FindAncestor=�$#	 PlacementTarget.SelectedItems�m� 54      -   5        $��Add to playlist��56      ��59   
   �� 5:      " m�6   .�5;      ��  � ��$ 15(�6-   �� 5<       �@m�Command 6,   �� $:	 4PlacementTarget.DataContext.AddSongToPlaylistCommand� ��  ContextMenu$ FindAncestor=��� 5=       �@m�CommandParameter 5>      `� 5?      8   .1�5A      �� $		 Tag�6M    6&   �� *Self+6M   �� 5C      $#	 PlacementTarget.SelectedItems�6t    6&   ��  ContextMenu$ FindAncestor=�6t   5D      5F      5G      5I      m� 5L      $��Remove from playlist��6,    5N      �� *;9PlacementTarget.DataContext.RemoveSongFromPlaylistCommand+ ��  ContextMenu$ FindAncestor=� 5O      ��  ��  ContextMenu$ FindAncestor=�$	 PlacementTarget��� 5V      " ��62   .R�5W   
   �� $ 5��6   
������ $��1q�6<   $
 True��6�   ��6P   �� $	 
Background� ��  ContextMenu$ FindAncestor=�._�5X      �� 5Y      5Z      �� 5]      .R�5^   
   �� $��	-20,0,0,0q�6   8��  6(   5_      �� 5b      " ��5c      .�5d   
   ��  � ��$ 1��5e      �� 5f   
    � ��$ 0,4,0,4q�5g      �� 5h   
    � �� 5i      �� 5j      " ��6"   .R�5k      �� $��1q�6   ��5l      �� 5m      # �  6.   5o      5p      5r      5s      �� 5w      8��  
�������$��0q�5z      8�� $��0q�5|       5}   
   ���� 5~      'm�       " m�6   .�5      ��  � �� 6/   ��  ��  ContextMenu$ FindAncestor=�$	 
Background��� 5�       � ��$ 10q�6,   �� 5�       � ��$ 0q�6+   �� 5�       � ��8  6/   �� 5�       � ��$ 0q�64    5�      P� 5�       �@J�IsMouseOver$
 True��65   . �5�      ��  � ��8  67   5�      5�      �� 5�      '��       " ��6   .�5�      ��  � �� �#5252526/   �� 5�       � ��$ 0,2,0,2q�6+   �� 5�       � |�$ false��6.   �� 5�       � �� 5�      �� 5�      " ��6*   .R�5�      �� 
����  �$��2q�5�   !   ���#525252$��1��5�   !   $
 true��5�   !   5�      5�      5�      5�      .@�5�   
   m� $��Add to queue��6    5�      �� *AddAllPlaylistSongsToQueue+�� 5�   
   m� 5�   
   $��Delete��6    5�      �� *RemoveCurrentPlaylistCommand+5�      �� 5�      -   5        $ Right=�5�   9   8��  
�������$��0q�5�      8�� $��0q�5�       5�   
   ���� 5�      'm�       " m�6   .�5�      ��  � �� 6/   ��  ��  ContextMenu$ FindAncestor=�$	 
Background��� 5�       � ��$ 10q�6,   �� 5�       � ��$ 0q�6+   �� 5�       � ��8  6/   �� 5�       � ��$ 0q�64    5�      P� 5�       �@J�IsMouseOver$
 True��65   . �5�      ��  � ��8  67   5�      5�      �� 5�      '��       " ��6   .�5�      ��  � �� �#5252526/   �� 5�       � ��$ 0,2,0,2q�6+   �� 5�       � |�$ false��6.   �� 5�       � �� 5�      �� 5�      " ��6*   .R�5�      �� 
����  �$��2q�5�   !   ���#525252$��1��5�   !   $
 true��5�   !   5�      5�      5�      5�      .@�5�   
   2 �� 5�   
   2 2 2 5�      �� 5�      -   5        $ Right=�5�   6   8��  
�������$��0q�5�      8�� $��0q�5�       5�   
   ���� 5�      'm�       " m�6   .�5�      ��  � �� 6/   ��  ��  ContextMenu$ FindAncestor=�$	 
Background��� 5�       � ��$ 10q�6,   �� 5�       � ��$ 0q�6+   �� 5�       � ��8  6/   �� 5�       � ��$ 0q�64    5�      P� 5�       �@J�IsMouseOver$
 True��65   . �5�      ��  � ��8  67   5�      5�      �� 5�      '��       " ��6   .�5�      ��  � �� �#5252526/   �� 5�       � ��$ 0,2,0,2q�6+   �� 5�       � |�$ false��6.   �� 5       � �� 5     �� 5     " ��6*   .R�5     �� 
����  �$��2q�5  !   ���#525252$��1��5  !   $
 true��5  !   5	     5     5     5     .@�5  
   2 2 �� 5  
   2 2 5     �� 5     -   5        $ Right=�5  *   8��  
�������$��0q�5!     8�� $��0q�5$      5%  
   ���� 5&     'm�       " m�6   .�5'     ��  � �� 6/   ��  ��  ContextMenu$ FindAncestor=�$	 
Background��� 5(      � ��$ 10q�6,   �� 5)      � ��$ 0q�6+   �� 5*      � ��8  6/   �� 5+      � ��$ 0q�64    5-     P� 5.      �@J�IsMouseOver$
 True��65   . �5/     ��  � ��8  67   50     52     �� 54     '��       " ��6   .�55     ��  � �� �#5252526/   �� 56      � ��$ 0,2,0,2q�6+   �� 57      � |�$ false��6.   �� 58      � �� 59     �� 5:     " ��6*   .R�5;     �� 
����  �$��2q�5=  !   ���#525252$��1��5?  !   $
 true��5@  !   5A     5C     5D     5E     .@�5H  
   2 �� 5J  
   2 2 5Q     �� 5T     " ]�6/    5U  
   P� 5V      �@J�IsMouseOver$
 True��6-   . �5W     ��  � �� �#3030306/   5X     P� 5Y      �@J�IsMouseOver$ False��6-   . �5Z     ��  � ��
 ���� 6/   5[     �� 5]      6   ��  �� $ FindAncestor=�" ��$	 
IsSelected� True5`     .a�5a     ��  � �� �#5a5a5a6/   5b     5d     �� 5h     " ��61   .�5i  
   ��  � �� 5j     �� 5k     " ��6"   .R�5l     �� 5m     5o     �� 5q  
    � �� 5r     �� 5s     .R�5t     ]� $��BackgroundBorder��6+   $��0q�6    8��  .+�5w     �� $��40��6s   $��40��6~   $
��Left=�6�   $��
20 10 0 10q�6�   ��6    �� $	 .�8
  6�   � 5y     $��20 0 0 0q�6   .+�5{     �� 
�������$��0 10 0 5q�6�   $	��Top=�6�   ��6(   �� $	 .�8
  6�   �� 5~     
�������$��0 10 0 7q�6�   $��Bottom=�6�   ��6(   �� $	 .�8
  6�   5     � 5�     $��Right=�6   $��Center=�6;   $��0 0 8 0q�6V   .+�5�     �� $ Center=�6�   $��Center=�6�   $��Medium"�6�   ���#a7a7a7$��10q�6�   ��6(   �� $	 .�8
  $ 
{0:mm\:ss}��6�   5�     5�     5�     5�     5�     �� 5�     " ��6%   .�5�  
   ��  � �� 5�     �� 5�     " ��6"   .R�5�     �� 5�     5�     �� 5�  
    � �� 5�     �� 5�     .R�5�     �  5�     �� 5�     ��6,   �� $	 Width� ��  materialDesign:Card$ FindAncestor=��� 5�     $��1* �6,   �� 5�     $��1* �6,   .+�5�     �� $��0��6!   $ 5��6.   $��10q�6?   ��5�     �� 5�  "   # �  6-    5�     	  5�  "   $  30Q�63   $! 3Q�6C   $" .8Q�6S   5�     �� 5�     $��1��6:   #���  5�  $   ��5�  $   �� *Name+5�     5�     5�     5�     5�     �� 5�     " ��6"   .�5�  
   ��  � ��
 �����6'   �� 5�  
    � ��$ 0q�6,   �� 5�  
    � ��
 ���� 6'   �� 5�  
    � ��8   6'   �� 5�  
    �@�ContextMenu8  6(   �� 5�  
    � ��$ 15(�6%   �� 5�  
    � ��8  6/   -   5        5�     �� 5�     " ��61   .�5�  
   ��  � ��
 �����6'   �� 5�  
    � ��$ 0q�6,   �� 5�  
    � ��
 ���� 6'   �� 5�  
    � ��8   6'   �� 5�  
    �@�ContextMenu8  6(   �� 5�  
    � ��$ 15(�6%   �� 5�  
    � ��8  6/   -   5        5�     �� 5�     " ��6)   .�5�  
   ��  � ��
 �����6'   �� 5�  
    � ��$ 0q�6,   �� 5�  
    � ��
 ���� 6'   �� 5�  
    � ��8   6'   �� 5�  
    � ��$ 15(�6%   �� 5�  
    � ��8  6/   5�     �� 5�     " ��6+   .�5�  
   ��  � ��# � # 6'   �� 5�  
    � ��$ 14(�6%   �� 5�  
    � ��
 �����6'   �� 5�  
    � ��$
 Left=�60   �� 5�  
    � ��$ 5q�6#   �� 5�  
    � ��$
 Bold"�6'   5�     �� 5�     " ��6*   .�5�  
   ��  � ��# � % 6'   �� 5�  
    � ��$ 14(�6%   �� 5�  
    � ��$
 Left=�60   �� 5�  
    � ��$ 5q�6#   5�     �� 5�     " ��6!   .�5�  
   ��  � �� 5�     �� 5�     .R�5�     A� $# 4��6"   5�     5�     �� 5�  
    � �� 5�     �� 5�     .R�5�     �� $	��150��6   $ 6��6)   $	��200��6:   $��	0 20 20 0q�6G   $ 5�       5�     $ % PreviewMouseLeftButtonDown��6.   .& 5�  "     ' 69   �� *(&DataContext.DisplayPlaylistPageCommand+ �� "  $ FindAncestor=�$( OneWay=�) 5   L   �� 5     ��5     �� 5     " ��6$   .�5  "   ��  � ��# � ' 6?    5  "   P� 5  &    �@J�IsMouseOver$
 True��6E   . �5	  *   ��  � ��# � ( 6G   5
  '   5     ._�5     �  5     �� 5  "   ��60   �� $	 Width� �� 
 Border$ FindAncestor=��� 5  "   $��1* �60   �� 5  "   $��1* �60   .+�5     �� $��0��6%   $ 5��62   $��10q�6C   ��5  "   �� 5  &   8   61    5  "   	  5  &   $  30Q�67   $! 3Q�6G   $" .8Q�6W   5     �� 5!     $��1��6>   #���  5"  !   ��5!  (   �� *Name+5#     5%     5&     5'     5)     5*     5,     