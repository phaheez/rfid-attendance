   M S B A M L   `   `   `  ����  ArduinoRFID    ArduinoRFID.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $5��/./Fonts/Quicksand-Regular.otf#Quicksand Regular)�5   	     � WindowStartupLocation$  CenterScreen=�5   	    J� SnapsToDevicePixels .  � Title$$ RFID Student Attendance System��5   	    � WindowState$ 	Maximized=�5   	    � Triggers 5	      :� 5
   
    :� RoutedEvent$ FrameworkElement.Loaded��6   .��5      � ���#e3e3e3$��	0,0,0,-45q�6     � ColumnDefinitions 5   
   �� 5      $	��93* �6   �� 5      $
��261* �6   �� 5      $	��26* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      6   .+�5   
   �� $��0��6    �� Orientation$ Vertical=�6"   	 K� ZIndex$	 1��69   $��3��6D   .+�5      ]� .+�5      o� $	��Top=�6   $��25��6,   .@�5      m� -   5        $��HomeMenu��5   .   $��_Home��6   6>   m� 5      -   5        $��StudentMenu��5   1   $��_Student��6   5#      m� 5%      -   5        $��_Course��5%      60   m� 5'      -   5        $��_Attendance��5'      64   m� 5)      -   5        $��_Attendance Sheet��5)      6:   5*      5+      �� 5-      ���#34495e$��70��6*   ._�5.      �� $��Center=�6   $��	350,0,0,0q�67   ���#ffffff
 �� IsHyphenationEnabled
 .  �� LineStackingStrategy$ BlockLineHeight=�6{    �� 
LineHeight$ 35.99999��6�    �� TextAlignment$
 Left=�6�   $
��Wrap=�6�   �� 5/      $��35.99999(�6   $/��)./Fonts/Quicksand-Bold.otf#Quicksand Bold)�6   $*�$RFID BASED STUDENT ATTENDANCE SYSTEM��6V   50      51      �� 52      ���#2c3e50$��5��6'   64   53      � 55   
   $��1��6   $��3��6   $��0��60    56      �� 57      $
��Auto �6#   62   .+�5:      � $��0��6   .+�5;       � -   5          �Name$ Frame��5;      $��Center=�6Q     � NavigationUIVisibility$ Hidden=�6n     � Source 6'   �� *
Navigate+ �� Mode$ OneWay=�6n   5<      5=      5>      5?      