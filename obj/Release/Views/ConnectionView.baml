   M S B A M L   `   `   `  ���� J  FIEC60335Develop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:IEC60335Develop.ViewsIEC60335Develop.Views  G(clr-namespace:IEC60335Develop.ViewModelsIEC60335Develop.ViewModels  *    $IEC60335Develop.Views.ConnectionView   U QScottPlot.WPF, Version=4.1.66.0, Culture=neutral, PublicKeyToken=e53b06131e34a3aaR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:IEC60335Develop.Views     7vm(clr-namespace:IEC60335Develop.ViewModels       � 	Resources     IconFont7   1IEC60335Develop.ViewModels.EnumToBooleanConverter  EnumToBooleanConverter�� %X   &	        &	 ?     *�95/Fonts/SourceHanSansCN-Normal.otf #Source Han Sans CN)�   � DataContext 4   .IEC60335Develop.ViewModels.ConnectionViewModel  .���  � ColumnDefinitions �� $	��300 ��� $��* �.+��� 
��� ���$��2q� �� CornerRadius$ 6�� �� Padding$ 4q�._��� .+��� $��连接设定��$��30(�#��[  
����ح�$��15,15,15,15q��� $��设备列表��$��30(�#��[  $��
15,0,15,15q�
����ح���  �� Orientation$ 
Horizontal=�$��
15,0,15,15q�.+�	� -    	�Name$ 
EtherRadio�� d� IsThreeState .  $��	Ether    �� 	� 	GroupName$ ConnectionWay��#��[  $��15(�	 d� 	IsChecked	 �� 
 �� Mode$
 TwoWay=� �� Path$ ConnectWayRadioBtn� �� 	Converter# [ /   )IEC60335Develop.ViewModels.ConnectWayEnum   Ether �� ConverterParameter# Z 	� -   $ USBRadio�� .  $��USB-TMC    ��$ ConnectionWay��#��[  $��15(�	 �� $
 TwoWay=�$ ConnectWayRadioBtn�# [    USBTMC# Z 	� -   $ 	GPIBRadio�� .  $��GPIB    ��$ ConnectionWay��#��[  $��15(�	 �� $
 TwoWay=�$ ConnectWayRadioBtn�# [    GPIB# Z �� $ 
Horizontal=�$��
15,0,15,15q�.+��� $��IP地址      ��#��[  $��20(��� $��20(�#��[  $	��150������ *WTConnectModel.IPAddr+$
 TwoWay=�{���  �� ElementName$ 
EtherRadio��$ 	IsChecked�$
 OneWay=�.���� $ 
Horizontal=�$��
15,0,15,15q�.+��� $��GPIB地址 ��#��[  $��20(��� $��20(�#��[  $	��150������ *WTConnectModel.GPIBAddr+$
 TwoWay=�{��� $ 	GPIBRadio��$ 	IsChecked�$
 OneWay=��� $ 
Horizontal=�$��
15,0,15,15q�.+��� $��序列号      ��#��[  $��20(��� $��20(�#��[  $	��150������ *WTConnectModel.SerialNum+$
 TwoWay=�{��� $ USBRadio��$ 	IsChecked�$
 OneWay=��� $��连接��$��30(�#��[  
����ح�$��
15,0,15,15q��� $��连接��#��[  $��
15,0,15,15q�$��30(�
����������� *ConnectClickCommand+�� $��断开��#��[  $��
15,0,15,15q�$��30(�
����������� *DisconnectClickCommand+�� $��1��
��� ���$��2q�$ 6��$ 4q�._��  � RowDefinitions �� �� .+�� $��0�� �� $	��300 ��� $	��200 ��� $��* �.+��� $G��Apack://application:,,,/IEC60335Develop;component/Resources/PC.png��$	��300��$	��Top=��� $	��300��$	��Top=�$��1��$
��0,50q����� *ImageSource+$
 TwoWay=��� $L��Fpack://application:,,,/IEC60335Develop;component/Resources/WT1800E.png��$	��300��$	��Top=�$��2��$
��0,50q�� $��1��.+��� 
�������$��20q�$��30(�#��[  ���� *	IDNInfo+