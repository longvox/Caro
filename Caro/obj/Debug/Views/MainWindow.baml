   M S B A M L   `   `   `  ����?  ;Caro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Caro.ViewModelsCaro.ViewModels  5   	       Caro.Views.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Caro.ViewModels     5   	   -   5          �Name$  
CaroWindow��5   	    � Title$
 Caro��5   	    � SizeToContent$ WidthAndHeight=�6   $	��720��5	   	   $	��700��6   $	��720��5
   	   $	��700��6    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ 
Gomoku.ico��5   &   .��5      �  � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      .+�5   
   o� ���	#FF337AB7
�������$��0��69   .@�5      m� -   5         m�Name$ menuFile��5      $
��File��6)   .@�5      m� -   5        $ menuNewGame��5      
���   �$��New Game (Ctrl + N)��6C    m� Icon 5      �� 5      ��5      �� 5   "    �� 	UriSource$ Resources/NewGame.ico?�6.   5      5      m� 5       -   5        $ menuExit��5       
���   �$
��Exit��6@    5!      �� 5"      ��5#      �� 5$   "   $ Resources/Exit.ico?�6.   5&      5(      5)      m� 5*      -   5        $ menuEdit��5*      $
��Edit��6)   .@�5+      m� -   5        $ menuMatchMode��5+      
���   �$��_Match Mode��6E   .@�5,      m� -   5        $ menuPvP��5,      
���   �$��Friendly Match��6C    5-      �� 5.      ��5/   "   �� 50   &   $ Resources/PvP.ico?�62   52      54      m� 55      -   5        $ menuPvC��55      
���   �$��Challenge Match��6C    56      �� 57      ��58   "   �� 59   &   $ Resources/PvC.ico?�62   5;      5=      5>      m� 5?      -	   5        $ menuUndo��5?      
���   �$��Undo (Ctrl + Z)��6@    5@      �� 5A      ��5B      �� 5C   "   $ Resources/Undo.ico?�6.   5E      5G      m� 5H      -
   5        $ menuRedo��5H      
���   �$��Redo (Ctrl + Y)��6@    5I      �� 5J      ��5K      �� 5L   "   $ Resources/Redo.ico?�6.   5N      5P      5Q      5R      � 5S   
   $��1��6   	 � ColumnDefinitions	 5T      �� 5U      $��* �6#   �� 5V      $
��Auto �6#   .+�5X      � $��0��6    5Y      �� 5Z      $��* �6$   �� 5[      $
��Auto �6$   .+�5]      �� $��0��6   
���   �$��2q�6:   $��10q�6N   ._�5^      � -   5        
 �Name$
 board��5^   (   $��0��6   $��Stretch=�67   $��Stretch=�6S   5_      5`      5a      5b      5c      5d      