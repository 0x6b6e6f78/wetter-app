   M S B A M L   `   `   `  ����D  @WetterApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:WetterApp	WetterApp  5   	       WetterApp.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:WetterApp     5   	     � Title$  	WetterApp��5   	   $	��860��6   $	��550��6(    � 
ResizeMode$ CanMinimize=�5	   	   ��5
      �� 5   
    �� 
StartPoint$	 0,0&�6    �� EndPoint$	 0,1&�6/   .��5      �  � Color$ #4A90E2��6    � Offset$ 0Q�6+   � 5      $ #6DBDD6��6   $ 1Q�6+   5      .��5      � .+�5   
   �� $��20q�6   .+�5      �� -   5         ��Name$ 
inputLabel��5      
�������$��16(�6;   .��6I   Stadt eingeben:6Z   �� 5      -   5         ��Name$ 	MyTextBox��5      $��25��5      $��0,0,0,10q�5      5      �� 5      -   5         ��Name$ 
MyComboBox��5      $��25��5      $��0,0,0,20q�5      ${�False��5      �� 5       -   5        $ MyLabel��5       $y�	Collapsed=�5!      
�������5"      �� 5%      -   5        	 ��Name$	 LocationText��5%      $��
London, UK��5&      $��32(�5'      $
��Bold"�5(      $��0,0,0,5q�5)      
�������5*      �� 5+      -   5        $	 DateText��5+      $��Monday 29 August��5,      $��20(�5-      
�������$��0,0,0,20q�5/      � 52      
 � ColumnDefinitions
 53      �� 54      $��* �6'   �� 55      $
��Auto �6'   .+�58      �� -   5         ��Name$ WeatherIcon��58      $	��100��59      $	��100��5:      $��UniformToFill=�5;      $
��Left=�5<      �� 5>      $��1��6   .+�5?      �� -   5        $	 CurrentTempText��5?       $��21°C��5@      $��64(�5A      $
��Bold"�5B      
�������$��Right=�5D      �� 5E      -	   5        $	 WeatherDescText��5E       $��Mostly sunny��5F      $��24(�5G      
������� J� Opacity$	 0.5Q�5I      5J      5K      �� 5M      
�������$��0,1,0,0q�6)   $��0, 25, 0, 25q�6C   � 5P      $��	30,0,30,0q�6   
 5Q      �� 5R      $��* �6'   �� 5S      $��* �6'   �� 5T      $��* �6'   .+�5W      �� $��0��6   .+�5X      �� -
   5        $	 HighTempText��5X       $
��23°��5Y      $��24(�5Z      
�������5[      �� 5\      $
��Hoch��6    
�������$��24(�5^      $	 0.5Q�5_      5`      �� 5b      $��1��6   .+�5c      �� -   5        $	 WindText��5c       $
��7mph��5d      $��24(�5e      
�������5f      �� 5g      $
��Wind��6    
�������$��24(�5i      $	 0.5Q�5j      5k      �� 5m      $��2��6   .+�5n      �� -   5        $	 SunriseText��5n       $��05:27��5o      $��24(�5p      
�������5q      �� 5r      $��Sunrise��6    
�������$��24(�5t      $	 0.5Q�5u      5v      5w      � 5y      $��
30,10,30,0q�6   
 5z      �� 5{      $��* �6'   �� 5|      $��* �6'   �� 5}      $��* �6'   .+�5�      �� $��0��6   .+�5�      �� -   5        $	 LowTempText��5�       $
��14°��5�      $��24(�5�      
�������5�      �� 5�      $
��Tief��6    
�������$��24(�5�      $	 0.5Q�5�      5�      �� 5�      $��1��6   .+�5�      �� -   5        $	 RainText��5�       $��0%��5�      $��24(�5�      
�������5�      �� 5�      $��Regen��6    
�������$��24(�5�      $	 0.5Q�5�      5�      �� 5�      $��2��6   .+�5�      �� -   5        $	 
SunsetText��5�       $��20:57��5�      $��24(�5�      
�������5�      �� 5�      $��Sunset��6    
�������$��24(�5�      $	 0.5Q�5�      5�      5�      �� 5�      
�������$��0,1,0,0q�6)   $��0, 25, 0, 25q�6C   �� 5�      $��5-Tage-Vorhersage��6   $��20(�5�      $��	0,20,0,10q�5�      
�������5�      �� 5�      -   5         ��Name$ ForecastList��5�      ��5�      �� 5�      .R�5�      A�  A� Columns$ 5��6&   5�      ��5�      �� 5�      .R�5�      ��  �� CornerRadius$ 10��6!   $��95��5�      $	��140��5�      $��1, 10, 0, 0q�5�      ��5�      �� 5�   "    �� Color$ 	#33111111��62   ._�5�      �� $	��2,2q�6)   .+�5�   "   �� $��20(�5�   %   
�������$��Center=�5�   %   $��0,0,0,10q�5�   %   ��5�   ,   �� *Date+5�   %   �� 5�   "   $��60��5�   %   $��60��5�   %   $��Uniform=�5�   %   $��Center=�5�   %   $��0,0,0,10q�5�   %   ��5�   (   �� *	IconUrl+5�   %   �� 5�   "   $��16(�6,   
�������$��Center=�5�   %   ��5�   &   `� 5�   *    �� StringFormat$ {0} - {1}°C��67   .1�5�   .   ��  �� Path$ MinTemp�66   �� 5�   .   $ MaxTemp�66   5�   +   5�   #   5�      5�      5�      5�      5�      5�      5�      