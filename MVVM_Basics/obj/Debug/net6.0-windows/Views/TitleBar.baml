   M S B A M L   `   `   `  ����F  BMVVM_Basics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MVVM_Basics.ViewsMVVM_Basics.Views  5      ?$clr-namespace:MVVM_Basics.ViewModelsMVVM_Basics.ViewModels  5           MVVM_Basics.Views.TitleBar   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:MVVM_Basics.Views     5      _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      5
viewmodels$clr-namespace:MVVM_Basics.ViewModels  5      -   5          >�Name$  
UCTitleBar��5         MaterialDesignBody#���   5	      $��Regular"�5
      $��13(�5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5         TextRenderingMode$
 Auto=�5        titleBarBackground#���  5       J� Opacity$	 0.8Q�5        MainFont#���  5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��130 �6   �� 5      �� 5      $	��132 �6   .+�5A   
   (  "MaterialDesignThemes.Wpf.ColorZone  $��2��6#      Mode$ PrimaryLight=�5B      ���#101010$��0q�5D      $	 0.8Q�5E         CornerRadius$ 0 0 0 0��5F      .��5G      ]� $��25��6   .+�5H      �� $��Right=�6   $��Right=�5I       �� Orientation$ 
Horizontal=�5J      .+�5K      �� $��25��5L        MaterialDesignFlatButton#���  5M      
������� � ToolTip$ Minimize��5Q      
������ 
������ ��5N      �� *MinimizeWindowCommand+��5O      �� 	 �� RelativeSource	 �� 
 �� AncestorType

 Window �� Mode$ FindAncestor=���5T      0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ Minus=�   Size 12�� 5U      $��25��5V      #���  5W      
�������$ Maximize��5[      
������ 
������ ��5X      �� *MaximizeWindowCommand+��5Y      �� 	 �� 

 Window$ FindAncestor=���5^        $ SquareRoundedOutline=� 12�� 5_      $��25��5`      #���  5a      
�������$ Close��5e      
������ 
������ ��5b      �� *CloseWindowCommand+��5c      �� 	 �� 

 Window$ FindAncestor=���5h        $ WindowClose=� 125i      5j      5k      5l      5m      