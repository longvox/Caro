   M S B A M L   `   `   `  ����A  =Gomoku, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Gomoku.ViewsGomoku.Views  5          Gomoku.Views.ChatMessage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Gomoku.Views     5      .��5	      � 
�������  � RowDefinitions  5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��10, 5, 0, 0q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5      �� -   5        $��lblName��5   $   $��0��6   $��18(�63   $
��Bold"�6A   $��Player��6S   $
��Left=�6d   �� 5      $��1��6   $��25��6$   $��25��6/   $
��Left=�6;   $	��Top=�6V   $��230, 3, 0, 0q�6n   ��5      �� 5       �� ImageSource$ Resources/ClockIcon.png��6!    j� Stretch$ UniformToFill=�6G   5      �� 5      -   5        $��lblTime��5   $   $��2��6   $��
0:00:00 PM��63   $
��Left=�6H   $��16(�6c   ���	#FF7E7A7A6q   5      �� 5   
   -   5        $��
lblMessage��5      $��1��6   $��
Good luck!��6/   $��16(�6D   $��10, 0, 0, 0q�6R   $��Stretch=�6g   � 5   
   $��2��6   .+�5      �� $��Bottom=�6   $��10, 0, 0, 0q�6/   ���	#FF7E7E7E$��2��6[   5       5!      5"      