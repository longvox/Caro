   M S B A M L   `   `   `  ����
  Gomoku     Gomoku.Views.LoadingScreen   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $	��310��5   	   $	��459��6     � WindowStyle$
  None=�5   	    � WindowStartupLocation$ CenterScreen=�6    � 
ResizeMode$ NoResize=�6A    � ShowInTaskbar .   � Topmost .  � 	Resources 5         showStoryBoard �� TargetProperty �� 
TargetName<  6System.Windows.Media.Animation.EasingThicknessKeyFrame p� KeyTime	 p� Value  HideStoryBoard��%�  &	        &	 �     �� 5   
   .��5	      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��6e   .	�5
        $ 0:0:0��6*   $	 0,0,0,0q�6:     5      $ 0:0:1��6*   $	 0,0,0,70q�6:   5      5      �� 5   
   .��5      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��6e   .	�5        $ 0:0:0��6*   $	 0,0,0,70q�6:     5      $ 0:0:1��6*   $	 	0,0,0,150q�6:   5      5      5      .��5      � ��5   
   �� 5      
 �� EndPoint$
 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FFB9BBFF��6    � Offset$ 0Q�61   � 5      $ White��6   $ 1Q�6-   � 5      $ White��6   $
 0.72Q�6-   5      .+�5   
   � $��Bottom=�6   $��Stretch=�6*   $	��150��6H   ���	#00FFFFFF J� OpacityMask 5      �� 5       $
 0.5,1&�6&   $ 0.5,0&�67   .��5!      � $ 	#00000000��6#   $ 0.283Q�65   � 5"      $ 	#00000000��6#   $ 0.778Q�65   � 5#      $ White��6#   $ 0.491Q�61   5$      .+�5&      �� -   5         ��Name$ 
txtLoading��5&       �� TextAlignment$ Center=�6,   $��16(�6C   $��Normal"�6Q   ���	#FF052045$
��Wrap=�6}   $��Bottom=�6�   $��Center=�6�   $	��421��6�    J� Effect 5'      3  -System.Windows.Media.Effects.DropShadowEffect  5(         
BlurRadius$ 6Q�6'      Color$ 	#FF797979��66      	Direction$	 201Q�6H      Opacity$
 0.68Q�6X      ShadowDepth$ 3Q�6g   5*      5,      �� 5.   
   $ Center=�6   $��Center=�6+   $��0,20,0,0q�6H   $
��Wrap=�6Z   $��GOMOKU��6n   $	��Top=�6|   
�������$
��Bold"�6�   $	��250��6�   $��50(�6�   $��60��6�    5/        50      $ 	#FF3E3E3E��6#   $	 201Q�65   $
 0.68Q�6E   $ 3Q�6T   $ 6Q�6d   52      �� 53   
   $	��100��6   $	��100��6   $��0,0,0,40q�6)   $��Center=�6;   $��Resources/Logo.png��6X   �� 54   
   $��16(�6   $
��Bold"�6"   ���	#FF052045$��Center=�6K   $��	0,170,0,0q�6h   $��Online��6{   $ Center=�6�   $
��Wrap=�6�   $	��Top=�6�   $	��134��6�    55        56      $ 6Q�6#   $ 	#FF797979��62   $	 201Q�6D   $
 0.68Q�6T   $ 3Q�6c   58      59      5:      