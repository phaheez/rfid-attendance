   M S B A M L   `   `   `  ����  ArduinoRFID    ArduinoRFID.DefaultPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  DefaultPage��5      .��5
      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6#   $��Center=�63   $��Center=�6P   .+�5      �� $��Connect To Arduino��6   $��	0,100,0,0q�63   $��Center=�6F   
�������$
��Auto��5   *   $	��600��68   $��Center=�6D   .��5      ��  �� Orientation$ 
Horizontal=�6   $
��Auto��66   $
��Auto��6C   .+�5      �� $��Center=�6    $��Center=�6=   $��Select Port Name:��6X   $��	10,0,10,0q�5   $   $��14(�67    �� Padding$ 5q�6E   �� 5      -   5         ��Name$ ArduinoPortName��5      $��40��68   $	��300��6D   $��10q�6P   $��14(�6]   5   #   �� 5      -   5         ��Name$ 
BtnConnect��5      $��8q�61   $��10,0,0,0q�6=   $��Connect��6O   
���P��
�������$	��120��5   0   6<   5      5      5      �� 5!   
   $��1��6   $��0��6#   $��Center=�63   $��Center=�6P   .+�5"      �� -   5         ��Name$ GroupBoxReg��5"   D   $��0,10,0,0q�6   $��Center=�6)   
�������$	��200��5#   *   $	��550��67   $��Center=�6C   .��5%      �� $ Vertical=�6   $	��535��64   $	��200��6@   .+�5&      �� $��Center=�6    $��Center=�6=   $��AVAILABLE COURSES��6X   $��0,20,0,0q�6q   $��14(�6�   $ 5q�6�   �� 5'      $ 
Horizontal=�6!   .+�5(      �� $
��Left=�6$   $��Select Course :��6?   $��	90,10,0,5q�6V   $ 8q�6i   $��14(�6u   �� 5)      -   5        $ 	CbxCourse��5)   #   $��40��66   $	��300��6B   $��10q�6N   $��14(�6[   54      �� 55      -   5        $ 	BtnEnroll��56   9   $��8q�55      $��	0,30,37,0q�6)   $��Center=�6<   $��Center=�6Y   $��Enroll Course��56   !   $��47��6L   $	��150��6X   6d   57      59      5:      5;      5<      