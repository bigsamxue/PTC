   M S B A M L   `   `   `  ����J  FIEC60335Develop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:IEC60335Develop.ViewsIEC60335Develop.Views  5      G(clr-namespace:IEC60335Develop.ViewModelsIEC60335Develop.ViewModels  5      *    $IEC60335Develop.Views.ConnectionView   5      U QScottPlot.WPF, Version=4.1.66.0, Culture=neutral, PublicKeyToken=e53b06131e34a3aaR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:IEC60335Develop.Views     5      7vm(clr-namespace:IEC60335Develop.ViewModels     5        � 	Resources  5
         IconFont7   1IEC60335Develop.ViewModels.EnumToBooleanConverter  EnumToBooleanConverter�� 5   
   %x   &	        &	 Q     *�5      95/Fonts/SourceHanSansCN-Normal.otf #Source Han Sans CN)�5        5      6(   5       � DataContext 5      4   .IEC60335Develop.ViewModels.ConnectionViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      $��* �6   .+�5   
   �� 
��� ���$��2q�6&    �� CornerRadius$ 6��6:    �� Padding$ 4q�6K   ._�5      �� .+�5      �� $��连接设定��6   $��30(�6(   #��[  66   
����ح�$��15,15,15,15q�6t   �� 5      $��设备列表��6   $��30(�6(   #��[  66   $��
15,0,15,15q�6]   
����ح�6q   �� 5       �� Orientation$ 
Horizontal=�6   $��
15,0,15,15q�66   .+�5!      	� -   5         	�Name$ 
EtherRadio��5!   K    d� IsThreeState .  $��	Ether    ��67    	� 	GroupName$ ConnectionWay��6_   #��[  5"   �   $��15(�6�   	 d� 	IsChecked	 6"   �� 
 �� Mode$
 TwoWay=� �� Path$ ConnectWayRadioBtn� �� 	Converter# [ /   )IEC60335Develop.ViewModels.ConnectWayEnum   Ether �� ConverterParameter# Z 6�   	� 5#      -   5        $ USBRadio��5#   M    .  $��USB-TMC    ��67   $ ConnectionWay��6_   #��[  5$   �   $��15(�6�   	 6$   �� $
 TwoWay=�$ ConnectWayRadioBtn�# [    USBTMC# Z 6�   	� 5%      -   5        $ 	GPIBRadio��5%   J    .  $��GPIB    ��67   $ ConnectionWay��6]   #��[  5&   �   $��15(�6�   	 6$   �� $
 TwoWay=�$ ConnectWayRadioBtn�# [    GPIB# Z 6�   5'      �� 5(      $ 
Horizontal=�6   $��
15,0,15,15q�66   .+�5)      �� $��IP地址      ��6    #��[  63   $��20(�6Z   �� 5*      $��20(�6R   #��[  6`   $	��150��6�   ��6   �� *WTConnectModel.IPAddr+$
 TwoWay=�{�6�   ��  �� ElementName$ 
EtherRadio��$ 	IsChecked�$
 OneWay=�.��6�   5,      5-      �� 5.      $ 
Horizontal=�6   $��
15,0,15,15q�66   .+�5/      �� $��GPIB地址 ��6    #��[  6/   $��20(�6V   �� 50      $��20(�6T   #��[  6b   $	��150��6�   ��6   �� *WTConnectModel.GPIBAddr+$
 TwoWay=�{�6�   �� $ 	GPIBRadio��$ 	IsChecked�$
 OneWay=�51      �� 52      $ 
Horizontal=�6   $��
15,0,15,15q�66   .+�53      �� $��序列号      ��6    #��[  61   $��20(�6X   �� 54      $��20(�6U   #��[  6c   $	��150��6�   ��6   �� *WTConnectModel.SerialNum+$
 TwoWay=�{�6�   �� $ USBRadio��$ 	IsChecked�$
 OneWay=�55      �� 56      $��连接��6   $��30(�6'   #��[  65   
����ح�$��
15,0,15,15q�6s   �� 57      $��连接��6A   #��[  6N   $��
15,0,15,15q�6v   $��30(�6�   
���������6   �� *ConnectClickCommand+6�   �� 58      $��断开��6D   #��[  6Q   $��
15,0,15,15q�6y   $��30(�6�   
���������6   �� *DisconnectClickCommand+6�   59      5:      �� 5;   
   $��1��6   
��� ���$��2q�66   $ 6��6J   $ 4q�6[   ._�5<      �  � RowDefinitions 5=      �� 5>      �� 5?      .+�5A      � $��0��6    5B      �� 5C      $	��300 �6+   �� 5D      $	��200 �6+   �� 5E      $��* �6+   .+�5G      �� $G��Apack://application:,,,/IEC60335Develop;component/Resources/PC.png��6   $	��300��6g   $	��Top=�6t   �� 5H      $	��300��6I   $	��Top=�6W   $��1��6o   $
��0,50q�6   ��6   �� *ImageSource+$
 TwoWay=�6�   �� 5I      $L��Fpack://application:,,,/IEC60335Develop;component/Resources/WT1800E.png��6   $	��300��6l   $	��Top=�6y   $��2��6�   $
��0,50q�6�   5J      � 5K      $��1��6   .+�5L      �� 
�������$��20q�6Q   $��30(�6]   #��[  6k   ��6    �� *	IDNInfo+6k   5M      5N      5P      5Q      5R      